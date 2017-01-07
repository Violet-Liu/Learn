using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using QZ.Foundation.Document;
using QZ.Instrument.Model;
using System.Text;
using QZ.Foundation.Utility;

namespace QZ.Service.Enterprise
{
    public class PDFReport : PDFBaseEx
    {
        private static float FontSize = 18 * 0.75f;
        private static float paddingBottom = 6f;

        #region 页眉

        /// <summary>
        /// 生成页眉
        /// </summary>
        /// <param name="fontFilePath"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public override PdfPTable GenerateHeader(iTextSharp.text.pdf.PdfWriter writer)
        {
            BaseFont baseFont = BaseFontForHeaderFooter;
            iTextSharp.text.Font font_logo = new iTextSharp.text.Font(baseFont, 30, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font font_header1 = new iTextSharp.text.Font(baseFont, 18 * 0.75f, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font font_header2 = new iTextSharp.text.Font(baseFont, 12, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font font_headerContent = new iTextSharp.text.Font(baseFont, 12, iTextSharp.text.Font.NORMAL);

            /*
            float[] widths = new float[] { 25, 300, 50, 90, 15, 20, 15 };

            PdfPTable header = new PdfPTable(widths);
            PdfPCell cell = cell = GenerateOnlyBottomBorderCell(1, iTextSharp.text.Element.ALIGN_CENTER);//new PdfPCell();
            cell.FixedHeight = 35;
            string imagePath = AppDomain.CurrentDomain.BaseDirectory + @"Images\logo.png"; // logo
            cell.Image = iTextSharp.text.Image.GetInstance(imagePath);
            header.AddCell(cell);

            cell = GenerateOnlyBottomBorderCell(1, iTextSharp.text.Element.ALIGN_LEFT);
            cell.Phrase = new Paragraph("企业信息报告", font_header1);
            header.AddCell(cell);


            cell = GenerateOnlyBottomBorderCell(1, iTextSharp.text.Element.ALIGN_RIGHT);
            cell.Phrase = new Paragraph("日期:", font_header2);
            header.AddCell(cell);

            cell = GenerateOnlyBottomBorderCell(1, iTextSharp.text.Element.ALIGN_LEFT);
            cell.Phrase = new Paragraph(DateTime.Now.ToString("yyyy-MM-dd"), font_headerContent);
            header.AddCell(cell);

            cell = GenerateOnlyBottomBorderCell(1, iTextSharp.text.Element.ALIGN_RIGHT);
            cell.Phrase = new Paragraph("第", font_header2);
            header.AddCell(cell);

            cell = GenerateOnlyBottomBorderCell(1, iTextSharp.text.Element.ALIGN_CENTER);
            cell.Phrase = new Paragraph(writer.PageNumber.ToString(), font_headerContent);
            header.AddCell(cell);

            cell = GenerateOnlyBottomBorderCell(1, iTextSharp.text.Element.ALIGN_RIGHT);
            cell.Phrase = new Paragraph("页", font_header2);
            header.AddCell(cell);
             */

            int borderWidth = 1;
            //float[] widths = new float[] { 40, 360, 80, 80};
            float[] widths = new float[] { 40, 440, 80 };
            PdfPTable header = new PdfPTable(widths);
            PdfPCell cell = GenerateOnlyBottomBorderCell(borderWidth, iTextSharp.text.Element.ALIGN_CENTER);//new PdfPCell();
            cell.FixedHeight = 35;
            string imagePath = AppDomain.CurrentDomain.BaseDirectory + @"Images\logo.png"; // logo
            cell.Image = iTextSharp.text.Image.GetInstance(imagePath);
            header.AddCell(cell);

            cell = GenerateOnlyBottomBorderCell(borderWidth, iTextSharp.text.Element.ALIGN_LEFT);
            font_header1.Color = new BaseColor(51, 51, 51);
            cell.Phrase = new Paragraph("企业信用报告", font_header1);
            cell.PaddingBottom = 15;
            header.AddCell(cell);


            //cell = GenerateOnlyBottomBorderCell(borderWidth, iTextSharp.text.Element.ALIGN_RIGHT);
            //cell.Phrase = new Paragraph("日期:", font_header2);
            //header.AddCell(cell);

            cell = GenerateOnlyBottomBorderCell(borderWidth, iTextSharp.text.Element.ALIGN_LEFT);
            font_headerContent.Color = new BaseColor(188, 188, 188);
            cell.Phrase = new Paragraph(DateTime.Now.ToString("yyyy-MM-dd"), font_headerContent);
            header.AddCell(cell);

            return header;

        }

        #endregion

        #region 页脚

        public override PdfPTable GenerateFooter(iTextSharp.text.pdf.PdfWriter writer)
        {
            BaseFont baseFont = BaseFontForHeaderFooter;
            iTextSharp.text.Font font_logo = new iTextSharp.text.Font(baseFont, 30, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font font_header1 = new iTextSharp.text.Font(baseFont, 16, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font font_header2 = new iTextSharp.text.Font(baseFont, 12, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font font_headerContent = new iTextSharp.text.Font(baseFont, 12, iTextSharp.text.Font.NORMAL);

            float[] widths = new float[] { 80, 280, 80 };
            PdfPTable footer = new PdfPTable(widths);

            PdfPCell cell = new PdfPCell();
            cell.BorderWidthTop = 1;
            cell.BorderColor = new BaseColor(240, 240, 240);
            cell.BorderWidthRight = 0;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthLeft = 0;
            cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            cell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            cell.Phrase = new Paragraph("第 " + writer.PageNumber.ToString() + " 页", font_header2);
            footer.AddCell(cell);
            cell.Phrase = new Paragraph("扫一扫，下载企业查询宝", font_header2);
            footer.AddCell(cell);

            string imagePath = AppDomain.CurrentDomain.BaseDirectory + @"Images\qr-app.png"; // logo
            cell.Image = iTextSharp.text.Image.GetInstance(imagePath);
            cell.FixedHeight = 85;//85
            footer.AddCell(cell);

            //imagePath = AppDomain.CurrentDomain.BaseDirectory + @"Images\qr-wx.png"; // logo
            //cell.Image = iTextSharp.text.Image.GetInstance(imagePath);
            //cell.Phrase = new Paragraph("", font_header2);
            //footer.AddCell(cell);

            return footer;
        }

        #endregion

        #region 生成只有底边的cell

        /// <summary>
        /// 生成只有底边的cell
        /// </summary>
        /// <param name="bottomBorder"></param>
        /// <param name="horizontalAlignment">水平对齐方式<see cref="iTextSharp.text.Element"/></param>
        /// <returns></returns>
        private PdfPCell GenerateOnlyBottomBorderCell(int bottomBorder,
                                                            int horizontalAlignment)
        {
            PdfPCell cell = GenerateOnlyBottomBorderCell(bottomBorder, horizontalAlignment, iTextSharp.text.Element.ALIGN_BOTTOM);
            cell.PaddingBottom = 10;
            cell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            cell.BorderColor = new BaseColor(240, 240, 240);
            return cell;
        }

        /// <summary>
        /// 生成只有底边的cell（带颜色）
        /// </summary>
        /// <param name="bottomBorder"></param>
        /// <param name="horizontalAlignment"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        private PdfPCell GenerateOnlyBottomBorderCell(int bottomBorder,
                                                           int horizontalAlignment, BaseColor color)
        {
            PdfPCell cell = GenerateOnlyBottomBorderCell(bottomBorder, horizontalAlignment, iTextSharp.text.Element.ALIGN_BOTTOM, color);
            cell.PaddingBottom = 5;
            return cell;
        }


        private PdfPCell GenerateOnlyBottomBorderCell(int bottomBorder,
                                                          int horizontalAlignment,
                                                          int verticalAlignment)
        {
            PdfPCell cell = GenerateOnlyBottomBorderCell(bottomBorder);
            cell.HorizontalAlignment = horizontalAlignment;
            cell.VerticalAlignment = verticalAlignment;
            return cell;
        }

        /// <summary>
        /// 生成只有底边的cell（带颜色）
        /// </summary>
        /// <param name="bottomBorder">边框大小</param>
        /// <param name="horizontalAlignment">水平对齐方式</param>
        /// <param name="verticalAlignment">垂直对齐方式</param>
        /// <param name="color">边框颜色</param>
        /// <returns></returns>
        private PdfPCell GenerateOnlyBottomBorderCell(int bottomBorder,
                                                            int horizontalAlignment,
                                                            int verticalAlignment, BaseColor color)
        {
            PdfPCell cell = GenerateOnlyBottomBorderCell(0);//bottomBorder
            cell.HorizontalAlignment = horizontalAlignment;
            cell.VerticalAlignment = verticalAlignment;
            //cell.BackgroundColor = color;
            return cell;
        }


        /// <summary>
        /// 生成只有底边的cell
        /// </summary>
        /// <param name="bottomBorder"></param>
        /// <returns></returns>
        private PdfPCell GenerateOnlyBottomBorderCell(int bottomBorder)
        {
            PdfPCell cell = new PdfPCell();
            cell.BorderWidthBottom = bottomBorder;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthTop = 0;
            cell.BorderWidthRight = 0;
            return cell;
        }

        #endregion

        #region 导出PDF

        /// <summary>
        /// 导出PDF
        /// </summary>
        /// <param name="path">导出路径</param>
        public static void ExportPDF(String path, string oc_area, string oc_code)
        {

            Document document = new Document();// new Document(PageSize.A4.Rotate(), -90, -90, 60, 10);//此处设置的偏移量是为了加大页面的可用范围，可以使用默认.
            PDFReport pdfReport = new PDFReport();
            document.SetMargins(0, 0, 80, 120); // 设置正文的Margins

            try
            {
                PdfWriter pdfWriter = PdfWriter.GetInstance(document, new FileStream(path, FileMode.Create));
                //PdfWriter pdfWriter = PdfWriter.GetInstance(document, stream);
                pdfWriter.PageEvent = pdfReport;//此处一定要赋值，才能触发页眉和页脚的处理
                document.Open();
                pdfReport.AddBody(document, oc_area, oc_code);
            }
            catch (Exception e)
            {
            }
            finally
            {
                document.Close();


                // 创建一个PdfReader对象
                //PdfReader reader = new PdfReader(path);
                // 获得文档页数
                //sumPageNum = reader.NumberOfPages;
            }

        }

        /// <summary>
        /// 导出PDF到流
        /// </summary>
        /// <param name="path">导出路径</param>
        public static void ExportPDF(string oc_area, string oc_code, MemoryStream stream)
        {

            Document document = new Document();// new Document(PageSize.A4.Rotate(), -90, -90, 60, 10);//此处设置的偏移量是为了加大页面的可用范围，可以使用默认.
            PDFReport pdfReport = new PDFReport();
            document.SetMargins(0, 0, 80, 120); // 设置正文的Margins

            try
            {
                //PdfWriter pdfWriter = PdfWriter.GetInstance(document, new FileStream(path, FileMode.Create));
                PdfWriter pdfWriter = PdfWriter.GetInstance(document, stream);
                pdfWriter.PageEvent = pdfReport;//此处一定要赋值，才能触发页眉和页脚的处理
                document.Open();
                pdfReport.AddBody(document, oc_area, oc_code);
                pdfWriter.Flush();
                stream.Flush();
            }
            catch (Exception ex)
            {
                LogError log = new LogError();
                log.err_source = ex.Source;
                log.err_time = DateTime.Now;
                log.err_stack = ex.StackTrace;
                log.err_referrer = "ExportPDF";
                log.err_type = "error log";
                log.err_url = "";
                log.err_ip = "";
                log.err_user = oc_code;
                log.err_message = ex.Message;
                var es = ex as ExceptionServer;
                log.err_guid = $"{DateTime.Now.ToString("yyyyMMdd")}-{Cipher_Md5.Md5_16(Guid.NewGuid().ToString())}";
                DataAccess.ErrorLog_Insert(log);
                return;
            }
            finally
            {
                document.Close();
            }
        }


        /// <summary>
        /// 导出PDF
        /// </summary>
        /// <param name="path">导出路径</param>
        //public static byte[] ExportPDF(string obj)
        //{
        //    PDFReport pdfReport = new PDFReport();
        //    Document document = new Document(PageSize.A4.Rotate(), -90, -90, 60, 10);//此处设置的偏移量是为了加大页面的可用范围，可以使用默认.
        //    MemoryStream ms = new MemoryStream();
        //    PdfWriter pdfWriter = PdfWriter.GetInstance(document, ms);
        //    pdfWriter.PageEvent = pdfReport;//此处一定要赋值，才能触发页眉和页脚的处理
        //    document.Open();
        //    pdfReport.AddBody(document);
        //    document.Close();
        //    byte[] buff = ms.ToArray();
        //    return buff;
        //}

        #endregion

        #region 添加正文内容

        private void AddBody(Document document, string oc_area, string oc_code)
        {
            Font font = new Font(BaseFontForBody, FontSize);
            Cover(oc_code); // 封面
            document.NewPage();


            BaseColor grey = new BaseColor(236, 126, 50);
            AddTitleWithBottomBorder("一、基本信息", grey);
            var info = DataAccess.OrgCompanySitePackSelect(oc_code);
            //工商基本信息
            AddOrgCompanyInfo(info, grey);
            AddText("", font, 120);
            document.NewPage();
            //分支机构
            AddFetchListInfo(info.list.oc_name, grey);
            AddText("", font, 68);
            document.NewPage();
            //变更记录
            AddDtl_Evt(info, grey);
            AddText("", font, 68);
            document.NewPage();
            //主要成员
            AddChengyuan(oc_area, oc_code, grey);
            AddText("", font, 68);
            document.NewPage();
            AddTitleWithBottomBorder("二、股东及出资信息", grey);
            //股东出资信息
            AddDtlGDInfo(oc_area, oc_code, info.dtl.IsNotNull() ? info.dtl.od_regM : 0);
            AddText("", font, 68);
            document.NewPage();
            AddTitleWithBottomBorder("三、企业对外投资", grey);
            //企业对外投资
            AddDWTZInfo(info, grey);
            AddText("", font, 68);
            document.NewPage();
            AddTitleWithBottomBorder("四、司法信息", grey);
            //4.1被执行人信息
            AddExecutedInfo(oc_code, grey);
            AddText("", font, 68);
            //4.2失信被执行信息
            AddShixinInfo(oc_code, grey);
            AddText("", font, 68);
            //4.3法院判决书
            AddJudgeInfo(oc_code, grey);
            AddText("", font, 68);
            document.NewPage();
            AddTitleWithBottomBorder("五、年报信息", grey);
            AddNBInfo(info, grey);
            AddText("", font, 68);
            document.NewPage();
            AddTitleWithBottomBorder("六、知识产权", grey);
            //6.1 商标信息
            AddBrandInfo(oc_code, grey);
            AddText("", font, 68);
            //6.2 专利
            AddPatentInfo(oc_code, grey);
            AddText("", font, 68);
            // 6.3 软件著作权
            AddSoftwareCopyrightInfo(oc_code, grey);
            AddText("", font, 68);
            //6.4 作品著作权
            AddProductCopyrightInfo(oc_code, grey);
            AddText("", font, 68);
            //6.5 资质认证
            AddCertificationInfo(oc_code, grey);
            AddText("", font, 68);
            //6.6域名信息
            AddSiteInfo(oc_code, grey);
            AddText("", font, 68);
            document.NewPage();
            AddTitleWithBottomBorder("七、经营信息", grey);
            //7.1 产品信息
            AddOrgGS1ItemInfo(oc_code, grey);
            AddText("", font, 68);
            //7.2 资招聘信息
            AddJobInfo(oc_code, grey);
            AddText("", font, 68);
            document.NewPage();
            AddTitleWithBottomBorder("八、参展信息", grey);
            AddExhibition(oc_code, grey);



        }

        /// <summary>
        /// 封面
        /// </summary>
        private void Cover(string oc_code)
        {
            //Font font = new Font(BaseFontForHeaderFooter, FontSize);

            Font font = new Font(BaseFontForHeaderFooter, 22);
            Font font1 = new Font(BaseFontForHeaderFooter, 16);
            Font font2 = new Font(BaseFontForHeaderFooter, 12);
            AddText("", font, 80);
            string imagePath = AppDomain.CurrentDomain.BaseDirectory + @"Images\企业查询宝.png";
            Image image = Image.GetInstance(imagePath);
            PdfPCell cell = new PdfPCell(image, true);
            cell.FixedHeight = 100;
            cell.Border = 0;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPTable table = new PdfPTable(1);
            table.SpacingBefore = 80;
            table.AddCell(cell);
            Document.Add(table);

            OrgCompanyListInfo listInfo = DataAccess.OrgCompanyList_Select(oc_code);
            //AddText("CHINA", font, 50);
            AddText("企业信用报告", font, 30);
            AddText("", font, 30);
            AddText(listInfo.oc_name, font, 30);
            AddText("", font, 30);
            AddText("一、企业基本信息：工商基本信息、分支机构、变更记录、主要成员", font1, 10,iTextSharp.text.Element.ALIGN_LEFT);
            AddText("二、股东及出资信息（含出资比例）", font1, 10, iTextSharp.text.Element.ALIGN_LEFT);
            AddText("三、企业对外投资（含出资比例）", font1, 10, iTextSharp.text.Element.ALIGN_LEFT);
            AddText("四、司法信息：被执行人、失信人、法院判决书", font1, 10, iTextSharp.text.Element.ALIGN_LEFT);
            AddText("五、年报信息", font1, 10, iTextSharp.text.Element.ALIGN_LEFT);
            AddText("六、知识产权：商标、专利、软件著作权、作品著作权、资质认证、域名", font1, 10, iTextSharp.text.Element.ALIGN_LEFT);
            AddText("七、经营信息：商品信息、招聘信息", font1, 10, iTextSharp.text.Element.ALIGN_LEFT);
            AddText("八 、参展信息", font1, 10, iTextSharp.text.Element.ALIGN_LEFT); 
            AddText("", font1, 50);
            AddText("敬启者：本内容是企业查询宝接受你的委托，查询公开信息所得结果，仅供参考", font2, 10);
            //string imagePath = AppDomain.CurrentDomain.BaseDirectory + @"Images\qr-app.png";
            //AddText(listInfo.oc_updatetime.ToString("yyyy-MM-dd"), font, 50);
        }


        #region 一、基本信息



        /// <summary>
        /// 主体信息
        /// </summary>
        private void AddDtlInfo(string oc_code)
        {
            OrgCompanyDtlInfo data = DataAccess.New_OrgCompanyDtl_Select(oc_code);
            BaseColor grey = new BaseColor(174, 216, 31);
            AddTitleWithBottomBorder("▪ 主体信息", grey);

            if (data == null || data.od_id <= 0)
            {
                Font font = new Font(BaseFontForBody, FontSize);
                AddText("暂无相关信息", font, 0);
                return;
            }
            AddText("企业名称:", data.oc_name, paddingBottom);
            AddText("工商注册号:", data.oc_number, paddingBottom);
            AddText("法定代表人:", data.od_faRen, paddingBottom);
            AddText("法人注册号:", data.oc_number, paddingBottom);
            AddText("注册资本:", data.od_regMoney, paddingBottom);
            AddText("市场主体类型:", data.od_regType, paddingBottom);
            AddText("注册登记日期:", data.od_regDate, paddingBottom);
            AddText("经营期限:", (string.IsNullOrEmpty(data.od_bussinessS) ? "****" : data.od_bussinessS) + " 至 " + (string.IsNullOrEmpty(data.od_bussinessE) ? "永续经营" : data.od_bussinessE), paddingBottom);
            AddText("经营状态:", Util.BusinessStatus_Get(data.od_ext), paddingBottom);
            AddText("经营范围:", data.od_bussinessDes, paddingBottom);
            AddText("地址:", data.oc_address, 0);
            AddText("备注:", data.od_ext, 0);

        }

        /// <summary>
        /// 机构信息
        /// </summary>
        /// <param name="oc_code"></param>
        /// <returns></returns>
        private void AddListInfo(string oc_code)
        {
            OrgCompanyListInfo listInfo = DataAccess.OrgCompanyList_Select(oc_code);


            BaseColor grey = new BaseColor(174, 216, 31);
            AddTitleWithBottomBorder("▪ 机构信息", grey);
            if (listInfo == null || listInfo.oc_id <= 0)
            {
                Font font = new Font(BaseFontForBody, FontSize);
                AddText("暂无相关信息", font, 0);
                return;
            }
            AddText("机构代码", listInfo.oc_code, paddingBottom);
            AddText("机构类型:", listInfo.oc_companytype, paddingBottom);
            AddText("注册登记机构:", listInfo.oc_regOrgName, paddingBottom);
            AddText("机构登记证号:", listInfo.oc_number, paddingBottom);
            AddText("所有地区:", listInfo.oc_areaName, 0);
            AddText("代码证有效期:", listInfo.oc_issuetime.ToString("yyyy-MM-dd") + " 至 " + listInfo.oc_invalidtime.ToString("yyyy-MM-dd"), 0);
        }

        /// <summary>
        /// 工商信息
        /// </summary>
        private void AddOrgCompanyInfo(OrgCompanyDtlPack4Site info, BaseColor grey)
        {
            AddTitleWithBottomBorder("1.1 工商基本信息", grey);
            if (info==null)
            {
                Font font = new Font(BaseFontForBody, FontSize);
                AddText("暂无相关信息", font, 0);
                return;
            }
            else
            {
                if (info.dtl.IsNotNull())
                {

                    AddText("企业名称:", info.dtl.oc_name.NullToString(info.list.IsNotNull() ? info.list.oc_name : ""), paddingBottom);
                    AddDoubleText("工商注册号:", info.list.oc_number, "社会信用代码:", info.list.oc_creditcode, paddingBottom);
                    AddDoubleText( "法定代表人:", info.dtl.od_faRen, "组织机构代码:", info.list.oc_code, paddingBottom);
                    AddDoubleText("经营状态:", Util.BusinessStatus_Get(info.dtl.od_ext), "工商注册号:", info.list.oc_number, paddingBottom);
                    AddDoubleText("企业类型:", info.dtl.od_regType, "成立日期:", info.dtl.od_regDate, paddingBottom);
                    AddDoubleText("注册资本:", info.dtl.od_regMoney, "经营状态:", Util.BusinessStatus_Get(info.dtl.od_ext), paddingBottom);

                    //AddText("工商注册号:", info.dtl.oc_number, paddingBottom);
                    //AddText("社会信用代码:", info.dtl.oc_creditcode, paddingBottom);
                    //AddText("法定代表人:", info.dtl.od_faRen, paddingBottom);
                    //AddText("组织机构代码:", info.dtl.oc_code, paddingBottom);             
                    //AddText("企业类型:", info.dtl.od_regType, paddingBottom);
                    //AddText("成立日期:", info.dtl.od_regDate, paddingBottom);
                    //AddText("注册资本:", info.dtl.od_regMoney, paddingBottom);
                    //AddText("经营状态:", OrgCompanyHelper.GetStatus(true, info.dtl.od_ext), paddingBottom);
                    AddText("注册地址:", info.list.oc_address, 0);
                    AddText("经营期限:", (string.IsNullOrEmpty(info.dtl.od_bussinessS) ? "****" : info.dtl.od_bussinessS) + " 至 " + (string.IsNullOrEmpty(info.dtl.od_bussinessE) ? "永续经营" : info.dtl.od_bussinessE), paddingBottom);
                    AddText("经营范围:", info.dtl.od_bussinessDes, paddingBottom);
                }
                if (info.list.IsNotNull())
                {
                    DateTime dt = DateTime.Now;
                    DateTime.TryParse(info.dtl.od_chkDate, out dt);
                    AddDoubleText("登记机关:", info.list.oc_regOrgName, "发照日期", dt.ToString("yyyy-MM-dd"), paddingBottom);
                    //"代码证有效期:", info.list.oc_issuetime.ToString("yyyy-MM-dd") + " 至 " + info.list.oc_invalidtime.ToString("yyyy-MM-dd"), paddingBottom);
                    //AddText("登记机关:", info.list.oc_regOrgName, paddingBottom);
                    //AddText("代码证有效期:", info.list.oc_issuetime.ToString("yyyy-MM-dd") + " 至 " + info.list.oc_invalidtime.ToString("yyyy-MM-dd"), 0);
                }

            }


        }

        /// <summary>
        /// 1.2分支机构
        /// </summary>
        /// <param name="ct_name"></param>
        /// <param name="grey"></param>
        private void AddFetchListInfo(string ct_name, BaseColor grey)
        {
            AddTitleWithBottomBorder("1.2 分支机构", grey);

            BaseFont baseFont = BaseFontForBody;
            iTextSharp.text.Font font_contentNormal = new iTextSharp.text.Font(baseFont, FontSize, iTextSharp.text.Font.NORMAL);
            // 模块标题

            if (ct_name==null)
            {
                Font font = new Font(BaseFontForBody, FontSize);
                AddText("暂无相关信息", font, 0);
                return;
            }
            //获取10条分支机构
           var fetchlist = CompanyNameIndex_Proxy.Company_Mini_Info_Get(ct_name.Replace("（", "(").Replace("）", ")"), 1, 10).ToList();
            var FetchListSet = new List<KeyValuePair<string, string>>();
            if (fetchlist.Count > 0)
            {
                //设置code编码
                fetchlist.ForEach(u => FetchListSet.Add(new KeyValuePair<string, string>(u.Key, u.Value.Length > 9 ? u.Value.Substring(0, 9).ToAesEncrypt16CodeKey() : u.Value.ToAesEncrypt16CodeKey())));
            }

            // 表格标题
            string[] titles = { "序号", "公司名称", "营业执照", "登记机关" };
            float[] widths = new float[] { 20, 50, 20, 30 };
            AddColumnTitle(titles, widths);
            // 表格内容
            PdfPTable bodyTable = new PdfPTable(widths);

            StringBuilder sboc_code = new StringBuilder();
            for (int i = 0; i < fetchlist.Count; i++)
            {
                var oc_code = fetchlist[i].Value.Length > 9 ? fetchlist[i].Value.Substring(0, 9) : fetchlist[i].Value;
                sboc_code.Append("'");
                sboc_code.Append(oc_code);
                if (i < fetchlist.Count - 1)
                {
                    sboc_code.Append("',");
                }
                else
                {
                    sboc_code.Append("'");
                }
            }

            if (sboc_code.ToString().IsNotNull())
            {
                var list = DataAccess.OrgCompanyList_Selectinoc_codes(sboc_code.ToString());
                for (int i = 0; i < FetchListSet.Count; i++)
                {
                    AddBodyContentCell(bodyTable, String.Format("{0}", (i + 1).ToString()), font_contentNormal, 1);
                    AddBodyContentCell(bodyTable, String.Format("{0}", fetchlist[i].Key), font_contentNormal, 1);
                    if (list.IsNotNull() && list.Count > 0)
                    {
                        var info = list.Where(u => u.oc_code == (fetchlist[i].Value.Length > 9 ? fetchlist[i].Value.Substring(0, 9) : fetchlist[i].Value)).FirstOrDefault();
                        if (info.IsNotNull())
                        {
                            AddBodyContentCell(bodyTable, String.Format("{0}", info.oc_number), font_contentNormal, 1);
                            AddBodyContentCell(bodyTable, String.Format("{0}", info.oc_regOrgName), font_contentNormal, 1, true, true);
                        }
                        else
                        {
                            AddBodyContentCell(bodyTable, String.Format("{0}", ""), font_contentNormal, 1);
                            AddBodyContentCell(bodyTable, String.Format("{0}", ""), font_contentNormal, 1, true, true);
                        }
                    }
                    else
                    {
                        AddBodyContentCell(bodyTable, String.Format("{0}", ""), font_contentNormal, 1);
                        AddBodyContentCell(bodyTable, String.Format("{0}", ""), font_contentNormal, 1, true, true);
                    }

                }

            }
            Document.Add(bodyTable);

        }

        /// <summary>
        /// 1.3最新变更信息
        /// </summary>
        /// <param name="info"></param>
        /// <param name="grey"></param>
        private void AddDtl_Evt(OrgCompanyDtlPack4Site info, BaseColor grey)
        {
            AddTitleWithBottomBorder("1.3 变更记录", grey);
            List<OrgCompanyGsxtBgsxInfo> bgList = new List<OrgCompanyGsxtBgsxInfo>();
            //台湾、香港没有股东、成员信息
            if (!info.list.oc_area.StartsWith("71") && !info.list.oc_area.StartsWith("81"))
            {
                if (info.list.oc_area == "4403")
                {
                    List<OrgCompanyDtl_EvtInfo> _bgList = info.dtlBgsxList != null ? info.dtlBgsxList : new List<OrgCompanyDtl_EvtInfo>();
                    foreach (var item in _bgList)
                    {
                        bgList.AddRange(SplitOrgCompanyGsxtBgsx(item));
                    }
                    if (bgList.Count > 0)
                    {
                        bgList = bgList.OrderByDescending(u => u.bgrq).ToList();
                    }
                }
                else
                {
                    bgList = info.gsxtBgsxList != null ? info.gsxtBgsxList : new List<OrgCompanyGsxtBgsxInfo>();
                }
            }
            if (bgList.IsNotNull() && bgList.Count > 0)
            {
                BaseFont baseFont = BaseFontForBody;
                iTextSharp.text.Font font_contentNormal = new iTextSharp.text.Font(baseFont, (float)(16 * 0.75), iTextSharp.text.Font.NORMAL);

                // 表格标题
                string[] titles = { "序号", "变更日期", "变更事项", "变更前内容", "变更后内容" };
                float[] widths = new float[] { 18, 18, 18, 30, 30 };
                AddColumnTitle(titles, widths);
                // 表格内容
                PdfPTable bodyTable = new PdfPTable(widths);
                for (int i = 0; i < bgList.Count; i++)
                {
                    AddBodyContentCell(bodyTable, String.Format("{0}", (i + 1).ToString()), font_contentNormal, 1, true, false);
                    AddBodyContentCell(bodyTable, String.Format("{0}", bgList[i].bgsx), font_contentNormal, 1, true, false);
                    AddBodyContentCell(bodyTable, String.Format("{0}", bgList[i].bgsx), font_contentNormal, 1, true, false);
                    AddBodyContentCell(bodyTable, String.Format("{0}", bgList[i].bgq), font_contentNormal, 1, true, false);
                    AddBodyContentCell(bodyTable, String.Format("{0}", bgList[i].bgh), font_contentNormal, 1, true, true);
                }
                Document.Add(bodyTable);
            }
        }

        /// <summary>
        /// 1.4成员信息
        /// </summary>
        private void AddChengyuan(string oc_area, string oc_code, BaseColor grey)
        {
            AddTitleWithBottomBorder("1.4 主要成员", grey);
            BaseFont baseFont = BaseFontForBody;
            iTextSharp.text.Font font_contentNormal = new iTextSharp.text.Font(baseFont, FontSize, iTextSharp.text.Font.NORMAL);
            // 表格标题
            string[] titles = { "序号", "姓名", "职位" };
            float[] widths = new float[] { 20, 20, 20 };
            AddColumnTitle(titles, widths);

            // 表格内容
            PdfPTable bodyTable = new PdfPTable(widths);
            //oc_area = oc_area.Substring(0, 2);

            //成员信息
            List<OrgCompanyGsxtDtlMgrInfo> cyList = new List<OrgCompanyGsxtDtlMgrInfo>();
            int rowcount = 0;

            //台湾、香港没有股东、成员信息

            if (oc_area == "4403")
            {
                List<OrgCompanyDtlMgrInfo> _cyList = DataAccess.OrgCompanyDtlMgr_Selectbyom_oc_code(oc_code);
                if (_cyList.IsNotNull() && _cyList.Count > 0)
                {
                    _cyList = _cyList.OrderBy(u => u.om_position).ToList();

                }

                for (int i = 0; i < _cyList.Count; i++)
                {
                    if (i >= 10)
                    {
                        break;
                    }
                    AddBodyContentCell(bodyTable, String.Format("{0}", (i + 1).ToString()), font_contentNormal, 1);
                    AddBodyContentCell(bodyTable, String.Format("{0}", _cyList[i].om_name), font_contentNormal, 1);
                    AddBodyContentCell(bodyTable, String.Format("{0}", _cyList[i].om_position), font_contentNormal, 1, true, true);
                }
            }

            else
            {
                if (!oc_area.StartsWith("71") && !oc_area.StartsWith("81"))
                {
                    DatabaseSearchModel model = new DatabaseSearchModel().SetWhere($"om_oc_code='{oc_code}'").SetOrder("om_id asc").SetPageIndex(1).SetPageSize(10);
                    cyList = DataAccess.OrgCompanyGsxtDtlMgr_Page_Select(model,oc_area.Substring(0, 2))
                        .Where(x => (x.om_name != "") && (x.om_status != 4)).OrderBy(u => u.om_position).ToList();
                    for (int i = 0; i < cyList.Count; i++)
                    {
                        if (i >= 10)
                        {
                            break;
                        }
                        AddBodyContentCell(bodyTable, String.Format("{0}", (i + 1).ToString()), font_contentNormal, 1);
                        AddBodyContentCell(bodyTable, String.Format("{0}", cyList[i].om_name), font_contentNormal, 1);
                        AddBodyContentCell(bodyTable, String.Format("{0}", cyList[i].om_position), font_contentNormal, 1, true, true);
                    }
                }
            }
            Document.Add(bodyTable);

        }


        #endregion

        #region 二、股东及出资信息
        /// <summary>
        /// 二、股东出资信息
        /// </summary>
        /// <param name="oc_area"></param>
        /// <param name="oc_code"></param>
        private void AddDtlGDInfo(string oc_area, string oc_code, decimal money)
        {
            #region 股东信息

            List<OrgCompanyGsxtDtlGDInfo> gdList = new List<OrgCompanyGsxtDtlGDInfo>();
            int gdrowcount = 0;
            //台湾、香港没有股东、成员信息
            if (!oc_area.StartsWith("71") && !oc_area.StartsWith("81"))
            {
                //深圳
                if (oc_area == "4403")
                {
                    DatabaseSearchModel model = new DatabaseSearchModel().SetWhere($"og_oc_code='{oc_code}'").SetOrder("og_int desc").SetPageIndex(1).SetPageSize(10);

                    var dtlGdList = DataAccess.OrgCompanyDtlGD_SelectPaged(model); 
                    List<OrgCompanyDtlGDInfo> _gdList = dtlGdList != null ? dtlGdList : new List<OrgCompanyDtlGDInfo>();
                    foreach (var item in _gdList)
                    {
                        gdList.Add(new OrgCompanyGsxtDtlGDInfo()
                        {
                            og_name = item.og_name,
                            og_type = item.og_type,
                            og_subscribeAccount = item.og_money,
                            og_realAccount = item.og_money,
                            og_unit = item.og_unit
                        });
                    }
                }
                else
                {
                    DatabaseSearchModel model = new DatabaseSearchModel().SetWhere($"og_oc_code='{oc_code}'").SetWhere("og_status <>4").SetOrder("og_int desc").SetPageIndex(1).SetPageSize(10);
                    var gsxtGdList = DataAccess.OrgCompanyGsxtDtlGD_Page_Select(model, oc_area);
                    gdList = gsxtGdList != null ? gsxtGdList : new List<OrgCompanyGsxtDtlGDInfo>();
                    if (gdList.IsNotNull() && gdList.Count > 0)
                    {
                        gdList = gdList.Where(x => x.og_status != 4).ToList();
                    }
                }
            }
            if (gdList.IsNotNull() && gdList.Count > 0)
            {
                BaseFont baseFont = BaseFontForBody;
                iTextSharp.text.Font font_contentNormal = new iTextSharp.text.Font(baseFont, FontSize, iTextSharp.text.Font.NORMAL);
                // 表格标题
                string[] titles = { "序号", "股东", "认缴出资额（万元）", "币种", "出资比例", "认缴出资日期" };
                float[] widths = new float[] { 15, 30, 20, 15, 25, 20 };
                AddColumnTitle(titles, widths);

                // 表格内容
                PdfPTable bodyTable = new PdfPTable(widths);

                for (int i = 0; i < gdList.Count; i++)
                {
                    //只显示10条数据
                    if (i >= 10)
                    {
                        break;
                    }
                    AddBodyContentCell(bodyTable, string.Format("{0}", (i + 1).ToString()), font_contentNormal, 1);
                    AddBodyContentCell(bodyTable, string.Format("{0}", gdList[i].og_name), font_contentNormal, 1);
                    AddBodyContentCell(bodyTable, string.Format("{0}", gdList[i].og_subscribeAccount.ToString("0.0") + gdList[i].og_unit), font_contentNormal, 1);
                    AddBodyContentCell(bodyTable, string.Format("{0}", gdList[i].og_unit), font_contentNormal, 1);
                    if (money.ToString("0.00") != "0.00")
                    {
                        AddBodyContentCell(bodyTable, string.Format("{0}", ((gdList[i].og_subscribeAccount / money) * 100).ToString("0.00") + "%"), font_contentNormal, 1);
                    }
                    else
                    {
                        AddBodyContentCell(bodyTable, string.Format("{0}", "无"), font_contentNormal, 1);
                    }
                    AddBodyContentCell(bodyTable, string.Format("{0}", gdList[i].og_subscribeDate.NullToString("无")), font_contentNormal, 1, true, true);
                }
                Document.Add(bodyTable);
            }
            else
            {
                Font font = new Font(BaseFontForBody, FontSize);
                AddText("暂无相关信息", font, 0);
                return;
            }

            #endregion 股东信息
        }
        #endregion

        #region 三、企业对外投资
        /// <summary>
        /// 对外投资
        /// </summary>
        private void AddDWTZInfo(OrgCompanyDtlPack4Site info, BaseColor grey)
        {
            BaseFont baseFont = BaseFontForBody;
            iTextSharp.text.Font font_contentNormal = new iTextSharp.text.Font(baseFont, FontSize, iTextSharp.text.Font.NORMAL);


            if (info.IsNull())
            {
                Font font = new Font(BaseFontForBody, FontSize);
                AddText("暂无相关信息", font, 0);
                return;
            }
            var listCompanyRelation = new List<CompanyRelation>();
            int listCompanyRelationRowCount = 0;
            if (info.list.oc_name.IsNotNull())
            {
                var orgName = info.list.oc_name.Replace("（", "(").Replace("）", ")");
                listCompanyRelation = CompanyMap_Proxy.Company_Invest_Get(orgName);
                listCompanyRelationRowCount = listCompanyRelation.Count;
                if (listCompanyRelation.IsNotNull() && listCompanyRelation.Count > 0)
                {
                    listCompanyRelation = listCompanyRelation.Skip(0).Take(10).ToList();
                }
            }
            if (listCompanyRelation.IsNotNull())
            {
                // 表格标题
                string[] titles = { "序号", "企业名称", "营业执照", "企业类型", "企业状态", "注册资本(万元)", "出资比例", "法定代表人" };
                float[] widths = new float[] { 15, 30, 30, 30, 20, 20, 20, 20 };
                AddColumnTitle(titles, widths);
                // 表格内容
                PdfPTable bodyTable = new PdfPTable(widths);

                for (int i = 0; i < listCompanyRelation.Count; i++)
                {
                    var infotwo = DataAccess.OrgCompanySitePackSelect(listCompanyRelation[i].code);
                    if (!infotwo.list.oc_area.StartsWith("71") && !infotwo.list.oc_area.StartsWith("81"))
                    {
                        AddBodyContentCell(bodyTable, String.Format("{0}", (i + 1).ToString()), font_contentNormal, 1);
                        AddBodyContentCell(bodyTable, String.Format("{0}", infotwo.list.oc_name), font_contentNormal, 1);
                        AddBodyContentCell(bodyTable, String.Format("{0}", infotwo.list.oc_number), font_contentNormal, 1);
                        AddBodyContentCell(bodyTable, String.Format("{0}", infotwo.dtl.IsNotNull() ? infotwo.dtl.od_regType : ""), font_contentNormal, 1);
                        AddBodyContentCell(bodyTable, String.Format("{0}", Util.BusinessStatus_Get(infotwo.dtl.IsNotNull() ? infotwo.dtl.od_ext : string.Empty)), font_contentNormal, 1);
                        AddBodyContentCell(bodyTable, String.Format("{0}", infotwo.dtl.IsNotNull() ? infotwo.dtl.od_regM.ToString() : ""), font_contentNormal, 1);
                        List<OrgCompanyGsxtDtlGDInfo> gdList = new List<OrgCompanyGsxtDtlGDInfo>();
                        if (infotwo.dtl.IsNotNull())
                        {
                            //深圳
                            if (infotwo.list.oc_area == "4403")
                            {
                                List<OrgCompanyDtlGDInfo> _gdList = infotwo.dtlGdList != null ? infotwo.dtlGdList : new List<OrgCompanyDtlGDInfo>();
                                foreach (var item in _gdList)
                                {
                                    gdList.Add(new OrgCompanyGsxtDtlGDInfo()
                                    {
                                        og_name = item.og_name,
                                        og_type = item.og_type,
                                        og_subscribeAccount = item.og_money,
                                        og_realAccount = item.og_money,
                                        og_unit = item.og_unit,
                                        og_oc_code = item.og_oc_code
                                    });
                                }
                            }
                            else
                            {
                                gdList = infotwo.gsxtGdList != null ? infotwo.gsxtGdList : new List<OrgCompanyGsxtDtlGDInfo>();
                            }

                            if (gdList.IsNotNull() && gdList.Count > 0)
                            {
                                gdList = gdList.Where(x => x.og_status != 4).ToList();
                            }
                            List<string> companyNameList = new List<string>();
                            foreach (var gd in gdList)
                            {
                                if (!gd.og_type.Trim().StartsWith("自然人"))
                                {
                                    if (gd.og_name.Length >= 5)
                                    {
                                        companyNameList.Add(gd.og_name.Replace("（", "(").Replace("）", ")"));
                                    }
                                }
                            }
                            // 根据公司名获得公司代码
                            List<KeyValuePair<string, string>> companyNameDic = CompanyNameIndex_Proxy.Company_Mini_Info_Get(companyNameList);
                            gdList.ForEach(x =>
                            {
                                // 如果股东类型不是"自然人"，即公司，则需要进一步查看该公司的信息
                                if (x.og_name.Trim().Length >= 5)
                                {
                                    foreach (var dic in companyNameDic)
                                    {
                                        if (dic.Key.IsNotNull())
                                        {
                                            if (dic.Key == x.og_name.Replace("（", "(").Replace("）", ")"))
                                            {
                                                x.og_oc_code = dic.Value;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    x.og_oc_code = x.og_oc_code;
                                }
                            });



                            var entity = gdList.Where(u => (u.og_oc_code.Length > 9 ? u.og_oc_code.Substring(0, 9) : u.og_oc_code) == info.list.oc_code).FirstOrDefault();
                            if (entity.IsNotNull())
                            {
                                if (infotwo.dtl.od_regM.ToString("0.00") != "0.00")
                                {
                                    AddBodyContentCell(bodyTable, String.Format("{0}", (entity.og_subscribeAccount / infotwo.dtl.od_regM * 100).ToString("0.00") + "%"), font_contentNormal, 1);
                                }
                                else
                                {
                                    AddBodyContentCell(bodyTable, String.Format("{0}", ""), font_contentNormal, 1);
                                }

                            }
                            else
                            {
                                AddBodyContentCell(bodyTable, String.Format("{0}", ""), font_contentNormal, 1);
                            }
                        }
                        else
                        {
                            AddBodyContentCell(bodyTable, String.Format("{0}", ""), font_contentNormal, 1);
                        }
                        AddBodyContentCell(bodyTable, String.Format("{0}", infotwo.dtl.IsNotNull() ? infotwo.dtl.od_faRen.ToString() : ""), font_contentNormal, 1, true, true);
                    }
                }
                Document.Add(bodyTable);
            }

        }
        #endregion

        #region 四、司法信息
        /// <summary>
        /// 被执行人信息
        /// </summary>
        private void AddExecutedInfo(string oc_code, BaseColor grey)
        {
            AddTitleWithBottomBorder("4.1 被执行人信息", grey);
            BaseFont baseFont = BaseFontForBody;
            iTextSharp.text.Font font_contentNormal = new iTextSharp.text.Font(baseFont, FontSize, iTextSharp.text.Font.NORMAL);
            int outcount = 0;
            DatabaseSearchModel model = new DatabaseSearchModel().SetWhere($"oc_code='{oc_code}'").SetOrder("zx_caseCreateTime desc").SetPageIndex(1).SetPageSize(10);
            var list = DataAccess.ZhiXing_SelectPaged(model, out outcount);

            if (list.IsNotNull() && list.Count > 0)
            {
                // 表格标题
                string[] titles = { "序号", "案号", "执行法院", "执行标的", "立案时间" };
                float[] widths = new float[] { 20, 50, 50, 30, 30 };
                AddColumnTitle(titles, widths);
                // 表格内容
                PdfPTable bodyTable = new PdfPTable(widths);
                for (int i = 0; i < list.Count; i++)
                {
                    AddBodyContentCell(bodyTable, String.Format("{0}", (i + 1).ToString()), font_contentNormal, 1);
                    AddBodyContentCell(bodyTable, String.Format("{0}", list[i].zx_caseCode.NullToString("--")), font_contentNormal, 1);
                    AddBodyContentCell(bodyTable, String.Format("{0}", list[i].zx_execCourtName.NullToString("--")), font_contentNormal, 1);
                    AddBodyContentCell(bodyTable, String.Format("{0}", list[i].zx_execMoney.NullToString("--")), font_contentNormal, 1);
                    AddBodyContentCell(bodyTable, String.Format("{0}", list[i].zx_caseCreateTime.IsNotNull() ? list[i].zx_caseCreateTime.ToString("yyyy-MM-dd") : "--"), font_contentNormal, 1, true, true);
                }

                Document.Add(bodyTable);
            }
            else
            {
                Font font = new Font(BaseFontForBody, FontSize);
                AddText("暂无相关信息", font, 0);
                return;
            }
        }


        /// <summary>
        /// 失信被执行人
        /// </summary>
        /// <param name="oc_code"></param>
        private void AddShixinInfo(string oc_code, BaseColor grey)
        {
            Font font = new Font(BaseFontForBody, FontSize);
            AddTitleWithBottomBorder("4.2 失信被执行人", grey);
            BaseFont baseFont = BaseFontForBody;
            iTextSharp.text.Font font_contentNormal = new iTextSharp.text.Font(baseFont, FontSize, iTextSharp.text.Font.NORMAL);
            var list = DataAccess.OrgCompanyDishonest_Page_Select(new DatabaseSearchModel().SetPageIndex(1).SetTable("Shixin")
                .SetPageSize(10).SetWhere($"oc_code='{oc_code}'").SetWhere("isHidden=0").SetOrder(" sx_publishDate desc"));
            if (list.IsNotNull() && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    AddText("失信被执行人" + (i + 1), font, 8);
                    AddText("执行法院:", list[i].sx_courtName, paddingBottom, new float[] { 140, 500 });
                    AddText("省份:", list[i].sx_areaName, paddingBottom, new float[] { 140, 500 });
                    AddText("执行依据文号:", list[i].sx_gistId, paddingBottom, new float[] { 140, 500 });
                    AddText("立案时间:", list[i].sx_regDate.IsNotNull() ? list[i].sx_regDate.ToString("yyyy-MM-dd") : "--", paddingBottom, new float[] { 140, 500 });
                    AddText("案号:", list[i].sx_caseCode, paddingBottom, new float[] { 140, 500 });
                    AddText("作出执行依据单位:", list[i].sx_gistUnit, paddingBottom, new float[] { 140, 500 });
                    AddText("生效法律文书确定义务:", list[i].sx_duty, paddingBottom, new float[] { 140, 500 });
                    AddText("失信被执行人为具体情形:", list[i].sx_disruptTypeName, paddingBottom, new float[] { 140, 500 });
                    AddText("被执行的履行情况:", list[i].sx_performance, paddingBottom, new float[] { 140, 500 });
                    AddText("发布时间:", list[i].sx_publishDate.IsNotNull() ? list[i].sx_publishDate.ToString("yyyy-MM-dd") : "--", paddingBottom, new float[] { 140, 500 });
                }
            }
            else
            {
                AddText("暂无相关信息", font, 0);
                return;
            }
        }

        /// <summary>
        /// 法文判决书
        /// </summary>
        /// <param name="oc_code"></param>
        /// <param name="grey"></param>
        private void AddJudgeInfo(string oc_code, BaseColor grey)
        {
            AddTitleWithBottomBorder("4.3 法文判决书", grey);
            BaseFont baseFont = BaseFontForBody;
            iTextSharp.text.Font font_contentNormal = new iTextSharp.text.Font(baseFont, FontSize, iTextSharp.text.Font.NORMAL);
            Req_Oc_Mini query = new Req_Oc_Mini();
            query.pg_index = 1;
            query.pg_size = 10;
            query.oc_code = oc_code;
            var resp_Judges = query.Judges_Get();
            if (resp_Judges.IsNotNull() && resp_Judges.judge_list.Count > 0)
            {

                // 表格标题
                string[] titles = { "序号", "标题", "执行法院", "案号", "发布日期" };
                float[] widths = new float[] { 20, 50, 50, 30, 30 };
                AddColumnTitle(titles, widths);
                // 表格内容
                PdfPTable bodyTable = new PdfPTable(widths);
                for (int i = 0; i < resp_Judges.judge_list.Count; i++)
                {
                    AddBodyContentCell(bodyTable, String.Format("{0}", (i + 1).ToString()), font_contentNormal, 1);
                    AddBodyContentCell(bodyTable, String.Format("{0}", resp_Judges.judge_list[i].title.NullToString("--")), font_contentNormal, 1);
                    AddBodyContentCell(bodyTable, String.Format("{0}", resp_Judges.judge_list[i].court.NullToString("--")), font_contentNormal, 1);
                    AddBodyContentCell(bodyTable, String.Format("{0}", resp_Judges.judge_list[i].reference.NullToString("--")), font_contentNormal, 1);
                    AddBodyContentCell(bodyTable, String.Format("{0}", resp_Judges.judge_list[i].date.IsNotNull() ? resp_Judges.judge_list[i].date : "--"), font_contentNormal, 1, true, true);
                }

                Document.Add(bodyTable);
            }
            else
            {
                Font font = new Font(BaseFontForBody, FontSize);
                AddText("暂无相关信息", font, 0);
                return;
            }
        }

        #endregion

        #region 五、年报信息
        /// <summary>
        /// 年报信息
        /// </summary>
        /// <param name="oc_code"></param>
        /// <param name="grey"></param>
        private void AddNBInfo(OrgCompanyDtlPack4Site pack, BaseColor grey)
        {
            Font font = new Font(BaseFontForBody, FontSize);
            BaseFont baseFont = BaseFontForBody;
            iTextSharp.text.Font font_contentNormal = new iTextSharp.text.Font(baseFont, FontSize, iTextSharp.text.Font.NORMAL);
            if (!pack.list.oc_area.StartsWith("71") && !pack.list.oc_area.StartsWith("81"))
            {
                var yearInfo = DataAccess.Company_Annual_Abs_Select(pack.list.oc_code, pack.list.oc_area, "");
                if (yearInfo.IsNotNull() && yearInfo.Count > 0)
                {
                    var info = DataAccess.Company_Annual_Dtl_Select(pack.list.oc_code, pack.list.oc_area, yearInfo[0].year);
                    if (info.IsNotNull())
                    {

                        #region 基本信息
                        if (info.annual.IsNotNull())
                        {
                            float[] widths = new float[] { 15, 30, 25, 30 };
                            // 表格内容
                            PdfPTable bodyTable = new PdfPTable(widths);
                            PdfPCell cell = new PdfPCell();
                            float defaultBorder = 0.5f;
                            cell.BorderColor = new BaseColor(0, 0, 0);
                            cell.BorderWidthTop = defaultBorder;
                            cell.BorderWidthBottom = defaultBorder;
                            cell.BorderWidthLeft = defaultBorder;
                            cell.BorderWidthRight = defaultBorder;
                            //cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            //cell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                            cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            cell.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                            cell.Rowspan = 1;
                            cell.Colspan = 4;
                            cell.Phrase = new Phrase("企业基本信息", font);
                            cell.SetLeading(10f, 0);
                            cell.PaddingLeft = cell.PaddingRight = 5;
                            cell.PaddingTop = cell.PaddingBottom = 5;
                            cell.Phrase.Font.SetColor(255, 255, 255);
                            cell.BackgroundColor = new BaseColor(0, 136, 204);
                            bodyTable.AddCell(cell);
                            float setloading = 10f;
                            AddBodyContentCell(bodyTable, setloading, String.Format("{0}", "注册号"), font_contentNormal, 1);
                            AddBodyContentCell(bodyTable, setloading, String.Format("{0}", info.annual.oc_number), font_contentNormal, 1);
                            AddBodyContentCell(bodyTable, setloading, String.Format("{0}", "企业经营状态"), font_contentNormal, 1);
                            AddBodyContentCell(bodyTable, setloading, String.Format("{0}", info.annual.oc_status), font_contentNormal, 1, 1, true, true);

                            AddBodyContentCell(bodyTable, setloading, String.Format("{0}", "电子邮箱"), font_contentNormal, 1);
                            AddBodyContentCell(bodyTable, setloading, String.Format("{0}", info.annual.oc_email), font_contentNormal, 1);
                            AddBodyContentCell(bodyTable, setloading, String.Format("{0}", "企业联系电话"), font_contentNormal, 1);
                            AddBodyContentCell(bodyTable, setloading, String.Format("{0}", info.annual.oc_tel), font_contentNormal, 1, 1, true, true);


                            AddBodyContentCell(bodyTable, setloading, String.Format("{0}", "邮政编码"), font_contentNormal, 1);
                            AddBodyContentCell(bodyTable, setloading, String.Format("{0}", info.annual.oc_post), font_contentNormal, 1);
                            AddBodyContentCell(bodyTable, setloading, String.Format("{0}", "从业人数"), font_contentNormal, 1);
                            AddBodyContentCell(bodyTable, setloading, String.Format("{0}", info.annual.numbers), font_contentNormal, 1, 1, true, true);

                            AddBodyContentCell(bodyTable, setloading, String.Format("{0}", "住所"), font_contentNormal, 1);
                            AddBodyContentCell(bodyTable, setloading, String.Format("{0}", info.annual.oc_addr), font_contentNormal, 1, 3, true, true);

                            AddBodyContentCell(bodyTable, 15f, String.Format("{0}", "有限责任公司本年度是否发生股东股权转让"), font_contentNormal, 1);
                            AddBodyContentCell(bodyTable, 15f, String.Format("{0}", info.annual.flag_stock_transfer ? "是" : "否"), font_contentNormal, 1);
                            AddBodyContentCell(bodyTable, 15f, String.Format("{0}", "企业是否有投资信息或购买其他公司股权"), font_contentNormal, 1);
                            AddBodyContentCell(bodyTable, 15f, String.Format("{0}", info.annual.flag_other_stock ? "是" : "否"), font_contentNormal, 1, 1, true, true);
                            Document.Add(bodyTable);
                        }
                        else
                        {
                            AddText("暂无相关信息", font, 0);
                            return;
                        }
                        #endregion
                        AddText("", font, 10);

                        #region 网站信息
                        if (info.site_list.IsNotNull())
                        {
                            float[] widths = new float[] { 15, 15, 70 };
                            // 表格内容
                            PdfPTable bodyTable = new PdfPTable(widths);
                            PdfPCell cell = new PdfPCell();
                            float defaultBorder = 0.5f;
                            cell.BorderColor = new BaseColor(0, 0, 0);
                            cell.BorderWidthTop = defaultBorder;
                            cell.BorderWidthBottom = defaultBorder;
                            cell.BorderWidthLeft = defaultBorder;
                            cell.BorderWidthRight = defaultBorder;
                            //cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            //cell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                            cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            cell.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                            cell.Rowspan = 1;
                            cell.Colspan = 3;
                            cell.Phrase = new Phrase("网站或网店信息", font);
                            cell.SetLeading(10f, 0);
                            cell.PaddingLeft = cell.PaddingRight = 5;
                            cell.PaddingTop = cell.PaddingBottom = 5;
                            cell.Phrase.Font.SetColor(255, 255, 255);
                            cell.BackgroundColor = new BaseColor(0, 136, 204);
                            bodyTable.AddCell(cell);
                            AddBodyContentCell(bodyTable, 10f, String.Format("{0}", "类型"), font_contentNormal, 1);
                            AddBodyContentCell(bodyTable, 10f, String.Format("{0}", "名称"), font_contentNormal, 1);
                            AddBodyContentCell(bodyTable, 10f, String.Format("{0}", "网址"), font_contentNormal, 1, 1, true, true);
                            for (int i = 0; i < info.site_list.Count; i++)
                            {
                                if (i > 9)
                                {
                                    break;
                                }
                                AddBodyContentCell(bodyTable, 10f, String.Format("{0}", info.site_list[i].type), font_contentNormal, 1);
                                AddBodyContentCell(bodyTable, 10f, String.Format("{0}", info.site_list[i].name), font_contentNormal, 1);
                                AddBodyContentCell(bodyTable, 10f, String.Format("{0}", info.site_list[i].website), font_contentNormal, 1, 1, true, true);
                            }

                            Document.Add(bodyTable);
                        }
                        else
                        {
                            AddText("暂无相关信息", font, 0);
                            return;
                        }

                        #endregion
                        AddText("", font, 10);

                        #region 发起人及出资信息
                        if (info.sh_contribute_list.IsNotNull())
                        {
                            float[] widths = new float[] { 1.2f, 2f, 1.5f, 0.9f, 2f, 1.5f, 0.9f };
                            // 表格内容
                            PdfPTable bodyTable = new PdfPTable(widths);
                            PdfPCell cell = new PdfPCell();
                            float defaultBorder = 0.5f;
                            cell.BorderColor = new BaseColor(0, 0, 0);
                            cell.BorderWidthTop = defaultBorder;
                            cell.BorderWidthBottom = defaultBorder;
                            cell.BorderWidthLeft = defaultBorder;
                            cell.BorderWidthRight = defaultBorder;
                            cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                            cell.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                            cell.Rowspan = 1;
                            cell.Colspan = 7;
                            cell.Phrase = new Phrase("发起人及出资信息", font);
                            cell.SetLeading(10f, 0);
                            cell.PaddingLeft = cell.PaddingRight = 5;
                            cell.PaddingTop = cell.PaddingBottom = 5;
                            cell.Phrase.Font.SetColor(255, 255, 255);
                            cell.BackgroundColor = new BaseColor(0, 136, 204);
                            bodyTable.AddCell(cell);
                            float setloading = 20f;
                            AddBodyContentCell(bodyTable, setloading, String.Format("{0}", "发起人"), font_contentNormal, 1);
                            AddBodyContentCell(bodyTable, setloading, String.Format("{0}", "认缴出资额(万元)"), font_contentNormal, 1);
                            AddBodyContentCell(bodyTable, setloading, String.Format("{0}", "认缴出资时间"), font_contentNormal, 1);
                            AddBodyContentCell(bodyTable, setloading, String.Format("{0}", "认缴出资方式"), font_contentNormal, 1);
                            AddBodyContentCell(bodyTable, setloading, String.Format("{0}", "实缴出资额(万元)"), font_contentNormal, 1);
                            AddBodyContentCell(bodyTable, setloading, String.Format("{0}", "出资时间"), font_contentNormal, 1);
                            AddBodyContentCell(bodyTable, setloading, String.Format("{0}", "出资方式"), font_contentNormal, 1, 1, true, true);
                            for (int i = 0; i < info.sh_contribute_list.Count; i++)
                            {
                                AddBodyContentCell(bodyTable, setloading, String.Format("{0}", info.sh_contribute_list[i].stock_holder), font_contentNormal, 1);
                                AddBodyContentCell(bodyTable, setloading, String.Format("{0}", info.sh_contribute_list[i].subscribe_capital), font_contentNormal, 1);
                                AddBodyContentCell(bodyTable, setloading, String.Format("{0}", info.sh_contribute_list[i].subscribe_time.ToString("yyyy-MM-dd")), font_contentNormal, 1);
                                AddBodyContentCell(bodyTable, setloading, String.Format("{0}", info.sh_contribute_list[i].subscribe_style), font_contentNormal, 1);
                                AddBodyContentCell(bodyTable, setloading, String.Format("{0}", info.sh_contribute_list[i].paid_contribute), font_contentNormal, 1);
                                AddBodyContentCell(bodyTable, setloading, String.Format("{0}", info.sh_contribute_list[i].contribute_time.ToString("yyyy-MM-dd")), font_contentNormal, 1);
                                AddBodyContentCell(bodyTable, setloading, String.Format("{0}", info.sh_contribute_list[i].contribute_style), font_contentNormal, 1, 1, true, true);
                            }

                            Document.Add(bodyTable);
                        }
                        else
                        {
                            AddText("暂无相关信息", font, 0);
                            return;
                        }
                        #endregion
                        AddText("", font, 10);

                        #region 对外投资信息
                        if (info.invest_list.IsNotNull())
                        {
                            float[] widths = new float[] { 5f, 5f };
                            // 表格内容
                            PdfPTable bodyTable = new PdfPTable(widths);
                            PdfPCell cell = new PdfPCell();
                            float defaultBorder = 0.5f;
                            cell.BorderColor = new BaseColor(0, 0, 0);
                            cell.BorderWidthTop = defaultBorder;
                            cell.BorderWidthBottom = defaultBorder;
                            cell.BorderWidthLeft = defaultBorder;
                            cell.BorderWidthRight = defaultBorder;
                            cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            cell.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                            cell.Rowspan = 1;
                            cell.Colspan = 2;
                            cell.Phrase = new Phrase("对外投资信息", font);
                            cell.SetLeading(10f, 0);
                            cell.PaddingLeft = cell.PaddingRight = 5;
                            cell.PaddingTop = cell.PaddingBottom = 5;
                            cell.Phrase.Font.SetColor(255, 255, 255);
                            cell.BackgroundColor = new BaseColor(0, 136, 204);
                            bodyTable.AddCell(cell);
                            float setloading = 20f;
                            for (int i = 0; i < info.invest_list.Count; i++)
                            {
                                AddBodyContentCell(bodyTable, setloading, String.Format("{0}", info.invest_list[i].invest_com), font_contentNormal, 1);
                                AddBodyContentCell(bodyTable, setloading, String.Format("{0}", info.invest_list[i].reg_num), font_contentNormal, 1, 1, true, true);
                            }
                            Document.Add(bodyTable);
                        }
                        else
                        {
                            AddText("暂无相关信息", font, 0);
                            return;
                        }
                        #endregion
                        AddText("", font, 10);

                        #region 企业资产状况
                        if (info.annual.IsNotNull())
                        {
                            float[] widths = new float[] { 2.5f, 2.5f, 2.5f, 2.5f };
                            // 表格内容
                            PdfPTable bodyTable = new PdfPTable(widths);
                            PdfPCell cell = new PdfPCell();
                            float defaultBorder = 0.5f;
                            cell.BorderColor = new BaseColor(0, 0, 0);
                            cell.BorderWidthTop = defaultBorder;
                            cell.BorderWidthBottom = defaultBorder;
                            cell.BorderWidthLeft = defaultBorder;
                            cell.BorderWidthRight = defaultBorder;
                            cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            cell.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                            cell.Rowspan = 1;
                            cell.Colspan = 4;
                            cell.Phrase = new Phrase("企业资产状况信息", font);
                            cell.SetLeading(10f, 0);
                            cell.PaddingLeft = cell.PaddingRight = 5;
                            cell.PaddingTop = cell.PaddingBottom = 5;
                            cell.Phrase.Font.SetColor(255, 255, 255);
                            cell.BackgroundColor = new BaseColor(0, 136, 204);
                            bodyTable.AddCell(cell);
                            float setloading = 10f;
                            AddBodyContentCell(bodyTable, setloading, String.Format("{0}", "资产总额"), font_contentNormal, 1);
                            AddBodyContentCell(bodyTable, setloading, String.Format("{0}", info.annual.oc_assets), font_contentNormal, 1);
                            AddBodyContentCell(bodyTable, setloading, String.Format("{0}", "所有者权益合计"), font_contentNormal, 1);
                            AddBodyContentCell(bodyTable, setloading, String.Format("{0}", info.annual.oc_equity_total), font_contentNormal, 1, 1, true, true);
                            AddBodyContentCell(bodyTable, setloading, String.Format("{0}", "营业总收入"), font_contentNormal, 1);
                            AddBodyContentCell(bodyTable, setloading, String.Format("{0}", info.annual.oc_income_total), font_contentNormal, 1);
                            AddBodyContentCell(bodyTable, setloading, String.Format("{0}", "利润总额"), font_contentNormal, 1);
                            AddBodyContentCell(bodyTable, setloading, String.Format("{0}", info.annual.oc_profit_total), font_contentNormal, 1, 1, true, true);
                            AddBodyContentCell(bodyTable, 15f, String.Format("{0}", "营业总收入中主营业务收入"), font_contentNormal, 1);
                            AddBodyContentCell(bodyTable, setloading, String.Format("{0}", info.annual.oc_income_main), font_contentNormal, 1);
                            AddBodyContentCell(bodyTable, setloading, String.Format("{0}", "净利润"), font_contentNormal, 1);
                            AddBodyContentCell(bodyTable, setloading, String.Format("{0}", info.annual.oc_profit_net), font_contentNormal, 1, 1, true, true);
                            AddBodyContentCell(bodyTable, setloading, String.Format("{0}", "纳税总额"), font_contentNormal, 1);
                            AddBodyContentCell(bodyTable, setloading, String.Format("{0}", info.annual.oc_tax_total), font_contentNormal, 1);
                            AddBodyContentCell(bodyTable, setloading, String.Format("{0}", "负债总额"), font_contentNormal, 1);
                            AddBodyContentCell(bodyTable, setloading, String.Format("{0}", info.annual.oc_debt_total), font_contentNormal, 1, 1, true, true);


                            Document.Add(bodyTable);
                        }
                        #endregion
                    }
                    else
                    {
                        AddText("暂无相关信息", font, 0);
                        return;
                    }
                }
                else
                {

                    AddText("暂无相关信息", font, 0);
                    return;
                }
            }
            else
            {

                AddText("暂无相关信息", font, 0);
                return;
            }

        }
        #endregion

        #region 六、知识产权
        /// <summary>
        /// 商标信息
        /// </summary>
        /// <param name="oc_code"></param>
        /// <param name="grey"></param>
        private void AddBrandInfo(string oc_code, BaseColor grey)
        {
            try
            {
                Font font = new Font(BaseFontForBody, FontSize);
                AddTitleWithBottomBorder("6.1 商标", grey);
                BaseFont baseFont = BaseFontForBody;
                iTextSharp.text.Font font_contentNormal = new iTextSharp.text.Font(baseFont, FontSize, iTextSharp.text.Font.NORMAL);
                Req_Oc_Mini query = new Req_Oc_Mini();
                query.pg_index = 1;
                query.pg_size = 10;
                query.oc_code = oc_code;
                var list = query.Brand_Page_Select();
                if (list.brand_list.IsNotNull())
                {
                    StringBuilder sbregNo = new StringBuilder();
                    for (int i = 0; i < list.brand_list.Count; i++)
                    {
                        sbregNo.Append("'");
                        sbregNo.Append(list.brand_list[i].reg_no);
                        if (i < list.brand_list.Count - 1)
                        {
                            sbregNo.Append("',");
                        }
                        else
                        {
                            sbregNo.Append("'");
                        }
                    }

                    DatabaseSearchModel model = new DatabaseSearchModel().SetWhere($"ob_oc_code='{oc_code}'")
                                                .SetOrder("ob_id").SetPageIndex(1).SetPageSize(10);
                    if (!string.IsNullOrEmpty(sbregNo.ToString()))
                        model.SetWhere($"ob_regNo in ({sbregNo.ToString()})");

                    List<OrgCompanyBrandInfo> listBrand = DataAccess.OrgCompanyBrand_SelectPaged(model);

                    // 表格标题
                    string[] titles = { "序号", "注册号", "商标名", "商品/服务列表", "状态", "使用期限" };
                    float[] widths = new float[] { 0.5f, 1.5f, 2f, 2f, 1f, 3f };
                    AddColumnTitle(titles, widths);
                    // 表格内容
                    PdfPTable bodyTable = new PdfPTable(widths);
                    for (int i = 0; i < list.brand_list.Count; i++)
                    {
                        AddBodyContentCell(bodyTable, String.Format("{0}", (i + 1).ToString()), font_contentNormal, 1);
                        AddBodyContentCell(bodyTable, String.Format("{0}", list.brand_list[i].reg_no), font_contentNormal, 1);
                        AddBodyContentCell(bodyTable, String.Format("{0}", list.brand_list[i].name), font_contentNormal, 1);
                        int outvalue = 0;
                        int.TryParse(list.brand_list[i].cat_no, out outvalue);
                        AddBodyContentCell(bodyTable, String.Format("{0}", list.brand_list[i].cat_no + "-" + QZ.Instrument.Model.Constants.Brand_Classes[outvalue - 1]), font_contentNormal, 1);
                        AddBodyContentCell(bodyTable, String.Format("{0}", list.brand_list[i].status), font_contentNormal, 1);
                        if (listBrand.IsNotNull())
                        {
                            var entity = listBrand.Where(u => u.ob_classNo == list.brand_list[i].cat_no).FirstOrDefault();
                            if (entity.IsNotNull())
                            {
                                AddBodyContentCell(bodyTable, String.Format("{0}", (entity.ob_zyksqx.IsNotNull() ? entity.ob_zyksqx + "至" : "") + entity.ob_zyjsqx), font_contentNormal, 1, true, true);
                            }
                            else
                            {
                                AddBodyContentCell(bodyTable, String.Format("{0}", "未知"), font_contentNormal, 1, true, true);
                            }
                        }
                        else
                        {
                            AddBodyContentCell(bodyTable, String.Format("{0}", "未知"), font_contentNormal, 1, true, true);
                        }

                    }

                    Document.Add(bodyTable);
                }
                else
                {
                    AddText("暂无相关信息", font, 0);
                    return;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 专利信息
        /// </summary>
        private void AddPatentInfo(string oc_code, BaseColor grey)
        {
            Font font = new Font(BaseFontForBody, FontSize);
            AddTitleWithBottomBorder("6.2 专利", grey);
            BaseFont baseFont = BaseFontForBody;
            iTextSharp.text.Font font_contentNormal = new iTextSharp.text.Font(baseFont, FontSize, iTextSharp.text.Font.NORMAL);
            DatabaseSearchModel model = new DatabaseSearchModel().SetOrWhere($"oc_code='{oc_code}'").SetOrder("Patent_Day DESC").SetPageIndex(1).SetPageSize(10);
            var list = DataAccess.OrgCompanyPatent_SelectPaged(model);

            if (list.IsNotNull() && list.Count > 0)
            {
                // 表格标题
                string[] titles = { "序号", "专利名称", "专利类型", "申请号", "申请日期", "公开号", "授权日期" };
                float[] widths = new float[] { 1, 2, 1, 2, 1, 2, 1 };
                AddColumnTitle(titles, widths);
                // 表格内容
                PdfPTable bodyTable = new PdfPTable(widths);
                for (int i = 0; i < list.Count; i++)
                {
                    AddBodyContentCell(bodyTable, String.Format("{0}", (i + 1).ToString()), font_contentNormal, 1);
                    AddBodyContentCell(bodyTable, String.Format("{0}", list[i].Patent_Name), font_contentNormal, 1);
                    AddBodyContentCell(bodyTable, String.Format("{0}", list[i].Patent_Type), font_contentNormal, 1);
                    AddBodyContentCell(bodyTable, String.Format("{0}", list[i].Patent_No), font_contentNormal, 1);
                    AddBodyContentCell(bodyTable, String.Format("{0}", list[i].Patent_Day.IndexOf("1900") < 0 ? list[i].Patent_Day.Replace('.', '-') : "--"), font_contentNormal, 1);
                    AddBodyContentCell(bodyTable, String.Format("{0}", list[i].Patent_gkh), font_contentNormal, 1);
                    AddBodyContentCell(bodyTable, String.Format("{0}", list[i].Patent_gkr), font_contentNormal, 1, true, true);
                }
                Document.Add(bodyTable);
            }
        }

        /// <summary>
        /// 软件著作权
        /// </summary>
        /// <param name="oc_code"></param>
        /// <param name="grey"></param>
        private void AddSoftwareCopyrightInfo(string oc_code, BaseColor grey)
        {
            int rowcount = 0;
            AddTitleWithBottomBorder("6.3 软件著作权", grey);
            BaseFont baseFont = BaseFontForBody;
            iTextSharp.text.Font font_contentNormal = new iTextSharp.text.Font(baseFont, FontSize, iTextSharp.text.Font.NORMAL);
            DatabaseSearchModel model=new DatabaseSearchModel().SetPageIndex(1).SetTable("SoftwareCopyright").SetPageSize(10)
                .SetWhere($"sc_oc_code='{oc_code}'").SetOrder(" sc_successDate desc ");
            var list = DataAccess.Software_Get(model);

            if (list.IsNotNull() && list.Item1.Count > 0)
            {
                // 表格标题
                string[] titles = { "序号", "登记号", "分类号", "软件全称", "软件简称", "版本号", "软件著作权人", "首次发表日期", "登记批准日期" };
                float[] widths = new float[] { 8, 10, 10, 10, 10, 8, 10, 10, 10 };
                AddColumnTitle(titles, widths);
                // 表格内容
                PdfPTable bodyTable = new PdfPTable(widths);
                for (int i = 0; i < list.Item1.Count; i++)
                {
                    AddBodyContentCell(bodyTable, String.Format("{0}", (i + 1).ToString()), font_contentNormal, 1);
                    AddBodyContentCell(bodyTable, String.Format("{0}", list.Item1[i].reg_no), font_contentNormal, 1);
                    AddBodyContentCell(bodyTable, String.Format("{0}", list.Item1[i].cat_no), font_contentNormal, 1);
                    AddBodyContentCell(bodyTable, String.Format("{0}", list.Item1[i].name), font_contentNormal, 1);
                    AddBodyContentCell(bodyTable, String.Format("{0}", list.Item1[i].s_name), font_contentNormal, 1);
                    AddBodyContentCell(bodyTable, String.Format("{0}", list.Item1[i].version), font_contentNormal, 1);
                    AddBodyContentCell(bodyTable, String.Format("{0}", list.Item1[i].author), font_contentNormal, 1);
                    AddBodyContentCell(bodyTable, String.Format("{0}", list.Item1[i].first_date), font_contentNormal, 1);
                    AddBodyContentCell(bodyTable, String.Format("{0}", list.Item1[i].reg_date), font_contentNormal, 1, true, true);
                }
                Document.Add(bodyTable);
            }


        }


        /// <summary>
        /// 作品著作权
        /// </summary>
        /// <param name="oc_code"></param>
        /// <param name="grey"></param>
        private void AddProductCopyrightInfo(string oc_code, BaseColor grey)
        {
            int rowcount = 0;
            AddTitleWithBottomBorder("6.4 作品著作权", grey);
            BaseFont baseFont = BaseFontForBody;
            iTextSharp.text.Font font_contentNormal = new iTextSharp.text.Font(baseFont, FontSize, iTextSharp.text.Font.NORMAL);
            DatabaseSearchModel model = new DatabaseSearchModel().SetPageIndex(1).SetTable("ProductCopyright").SetPageSize(10)
                .SetWhere($"pc_oc_code='{oc_code}'").SetOrder(" pc_regDate desc ");
            var list = DataAccess.Product_Get(model);

            if (list.IsNotNull() && list.Item1.Count > 0)
            {
                // 表格标题
                string[] titles = { "序号", "登记号", "登记日期", "作品名称", "作品类别", "著作权人", "创作完成日期", "首次发表日期" };
                float[] widths = new float[] { 8, 14, 14, 15, 10, 15, 10, 10 };
                AddColumnTitle(titles, widths);
                // 表格内容
                PdfPTable bodyTable = new PdfPTable(widths);
                for (int i = 0; i < list.Item1.Count; i++)
                {
                    AddBodyContentCell(bodyTable, String.Format("{0}", (i + 1).ToString()), font_contentNormal, 1);
                    AddBodyContentCell(bodyTable, String.Format("{0}", list.Item1[i].reg_no), font_contentNormal, 1);
                    AddBodyContentCell(bodyTable, String.Format("{0}", list.Item1[i].reg_date), font_contentNormal, 1);
                    AddBodyContentCell(bodyTable, String.Format("{0}", list.Item1[i].name), font_contentNormal, 1);
                    AddBodyContentCell(bodyTable, String.Format("{0}", list.Item1[i].type), font_contentNormal, 1);
                    AddBodyContentCell(bodyTable, String.Format("{0}", list.Item1[i].author), font_contentNormal, 1);
                    AddBodyContentCell(bodyTable, String.Format("{0}", list.Item1[i].finish_date), font_contentNormal, 1);
                    AddBodyContentCell(bodyTable, String.Format("{0}", list.Item1[i].publish_date), font_contentNormal, 1, true, true);
                }
                Document.Add(bodyTable);
            }
        }

        /// <summary>
        /// 6.5资质认证
        /// </summary>
        /// <param name="oc_code"></param>
        /// <param name="grey"></param>
        private void AddCertificationInfo(string oc_code, BaseColor grey)
        {
            int rowcount = 0;
            AddTitleWithBottomBorder("6.5 资质认证", grey);
            BaseFont baseFont = BaseFontForBody;
            iTextSharp.text.Font font_contentNormal = new iTextSharp.text.Font(baseFont, FontSize, iTextSharp.text.Font.NORMAL);
            DatabaseSearchModel model = new DatabaseSearchModel().SetWhere($" ci_oc_code='{oc_code}'").SetOrder("ci_expiredDate DESC ").SetPageIndex(1).SetPageSize(10);
            var list = DataAccess.Certificatelst_Get(model, out rowcount);
            var siteLists = list.Select(p => new
            {
                ci_certNo = p.ci_certNo,
                ci_certificationProgram = p.ci_certificationProgram,
                ci_certStatus = p.ci_certStatus,
                ci_expiredDate = p.ci_expiredDate.ToString("yyyy-MM-dd"),
                ci_id = p.ci_id.ToString().ToAesEncrypt16CodeKey(),
                ci_oc_code = p.ci_oc_code.ToAesEncrypt16CodeKey()
            }).ToList();

            if (list.IsNotNull() && list.Count > 0)
            {
                // 表格标题
                string[] titles = { "序号", "认证项目/产品类别", "证书编号", "证书状态", "证书到时日期", "获证组织名称/生产企业", "发证机构" };
                float[] widths = new float[] { 10, 17.5f, 20, 10, 10, 20, 10 };
                AddColumnTitle(titles, widths);
                // 表格内容
                PdfPTable bodyTable = new PdfPTable(widths);
                for (int i = 0; i < list.Count; i++)
                {
                    AddBodyContentCell(bodyTable, String.Format("{0}", (i + 1).ToString()), font_contentNormal, 1);
                    AddBodyContentCell(bodyTable, String.Format("{0}", list[i].ci_certificationProgram), font_contentNormal, 1);
                    AddBodyContentCell(bodyTable, String.Format("{0}", list[i].ci_certNo), font_contentNormal, 1);
                    AddBodyContentCell(bodyTable, String.Format("{0}", list[i].ci_certStatus), font_contentNormal, 1);
                    AddBodyContentCell(bodyTable, String.Format("{0}", list[i].ci_expiredDate), font_contentNormal, 1);
                    AddBodyContentCell(bodyTable, String.Format("{0}", list[i].ci_oc_name), font_contentNormal, 1);
                    AddBodyContentCell(bodyTable, String.Format("{0}", list[i].ci_issuedCompanyName), font_contentNormal, 1, true, true);
                }
                Document.Add(bodyTable);
            }

        }

        /// <summary>
        /// 域名备案
        /// </summary>
        /// <param name="oc_code"></param>
        private void AddSiteInfo(string oc_code, BaseColor grey)
        {
            Font font = new Font(BaseFontForBody, FontSize);
            int rowcount = 0;

            var search = new DatabaseSearchModel().SetWhere($"ocs_oc_code = '{oc_code}'").SetPageIndex(1).SetPageSize(10).SetOrder("ocs_id desc");
            BaseFont baseFont = BaseFontForBody;
            iTextSharp.text.Font font_contentNormal = new iTextSharp.text.Font(baseFont, FontSize, iTextSharp.text.Font.NORMAL);
            List<Company_Icpl> list = DataAccess.Company_Icpl_Select(search);
            AddTitleWithBottomBorder("6.6 域名", grey);

            if (list == null || list.Count <= 0)
            {
                AddText("暂无相关信息", font, 0);
                return;
            }
            // 表格标题
            string[] titles = { "序号", "域名", "备案号", "网站名称", "网站首页", "审核时间" };
            float[] widths = new float[] { 15, 15, 15, 20, 11, 15 };
            AddColumnTitle(titles, widths);
            // 表格内容
            PdfPTable bodyTable = new PdfPTable(widths);
            for (int i = 0; i < list.Count; i++)
            {
                AddBodyContentCell(bodyTable, String.Format("{0}", (i + 1).ToString()), font_contentNormal, 1);
                AddBodyContentCell(bodyTable, String.Format("{0}", list[i].icpl_domain), font_contentNormal, 1);
                AddBodyContentCell(bodyTable, String.Format("{0}", list[i].icpl_number), font_contentNormal, 1);
                AddBodyContentCell(bodyTable, String.Format("{0}", list[i].icpl_name), font_contentNormal, 1);
                AddBodyContentCell(bodyTable, String.Format("{0}", list[i].icpl_uri), font_contentNormal, 1);
                AddBodyContentCell(bodyTable, String.Format("{0}", list[i].icpl_check_time.ToString("yyyy-MM-dd")), font_contentNormal, 1, true, true);
            }
            Document.Add(bodyTable);
        }

        #endregion

        #region 七、经营信息

        /// <summary>
        /// 产品信息
        /// </summary>
        /// <param name="oc_code"></param>
        /// <param name="grey"></param>
        private void AddOrgGS1ItemInfo(string oc_code, BaseColor grey)
        {
            Font font = new Font(BaseFontForBody, FontSize);
            BaseFont baseFont = BaseFontForBody;
            iTextSharp.text.Font font_contentNormal = new iTextSharp.text.Font(baseFont, FontSize, iTextSharp.text.Font.NORMAL);
            AddTitleWithBottomBorder("7.1 商品信息", grey);

            int rowcount = 0;
            var model = new DatabaseSearchModel().SetWhere($" ogs_oc_code='{oc_code}'").SetOrder("ogs_id").SetPageIndex(1).SetPageSize(10);
            var list = DataAccess.Invlst_Get(model, out rowcount);



            if (list == null || list.Count <= 0)
            {
                AddText("暂无相关信息", font, 0);
                return;
            }
            // 表格标题
            string[] titles = { "序号", "商品条码", "商品名称", "品牌名称" };
            float[] widths = new float[] { 10, 30, 30, 30 };
            AddColumnTitle(titles, widths);
            // 表格内容
            PdfPTable bodyTable = new PdfPTable(widths);
            for (int i = 0; i < list.Count; i++)
            {
                AddBodyContentCell(bodyTable, String.Format("{0}", (i + 1).ToString()), font_contentNormal, 1);
                AddBodyContentCell(bodyTable, String.Format("{0}", list[i].ogs_code), font_contentNormal, 1);
                AddBodyContentCell(bodyTable, String.Format("{0}", list[i].ogs_itemName), font_contentNormal, 1);
                AddBodyContentCell(bodyTable, String.Format("{0}", list[i].ogs_brandName), font_contentNormal, 1, true, true);
            }
            Document.Add(bodyTable);
        }

        /// <summary>
        /// 资招聘信息
        /// </summary>
        /// <param name="oc_code"></param>
        /// <param name="grey"></param>
        private void AddJobInfo(string oc_code, BaseColor grey)
        {
            Font font = new Font(BaseFontForBody, FontSize);
            BaseFont baseFont = BaseFontForBody;
            iTextSharp.text.Font font_contentNormal = new iTextSharp.text.Font(baseFont, FontSize, iTextSharp.text.Font.NORMAL);
            AddTitleWithBottomBorder("7.2 招聘信息", grey);

            int rowcount = 0;
            var model = new DatabaseSearchModel().SetWhere($"ep_code='{oc_code}'").SetOrder("ep_Date desc").SetPageIndex(1).SetPageSize(10);
            var list = DataAccess.QZEmploy_SelectPaged(model, out rowcount);
            if (list == null || list.Count <= 0)
            {
                AddText("暂无相关信息", font, 0);
                return;
            }
            // 表格标题
            string[] titles = { "序号", "职位名称", "工作地点", "薪资", "发布时间" };
            float[] widths = new float[] { 10, 25, 20, 25, 20 };
            AddColumnTitle(titles, widths);
            // 表格内容
            PdfPTable bodyTable = new PdfPTable(widths);
            for (int i = 0; i < list.Count; i++)
            {
                AddBodyContentCell(bodyTable, String.Format("{0}", (i + 1).ToString()), font_contentNormal, 1);
                AddBodyContentCell(bodyTable, String.Format("{0}", list[i].ep_Name), font_contentNormal, 1);
                AddBodyContentCell(bodyTable, String.Format("{0}", list[i].ep_City), font_contentNormal, 1);
                AddBodyContentCell(bodyTable, String.Format("{0}", list[i].ep_PriceTxt.IsNull() ? "面议" : list[i].ep_PriceTxt), font_contentNormal, 1);
                AddBodyContentCell(bodyTable, String.Format("{0}", list[i].ep_Date), font_contentNormal, 1, true, true);
            }
            Document.Add(bodyTable);
        }

        #endregion

        #region 八、参展信息

        public void AddExhibition(string oc_code, BaseColor grey)
        {

            Font font = new Font(BaseFontForBody, FontSize);
            BaseFont baseFont = BaseFontForBody;
            iTextSharp.text.Font font_contentNormal = new iTextSharp.text.Font(baseFont, FontSize, iTextSharp.text.Font.NORMAL);


            int rowcount = 0;
            var model = new DatabaseSearchModel().SetWhere($"ee_oc_code='{oc_code}'").SetOrder("ee_exhCreateTime desc").SetPageIndex(1).SetPageSize(10);
     
            var list = DataAccess.ExhibitionEnterprise_SelectPaged(model, out rowcount);
            if (list == null || list.Count <= 0)
            {
                AddText("暂无相关信息", font, 0);
                return;
            }
            // 表格标题
            string[] titles = { "序号", "展会名称", "展会行业", "联系方式", "邮箱", "传真", "网站" };
            float[] widths = new float[] { 10, 20, 10, 15, 15, 15, 15 };
            AddColumnTitle(titles, widths);
            // 表格内容
            PdfPTable bodyTable = new PdfPTable(widths);
            for (int i = 0; i < list.Count; i++)
            {
                AddBodyContentCell(bodyTable, String.Format("{0}", (i + 1).ToString()), font_contentNormal, 1);
                AddBodyContentCell(bodyTable, String.Format("{0}", list[i].ee_exhName), font_contentNormal, 1);
                AddBodyContentCell(bodyTable, String.Format("{0}", list[i].ee_exhTrade), font_contentNormal, 1);
                AddBodyContentCell(bodyTable, String.Format("{0}", (list[i].ee_contact + " " + list[i].ee_phone).NullToString("--")), font_contentNormal, 1);
                AddBodyContentCell(bodyTable, String.Format("{0}", list[i].ee_mail), font_contentNormal, 1);
                AddBodyContentCell(bodyTable, String.Format("{0}", list[i].ee_fax), font_contentNormal, 1);
                AddBodyContentCell(bodyTable, String.Format("{0}", list[i].ee_site), font_contentNormal, 1, true, true);
            }
            Document.Add(bodyTable);
        }
        #endregion




        #endregion

        #region 表格文本生成
        /// <summary>
        /// 添加表格标题
        /// </summary>
        /// <param name="titles"></param>
        private void AddColumnTitle(string[] titles, float[] widths)
        {
            BaseFont baseFont = BaseFontForBody;
            iTextSharp.text.Font font_columnHeader = new iTextSharp.text.Font(baseFont, FontSize, iTextSharp.text.Font.NORMAL);
            int colCount = titles.Count();
            PdfPTable table = new PdfPTable(widths);
            int i = 0;
            foreach (string title in titles)
            {
                var isright = false;
                if (i == colCount)
                {
                    isright = true;
                }
                AddColumnHeaderCell(table, title, font_columnHeader, true, isright);

            }
            table.SpacingBefore = 20f;
            Document.Add(table);
        }

        /// <summary>
        /// 带下划线的标题
        /// </summary>
        /// <param name="title"></param>
        private void AddTitleWithBottomBorder(string title, BaseColor color)
        {
            PdfPCell cell = GenerateOnlyBottomBorderCell(2, iTextSharp.text.Element.ALIGN_LEFT, color);

            cell.Phrase = new Paragraph(title, new iTextSharp.text.Font(BaseFontForHeaderFooter, FontSize,0,color)); ;
            PdfPTable titleTable = new PdfPTable(1);
            titleTable.AddCell(cell);
            Document.Add(titleTable);
        }

        /// <summary>
        /// 增加键值对样式的文字
        /// </summary>
        /// <param name="desc"></param>
        /// <param name="text"></param>
        /// <param name="PaddingBottom"></param>
        private void AddText(string desc, string text, float PaddingBottom)
        {
            float[] widths = new float[] { 100, 520 };
            PdfPTable table = new PdfPTable(widths);
            PdfPCell cell = new PdfPCell();
            cell.Border = 0;
            cell.SetLeading((float)(1.667 * 18 * 0.75), 0);
            iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph(desc, new iTextSharp.text.Font(BaseFontForHeaderFooter, FontSize));
            p.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
            cell.Phrase = p;
            cell.PaddingBottom = PaddingBottom;
            table.AddCell(cell);

            p = new iTextSharp.text.Paragraph(text, new iTextSharp.text.Font(BaseFontForBody, FontSize));
            p.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
            cell.Phrase = p;
            cell.PaddingBottom = PaddingBottom;
            table.AddCell(cell);

            Document.Add(table);
        }

        private void AddText(string desc, string text, float PaddingBottom, float[] widths)
        {

            PdfPTable table = new PdfPTable(widths);
            PdfPCell cell = new PdfPCell();
            cell.Border = 0;
            cell.SetLeading((float)(1.667 * 18 * 0.75), 0);
            iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph(desc, new iTextSharp.text.Font(BaseFontForHeaderFooter, FontSize));
            p.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
            cell.Phrase = p;
            cell.PaddingBottom = PaddingBottom;
            table.AddCell(cell);

            p = new iTextSharp.text.Paragraph(text, new iTextSharp.text.Font(BaseFontForBody, FontSize));
            p.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
            cell.Phrase = p;
            cell.PaddingBottom = PaddingBottom;
            table.AddCell(cell);

            Document.Add(table);
        }


        private void AddDoubleText(string desc1, string text1, string desc2, string text2, float PaddingBottom)
        {
            float[] widths = new float[] { 100, 200, 120, 200 };
            PdfPTable table = new PdfPTable(widths);
            PdfPCell cell = new PdfPCell();
            cell.Border = 0;
            cell.SetLeading((float)(1.667 * 18 * 0.75), 0);
            iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph(desc1, new iTextSharp.text.Font(BaseFontForHeaderFooter, FontSize));
            p.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
            cell.Phrase = p;
            cell.PaddingBottom = PaddingBottom;
            table.AddCell(cell);

            p = new iTextSharp.text.Paragraph(text1, new iTextSharp.text.Font(BaseFontForBody, FontSize));
            p.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
            cell.Phrase = p;
            cell.PaddingBottom = PaddingBottom;
            table.AddCell(cell);

            p = new iTextSharp.text.Paragraph(desc2, new iTextSharp.text.Font(BaseFontForHeaderFooter, FontSize));
            p.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
            cell.Phrase = p;
            cell.PaddingBottom = PaddingBottom;
            table.AddCell(cell);

            p = new iTextSharp.text.Paragraph(text2, new iTextSharp.text.Font(BaseFontForBody, FontSize));
            p.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
            cell.Phrase = p;
            cell.PaddingBottom = PaddingBottom;
            table.AddCell(cell);

            Document.Add(table);
        }

        /// <summary>
        /// 增加文字
        /// </summary>
        /// <param name="text"></param>
        /// <param name="PaddingBottom"></param>
        private void AddText(string text, Font font, float PaddingBottom, int elment = iTextSharp.text.Element.ALIGN_CENTER)
        {
            PdfPTable table = new PdfPTable(1);
            PdfPCell cell = new PdfPCell();
            cell.Border = 0;
            cell.SetLeading((float)(1.667 * 18 * 0.75), 0);

            iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph(text, font);
            p.Alignment = elment; //iTextSharp.text.Element.ALIGN_CENTER;
            cell.Phrase = p;
            cell.PaddingBottom = PaddingBottom;
            cell.HorizontalAlignment = elment;
            cell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            table.AddCell(cell);
            //table.WidthPercentage = 100;

            Document.Add(table);
        }

        #region 添加表格主体

        private static void AddBodyContentCell(PdfPTable bodyTable,
                                               String text,
                                               iTextSharp.text.Font font,
                                               int rowspan = 1,

                                               bool needLeftBorder = true,
                                               bool needRightBorder = false)
        {
            PdfPCell cell = new PdfPCell();
            float defaultBorder = 0.5f;
            cell.BorderColor = new BaseColor(221, 221, 221);
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = defaultBorder;
            cell.BorderWidthLeft = needLeftBorder ? defaultBorder : 0;
            cell.BorderWidthRight = needRightBorder ? defaultBorder : 0;
            cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
            cell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            cell.Rowspan = rowspan;
            cell.Phrase = new Phrase(text, font);
            cell.SetLeading((float)(1.667 * 18 * 0.75), 0);

            cell.PaddingLeft = cell.PaddingRight = 10;

            cell.PaddingTop = cell.PaddingBottom = 10;
            bodyTable.AddCell(cell);
        }

        private static void AddBodyContentCell(PdfPTable bodyTable,
                                           float setLeading,
                                           String text,
                                           iTextSharp.text.Font font,
                                           int rowspan = 1,
                                           int colspan = 1,
                                           bool needLeftBorder = true,
                                           bool needRightBorder = false)
        {
            PdfPCell cell = new PdfPCell();
            float defaultBorder = 0.5f;
            cell.BorderColor = new BaseColor(221, 221, 221);
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = defaultBorder;
            cell.BorderWidthLeft = needLeftBorder ? defaultBorder : 0;
            cell.BorderWidthRight = needRightBorder ? defaultBorder : 0;
            cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
            cell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            cell.Rowspan = rowspan;
            cell.Colspan = colspan;
            cell.Phrase = new Phrase(text, font);
            cell.SetLeading(setLeading, 0);
            cell.PaddingLeft = cell.PaddingRight = 10;
            cell.PaddingTop = cell.PaddingBottom = 10;
            bodyTable.AddCell(cell);
        }
        #endregion

        #region 添加表格标题

        /// <summary>
        /// 添加列标题单元格
        /// </summary>
        /// <param name="table">表格行的单元格列表</param>
        /// <param name="header">标题</param>
        /// <param name="font">字段</param>
        /// <param name="colspan">列空间</param>
        /// <param name="rowspan">行空间</param>
        /// <param name="needLeftBorder">是否需要左边框</param>
        /// <param name="needRightBorder">是否需要右边框</param>
        public void AddColumnHeaderCell(PdfPTable table,
                                                String header,
                                                iTextSharp.text.Font font,
                                                int colspan,
                                                int rowspan,
                                                bool needLeftBorder = true,
                                                bool needRightBorder = true)
        {
            PdfPCell cell = GenerateColumnHeaderCell(header, font, needLeftBorder, needRightBorder);
            if (colspan > 1)
            {
                cell.Colspan = colspan;
            }

            if (rowspan > 1)
            {
                cell.Rowspan = rowspan;
            }

            table.AddCell(cell);
        }

        /// <summary>
        /// 添加列标题单元格
        /// </summary>
        /// <param name="table">表格</param>
        /// <param name="header">标题</param>
        /// <param name="font">字段</param>
        /// <param name="needLeftBorder">是否需要左边框</param>
        /// <param name="needRightBorder">是否需要右边框</param>
        public void AddColumnHeaderCell(PdfPTable table,
                                                String header,
                                                iTextSharp.text.Font font,
                                                bool needLeftBorder = true,
                                                bool needRightBorder = true)
        {
            //PdfPCell cell = GenerateColumnHeaderCell(header, font, needLeftBorder, needRightBorder);
            PdfPCell cell = new PdfPCell();
            float border = 0.5f;
            cell.BorderColor = new BaseColor(221, 221, 221);
            cell.BorderWidthBottom = border;
            if (needLeftBorder)
            {
                cell.BorderWidthLeft = border;
            }
            else
            {
                cell.BorderWidthLeft = 0;
            }
            cell.BorderWidthTop = border;
            if (needRightBorder)
            {
                cell.BorderWidthRight = border;
            }
            else
            {
                cell.BorderWidthRight = 0;
            }

            cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            cell.VerticalAlignment = iTextSharp.text.Element.ALIGN_BASELINE;
            cell.PaddingBottom = 4;
            cell.Phrase = new Phrase(header, font);
            cell.Phrase.Font.SetColor(255, 255, 255);
            cell.BackgroundColor = new BaseColor(0, 136, 204);
            table.AddCell(cell);
        }
        #endregion

        /// <summary>
        /// 生成列标题单元格
        /// </summary>
        /// <param name="header">标题</param>
        /// <param name="font">字段</param>
        /// <param name="needLeftBorder">是否需要左边框</param>
        /// <param name="needRightBorder">是否需要右边框</param>
        /// <returns></returns>
        private PdfPCell GenerateColumnHeaderCell(String header,
                                                        iTextSharp.text.Font font,
                                                        bool needLeftBorder = true,
                                                        bool needRightBorder = true)
        {
            PdfPCell cell = new PdfPCell();
            float border = 0.5f;
            cell.BorderWidthBottom = border;
            if (needLeftBorder)
            {
                cell.BorderWidthLeft = border;
            }
            else
            {
                cell.BorderWidthLeft = 0;
            }
            cell.BorderWidthTop = border;
            if (needRightBorder)
            {
                cell.BorderWidthRight = border;
            }
            else
            {
                cell.BorderWidthRight = 0;
            }

            cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            cell.VerticalAlignment = iTextSharp.text.Element.ALIGN_BASELINE;
            cell.PaddingBottom = 4;
            cell.Phrase = new Phrase(header, font);
            cell.Phrase.Font.SetColor(255, 255, 255);
            cell.BackgroundColor = new BaseColor(0, 0, 128);
            return cell;
        }

        #endregion


        #region 根据旧更新详情解析得到变更前后内容

        /// <summary>
        /// 根据旧更新详情解析得到变更前后内容
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private List<OrgCompanyGsxtBgsxInfo> SplitOrgCompanyGsxtBgsx(OrgCompanyDtl_EvtInfo info)
        {
            List<OrgCompanyGsxtBgsxInfo> bgsx = new List<OrgCompanyGsxtBgsxInfo>();

            string[] sxs = info.oe_dtl.Split(new string[] { "变更前" }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < sxs.Length; i++)
            {
                OrgCompanyGsxtBgsxInfo bgsxInfo = new OrgCompanyGsxtBgsxInfo();
                if (!string.IsNullOrEmpty(info.oe_date))
                {
                    bgsxInfo.bgrq = Convert.ToDateTime(info.oe_date); // 变更日期
                }
                string[] sx = sxs[i].Split(new string[] { "变更后" }, StringSplitOptions.RemoveEmptyEntries);
                if (sx.Length == 2)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        string[] strs = sx[j].Split(new string[] { ":", "：" }, StringSplitOptions.RemoveEmptyEntries);
                        if (strs.Length > 1)
                        {
                            if (j == 0)
                            {
                                bgsxInfo.bgsx = strs[0] ?? ""; // 变更类别
                                bgsxInfo.bgq = strs[1] ?? ""; // 变更前
                            }
                            if ((j == 1))
                            {
                                bgsxInfo.bgh = strs[1] ?? ""; // 变更后
                            }
                        }
                        else
                        {
                            string[] st = sx[j].Split(new string[] { "\t", "：" }, StringSplitOptions.RemoveEmptyEntries);
                            if (j == 0)
                            {
                                bgsxInfo.bgsx = st[0] ?? ""; // 变更类别
                                bgsxInfo.bgq = st[1] ?? ""; // 变更前
                            }
                            if ((j == 1))
                            {
                                bgsxInfo.bgh = st[1] ?? ""; // 变更后
                            }
                        }
                    }
                    bgsx.Add(bgsxInfo);
                }
            }

            return bgsx;
        }

        #endregion 根据旧更新详情解析得到变更前后内容
    }
}