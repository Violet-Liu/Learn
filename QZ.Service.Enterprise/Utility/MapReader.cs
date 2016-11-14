using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using QZ.Instrument.Utility;

namespace QZ.Service.Enterprise
{
    /// <summary>
    /// 图谱读取器
    /// </summary>
    public class MapReader
    {


        /// <summary>
        /// 空数组
        /// </summary>
        static List<Node> EMPTY_LIST = new List<Node>(0);

        /// <summary>
        /// 空数组
        /// </summary>
        static Node[] EmptyArray = new Node[0];

        /// <summary>
        /// 空数组
        /// </summary>
        static NodeChild[] EmptyNCArray = new NodeChild[0];


        /// <summary>
        /// 原始关系对象
        /// </summary>
        private CompanyRelation rawRelation;


        /// <summary>
        /// 原始输入简单关系集合
        /// </summary>
        private List<RelationSimple> rawSimpleList;


        /// <summary>
        /// 最大层数
        /// </summary>
        public int maxIndex;

        /// <summary>
        /// 当前索引
        /// </summary>
        private int currIndex;

        /// <summary>
        /// 根节点集合
        /// </summary>
        public List<Node> rootNodeList;


        /// <summary>
        /// 总循环圈数
        /// </summary>
        public int totalLoops;


        /// <summary>
        /// 分层元素
        /// </summary>
        //private Dictionary<int, List<Node>> stages;

        /// <summary>
        /// 名称到ID的映射
        /// </summary>
        private Dictionary<string, List<Node>> nameMap;


        /// <summary>
        /// 主题元素
        /// </summary>
        private Dictionary<int, Node> elements;


        /// <summary>
        /// 获取元素
        /// </summary>
        public Dictionary<int, Node> Elements
        {
            get
            {
                return elements;
            }
        }


        /// <summary>
        /// 是否允许名称中出现地区名
        /// </summary>
        private bool enableAreaName;


        /// <summary>
        /// 获取设定是否允许公司名中加入地区名称
        /// </summary>
        public bool EnableAreaName
        {
            get
            {
                return this.enableAreaName;
            }
        }


        /// <summary>
        /// 实例化一个图谱阅读器
        /// </summary>
        /// <param name="realtion"></param>
        public MapReader(CompanyRelation realtion, bool enableAreaName)
        {

            this.rawRelation = realtion;
            this.enableAreaName = enableAreaName;
            elements = new Dictionary<int, Node>(0x400);
            if (!enableAreaName)
            {
                nameMap = new Dictionary<string, List<Node>>(0x400);
            }
            Init();
        }

        /// <summary>
        /// 实例化一个图谱阅读器
        /// </summary>
        /// <param name="relationList"></param>
        /// <param name="enableAreaName"></param>
        public MapReader(List<RelationSimple> relationList, bool enableAreaName)
        {
            this.rawSimpleList = relationList;
            this.enableAreaName = enableAreaName;
            elements = new Dictionary<int, Node>(relationList.Count);
            if (!enableAreaName)
            {
                nameMap = new Dictionary<string, List<Node>>(relationList.Count);
            }
            Init2();
        }



        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            if (this.rawRelation == null)
                return;

            //Dictionary<int, int> visitfilter = new Dictionary<int, int>();

            ToNode(this.rawRelation, 0);
            if (!enableAreaName)
            {
                IntrenalCheckNameMap();
            }

        }


        private void Init2()
        {
            if (this.rawSimpleList == null)
                return;

            foreach (RelationSimple simple in this.rawSimpleList)
            {
                ToNodeV2(simple);

            }

            this.InternalCheckChildren();
            //check children


            if (!enableAreaName)
            {
                IntrenalCheckNameMap();
            }
        }




        /// <summary>
        /// 检查公司名，修复需要加入地区的名称
        /// </summary>
        private void IntrenalCheckNameMap()
        {
            Node d;
            foreach (KeyValuePair<string, List<Node>> pair in nameMap)
            {
                if (pair.Value.Count > 1)
                {
                    d = pair.Value[0];
                    d.name = string.Format("{0}{1}", d.areaName, pair.Key);
                }
            }
        }



