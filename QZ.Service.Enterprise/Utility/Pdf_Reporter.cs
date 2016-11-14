using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using QZ.Foundation.Document;
using QZ.Instrument.Model;

namespace QZ.Service.Enterprise
{
    public class Pdf_Reporter : PdfBase
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
            cell.Phrase = new Paragraph("企业信息报告", font_header1);
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

            float[] widths = new float[] { 80, 280, 80, 80 };
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

            string imagePath = AppDomain.CurrentDomain.BaseDirectory + @"Images\qrcode.png"; // logo
            cell.Image = iTextSharp.text.Image.GetInstance(imagePath);
            //cell.FixedHeight =85;//85
            footer.AddCell(cell);

            imagePath = AppDomain.CurrentDomain.BaseDirectory + @"Images\qrcode_weixin.jpg"; // logo
            cell.Image = iTextSharp.text.Image.GetInstance(imagePath);
            footer.AddCell(cell);

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
            //cell.BorderColor = color;
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
            Pdf_Reporter pdfReport = new Pdf_Reporter();
            Document document = new Document();// new Document(PageSize.A4.Rotate(), -90, -90, 60, 10);//此处设置的偏移量是为了加大页面的可用范围，可以使用默认.
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
            Pdf_Reporter pdfReport = new Pdf_Reporter();
            Document document = new Document();// new Document(PageSize.A4.Rotate(), -90, -90, 60, 10);//此处设置的偏移量是为了加大页面的可用范围，可以使用默认.
            document.SetMargins(0, 0, 80, 120); // 设置正文的Margins

