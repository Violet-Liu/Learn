/********************************************************
 *
 *  File:   VisitorCounter.cs
 * 
 *  Class:  VisitorCounter
 * 
 *  Description:
 * 
 *  Author: Sha Jianjian
 * 
 *  Create: 2016/5/31 20:33:06
 * 
 *  Copyright(c) 2016 深圳前瞻资讯股份有限公司 all rights reserved
 * 
 *  Revision history:
 *      R1:
 *          修改作者：   
 *          修改日期：   
 *          修改理由：   
 *                                                 
 *
 ********************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.ServiceModel.Web;

namespace QZ.Service.Enterprise
{
    /// <summary>
    /// 初始化访问统计器
    /// 在程序根目录建立文件夹：FWDBAN
    /// 应用程序对 FWDBAN 文件夹有写入权限
    /// </summary>
    public class VisitorCounter
    {

        #region static profile

        /// <summary>
        /// 可以提交到下一代的阈值
        /// </summary>
        public static int prf_gen_commit;

        /// <summary>
        /// 当访问速度大于这个数时必须屏蔽
        /// </summary>
        public static int prf_block_speed;

        /// <summary>
        /// 最大连续访问数，超出则屏蔽
        /// </summary>
        public static int prf_max_visited;

        /// <summary>
        /// io日志锁
        /// </summary>
        static object io;

        /// <summary>
        /// ip统计
        /// </summary>
        private Dictionary<int, IpInfo> ipStatistics;

        /// <summary>
        /// ip统计锁
        /// </summary>
        private object ipso;

        /// <summary>
        /// 静态加载
        /// </summary>
        static VisitorCounter()
        {
            prf_gen_commit = Convert.ToInt32(ConfigurationManager.AppSettings["Prf_gen_commit"]);
            prf_block_speed = Convert.ToInt32(ConfigurationManager.AppSettings["Prf_block_speed"]);
            prf_max_visited = Convert.ToInt32(ConfigurationManager.AppSettings["Prf_max_visited"]);
            io = new object();

        }

        #endregion

        /// <summary>
        /// 统计池，0代，每分钟扫描一次 20次提升
        /// </summary>
        public VisitorPool gen0;

        /// <summary>
        /// 统计池，1代，2分钟扫描一次  40次提升
        /// </summary>
        public VisitorPool gen1;

        /// <summary>
        /// 统计池，2代，4分钟扫描一次  80次提升
        /// </summary>
        public VisitorPool gen2;

        /// <summary>
        /// 统计池，3代，8分钟扫描一次  160次提升
        /// </summary>
        public VisitorPool gen3;

        /// <summary>
        /// 统计池，终极  count/scanCount>4 5分钟
        /// </summary>
        public VisitorPool gen4;

        /// <summary>
        /// 可疑对象
        /// </summary>
        public VisitorPool doubtableGen;

        /// <summary>
        /// 一分钟检查一次
        /// </summary>
        public const int WATCH_SLEEP_TIMER = 6000;

        /// <summary>
        /// 扫描次数
        /// </summary>
        public int scanTimes;

        /// <summary>
        /// 是否可以开始统计了
        /// </summary>
        bool started;

        /// <summary>
        /// 最后扫描时间
        /// </summary>
        public DateTime lastScanTime;

        /// <summary>
        /// 文件名
        /// </summary>
        public const string FILENAME = "doubtableIpLog.txt";


        /// <summary>
        /// 屏蔽字典
        /// </summary>
        private Dictionary<string, DoubtableVisitor> banDic;

        /// <summary>
        /// 可疑需要屏蔽的路径和字典。
        /// </summary>
        private Dictionary<string, bool> banUserAgentPath;



        /// <summary>
        /// 检测到可疑IP通知
        /// </summary>
        /// <param name="vistor"></param>
        /// <returns></returns>
        public delegate bool BeginBanNotify(DoubtableVisitor vistor);


        /// <summary>
        /// 通知委托
        /// </summary>
        private BeginBanNotify notify;




        /// <summary>
        /// 启动统计器
        /// </summary>
        public VisitorCounter(BeginBanNotify notify)
        {
            this.notify = notify;
            gen0 = new VisitorPool(prf_gen_commit, 1);
            gen1 = new VisitorPool(1, prf_gen_commit * 2, 2);
            gen2 = new VisitorPool(1, prf_gen_commit * 4, 4);
            gen3 = new VisitorPool(1, prf_gen_commit * 8, 8);
            gen4 = new VisitorPool(1, prf_gen_commit * 16, 5);
            doubtableGen = new VisitorPool(1, prf_gen_commit * 18, 1);
            banDic = new Dictionary<string, DoubtableVisitor>(0x1000);
            banUserAgentPath = new Dictionary<string, bool>(0x1000);
            lastScanTime = DateTime.Now;
            ipStatistics = new Dictionary<int, IpInfo>(0x10000);
            ipso = new object();
            InitWatcher();
        }

        /// <summary>
        /// 初始化监控线程
        /// </summary>
        private void InitWatcher()
        {
            System.Threading.Thread t = new System.Threading.Thread(Watcher);
            t.IsBackground = true;
            t.Name = "访问检查";
            t.Start();
        }

        /// <summary>
        /// 执行监视
        /// </summary>
        private void Watcher()
        {
            int sec = WATCH_SLEEP_TIMER / 1000;
            while (DateTime.Now.Second % sec != 0)
                System.Threading.Thread.Sleep(1);
            started = true;
            System.Threading.Thread.Sleep(WATCH_SLEEP_TIMER);
            while (true)
            {
                DoWatcher();
                System.Threading.Thread.Sleep(WATCH_SLEEP_TIMER);
            }
        }

        /// <summary>
        /// 执行监视
        /// </summary>
        private void DoWatcher()
        {

            scanTimes++;

            try
            {
                if (scanTimes % gen0.oppositeScanFrequence == 0)
                {
                    gen0.ScanTo(gen1);

                    IEnumerable<Visitor> vs = gen1[0].GetVistors();
                    List<Visitor> rmVisitor = new List<Visitor>();
                    //int[] cookieScan;
                    foreach (Visitor v in vs)
                    {
                        v.gen = 1;
                        //检查是否能够直接晋级
                        if (v.GetCountNoLock() > gen4.summitFrequence)
                        {
                            gen4.Add4Gen(v);
                            rmVisitor.Add(v);
                        }
                    }

                    //对直接列为可疑对象访问记录从gen1中移除
                    foreach (Visitor v in rmVisitor)
                    {
                        gen1[0].Remove(v);
                    }

                }
                if (scanTimes % gen1.oppositeScanFrequence == 0)
                    gen1.ScanTo(gen2);
                if (scanTimes % gen2.oppositeScanFrequence == 0)
                    gen2.ScanTo(gen3);
                if (scanTimes % gen3.oppositeScanFrequence == 0)
                    gen3.ScanTo(gen4);

                BanScan();
                IEnumerable<Visitor> dtvc = gen4[0].GetVistors();
                List<Visitor> notifyVi = new List<Visitor>();
                foreach (Visitor dv in dtvc)
                {

                    int count = dv.GetCountNoLock();
                    double speed = (double)count / dv.gen;

                    //可疑的很啦
                    if (count > prf_max_visited || speed > prf_block_speed || (dv.gen > 32 && speed > gen0.summitFrequence))
                    {
                        DoubtableVisitor dbv = new DoubtableVisitor();
                        dbv.createTime = DateTime.Now;
                        dbv.notice = false;
                        dbv.vistor = new Visitor(dv);
                        dbv.reason = speed > prf_block_speed ? BanReason.访问速度过快 : BanReason.累计访问次数过多;
                        dbv.status = BanStatus.已通知;
                        dbv.initGen = dv.gen;
                        dbv.initRecCount = dv.count;
                        dbv.uap = dv.uap;
                        notifyVi.Add(dv);
                        InternalBanHandler(dbv, speed);
                    }

                }

                //移除掉已通知的可疑项目
                foreach (Visitor dv in notifyVi)
                {
                    gen4[0].Remove(dv);
                }





            }
            catch { }
            finally
            {
                DateTime dt = DateTime.Now;
                if (Math.Abs(dt.Day - lastScanTime.Day) > 0)
                {
                    gen1.Clear4Gen();
                    gen2.Clear4Gen();
                    gen3.Clear4Gen();
                    gen4.Clear4Gen();
                    //转移到BanScan中
                    //banDic.Clear();
                }
                lastScanTime = dt;

            }

        }

        /// <summary>
        /// 遍历cookie找出单个cookie很多的对象
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        private bool CookieScanFilter(Visitor v)
        {
            int[] cookieScan = v.CookieScan4Once();
            return cookieScan[1] > v.GetCountNoLock() / 2;
        }




        /// <summary>
        /// 处理屏蔽通知
        /// </summary>
        /// <param name="dbv"></param>
        /// <param name="speed"></param>
        private void InternalBanHandler(DoubtableVisitor dbv, double speed)
        {

            //已经被通知了
            DoubtableVisitor y;
            if (banDic.TryGetValue(dbv.vistor.ip, out y))
            {
                y.vistor.gen += dbv.vistor.gen;
                y.reason = dbv.reason;
                y.vistor.count += dbv.vistor.count;
                if (y.status == BanStatus.已认证)
                {
                    y.status = BanStatus.已通知;
                }
                else
                {
                    if (!y.vistor.isSpider)
                    {
                        y.status = BanStatus.已屏蔽;
                        y.expireAt = DateTime.Now.AddMinutes(y.vistor.gen * 5);
                    }
                }

            }
            else
            {
                y = dbv;
                banDic.Add(dbv.vistor.ip, dbv);
            }

            string uap = dbv.uap;
            if (uap.Length > 0)
            {
                if (!banUserAgentPath.ContainsKey(uap))
                {
                    banUserAgentPath.Add(uap, true);
                }
                else
                {
                    banUserAgentPath[uap] = true;
                }
            }


            if (y.status == BanStatus.已通知)
            {
                if (notify != null && !y.vistor.isSpider)
                {
                    notify(y);
                    y.status = BanStatus.确认中;
                }
            }
            WriteLog(y.vistor, uap, speed);

        }


        /// <summary>
        /// 获取用户请求头和路径,没有返回string.empty
        /// </summary>
        /// <returns></returns>
        public static string GetUserAgentAndPath(string ip)
        {
            WebOperationContext woc = WebOperationContext.Current;
            if (woc != null)
            {
                var headers = woc.IncomingRequest.Headers;
                return string.Format("{0}-{1}", ip, woc.IncomingRequest.UriTemplateMatch.Template.ToString());
            }
            return string.Empty;
        }


        /// <summary>
        /// 获取请求路径
        /// </summary>
        /// <returns></returns>
        public static string GetReqPath()
        {
            WebOperationContext woc = WebOperationContext.Current;
            if (woc != null)
            {
                return woc.IncomingRequest.UriTemplateMatch.Template.ToString().ToLower();
            }
            return string.Empty;
        }


        /// <summary>
        /// 获取代理IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetProxyIp()
        {
            WebOperationContext woc = WebOperationContext.Current;
            if (woc != null)
            {
                string proxy = woc.IncomingRequest.Headers["HTTP_X_FORWARDED_FOR"];
                if (proxy == null || proxy.Length < 1)
                {
                    proxy = woc.IncomingRequest.Headers["REMOTE_ADDR"];
                    if (proxy == null)
                        return string.Empty;
                    //ips=woc.IncomingMessageProperties.
                }
                return proxy;
            }

            return string.Empty;
        }

        /// <summary>
        /// 扫描过期对象
        /// </summary>
        private void BanScan()
        {
            if (banDic.Count < 1)
                return;
            Dictionary<string, DoubtableVisitor> dbt = new Dictionary<string, DoubtableVisitor>(banDic.Count * 2);
            Dictionary<string, bool> newUapDic = new Dictionary<string, bool>(banUserAgentPath.Count * 2);
            foreach (KeyValuePair<string, DoubtableVisitor> pair in banDic)
            {
                if (pair.Value.status == BanStatus.已屏蔽 && pair.Value.expireAt < DateTime.Now)
                {
                    continue;
                }

                //检查客户端可疑度
                if (pair.Value.status == BanStatus.已通知)
                {
                    if (pair.Value.createTime.AddMinutes(8) < DateTime.Now)
                    {
                        if (pair.Value.initGen == pair.Value.vistor.gen)
                        {
                            continue;
                        }
                    }
                    else if (pair.Value.createTime.AddHours(12) < DateTime.Now)
                        continue;

                }

                if (!newUapDic.ContainsKey(pair.Value.uap))
                    newUapDic.Add(pair.Value.uap, true);


                dbt.Add(pair.Key, pair.Value);
            }
            this.banDic = dbt;


            this.banUserAgentPath = newUapDic;


        }


        /// <summary>
        /// 扫描统计，必要时衰减
        /// </summary>
        private void StatisticScan()
        {

            if (ipStatistics.Count < 0x8000)
            {
                return;
            }

            //对快要饱和的统计做衰减
            WriteText(string.Format("需要对IP统计数据做衰减，ip个数达到阈值：{0}", ipStatistics.Count));

            Dictionary<int, IpInfo> dic = new Dictionary<int, IpInfo>(0x10000);
            IpInfo[] values;
            lock (ipso)
                values = ipStatistics.Values.ToArray();

            IEnumerable<IpInfo> tops = values.OrderByDescending(a => a.CreateTime).Take(0x4000);
            foreach (IpInfo inf in tops)
            {
                dic.Add(inf.Ip, inf);
            }
            Dictionary<int, IpInfo> tmp = ipStatistics;
            lock (ipso)
            {
                ipStatistics = dic;
            }
            tmp.Clear();
            WriteText("IP统计数据衰减完毕");
        }

        /// <summary>
        /// 设置IP为已授权
        /// </summary>
        /// <param name="ip"></param>
        public bool SetIpAuthorized(string ip)
        {

            string uap = GetUserAgentAndPath(ip);
            if (uap.Length > 0 && banUserAgentPath.ContainsKey(uap))
            {
                banUserAgentPath[uap] = false;
            }
            InternalRemoveStatistic(ip);
            DoubtableVisitor dbv;
            if (banDic.TryGetValue(ip, out dbv))
            {
                dbv.status = BanStatus.已认证;
                return true;
            }



            return false;

        }

        /// <summary>
        /// 如果ip统计里存在对象，删除统计
        /// </summary>
        /// <param name="ip"></param>
        private void InternalRemoveStatistic(string ip)
        {
            int ip32 = IpInfo.IpToInt32(ip);

            if (ipStatistics.ContainsKey(ip32))
            {
                lock (ipso)
                    ipStatistics.Remove(ip32);
            }
        }


        /// <summary>
        /// 此IP是否可以访问
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public bool IsIpAuthorized(string ip)
        {


            if (WebOperationContext.Current != null)
            {
                string ua = WebOperationContext.Current.IncomingRequest.UserAgent;
                if (ua == null || ua.Length < 1 || ua.Trim().Length < 1)
                    return false;
            }

            //WriteLog(new Visitor("-1-" + System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()), System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), 10);

            DoubtableVisitor dbv;
            if (banDic.TryGetValue(ip, out dbv))
            {


                int cc = dbv.vistor.count;
                int cg = dbv.vistor.gen;
                double avgSpeed = cc / cg;

                //这里不知道为什么当前线程休眠的时候也会阻止其他线程
                /*
                if (avgSpeed>prf_block_speed/2 && new System.Random().Next(0, 6) % 5 == 0)
                {
                    int time = (int)Math.Truncate(avgSpeed) * 1000;
                    if (time > 3 * 60000)
                        time = 3 * 60000;
                    time = new Random().Next(24000,time);
                    WriteLog(dbv.vistor, string.Format("对高频访问拖慢{0}秒",time/1000), avgSpeed);
                    System.Threading.Thread.Sleep(time);
                }
                */

                //像公司重度访问，被误屏蔽
                if (cg < 3 || avgSpeed < 4000)
                    return true;


                if (cg < 7 && avgSpeed < prf_block_speed * 2)
                    return false;

                if (dbv.vistor.isSpider)
                    return true;
                if (dbv.status == BanStatus.已屏蔽 && dbv.expireAt < DateTime.Now)
                    return true;
                if (dbv.status == BanStatus.已认证)
                    return true;

                return false;
            }
            /*
            string proxy = GetProxyIp();
            if (proxy.Length > 0 && proxy != ip)
            {
                ip = proxy;
                if (banDic.TryGetValue(ip, out dbv))
                {

                    int cc = dbv.vistor.count;
                    int cg = dbv.vistor.gen;
                    int avgSpeed = cc / cg;
                    if (cg < 3 && avgSpeed < prf_block_speed * 2)
                        return false;

                    if (dbv.vistor.isSpider)
                        return true;
                    if (dbv.status == BanStatus.已屏蔽 && dbv.expireAt < DateTime.Now)
                        return true;
                    if (dbv.status == BanStatus.已认证)
                        return true;

                    return false;
                }
            }*/

            string uap = GetUserAgentAndPath(ip);
            if (uap.Length > 0)
            {
                bool ban;
                if (this.banUserAgentPath.TryGetValue(uap, out ban))
                {

                    return !ban;
                }
            }



            return true;
        }


        /// <summary>
        /// 设置IP为已屏蔽
        /// </summary>
        /// <param name="dbv">访问对象，通过GetBanInfo方法获取</param>
        /// <param name="minutes">为0时系统自动计算</param>
        /// <returns></returns>
        public bool SetIpBaned(string ip, int minutes)
        {

            DoubtableVisitor dbv;
            if (banDic.TryGetValue(ip, out dbv))
            {
                dbv.status = BanStatus.已屏蔽;
                if (minutes > 0)
                    dbv.expireAt = DateTime.Now.AddMinutes(minutes);
                else
                    dbv.expireAt = DateTime.Now.AddMinutes(dbv.vistor.gen * 5);
                return true;
            }
            return false;

        }


        /// <summary>
        /// 获取IP的屏蔽信息，如果IP为进入监测名单则为NULL
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public DoubtableVisitor GetBanInfo(string ip)
        {
            DoubtableVisitor dbv;
            if (banDic.TryGetValue(ip, out dbv))
            {
                if (dbv.status == BanStatus.已屏蔽)
                {
                    if (dbv.expireAt < DateTime.Now)
                    {
                        return null;
                    }

                }
                return dbv;
            }
            return null;
        }


        /// <summary>
        /// 是否需要确认
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public bool IsIpNeedNotify(string ip)
        {
            DoubtableVisitor dbv;
            if (banDic.TryGetValue(ip, out dbv))
            {
                return dbv.status == BanStatus.已通知 || dbv.status == BanStatus.确认中;
            }
            return false;
        }

        /// <summary>
        /// 是否需要
        /// </summary>
        /// <param name="dbv"></param>
        /// <returns></returns>
        public bool IsIpNeedNotify(DoubtableVisitor dbv)
        {
            return dbv.status == BanStatus.已通知 || dbv.status == BanStatus.确认中;
        }



        /// <summary>
        /// 将可疑对象写入日志
        /// </summary>
        /// <param name="doubtableVistor"></param>
        /// <param name="speed"></param>
        public static void WriteLog(Visitor doubtableVistor, string uap, double speed)
        {

            try
            {
                string msg = string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}/min\t{6}\t ckco:{7}/{8}"
                        , DateTime.Now
                        , doubtableVistor.ip
                        , doubtableVistor.isSpider
                        , doubtableVistor.gen
                        , doubtableVistor.GetCountNoLock()
                        , speed
                        , uap
                        , doubtableVistor.CookieOnceCount()
                        , doubtableVistor.cookies.Count
                        );
                //lock(io)
                WriteText(msg);
                //System.Diagnostics.EventLog.WriteEntry("Application", msg);
            }
            catch { }
            //WriteText(msg);
        }

        public static void WriteText(string text)
        {
            lock (io)
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "FWDBAN\\" + FILENAME, true, Encoding.UTF8))
                {
                    sw.WriteLine(text);
                }
            }
        }



        /// <summary>
        /// 添加纪录
        /// </summary>
        /// <param name="ip">ip地址</param>
        /// <param name="spider">蜘蛛</param>
        public void AddRec(string ip)
        {
            //为了保证统计精准度，当后台线程确认为到了整分是则可以开始；最差的情况会丢失启动时59秒的数据;
            if (!started)
                return;

            if (ip == null || ip.Length < 1)
                return;
            gen0.Add(ip);

            /*
            string proxy = GetProxyIp();
            if (proxy.Length > 0 && proxy != ip)
            {
                gen0.Add(proxy);
            }*/

        }

        /// <summary>
        /// 特殊添加纪录，手动指定count增益参数, 用于临时屏蔽已知的可以IP
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="spider"></param>
        /// <param name="count"></param>
        public void AddRecCount(string ip, int count)
        {
            if (!started)
                return;

            if (ip == null || ip.Length < 1)
                return;

            gen0.AddCount(ip, count);
        }


        /// <summary>
        /// 获取IP信息
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="onlyGet">是否只获取，不统计</param>
        /// <returns></returns>
        public IpInfo GetIpInfo(string ip, bool onlyGet)
        {

            IpInfo item;
            int ip32 = IpInfo.IpToInt32(ip);
            string path = GetReqPath();
            if (ipStatistics.TryGetValue(ip32, out item))
            {
                if (onlyGet)
                    return item;

                item.AddPath(path);
                return item;
            }

            if (onlyGet)
                return item;

            lock (ipso)
            {
                if (ipStatistics.TryGetValue(ip32, out item))
                {
                    item.AddPath(path);
                    return item;
                }

                item = new IpInfo(ip32, path);
                ipStatistics.Add(ip32, item);
                return item;
            }

        }


        /// <summary>
        /// 判断是否是机器IP
        /// </summary>
        /// <param name="inf"></param>
        /// <returns></returns>
        public bool IsMechineIpInfo(IpInfo inf)
        {

            if (inf.Count > 10000)
                return true;
            if (inf.Count < 0x100)
                return false;
            //if (inf.ContainsPath("orgcompany/company/evaluate/detail"))
            //    return false;
            //if (!inf.ContainsPath("orgcompany/combine/detail"))
            //    return false;

            return false;

            int max = 0;
            List<KeyValuePair<int, int>> pathset = inf.GetPathPairList;
            int count = pathset.Count;
            //if (count < 4)
            //    return false;



            int searchPath = inf.GetPathId("orgcompany/combine/search");
            int detailPath = inf.GetPathId("orgcompany/combine/detail");
            int evaluatePath = inf.GetPathId("orgcompany/company/evaluate/detail");

            max = pathset.Max(a => a.Value);
            int maxPathCount = 0;
            int searchCount = 0;
            int detailCount = 0;
            int evaluateCount = 0;

            int pathKey;
            //int min = Int32.MaxValue;
            for (int i = 0; i < count; i++)
            {
                int vc = pathset[i].Value;
                pathKey = pathset[i].Key;
                if (pathKey == searchPath)
                    searchCount = vc;
                else if (pathKey == detailPath)
                    detailCount = vc;
                else if (pathKey == evaluatePath)
                    evaluateCount = vc;

                if (vc > 1 && Math.Abs(vc - max) < 3)
                {
                    maxPathCount++;
                }
                //if (vc < min)
                //    min = vc;
            }

            if (searchCount == inf.Count || detailCount == inf.Count)
                return true;

            if (searchCount + detailCount == inf.Count)
                return true;


            if (detailCount > 8 && evaluateCount == 0)
                return true;

            //if (evaluateCount > detailCount || (detailCount > 8 && detailCount - evaluateCount > 3))
            //    return true;

            //if (searchCount > 0x1F && searchCount / (double)inf.Count >= 0.9d)
            //    return true;

            //如果搜索是最大访问路径占比在50以上
            //if (searchCount == maxPathCount && ((double)searchCount / (double)inf.Count) >= 0.5d)
            //    return true;

            //if (count < 3)
            //    return true;

            //int avg = inf.Count / count;

            //if (maxPathCount == count - 1 

            //    //&& max / min >= 2

            //    )
            //{
            //    //说明是 1 个搜索或者提示 多次访问详细的IP
            //    if (count <=6)
            //        return true;
            //}







            return false;
        }


        /// <summary>
        /// 给ip绑定cookie
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="cookie"></param>
        public void AddCookie(string ip, string cookie)
        {
            if (!started)
                return;

            if (ip == null || ip.Length < 1)
                return;

            gen0.AddCookie(ip, cookie);
        }







        /// <summary>
        /// 可疑访客
        /// </summary>
        public class DoubtableVisitor
        {

            /// <summary>
            /// 是否已通知管理员
            /// </summary>
            public bool notice;

            /// <summary>
            /// 禁止了?
            /// </summary>
            //public bool ban;

            /// <summary>
            /// 初始化时的记录
            /// </summary>
            public int initRecCount;

            /// <summary>
            /// 初使化记录的代数
            /// </summary>
            public int initGen;

            /// <summary>
            /// 对象状态
            /// </summary>
            public BanStatus status;

            /// <summary>
            /// 被屏蔽原因
            /// </summary>
            public BanReason reason;

            /// <summary>
            /// userAgent and path
            /// </summary>
            public string uap;

            /// <summary>
            /// 创建时间
            /// </summary>
            public DateTime createTime;

            /// <summary>
            /// 屏蔽过期时间
            /// </summary>
            public DateTime expireAt;


            /// <summary>
            /// 访问统计对象
            /// </summary>
            public Visitor vistor;
        }

        /// <summary>
        /// 屏蔽状态
        /// </summary>
        public enum BanStatus
        {
            /// <summary>
            /// 通知了前端程序
            /// </summary>
            已通知,
            /// <summary>
            /// 前段程序在确认IP是否合法中
            /// </summary>
            确认中,

            /// <summary>
            /// 前端程序已确认该IP为安全
            /// </summary>
            已认证,
            /// <summary>
            /// 此IP认证失败或者被系统自动屏蔽
            /// </summary>
            已屏蔽
        }


        /// <summary>
        /// 被屏蔽原因
        /// </summary>
        public enum BanReason
        {
            访问速度过快,
            累计访问次数过多
        }




        /// <summary>
        /// 访问池对象
        /// </summary>
        public class VisitorPool
        {

            /// <summary>
            /// 存储池
            /// </summary>
            VisitorCollection[] pool;

            /// <summary>
            /// 提升访问临界值
            /// </summary>
            public int summitFrequence;

            /// <summary>
            /// 相对扫描频率
            /// </summary>
            public int oppositeScanFrequence;

            /// <summary>
            /// 初始化一个存储池
            /// </summary>
            /// <param name="summitFrequence">提升访问临界值</param>
            /// <param name="oppositeScanFrequence">相对(gen0)扫描频率</param>
            public VisitorPool(int summitFrequence, int oppositeScanFrequence)
            {

                pool = new VisitorCollection[61];
                InitPool(0, 61);
                this.summitFrequence = summitFrequence;
                this.oppositeScanFrequence = oppositeScanFrequence;

            }

            /// <summary>
            /// 填充池
            /// </summary>
            /// <param name="start">开始初始化索引</param>
            /// <param name="len">初始化个数</param>
            private void InitPool(int start, int len)
            {
                for (int i = start; i < len; i++)
                {
                    pool[i] = new VisitorCollection();
                }
            }

            /// <summary>
            /// 初始化一个存储池，初始化池大小
            /// </summary>
            /// <param name="len">设置池容量，大小</param>]
            /// <param name="summitFrequence">提升访问临界值</param>
            /// <param name="oppositeScanFrequence">相对(gen0)扫描频率</param>
            public VisitorPool(int len, int summitFrequence, int oppositeScanFrequence)
            {
                pool = new VisitorCollection[len];
                InitPool(0, len);
                this.summitFrequence = summitFrequence;
                this.oppositeScanFrequence = oppositeScanFrequence;

            }


            /// <summary>
            /// 增加一次访问
            /// </summary>
            /// <param name="ip"></param>
            public void Add(string ip)
            {

                //int seed = IncTotalHands();
                int idx = DateTime.Now.Second;
                Visitor v = new Visitor(ip);
                pool[idx].Inc(v);
            }

            /// <summary>
            /// 增益统计
            /// </summary>
            /// <param name="ip"></param>
            /// <param name="spider"></param>
            /// <param name="count"></param>
            public void AddCount(string ip, int count)
            {
                int idx = DateTime.Now.Second;
                Visitor v = new Visitor(ip);
                v.IncNoLock(count);
                pool[idx].Inc(v);
            }


            /// <summary>
            /// 增加cookie
            /// </summary>
            /// <param name="ip"></param>
            /// <param name="cookie"></param>
            public void AddCookie(string ip, string cookie)
            {
                pool[DateTime.Now.Second].IncCookie(ip, cookie);
            }






            /// <summary>
            /// 增加或者合并一个符合规则的访问IP对象
            /// </summary>
            /// <param name="v"></param>
            public void Add4Gen(Visitor v)
            {
                pool[0].IncNoLock(v);
            }



            /// <summary>
            /// 扫描并提升
            /// </summary>
            /// <param name="genY"></param>
            public void ScanTo(VisitorPool genY)
            {

                foreach (VisitorCollection vc in pool)
                {
                    if (vc != null)
                    {
                        vc.ScanTo(genY);
                    }
                }

            }


            /// <summary>
            /// 执行清理 0代以后才能使用
            ///
            /// </summary>
            public void Clear4Gen()
            {
                foreach (VisitorCollection vc in pool)
                {
                    if (vc != null)
                        vc.Clear();
                }
            }


            /// <summary>
            /// 根据索引获取访问集合
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            public VisitorCollection this[int index]
            {
                get
                {
                    return pool[index];
                }
            }


        }

        /// <summary>
        /// 访问器(基本单元)
        /// </summary>
        public class Visitor
        {
            /// <summary>
            /// 初始化
            /// </summary>
            /// <param name="ip">IP地址</param>
            public Visitor(string ip)
            {
                this.ip = ip;

                this.hashCode = ip.GetHashCode();
                this.lockObj = new object();
                this.count = 1;
                this.gen = 1;
                this.uap = GetUserAgentAndPath(ip);
                this.cookies = new Dictionary<string, int>();
                this.ckLock = new object();

            }

            /// <summary>
            /// 创建对象y的浅副本
            /// </summary>
            /// <param name="y"></param>
            public Visitor(Visitor y)
            {

                this.ip = y.ip;
                this.count = y.count;
                this.gen = y.gen;
                this.uap = y.uap;
                this.hashCode = y.hashCode;
                this.isSpider = y.isSpider;
                this.lockObj = new object();
                this.ckLock = new object();
                this.cookies = y.InternalCookieCpy();

            }

            /// <summary>
            /// 可能是蜘蛛？
            /// </summary>
            public bool isSpider;

            /// <summary>
            /// IP地址
            /// </summary>
            public string ip;

            /// <summary>
            /// hash
            /// </summary>
            public int hashCode;

            /// <summary>
            /// 总数
            /// </summary>
            internal int count;

            /// <summary>
            /// 锁
            /// </summary>
            private object lockObj;

            /// <summary>
            /// cookie对应的锁
            /// </summary>
            internal object ckLock;


            /// <summary>
            /// 代数
            /// </summary>
            public int gen;

            /// <summary>
            /// userAgent and path
            /// </summary>
            public string uap;

            /// <summary>
            /// maps to cookie
            /// </summary>
            public Dictionary<string, int> cookies;

            /// <summary>
            /// 增加访问计数
            /// </summary>
            public void Inc()
            {
                lock (lockObj)
                {
                    count++;
                }
            }

            /// <summary>
            /// 增加 ip对应的 cookie数
            /// </summary>
            /// <param name="cookie"></param>
            /// <returns></returns>
            public int IncCookie(string cookie)
            {
                if (cookie == null || cookie.Length < 1)
                    return cookies.Count;
                lock (ckLock)
                {

                    if (cookies.ContainsKey(cookie))
                    {
                        return cookies[cookie]++;
                    }
                    cookies.Add(cookie, 1);
                    return 1;
                }
            }

            /// <summary>
            /// 遍历次IP 对应的cookie的个数
            /// </summary>
            /// <returns></returns>
            public int[] CookieScan4Once()
            {
                int count = VisitorCounter.prf_gen_commit + 1;
                int[] scans = new int[count];
                foreach (KeyValuePair<string, int> kv in this.cookies)
                {
                    if (kv.Value < count)
                    {
                        scans[kv.Value]++;
                    }
                }
                return scans;
            }

            /// <summary>
            /// 获取只有一个cookie的数据量
            /// </summary>
            /// <returns></returns>
            public int CookieOnceCount()
            {
                lock (ckLock)
                {
                    int res = 0;
                    foreach (KeyValuePair<string, int> kv in this.cookies)
                    {
                        if (kv.Value < 2)
                            res++;
                    }
                    return res;
                }
            }




            /// <summary>
            /// 合并一个cookie字典
            /// </summary>
            /// <param name="dic"></param>
            internal void BulkCookieCombie(Dictionary<string, int> dic)
            {
                foreach (KeyValuePair<string, int> kv in dic)
                {
                    if (this.cookies.ContainsKey(kv.Key))
                    {
                        this.cookies[kv.Key] += kv.Value;
                        continue;
                    }
                    this.cookies.Add(kv.Key, kv.Value);
                }
            }

            /// <summary>
            /// 复制词典
            /// </summary>
            /// <returns></returns>
            internal Dictionary<string, int> InternalCookieCpy()
            {
                if (cookies.Count < 1)
                    return new Dictionary<string, int>();
                lock (ckLock)
                {
                    Dictionary<string, int> dic = new Dictionary<string, int>(cookies.Count);
                    foreach (KeyValuePair<string, int> pair in cookies)
                    {
                        dic.Add(pair.Key, pair.Value);
                    }
                    return dic;
                }
            }

            /// <summary>
            /// 对输入对象检查并增益
            /// </summary>
            /// <param name="v"></param>
            internal void InternalIncWithCheck(Visitor v)
            {

                int fq = 1;
                string uap = v.uap;
                if (uap != null && uap.Length > 1)
                {

                    if (uap.EndsWith("register/select/page"))
                        fq = 4;
                    else if (uap.EndsWith("nb/list"))
                        fq = 3;
                    else if (uap.EndsWith("branch/select/page"))
                        fq = 2;
                    else if (uap.EndsWith("update/select/page"))
                        fq = 2;

                }
                lock (lockObj)
                {
                    count += fq;
                }
            }

            /// <summary>
            /// 增加访问计数，无锁
            /// </summary>
            public void IncNoLock(int times)
            {
                count += times;
            }


            /// <summary>
            /// 增加访问计数
            /// </summary>
            /// <param name="times"></param>
            public void IncTimes(int times)
            {
                lock (lockObj)
                {
                    count += times;
                }
            }

            /// <summary>
            /// 获取访问次数，无锁，不精确
            /// </summary>
            /// <returns></returns>
            public int GetCountNoLock()
            {
                return count;
            }


            /// <summary>
            /// 获取访问次数
            /// </summary>
            public int GetCount
            {
                get
                {
                    lock (lockObj)
                    {
                        return count;
                    }
                }
            }



            /// <summary>
            /// 获取hash码
            /// </summary>
            /// <returns></returns>
            public override int GetHashCode()
            {
                return hashCode;
            }


            /// <summary>
            /// 是否相等
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public override bool Equals(object obj)
            {
                return ((Visitor)obj).ip == ip;
                //return base.Equals(obj);
            }


        }


        /// <summary>
        /// 访问集合
        /// </summary>
        public class VisitorCollection
        {


            /// <summary>
            /// 锁对象
            /// </summary>
            private object lockObj;

            /// <summary>
            /// 字典
            /// </summary>
            Dictionary<Visitor, Visitor> dic;

            /// <summary>
            /// 初始化一个访问集
            /// </summary>
            /// <param name="index"></param>
            public VisitorCollection()
            {
                lockObj = new object();
                dic = new Dictionary<Visitor, Visitor>(0x1000);

            }


            /// <summary>
            /// 增加访问频率
            /// </summary>
            /// <param name="v"></param>
            public void Inc(Visitor v)
            {
                Visitor x;
                if (dic.TryGetValue(v, out x))
                {
                    if (v.count > 1)
                        x.IncTimes(v.count);
                    else
                        x.InternalIncWithCheck(v);
                    //x.Inc();
                    return;
                }
                lock (lockObj)
                {
                    if (dic.TryGetValue(v, out x))
                    {
                        if (v.count > 1)
                            x.IncTimes(v.count);
                        else
                            x.InternalIncWithCheck(v);
                        //x.Inc();
                        return;
                    }
                    dic.Add(v, v);
                    //v.Inc();
                }

            }

            /// <summary>
            /// 增加cookie数目
            /// </summary>
            /// <param name="ip"></param>
            /// <param name="cookie"></param>
            public void IncCookie(string ip, string cookie)
            {
                Visitor x;
                if (dic.TryGetValue(new Visitor(ip), out x))
                {
                    x.IncCookie(cookie);
                }
            }

            /// <summary>
            /// 无锁新增,扫描合并的时候用
            /// </summary>
            /// <param name="v"></param>
            public void IncNoLock(Visitor v)
            {
                Visitor x;
                if (dic.TryGetValue(v, out x))
                {
                    x.IncNoLock(v.GetCountNoLock());
                    lock (v.ckLock)
                    {
                        x.BulkCookieCombie(v.cookies);
                    }
                    x.gen += v.gen;
                    return;
                }
                dic.Add(v, v);

            }


            /// <summary>
            /// 清理所有的键和值
            /// </summary>
            public void Clear()
            {
                lock (lockObj)
                {
                    dic.Clear();
                }
            }


            /// <summary>
            /// 获取访问对象
            /// </summary>
            /// <param name="x"></param>
            /// <returns></returns>
            public Visitor this[Visitor x]
            {
                get
                {
                    Visitor y;
                    if (dic.TryGetValue(x, out y))
                    {
                        return y;
                    }
                    return null;
                }
            }

            /// <summary>
            /// 扫描,扫描完后会重置(清理)此集合
            /// </summary>
            /// <returns></returns>
            public void ScanTo(VisitorPool pool)
            {
                Visitor[] vs;

                lock (lockObj)
                {
                    if (dic.Count < 1)
                        return;

                    vs = new Visitor[dic.Count];
                    dic.Keys.CopyTo(vs, 0);
                    Clear();
                }
                int sf = pool.summitFrequence - 1;
                int oneCookieCount = sf / 4;
                Visitor currVs;
                int ckc = 0;
                for (int i = 0; i < vs.Length; i++)
                {
                    currVs = vs[i];
                    ckc = currVs.CookieOnceCount();
                    if (currVs.gen == 1 || currVs.GetCount > sf)
                    {
                        pool.Add4Gen(currVs);
                    }
                    else if (ckc > oneCookieCount)
                    {
                        //对单cookie访问增益处理
                        currVs.IncTimes(oneCookieCount * 3);
                        pool.Add4Gen(currVs);
                    }
                }


            }

            /// <summary>
            /// 获取访问对象
            /// </summary>
            /// <returns></returns>
            public IEnumerable<Visitor> GetVistors()
            {
                return dic.Keys;
            }


            /// <summary>
            /// 直接删除对象，可疑池专用
            /// </summary>
            /// <param name="v"></param>
            public void Remove(Visitor v)
            {
                dic.Remove(v);
            }


        }



        /// <summary>
        /// ip 静态统计对象
        /// </summary>
        public class IpInfo
        {


            /// <summary>
            /// 路径锁
            /// </summary>
            private static object po;


            /// <summary>
            /// 路径
            /// </summary>
            private static int _pathIdx;


            /// <summary>
            /// 路径字典
            /// </summary>
            private static Dictionary<string, int> pathDic;

            /// <summary>
            /// 路径表
            /// </summary>
            private static List<string> pathTable;

            /// <summary>
            /// 静态加载
            /// </summary>
            static IpInfo()
            {
                po = new object();
                _pathIdx = -1;
                pathDic = new Dictionary<string, int>(0x1000);
                pathTable = new List<string>(0x1000);
            }


            /// <summary>
            /// 添加路径
            /// </summary>
            /// <param name="path"></param>
            /// <returns></returns>
            public static int AddInfoPath(string path)
            {
                int idx;
                if (pathDic.TryGetValue(path, out idx))
                {
                    return idx;
                }

                lock (po)
                {
                    if (pathDic.TryGetValue(path, out idx))
                    {
                        return idx;
                    }
                    _pathIdx++;
                    pathDic.Add(path, _pathIdx);
                    pathTable.Add(path);
                    return _pathIdx;
                }

            }


            /// <summary>
            /// 根ID获取路径
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            public static string GetPath(int id)
            {
                if (id > -1 && id < pathTable.Count)
                    return pathTable[id];
                return string.Empty;
            }



            /// <summary>
            /// 根据路径ID获取路径对象
            /// </summary>
            /// <param name="pathids"></param>
            /// <returns></returns>
            public static List<string> GetPathes(int[] pathids)
            {
                int len = pathids.Length;
                List<string> pathes = new List<string>(len);
                for (int i = 0; i < len; i++)
                {
                    pathes.Add(GetPath(pathids[i]));
                }
                return pathes;
            }


            /// <summary>
            /// ip地址
            /// </summary>
            private int ip;


            /// <summary>
            /// 次数
            /// </summary>
            private int count;


            /// <summary>
            /// 锁
            /// </summary>
            private object ipo;

            /// <summary>
            /// 创建时间
            /// </summary>
            private DateTime createTime;

            /// <summary>
            /// 路径集合
            /// </summary>
            private Dictionary<int, int> pathSet;

            /// <summary>
            /// 实例化一个IP信息对象
            /// </summary>
            /// <param name="ip"></param>
            /// <param name="path"></param>
            public IpInfo(int ip, string path)
            {

                this.ip = ip;
                this.pathSet = new Dictionary<int, int>();
                this.ipo = new object();
                this.createTime = DateTime.Now;
                pathSet.Add(AddInfoPath(path), 1);
                this.count = 1;

            }

            /// <summary>
            /// 添加访问路径
            /// </summary>
            /// <param name="path"></param>
            /// <returns></returns>
            public int AddPath(string path)
            {
                int idx = AddInfoPath(path);
                lock (ipo)
                {
                    if (pathSet.ContainsKey(idx))
                        pathSet[idx]++;
                    else
                        pathSet.Add(idx, 1);
                    //pathSet.Add(idx);
                    this.count++;
                }
                return idx;
            }

            /// <summary>
            /// 获取路径ID
            /// </summary>
            /// <param name="path"></param>
            /// <returns></returns>
            public int GetPathId(string path)
            {
                return AddInfoPath(path);
            }


            /// <summary>
            /// 是否存在某个路径
            /// </summary>
            /// <param name="path"></param>
            /// <returns></returns>
            public bool ContainsPath(string path)
            {
                int idx = AddInfoPath(path);
                return pathSet.ContainsKey(idx);
            }


            /// <summary>
            /// 获取路径集合
            /// </summary>
            public List<string> Pathes
            {
                get
                {
                    return GetPathes(this.pathSet.Keys.ToArray());
                }
            }

            /// <summary>
            /// IP地址
            /// </summary>
            public int Ip
            {
                get
                {
                    return this.ip;
                }
            }


            /// <summary>
            /// 该IP地址的访问总数
            /// </summary>
            public int Count
            {
                get
                {
                    return this.count;
                }
            }

            /// <summary>
            /// 访问路径总数
            /// </summary>
            public int PathCount
            {
                get
                {
                    return this.pathSet.Count;
                }
            }

            /// <summary>
            /// 获取路径访问数
            /// </summary>
            public List<KeyValuePair<int, int>> GetPathPairList
            {
                get
                {

                    List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>(count);
                    lock (ipo)
                    {
                        foreach (KeyValuePair<int, int> p in pathSet)
                            list.Add(p);
                    }
                    return list;
                }
            }

            /// <summary>
            /// 获取对象创建时间
            /// </summary>
            public DateTime CreateTime
            {
                get
                {
                    return this.createTime;
                }
            }



            /// <summary>
            /// ip地址转int32
            /// </summary>
            /// <param name="ip"></param>
            /// <returns></returns>
            public static int IpToInt32(string ip)
            {
                string[] sgs = ip.Split('.');
                if (sgs.Length != 4)
                    return 0;
                return Convert.ToInt32(sgs[0]) << 24 | Convert.ToInt32(sgs[1]) << 16 | Convert.ToInt32(sgs[2]) << 8 | Convert.ToInt32(sgs[3]);
            }

        }

    }
}