        /// <summary>
        /// 遍历节点
        /// </summary>
        /// <param name="relation"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        private Node ToNode(CompanyRelation relation, int direction)
        {

            this.totalLoops++;
            Node node = new Node();

            node.type = relation.term.companyTermId > 0 ? 1 : 2;

            if (node.type == 1)
            {
                node.id = -relation.term.companyTermId;
            }
            else
            {
                node.id = relation.memoryStoreId;
            }

            if (elements.ContainsKey(node.id))
            {

                Node storeNode = elements[node.id];
                if (storeNode.name != null && storeNode.name.Length > 0)
                    return storeNode;
                node = storeNode;
            }
            else
            {
                elements.Add(node.id, node);
                node.nodeIndex = currIndex++;
                //if (node.index == 767)
                //{
                //    Console.WriteLine("here comes");
                //}
            }


            node.index = relation.index;
            node.direction = direction;
            //if (stages.ContainsKey(node.index))
            //{
            //    stages[node.index].Add(node);
            //}
            //else
            //{
            //    stages.Add(node.index, new List<Node>() { node});
            //}

            if (node.id < 0)
            {
                node.longName = relation.name;
                //node.name = string.Format(string.Format("{0}{1}{2}", relation.areaSegment, relation.nameSegment, relation.tradeSegment));
                node.areaName = relation.areaSegment;
                node.sgName = relation.nameSegment;
                node.sgTrade = relation.tradeSegment;
                node.sgBranch = relation.branchSegment;



                if (node.longName != null && node.areaName != null && node.longName.IndexOf(node.areaName) == -1)
                {
                    //说明是大公司或者其他机构

                    if (relation.typeSegment != null && relation.typeSegment.Length < 4)
                    {
                        node.name = string.Format("{0}{1}{2}", relation.nameSegment, relation.tradeSegment, relation.typeSegment);
                    }
                    else
                    {
                        node.name = string.Format("{0}{1}", relation.nameSegment, relation.tradeSegment);
                    }
                }
                else
                {
                    string checkName = string.Format("{0}{1}", relation.nameSegment, relation.tradeSegment);
                    if (!enableAreaName && nameMap.ContainsKey(checkName))
                    {
                        if (relation.typeSegment != null && relation.typeSegment.Length < 4)
                        {
                            node.name = string.Format("{0}{1}{2}{3}", relation.areaSegment, relation.nameSegment, relation.tradeSegment, relation.typeSegment);
                        }
                        else
                        {
                            node.name = string.Format("{0}{1}{2}", relation.areaSegment, relation.nameSegment, relation.tradeSegment);
                        }
                        nameMap[checkName].Add(node);

                    }
                    else
                    {
                        if (!enableAreaName)
                        {
                            if (relation.typeSegment != null && relation.typeSegment.Length < 4)
                            {
                                node.name = string.Format("{0}{1}{2}", relation.nameSegment, relation.tradeSegment, relation.typeSegment);
                            }
                            else
                            {
                                node.name = string.Format("{0}{1}", relation.nameSegment, relation.tradeSegment);
                            }

                            nameMap.Add(checkName, new List<Node>() { node });
                        }
                        else
                        {
                            if (relation.typeSegment != null && relation.typeSegment.Length < 4)
                            {
                                node.name = string.Format("{0}{1}{2}{3}", relation.areaSegment, relation.nameSegment, relation.tradeSegment, relation.typeSegment);
                            }
                            else
                            {
                                node.name = string.Format("{0}{1}{2}", relation.areaSegment, relation.nameSegment, relation.tradeSegment);
                            }
                        }

                    }


                }

                node.code = relation.code;
                if (node.code == null)
                {
                    this.elements.Remove(node.id);
                    this.currIndex--;
                }
            }
            else
            {
                node.name = relation.name;
                node.longName = node.name;

            }

            if (node.index > maxIndex)
                maxIndex = node.index;

            node.weight = relation.weight;
            node.areaCode = relation.areaCode;

            if (relation.nextRelations != null)
            {
                Node tmp;
                foreach (CompanyRelation nextRelation in relation.nextRelations)
                {
                    tmp = ToNode(nextRelation, 1);
                    if (tmp != null)
                    {
                        tmp.relationType = 1;
                        node.AddChildren(tmp);
                    }
                }
            }

            if (relation.invisibleRelations != null)
            {
                Node tmp;

                int i = 0, len = relation.invisibleRelations.Count;
                CompanyRelation inviRelation;
                while (i < len)
                {
                    inviRelation = relation.invisibleRelations[i];
                    tmp = ToNode(inviRelation, -2);
                    if (tmp != null)
                    {
                        tmp.relationType = 2;
                    }
                    i++;
                }

                //foreach (CompanyRelation invisibleRelation in relation.invisibleRelations)
                //{
                //    tmp=ToNode(invisibleRelation);
                //    if (tmp != null)
                //    { 

                //    }
                //}
            }

            if (relation.previousRelations != null)
            {
                Node tmp;
                foreach (CompanyRelation gdRelation in relation.previousRelations)
                {
                    //if (gdRelation.name!=null &&gdRelation.name.StartsWith("浙江蚂蚁小微"))
                    //    Console.WriteLine("here comes.");

                    tmp = ToNode(gdRelation, -1);
                    if (tmp != null)
                    {
                        //if(tmp.name!=null&&tmp.name.StartsWith("浙江蚂蚁小微"))
                        //    Console.WriteLine("here comes.");

                        //tmp.AddChildren(node);
                        tmp.AddChildren(node);
                        tmp.relationType = -1;
                    }

                }
            }

            return node;

        }