            try
            {


                //PdfWriter pdfWriter = PdfWriter.GetInstance(document, new FileStream(path, FileMode.Create));
                PdfWriter pdfWriter = PdfWriter.GetInstance(document, stream);
                pdfWriter.PageEvent = pdfReport;//此处一定要赋值，才能触发页眉和页脚的处理
                document.Open();
                pdfReport.AddBody(document, oc_area, oc_code);
                pdfWriter.Flush();
                //stream.Flush();

                stream.Flush();
                //byte[] bs = stream.ToArray();



                //return bs;
            }
            catch(Exception e)
            {
                #region debug
                Util.Log_Error(nameof(ExportPDF), e.Message, "error");
                #endregion
            }
            finally
            {
                document.Close();
                //document.Close();


                // 创建一个PdfReader对象
                //PdfReader reader = new PdfReader(path);
                // 获得文档页数
                //sumPageNum = reader.NumberOfPages;
            }

        }



        #endregion

        #region 添加正文内容

        private void AddBody(Document document, string oc_area, string oc_code)
        {
            Font font = new Font(BaseFontForBody, FontSize);
            Cover(oc_code); // 封面
            document.NewPage();

            AddListInfo(oc_code);
            AddText("", font, 68); // 增加上下间距
            AddDtlInfo(oc_code);

            AddText("", font, 68);
            AddChengyuan(oc_area, oc_code);
            AddText("", font, 68);
            AddDtlGDInfo(oc_area, oc_code);
            AddText("", font, 68);
            AddSiteInfo(oc_code);
            AddText("", font, 68);
            AddDtl_Evt(oc_code, oc_area);
        }

        /// <summary>
        /// 封面
        /// </summary>
        private void Cover(string oc_code)
        {
            Font font = new Font(BaseFontForBody, FontSize);
            AddText("", font, 80);

            string imagePath = AppDomain.CurrentDomain.BaseDirectory + @"Images\guoqi.png";
            Image image = Image.GetInstance(imagePath);
            PdfPCell cell = new PdfPCell(image, true);
            cell.FixedHeight = 100;
            cell.Border = 0;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPTable table = new PdfPTable(1);
            table.SpacingBefore = 80;
            table.AddCell(cell);
            Document.Add(table);

            OrgCompanyListInfo listInfo = DataAccess.OrgCompanyList_Select(oc_code);//  .OrgCompanyList_Selectbyoc_code(oc_code);
            AddText("CHINA", font, 50);
            AddText(listInfo.oc_name, font, 0);
            AddText(listInfo.oc_number, font, 50);
            AddText("更新时间", font, 0);
            AddText(listInfo.oc_updatetime.ToString("yyyy-MM-dd"), font, 50);
        }

        /// <summary>
        /// 主体信息
        /// </summary>
        private void AddDtlInfo(string oc_code)
        {
            OrgCompanyDtlInfo data = DataAccess.OrgCompanyDtl_Select(oc_code); //.OrgCompanyDtl_Selectbyod_oc_code(oc_code);
            BaseColor grey = new BaseColor(174, 216, 31);
            AddTitleWithBottomBorder("▪ 主体信息", grey);

            if (data == null || data.od_id <= 0)
            {
                Font font = new Font(BaseFontForBody, FontSize);
                AddText("暂无相关信息", font, 0);
                return;
            }


            AddText("名称:", data.oc_name, paddingBottom);
            AddText("法人:", data.od_faRen, paddingBottom);
            //AddText("法人代码:", data.od_oc_code, paddingBottom);
            AddText("法人注册号:", data.oc_number, paddingBottom);
            AddText("注册资本:", data.od_regMoney, paddingBottom);
            //AddText("实收资本:", data.od_factMoney, paddingBottom);
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
            OrgCompanyListInfo listInfo = DataAccess.OrgCompanyList_Select(oc_code); //.OrgCompanyList_Selectbyoc_code(oc_code);


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
        /// 成员信息
        /// </summary>
        private void AddChengyuan(string oc_area, string oc_code)
        {
            BaseFont baseFont = BaseFontForBody;
            iTextSharp.text.Font font_contentNormal = new iTextSharp.text.Font(baseFont, FontSize, iTextSharp.text.Font.NORMAL);

            // 模块标题
            BaseColor grey = new BaseColor(174, 216, 31);
            AddTitleWithBottomBorder("▪ 成员信息", grey);

            // 表格标题
            string[] titles = { "序号", "姓名", "职位" };
            float[] widths = new float[] { 20, 20, 20 };
            AddColumnTitle(titles, widths);

            // 表格内容
            PdfPTable bodyTable = new PdfPTable(widths);
            //oc_area = oc_area.Substring(0, 2);
            if (oc_area.StartsWith("4403")) // 深圳市未采用新的公示系统分表
            {
                List<OrgCompanyDtlMgrInfo> chenyuan = DataAccess.OrgCompanyDtlMgr_Select(oc_code); //.OrgCompanyDtlMgr_Selectbyom_oc_code(oc_code);
                for (int i = 0; i < chenyuan.Count; i++)
                {
                    AddBodyContentCell(bodyTable, String.Format("{0}", (i + 1).ToString()), font_contentNormal, 1, true);
                    AddBodyContentCell(bodyTable, String.Format("{0}", chenyuan[i].om_name), font_contentNormal, 1, true);
                    AddBodyContentCell(bodyTable, String.Format("{0}", chenyuan[i].om_position), font_contentNormal, 1, false);
                }
            }
            else
            {
                List<OrgCompanyGsxtDtlMgrInfo> GsxtChenyuan = new List<OrgCompanyGsxtDtlMgrInfo>();
                if (!oc_area.StartsWith("71") && !oc_area.StartsWith("81")) // 台湾、香港暂时只有基础信息
                {
                    //int rowcount = 0;

                    GsxtChenyuan = DataAccess.OrgCompanyGsxtDtlMgr_Page_Select(new DatabaseSearchModel().SetPageSize(100).SetOrder("om_id asc").SetWhere($"om_oc_code = '{oc_code}'"), oc_area);
                       // (oc_area, "*", string.Format("where om_oc_code = '{0}'", oc_code), "om_id asc", 1, 100, out rowcount);
                    for (int i = 0; i < GsxtChenyuan.Count; i++)
                    {
                        AddBodyContentCell(bodyTable, String.Format("{0}", (i + 1).ToString()), font_contentNormal, 1, true);
                        AddBodyContentCell(bodyTable, String.Format("{0}", GsxtChenyuan[i].om_name), font_contentNormal, 1, true);
                        AddBodyContentCell(bodyTable, String.Format("{0}", GsxtChenyuan[i].om_position), font_contentNormal, 1, false);
                    }

                }
            }

            Document.Add(bodyTable);
        }

        /// <summary>
        /// 股东信息
        /// </summary>
        /// <param name="oc_area"></param>
        /// <param name="oc_code"></param>
        private void AddDtlGDInfo(string oc_area, string oc_code)
        {
            BaseFont baseFont = BaseFontForBody;
            iTextSharp.text.Font font_contentNormal = new iTextSharp.text.Font(baseFont, FontSize, iTextSharp.text.Font.NORMAL);

            // 模块标题
            BaseColor grey = new BaseColor(174, 216, 31);
            AddTitleWithBottomBorder("▪ 股东信息", grey);

            // 表格标题
            string[] titles = { "序号", "股东名称", "出资额", "出资比例", "股东属性" };
            float[] widths = new float[] { 20, 20, 20, 20, 20 };
            AddColumnTitle(titles, widths);

            // 表格内容
            PdfPTable bodyTable = new PdfPTable(5);
            //oc_area = oc_area.Substring(0, 2);

            if (oc_area.StartsWith("4403")) // 深圳市未采用新的公示系统分表
            {
                List<OrgCompanyDtlGDInfo> listGD = DataAccess.OrgCompanyDtlGD_FromOccode_Select(oc_code);
                for (int i = 0; i < listGD.Count; i++)
                {
                    AddBodyContentCell(bodyTable, String.Format("{0}", (i + 1).ToString()), font_contentNormal, 1, true);
                    AddBodyContentCell(bodyTable, String.Format("{0}", listGD[i].og_name), font_contentNormal, 1, true);
                    AddBodyContentCell(bodyTable, String.Format("{0}", listGD[i].og_money.ToString("0.0") + listGD[i].og_unit), font_contentNormal, 1, true);
                    AddBodyContentCell(bodyTable, String.Format("{0}", ((int)(listGD[i].og_BL)).ToString() + "%"), font_contentNormal, 1, true);
                    AddBodyContentCell(bodyTable, String.Format("{0}", listGD[i].og_pro), font_contentNormal, 1, false);
                }
            }
            else
            {
                List<OrgCompanyGsxtDtlGDInfo> GsxtGD = new List<OrgCompanyGsxtDtlGDInfo>();
                if (!oc_area.StartsWith("71") && !oc_area.StartsWith("81")) // 台湾、香港暂时只有基础信息
                {
                    //int rowcount = 0;
                    GsxtGD = DataAccess.OrgCompanyGsxtDtlGD_Page_Select(new DatabaseSearchModel().SetPageSize(100).SetOrder(" og_int asc ")
                        .SetWhere($"og_oc_code = '{oc_code}'"), oc_area.Substring(0, 2)); //, "*", string.Format("where og_oc_code = '{0}' ", oc_code), "og_int asc", 1, 100, out rowcount);
                    for (int i = 0; i < GsxtGD.Count; i++)
                    {
                        AddBodyContentCell(bodyTable, String.Format("{0}", (i + 1).ToString()), font_contentNormal, 1, true);
                        AddBodyContentCell(bodyTable, String.Format("{0}", GsxtGD[i].og_name), font_contentNormal, 1, true);
                        AddBodyContentCell(bodyTable, String.Format("{0}", GsxtGD[i].og_subscribeAccount + GsxtGD[i].og_unit), font_contentNormal, 1, true);
                        AddBodyContentCell(bodyTable, String.Format("{0}", ""), font_contentNormal, 1, true);
                        AddBodyContentCell(bodyTable, String.Format("{0}", GsxtGD[i].og_type), font_contentNormal, 1, false);
                    }

                }
            }

            Document.Add(bodyTable);
        }

        /// <summary>
        /// 变更信息
        /// </summary>
        /// <param name="oc_code"></param>
        private void AddDtl_Evt(string oc_code)
        {
            BaseFont baseFont = BaseFontForBody;
            //iTextSharp.text.Font font_contentNormal = new iTextSharp.text.Font(baseFont, FontSize, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font font_contentNormal = new iTextSharp.text.Font(baseFont, (float)(16 * 0.75), iTextSharp.text.Font.NORMAL);

            // 模块标题
            BaseColor grey = new BaseColor(174, 216, 31);
            AddTitleWithBottomBorder("▪ 变更信息", grey);

            // 表格标题
            string[] titles = { "变更时间", "变更事项", "变更详情" };
            float[] widths = new float[] { 18, 18, 60 };
            AddColumnTitle(titles, widths);

            // 表格内容
            PdfPTable bodyTable = new PdfPTable(widths);
            //OrgComapanyDtl_EvtSearchInfo search = new Models.OrgComapanyDtl_EvtSearchInfo()
            //{
            //    page = 1,
            //    pagesize = 10,
            //    oe_oc_code = oc_code
            //};
            //int rowcount = 0;
            List<OrgCompanyDtl_EvtInfo> Evt = DataAccess.OrgCompanyDtl_Evt_Select(new DatabaseSearchModel<OrgCompanyDtl_EvtInfo>()
                .SetPageIndex(1).SetPageSize(10).SetWhereClause($"oe_oc_code='{oc_code}'").Ascend(false).SetOrderField(info => info.oe_id)); //.OrgComapanyDtl_Evt_SelectPaged(search, out rowcount);
            if (Evt == null || Evt.Count < 1)
            {
                Evt = new List<OrgCompanyDtl_EvtInfo>();
            }
            for (int i = 0; i < Evt.Count; i++)
            {
                //AddBodyContentCell(bodyTable, String.Format("{0}", (i + 1).ToString()), font_contentNormal, 1, true);
                AddBodyContentCell(bodyTable, String.Format("{0}", Evt[i].oe_date), font_contentNormal, 1, true);
                AddBodyContentCell(bodyTable, String.Format("{0}", Evt[i].oe_type), font_contentNormal, 1, true);
                AddBodyContentCell(bodyTable, String.Format("{0}", Evt[i].oe_dtl), font_contentNormal, 1, false);


            }

            Document.Add(bodyTable);
        }

        #region 最新变更信息

        /// <summary>
        /// 最新变更信息
        /// </summary>
        /// <param name="oc_code"></param>
        /// <param name="oc_area"></param>
        private void AddDtl_Evt(string oc_code, string oc_area)
        {
            BaseFont baseFont = BaseFontForBody;
            iTextSharp.text.Font font_contentNormal = new iTextSharp.text.Font(baseFont, (float)(16 * 0.75), iTextSharp.text.Font.NORMAL);

            // 模块标题
            BaseColor grey = new BaseColor(174, 216, 31);
            AddTitleWithBottomBorder("▪ 变更信息", grey);

            // 表格标题
            string[] titles = { "变更时间", "变更事项", "变更前", "变更后" };
            float[] widths = new float[] { 18, 18, 30, 30 };
            AddColumnTitle(titles, widths);

            int page = 1;
            int pagesize = 5;

            // 表格内容
            PdfPTable bodyTable = new PdfPTable(widths);
            Dictionary<string, List<object>> bgsx = GetBgsxGroupByDate(oc_code, oc_area, page, pagesize);
            if (bgsx != null)
            {
                foreach (string key in bgsx.Keys)
                {
                    AddBodyContentCell(bodyTable, String.Format("{0}", key), font_contentNormal, bgsx[key].Count, true);

                    foreach (object obj in bgsx[key])
                    {
                        var obj2 = ChangeType(obj, new
                        {
                            bgsx = "",
                            bgq = "",
                            bgh = ""
                        }
                           );
                        AddBodyContentCell(bodyTable, String.Format("{0}", obj2.bgsx), font_contentNormal, 1, true);
                        AddBodyContentCell(bodyTable, String.Format("{0}", obj2.bgq), font_contentNormal, 1, true);
                        AddBodyContentCell(bodyTable, String.Format("{0}", obj2.bgh), font_contentNormal, 1, false);
                    }

                }

            }

            Document.Add(bodyTable);

        }

        private static T ChangeType<T>(object obj, T t)
        {
            return (T)obj;
        }
        /// <summary>
        /// 按时间分组获得变更事项
        /// </summary>
        /// <param name="oc_code"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        private static Dictionary<string, List<object>> GetBgsxGroupByDate(string oc_code, string oc_area, int page, int pagesize)
        {

            Dictionary<string, List<object>> dic = new Dictionary<string, List<object>>();

            if (!oc_area.StartsWith("71") && !oc_area.StartsWith("81")) // 台湾、香港暂时只有基础信息
            {
                if (oc_area == "4403") // 深圳还是老系统，烦
                {
                    dic = GetOrgCompanyGsxtBgsx(oc_code, page, pagesize);
                }
                else
                {
                    //int rowcount = 0;
                    List<OrgCompanyGsxtBgsxInfo> bgsx = DataAccess.OrgCompanyGsxtBgsx_Select(new DatabaseSearchModel<OrgCompanyGsxtBgsxInfo>().SetPageIndex(page).SetPageSize(pagesize)
                        .SetOrderField(info => info.bgrq).Ascend(false).SetWhereClause($"oc_code='{oc_code}'"), oc_area.Substring(0, 2));
                    //List<OrgCompanyGsxtBgsxInfo> bgsx = DBHelper.DBHelper.OrgCompanyGsxtBgsx_SelectPaged(oc_area.Substring(0, 2), "*", string.Format("where oc_code = '{0}' ", oc_code), "bgrq desc", page, pagesize, out rowcount);
                    if ((bgsx != null) && (bgsx.Count > 0))
                    {
                        string date = "";
                        foreach (var info in bgsx)
                        {
                            date = (info.bgrq != null) ? info.bgrq.ToString("yyyy-MM-dd") : "";
                            if (dic.Keys.Contains(date))
                            {
                                dic[date].Add(
                                    new
                                    {
                                        bgsx = info.bgsx,
                                        bgq = info.bgq,
                                        bgh = info.bgh
                                    });
                            }
                            else
                            {
                                dic.Add(date, new List<object>() {
                                 new
                                 {
                                     bgsx = info.bgsx,
                                     bgq = info.bgq,
                                     bgh = info.bgh
                                 }
                             });
                            }
                        }

                    }
                }
            }

            return dic;

        }

        /// <summary>
        /// 针对深圳采用的旧变更事项，转化新的变更事项
        /// </summary>
        /// <param name="oc_code"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        private static Dictionary<string, List<object>> GetOrgCompanyGsxtBgsx(string oc_code, int page, int pagesize)
        {
            Dictionary<string, List<object>> dic = new Dictionary<string, List<object>>();

            //OrgComapanyDtl_EvtSearchInfo search = new Models.OrgComapanyDtl_EvtSearchInfo()
            //{
            //    page = page,
            //    pagesize = pagesize,
            //    oe_oc_code = oc_code
            //};

            // int rowcount = 0;
            List<OrgCompanyDtl_EvtInfo> Evt = DataAccess.OrgCompanyDtl_Evt_Select(new DatabaseSearchModel<OrgCompanyDtl_EvtInfo>()
                .SetPageIndex(page).SetPageSize(pagesize).SetWhereClause($"oe_oc_code='{oc_code}'").Ascend(false).SetOrderField(info => info.oe_id));
            //List<OrgCompanyDtl_EvtInfo> Evt = DBHelper.DBHelper.OrgComapanyDtl_Evt_SelectPaged(search, out rowcount);

            if ((Evt != null) && (Evt.Count > 0))
            {
                foreach (OrgCompanyDtl_EvtInfo info2 in Evt)
                {
                    List<OrgCompanyGsxtBgsxInfo> bgsx = SplitOrgCompanyGsxtBgsx(info2);

                    if ((bgsx != null) && (bgsx.Count > 0))
                    {
                        string date = "";
                        foreach (var info in bgsx)
                        {
                            date = (info.bgrq != null) ? info.bgrq.ToString("yyyy-MM-dd") : "";
                            if (dic.Keys.Contains(date))
                            {
                                dic[date].Add(
                                    new
                                    {
                                        bgsx = info.bgsx,
                                        bgq = info.bgq,
                                        bgh = info.bgh
                                    });
                            }
                            else
                            {
                                dic.Add(date, new List<object>() {
                                 new
                                 {
                                     bgsx = info.bgsx,
                                     bgq = info.bgq,
                                     bgh = info.bgh
                                 }
                             });
                            }
                        }

                    }
                }

            }

            return dic;

        }

        /// <summary>
        /// 根据旧更新详情解析得到变更前后内容
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private static List<OrgCompanyGsxtBgsxInfo> SplitOrgCompanyGsxtBgsx(OrgCompanyDtl_EvtInfo info)
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
                        string[] strs = sx[j].Replace("\t", string.Empty).Split(new string[] { ":", "：" }, StringSplitOptions.RemoveEmptyEntries);
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

                    bgsx.Add(bgsxInfo);
                }

            }

            return bgsx;
        }

        #endregion

        /// <summary>
        /// 域名备案
        /// </summary>
        /// <param name="oc_code"></param>
        private void AddSiteInfo(string oc_code)
        {
            Font font = new Font(BaseFontForBody, FontSize);
            //int rowcount = 0;
            //OrgCompanySitSearchInfo search = new QZ.NewSite.CorpInfoService.Models.OrgCompanySitSearchInfo()
            //{
            //    ocs_oc_code = oc_code,
            //    page = 1,
            //    pagesize = 10
            //};

            List<OrgCompanySiteInfo> sites = DataAccess.OrgCompanySite_SelectPaged(new DatabaseSearchModel().SetWhere(""));
            BaseColor grey = new BaseColor(174, 216, 31);
            AddTitleWithBottomBorder("▪ 域名备案", grey);

            if (sites == null || sites.Count <= 0)
            {
                AddText("暂无相关信息", font, 0);
                return;
            }

            for (int i = 0; i < sites.Count; i++)
            {
                AddText("备案" + (i + 1), font, 8);
                AddText("公司名称:", sites[i].ocs_host, paddingBottom);
                AddText("域名:", sites[i].ocs_domain, paddingBottom);
                AddText("网站名:", sites[i].ocs_siteName, paddingBottom);
                AddText("机构代码:", sites[i].ocs_oc_code, paddingBottom);
                AddText("主办单位性质:", sites[i].ocs_hostType, paddingBottom);
                AddText("备案号:", sites[i].ocs_number, paddingBottom);
                AddText("公司首页:", sites[i].ocs_siteHomePage, paddingBottom);
                AddText("审核时间:", sites[i].ocs_checkTime.ToString("yyyy-MM-dd"), paddingBottom);
                AddText("状态:", sites[i].ocs_status == 0 ? "正常" : "注销", 25);
            }

        }

        /// <summary>
        /// 添加表格标题
        /// </summary>
        /// <param name="titles"></param>
        private void AddColumnTitle(string[] titles, float[] widths)
        {
            BaseFont baseFont = BaseFontForBody;
            iTextSharp.text.Font font_columnHeader = new iTextSharp.text.Font(baseFont, FontSize, iTextSharp.text.Font.BOLD);
            int colCount = titles.Count();
            PdfPTable table = new PdfPTable(widths);
            foreach (string title in titles)
            {
                AddColumnHeaderCell(table, title, font_columnHeader, true, true);
            }
            table.SpacingBefore = 5;
            Document.Add(table);
        }

        /// <summary>
        /// 带下划线的标题
        /// </summary>
        /// <param name="title"></param>
        private void AddTitleWithBottomBorder(string title, BaseColor color)
        {
            PdfPCell cell = GenerateOnlyBottomBorderCell(2, iTextSharp.text.Element.ALIGN_LEFT, color);
            cell.Phrase = new Paragraph(title, new iTextSharp.text.Font(BaseFontForHeaderFooter, FontSize)); ;
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
            float[] widths = new float[] { 100, 300 };
            PdfPTable table = new PdfPTable(widths);
            PdfPCell cell = new PdfPCell();
            cell.Border = 0;
            cell.SetLeading((float)(1.667 * 18 * 0.75), 0);

            iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph(desc, new iTextSharp.text.Font(BaseFontForBody, FontSize));
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

        /// <summary>
        /// 增加文字
        /// </summary>
        /// <param name="text"></param>
        /// <param name="PaddingBottom"></param>
        private void AddText(string text, Font font, float PaddingBottom)
        {
            PdfPTable table = new PdfPTable(1);
            PdfPCell cell = new PdfPCell();
            cell.Border = 0;
            cell.SetLeading((float)(1.667 * 18 * 0.75), 0);

            iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph(text, font);
            p.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            cell.Phrase = p;
            cell.PaddingBottom = PaddingBottom;
            cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
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
                                               bool needRightBorder = true)
        {
            PdfPCell cell = new PdfPCell();
            float defaultBorder = 1f;
            //cell.BorderWidthLeft = defaultBorder;
            //cell.BorderWidthTop = 0;
            //cell.BorderWidthRight = needRightBorder ? defaultBorder : 0;
            //cell.BorderWidthBottom = defaultBorder;
            cell.BorderColor = new BaseColor(0, 0, 128);
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = defaultBorder;
            cell.BorderWidthLeft = 0;
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
                                                bool needRightBorder = false)
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
                                                bool needRightBorder = false)
        {
            PdfPCell cell = GenerateColumnHeaderCell(header, font, needLeftBorder, needRightBorder);
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
                                                        bool needRightBorder = false)
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
    }
}