        /// <summary>
        /// 将简单关系对象转换为节点
        /// </summary>
        /// <param name="relation"></param>
        private void ToNodeV2(RelationSimple relation)
        {

            Node node = new Node();

            node.type = relation.termId > 0 ? 1 : 2;

            if (node.type == 1)
            {
                node.id = -relation.termId;
            }
            else
            {
                node.id = relation.memoryStoreId;
            }

            node.areaCode = relation.areaCode;
            node.areaName = relation.areaSegment;
            node.code = relation.code;
            node.direction = relation.relationType;



            //Node parent;
            //if (elements.TryGetValue(relation.parentId, out parent))
            //{
            //    parent.AddChildren(node);
            //}
            //else
            //{
            //    if (relation.parentId != 0)
            //        throw new ArgumentNullException("无法找到父节点");
            //}

            Node storeNode;

            if (elements.TryGetValue(node.id, out storeNode))
            {
                if (storeNode.name != null && storeNode.name.Length > 0)
                    return;
                node = storeNode;
            }
            else
            {
                elements.Add(node.id, node);
                node.nodeIndex = currIndex++;
            }

            node.index = relation.index;


            if (node.id < 0)
            {
                node.longName = relation.name;
                //node.name = string.Format(string.Format("{0}{1}{2}", relation.areaSegment, relation.nameSegment, relation.tradeSegment));
                node.areaName = relation.areaSegment;
                node.sgName = relation.nameSegment;
                node.sgTrade = relation.tradeSegment;
                node.sgBranch = relation.branchSegment;



                if (node.longName != null && node.areaName != null && node.longName.IndexOf(node.areaName) == -1)
                {
                    //说明是大公司或者其他机构

                    if (relation.typeSegment != null && relation.typeSegment.Length < 4)
                    {
                        node.name = string.Format("{0}{1}{2}", relation.nameSegment, relation.tradeSegment, relation.typeSegment);
                    }
                    else
                    {
                        node.name = string.Format("{0}{1}", relation.nameSegment, relation.tradeSegment);
                    }
                }
                else
                {
                    string checkName = string.Format("{0}{1}", relation.nameSegment, relation.tradeSegment);
                    if (!enableAreaName && nameMap.ContainsKey(checkName))
                    {
                        if (relation.typeSegment != null && relation.typeSegment.Length < 4)
                        {
                            node.name = string.Format("{0}{1}{2}{3}", relation.areaSegment, relation.nameSegment, relation.tradeSegment, relation.typeSegment);
                        }
                        else
                        {
                            node.name = string.Format("{0}{1}{2}", relation.areaSegment, relation.nameSegment, relation.tradeSegment);
                        }
                        nameMap[checkName].Add(node);

                    }
                    else
                    {
                        if (!enableAreaName)
                        {
                            if (relation.typeSegment != null && relation.typeSegment.Length < 4)
                            {
                                node.name = string.Format("{0}{1}{2}", relation.nameSegment, relation.tradeSegment, relation.typeSegment);
                            }
                            else
                            {
                                node.name = string.Format("{0}{1}", relation.nameSegment, relation.tradeSegment);
                            }

                            nameMap.Add(checkName, new List<Node>() { node });
                        }
                        else
                        {
                            if (relation.typeSegment != null && relation.typeSegment.Length < 4)
                            {
                                node.name = string.Format("{0}{1}{2}{3}", relation.areaSegment, relation.nameSegment, relation.tradeSegment, relation.typeSegment);
                            }
                            else
                            {
                                node.name = string.Format("{0}{1}{2}", relation.areaSegment, relation.nameSegment, relation.tradeSegment);
                            }
                        }

                    }


                }

                node.code = relation.code;
                if (node.code == null)
                {
                    this.elements.Remove(node.id);
                    this.currIndex--;
                }
            }
            else
            {
                node.name = relation.name;
                node.longName = node.name;

            }

            if (node.index > maxIndex)
                maxIndex = node.index;

            node.relationType = relation.relationType;
            node.weight = relation.weight;
            node.areaCode = relation.areaCode;

            //node.type = relation.storeType;

        }


        /// <summary>
        /// 遍历字典，修复子节点
        /// </summary>
        private void InternalCheckChildren()
        {
            List<RelationSimple> eles = this.rawSimpleList;


            foreach (RelationSimple node in eles)
            {
                InternalCehckOneNode(node);
            }
        }


        private void InternalCehckOneNode(RelationSimple item)
        {
            if (item.parentId == 0)
                return;

            int id = item.storeType == 2 ? item.memoryStoreId : -item.termId;

            Node node;
            if (!this.elements.TryGetValue(id, out node))
            {
                Console.WriteLine("node not found {0}", id);
                return;
            }

            //Node node = this.elements[id];

            Node parent = this.elements[item.parentId];
            switch (item.relationType)
            {
                case -1:
                case -2:
                    node.AddChildren(parent);
                    break;
                case 1:
                    parent.AddChildren(node);
                    break;



                default:
                    break;
            }

        }


        /// <summary>
        /// 转为模板名称
        /// </summary>
        /// <param name="modelName"></param>
        /// <returns></returns>
        public string ToModelString(string modelName)
        {



            //string filePath = System.Configuration.ConfigurationManager.AppSettings[modelName];
            //string htmlText = System.IO.File.ReadAllText(
            //    string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, filePath.TrimStart('\\')));
            string htmlText = MapHtmlCache.GetMap(modelName);
            int jsonIndex = htmlText.IndexOf("[[[]]]");
            if (jsonIndex == -1)
                throw new ArgumentOutOfRangeException(string.Format("未能在HTML模板中找到JOSN数据占位符 {0}", modelName));

            string jsonStr = ToJsonStringByFastJson();

            StringBuilder sb = new StringBuilder(htmlText.Length + jsonStr.Length);

            sb.Append(htmlText.ToCharArray(0, jsonIndex));
            sb.Append(jsonStr);
            sb.Append(htmlText.ToCharArray(jsonIndex + 6, htmlText.Length - (jsonIndex + 6)));

            return sb.ToString();

        }

        /// <summary>
        /// 根据模板将JSON字符串填充到模板中
        /// </summary>
        /// <param name="modelName"></param>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static string ToModelStringStatic(string modelName, string jsonStr)
        {
            string htmlText = MapHtmlCache.GetMap(modelName);
            int jsonIndex = htmlText.IndexOf("[[[]]]");
            if (jsonIndex == -1)
                throw new ArgumentOutOfRangeException(string.Format("未能在HTML模板中找到JOSN数据占位符 {0}", modelName));

            //string jsonStr = ToJsonStringByFastJson();

            StringBuilder sb = new StringBuilder(htmlText.Length + jsonStr.Length);

            sb.Append(htmlText.ToCharArray(0, jsonIndex));
            sb.Append(jsonStr);
            sb.Append(htmlText.ToCharArray(jsonIndex + 6, htmlText.Length - (jsonIndex + 6)));

            return sb.ToString();

        }

        

        /// <summary>
        /// 将对象格式化JSON
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToJsonWhitFastJsonStatic(string mapName, JsonResult jr)
        {

            return ToModelStringStatic(mapName, DecodeFromJsonResult(jr));
        }

        /// <summary>
        /// 获取JSON字符串
        /// </summary>
        /// <returns></returns>
        private string InternalNodeToJson()
        {
            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            jss.MaxJsonLength = Int32.MaxValue;
            return jss.Serialize(
                this.elements.Values.Select(a => new
                {

                    id = a.id,
                    nodeIndex = a.nodeIndex,
                    code = a.code,
                    name = a.name,
                    area = a.areaCode,
                    trade = a.sgTrade,
                    sgName = a.sgName,
                    areaName = a.areaName,
                    longName = a.longName,
                    weight = a.weight,
                    type = a.type,

                    direction = a.direction,

                    children = a.children.Select(
                    x => new
                    {
                        nodeIndex = x.nodeIndex
                    }
                    )

                })
                );
        }


        /// <summary>
        /// 转为JSONstr
        /// </summary>
        /// <returns></returns>
        public string ToJsonStringByFastJson()
        {
            Dictionary<int, Node> ele = this.elements;
            JsonNode[] jn = new JsonNode[ele.Count];
            int i = 0;
            foreach (Node n in ele.Values)
            {
                jn[i] = JsonNode.Create(n);
                i++;
            }
            //fastJSON.JSON.ClearReflectionCache();
            return jn.ToJson_Fast();
        }




        /// <summary>
        /// 节点对象
        /// </summary>
        public class Node
        {

            /// <summary>
            /// 节点索引
            /// </summary>
            public int nodeIndex;

            /// <summary>
            /// 节点id
            /// </summary>
            public int id;

            /// <summary>
            /// 节点类别
            /// </summary>
            public int type;

            /// <summary>
            /// 层级
            /// </summary>
            public int index;

            /// <summary>
            /// 权重
            /// </summary>
            public int weight;

            /// <summary>
            /// 代码
            /// </summary>
            public string code;

            /// <summary>
            /// 地区码
            /// </summary>
            public string areaCode;


            /// <summary>
            /// 名称
            /// </summary>
            public string name;

            /// <summary>
            /// 长名称
            /// </summary>
            public string longName;

            /// <summary>
            /// 行业特征
            /// </summary>
            public string sgTrade;

            /// <summary>
            /// 分词名
            /// </summary>
            public string sgName;

            /// <summary>
            /// 地区名
            /// </summary>
            public string areaName;

            /// <summary>
            /// 分支
            /// </summary>
            public string sgBranch;

            /// <summary>
            /// 关系类别
            /// </summary>
            public int relationType;

            /// <summary>
            /// 方向 0为圆点，1为对外投资，-1为 股东，-2为隐
            /// </summary>
            public int direction;

            /// <summary>
            /// 子节点
            /// </summary>
            public IEnumerable<Node> children
            {
                get
                {
                    if (filter != null)
                    {
                        return filter.AsEnumerable();
                    }
                    return EMPTY_LIST;
                }
            }

            public HashSet<Node> filter;


            /// <summary>
            /// 添加子节点
            /// </summary>
            /// <param name="child"></param>
            public void AddChildren(Node child)
            {
                if (filter == null)
                {
                    //children = new List<Node>();
                    filter = new HashSet<Node>();
                }
                if (!filter.Contains(child))
                {
                    //children.Add(child);
                    filter.Add(child);
                }

            }

            public override int GetHashCode()
            {
                return id.GetHashCode();
                //return base.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                if (!(obj is Node))
                    return false;
                return ((Node)obj).id == this.id;
                //return base.Equals(obj);
            }

            public override string ToString()
            {
                return string.Format("{0} {1}", this.id, this.name);
                //return base.ToString();
            }

        }



        public class JsonNode
        {

            /// <summary>
            /// id 为负时为已识别主体
            /// </summary>
            public int id;

            /// <summary>
            /// 节点索引
            /// </summary>
            public int nodeIndex;

            /// <summary>
            /// 机构代码
            /// </summary>
            public string code;

            /// <summary>
            /// 格式化名称
            /// </summary>
            public string name;

            /// <summary>
            /// 地区
            /// </summary>
            public string area;

            /// <summary>
            /// 地区代码
            /// </summary>
            public string areaName;

            /// <summary>
            /// 行业
            /// </summary>
            public string trade;

            /// <summary>
            /// 字号
            /// </summary>
            public string sgName;

            /// <summary>
            /// 长名称
            /// </summary>
            public string longName;

            /// <summary>
            /// 权重
            /// </summary>
            public int weight;

            /// <summary>
            /// 类型
            /// </summary>
            public int type;

            /// <summary>
            /// 关系类型
            /// </summary>
            public int relationType;

            /// <summary>
            /// 层级
            /// </summary>
            public int dimession;

            /// <summary>
            /// 子节点
            /// </summary>
            public NodeChild[] children;


            /// <summary>
            /// 通过节点对象创建一个
            /// </summary>
            /// <param name="node"></param>
            /// <returns></returns>
            public static JsonNode Create(Node node)
            {
                JsonNode jn = new JsonNode();
                jn.id = node.id;
                jn.nodeIndex = node.nodeIndex;
                jn.code = node.code;
                jn.name = node.name;
                jn.area = node.areaCode;
                jn.trade = node.sgTrade;
                jn.sgName = node.sgName;
                jn.areaName = node.areaName;
                jn.longName = node.longName;
                jn.weight = node.weight;
                jn.type = node.type;
                jn.dimession = node.index;
                jn.relationType = node.relationType;
                if (node.filter != null && node.filter.Count > 0)
                {
                    int len = node.filter.Count;
                    NodeChild[] nc = new NodeChild[len];
                    int i = 0;
                    foreach (Node n in node.filter)
                    {
                        nc[i].nodeIndex = n.nodeIndex;
                        i++;
                    }
                    jn.children = nc;
                }
                else
                {
                    jn.children = EmptyNCArray;
                }

                return jn;
            }




        }

        /// <summary>
        /// nodechild
        /// </summary>
        public struct NodeChild
        {
            public int nodeIndex;
        }



        /// <summary>
        /// 读取JSONresult中的数据
        /// </summary>
        /// <param name="jr"></param>
        /// <returns></returns>
        public static string DecodeFromJsonResult(JsonResult jr)
        {

            byte[] bs = InternalReadBytesFromJsonResult(jr);
            return Encoding.UTF8.GetString(bs);

        }


        /// <summary>
        /// 解压数据
        /// </summary>
        /// <param name="jr"></param>
        /// <returns></returns>
        private static byte[] InternalReadBytesFromJsonResult(JsonResult jr)
        {
            if (!jr.compressed)
                return jr.bytes;
            double totalSize = jr.rawLength / 1024d / 1024d;
            if (totalSize > 2.0d)
            {
                return Encoding.UTF8.GetBytes(
                    string.Format(
                    "[{0},{1},{2}]"
                    , "{\"id\":0,\"nodeIndex\":0,\"code\":null,\"name\":\"图谱过大！您的设备已无法绘制\",\"area\":null,\"areaName\":null,\"trade\":null,\"sgName\":null,\"longName\":\"图谱过大！您的设备已无法绘制\",\"weight\":0,\"type\":2,\"relationType\":0,\"dimession\":0,\"children\":[]}"

                    , "{\"id\":0,\"nodeIndex\":1,\"code\":null,\"name\":\"已终止返回数据,请尝试降低层级。\",\"area\":null,\"areaName\":null,\"trade\":null,\"sgName\":null,\"longName\":\"已终止返回数据，请尝试降低层级\",\"weight\":0,\"type\":2,\"relationType\":0,\"dimession\":0,\"children\":[{\"nodeIndex\":0}]}"


                    , string.Format("{{\"id\":0,\"nodeIndex\":2,\"code\":null,\"name\":\"{0}\",\"area\":null,\"areaName\":null,\"trade\":null,\"sgName\":null,\"longName\":\"{0}\",\"weight\":0,\"type\":2,\"relationType\":0,\"dimession\":0,\"children\":[{{\"nodeIndex\":0}},{{\"nodeIndex\":1}}]}}"
                    , string.Format("节点:{0}，连接:{1}，数据：{2}MB"
                    , jr.nodes, jr.links, (jr.rawLength / 1024d / 1024d).ToString("N2")
                        )
                    )

                    )
                    );
            }

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(jr.bytes))
            {

                using (System.IO.Compression.GZipStream gzip = new System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Decompress))
                {
                    using (System.IO.MemoryStream dcms = new System.IO.MemoryStream(jr.rawLength + 0x100))
                    {
                        gzip.CopyTo(dcms, 0x6000);
                        return dcms.ToArray();

                    }


                }

            }

        }

    }
}