using System;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using NPOI.Util;
using NPOI.XWPF.UserModel;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Collections;
//workbook->worksheet->row->cell. 
namespace testExcel
{
    class Program
    {


        // 读写excel
        static void Main2(string[] args)
        {
            //创新一个新的excel文件的workbook对象：
            HSSFWorkbook wb;
            wb = new HSSFWorkbook();


            //打开老的sheet: wb.GetSheet(sheet的名称)
            ISheet sheet;
            sheet = wb.GetSheet("sheet1");

            //创建一个新的sheet:wb.CreateSheet(sheet的名称）
            ISheet sheet1;
            sheet1 = wb.CreateSheet("sheet1");


            //创建某个行：CreateRow(i)，i是行号，从0开始计数
            int i = 10;
            int j = 10;
            IRow row = sheet.CreateRow(i);

            //获取某一行：GetRow(i），i是行号，从0开始计数
            // 创建某一列： 需要在定位到行的基础上
            NPOI.SS.UserModel.ICell cell = row.CreateCell(j);//j是列号，从0开始计数

            //sheet.GetRow(i).CreateCell(j);在i行创建第j列

            //获取某一行：GetCell(j),j是列号，从0开始计数
            sheet.GetRow(i).GetCell(j);


            // 读取单元格
            sheet.GetRow(i).GetCell(j); //就会返回第i行j列的内容。

            //写单元格：
            sheet.GetRow(i).GetCell(j).SetCellValue("内容");

            //保存数据到文件中
            string filepath = "";
            FileStream file = new FileStream(filepath, FileMode.Open, FileAccess.Write);
            wb.Write(file);
            file.Close();
            wb.Close();

            //另外 NPOI 使用 HSSFWorkbook 类来处理 xls，XSSFWorkbook 类来处理 xlsx，它们都继承接口 IWorkbook，因此可以通过 IWorkbook 来统一处理 xls 和 xlsx 格式的文件。 
        }
        // 替换word 文本, 分为从 1.  实体替换 2. 从dictionary替换
        static void Main111(string[] args)
        {
            // Student s = new Student()
            // {
            //     StuName = "张三",
            //     Phone = "1100"
            // };
            // string inPath = @"C:\Users\Wesley\source\dotnetCore\UselessProjects\testExcel\userfile\a.docx";
            // string toPath = @"C:\Users\Wesley\source\dotnetCore\UselessProjects\testExcel\userfile\b.docx";
            // using (MemoryStream stream = Export<Student>(inPath, s))
            // {

            //     byte[] vs = new byte[stream.Length];
            //     stream.Seek(0, SeekOrigin.Begin);
            //     int cou = stream.Read(vs, 0, (int)vs.Length);


            //     FileStream fs = new FileStream(toPath, FileMode.Create, FileAccess.Write);
            //     fs.Write(vs, 0, vs.Length);
            //     fs.Dispose();
            // }

            Dictionary<string, string> candidates = new Dictionary<string, string>()
            {
                {"StuName","李四"},
                {"Phone","2200"}
            };

            string inPath = @"C:\Users\Wesley\source\dotnetCore\UselessProjects\testExcel\userfile\a.docx";
            string toPath = @"C:\Users\Wesley\source\dotnetCore\UselessProjects\testExcel\userfile\b.docx";
            using (MemoryStream stream = Export(inPath, candidates))
            {

                byte[] vs = new byte[stream.Length];
                stream.Seek(0, SeekOrigin.Begin);
                int cou = stream.Read(vs, 0, (int)vs.Length);


                FileStream fs = new FileStream(toPath, FileMode.Create, FileAccess.Write);
                fs.Write(vs, 0, vs.Length);
                fs.Dispose();
            }
        }

        // 给world 加入表格
        static void Main2121()
        {
            string inputPath = @"C:\Users\Wesley\source\dotnetCore\UselessProjects\testExcel\userfile\a.docx";
            string outpath = @"C:\Users\Wesley\source\dotnetCore\UselessProjects\testExcel\userfile\c.docx";

            XWPFDocument doc = null;
            using (FileStream fs = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
            {
                doc = new XWPFDocument(fs);
            }

            // doc.InsertTable()
            AddTable(doc);


            FileStream file = new FileStream(outpath, FileMode.Create, FileAccess.Write);
            doc.Write(file);
            file.Close();
            doc.Close();

        }

        // 给表格追加行
        static void Main4312()
        {
            string inputPath = @"C:\Users\Wesley\source\dotnetCore\UselessProjects\testExcel\userfile\a.docx";
            string outpath = @"C:\Users\Wesley\source\dotnetCore\UselessProjects\testExcel\userfile\c.docx";

            XWPFDocument doc = null;
            using (FileStream fs = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
            {
                doc = new XWPFDocument(fs);
            }

            // doc.InsertTable()
            UpdateTable(doc.Tables[0]);


            FileStream file = new FileStream(outpath, FileMode.Create, FileAccess.Write);
            doc.Write(file);
            file.Close();
            doc.Close();

        }

        class Student
        {
            public string StuName { get; set; }
            public string Phone { get; set; }
        }

        public static MemoryStream Export<T>(string path, T obj)
        {
            // string filepath = @"";
            using (FileStream stream = File.OpenRead(path))
            {
                XWPFDocument doc = new XWPFDocument(stream);
                //遍历段落
                foreach (var para in doc.Paragraphs)
                {
                    ReplaceKey<T>(para, obj);
                }
                //遍历表格
                var tables = doc.Tables;
                foreach (var table in tables)
                {
                    foreach (var row in table.Rows)
                    {
                        foreach (var cell in row.GetTableCells())
                        {
                            foreach (var para in cell.Paragraphs)
                            {
                                ReplaceKey<T>(para, obj);
                            }
                        }
                    }
                }
                MemoryStream ms = new MemoryStream();

                doc.Write(ms);
                ms.Seek(0, SeekOrigin.Begin);
                return ms;

            }
        }
        private static void ReplaceKey<T>(XWPFParagraph para, T obj)
        {
            string text = para.ParagraphText;
            var runs = para.Runs;
            string styleid = para.Style;
            for (int i = 0; i < runs.Count; i++)
            {
                var run = runs[i];
                text = run.ToString();
                Type t = typeof(T);
                PropertyInfo[] pi = t.GetProperties();
                foreach (PropertyInfo p in pi)
                {
                    if (text.Contains("$" + p.Name + "$"))
                    {
                        text = text.Replace("$" + p.Name + "$", p.GetValue(obj, null).ToString());
                    }
                }
                runs[i].SetText(text, 0);
            }
        }
        public static MemoryStream Export(string path, Dictionary<string, string> infos)
        {
            using (FileStream stream = File.OpenRead(path))
            {
                XWPFDocument doc = new XWPFDocument(stream);
                //遍历段落
                foreach (var para in doc.Paragraphs)
                {
                    ReplaceKey(para, infos);
                }
                //遍历表格
                var tables = doc.Tables;
                foreach (var table in tables)
                {
                    foreach (var row in table.Rows)
                    {
                        foreach (var cell in row.GetTableCells())
                        {
                            foreach (var para in cell.Paragraphs)
                            {
                                ReplaceKey(para, infos);
                            }
                        }
                    }
                }
                MemoryStream ms = new MemoryStream();
                doc.Write(ms);
                ms.Seek(0, SeekOrigin.Begin);
                return ms;

            }
        }
        private static void ReplaceKey(XWPFParagraph para, Dictionary<string, string> infos)
        {
            string text = "";
            var runs = para.Runs;
            for (int i = 0; i < runs.Count; i++)
            {
                var run = runs[i];
                text = run.ToString();

                foreach (var p in infos)
                {
                    if (text.Contains("$" + p.Key + "$"))
                    {
                        text = text.Replace("$" + p.Key + "$", p.Value);
                    }
                }
                // runs[i].
                runs[i].SetText(text, 0);
            }
        }
        private static void AddTable(XWPFDocument doc)
        {
            // XWPFDocument doc = new XWPFDocument();
            //创建表格-提前创建好表格后填数
            XWPFTable tableContent = doc.CreateTable(4, 5);//4行5列
            tableContent.Width = 1000 * 5;
            tableContent.SetColumnWidth(0, 1000);/* 设置列宽 */
            tableContent.SetColumnWidth(1, 1500);
            tableContent.SetColumnWidth(2, 1500);
            tableContent.SetColumnWidth(3, 1000);

            tableContent.GetRow(0).GetCell(0).SetParagraph(SetCellText(doc, tableContent, "地点"));
            tableContent.GetRow(0).GetCell(1).SetParagraph(SetCellText(doc, tableContent, "日期"));
            tableContent.GetRow(0).GetCell(2).SetParagraph(SetCellText(doc, tableContent, "男性"));
            tableContent.GetRow(0).GetCell(3).SetParagraph(SetCellText(doc, tableContent, "女性"));
            tableContent.GetRow(0).GetCell(4).SetParagraph(SetCellText(doc, tableContent, "合计"));

            //测试数据格式
            List<ArrayList> list = new List<ArrayList>() {
             new ArrayList()   { "航天桥", "-", "0", "0", "0" },
              new ArrayList()  { "马甸", "-", "0", "0", "0" },
              new ArrayList()  { "洋桥", "04月16日 - 05月31日", "0", "0", "0" } };
            //  List<ArrayList> list = Common.PubVars.listTable;
            for (int i = 0; i < list.Count; i++)//有3个数组
            {
                ArrayList ls = list[i];
                for (int j = 0; j < ls.Count; j++)
                {
                    tableContent.GetRow(i + 1).GetCell(j).SetParagraph(SetCellText(doc, tableContent, ls[j].ToString()));
                }
            }

        }
        // 给一个table , 追加行
        private static void UpdateTable(XWPFTable table)
        {
            //创建表格-提前创建好表格后填数
            //      XWPFTable tableContent = doc.Tables[0];//4行5列

            for (int i = 0; i < 10; i++)
            {
                XWPFTableRow row = table.CreateRow();
                row.GetCell(0).SetText("name");
                row.GetCell(1).SetText("age");
                row.GetCell(2).SetText("sex");
                row.GetCell(3).SetText("mark");
            }

            // tableContent.AddNewCol();
            // tableContent.AddNewCol();
            // tableContent.AddNewCol();
            // tableContent.AddNewCol();


            // tableContent.CreateRow();
            // tableContent.CreateRow();
            // tableContent.CreateRow();
            // tableContent.CreateRow();
            // tableContent.CreateRow();

            // // tableContent.Width = 1000 * 5;
            // // tableContent.SetColumnWidth(0, 1000);/* 设置列宽 */
            // // tableContent.SetColumnWidth(1, 1500);
            // // tableContent.SetColumnWidth(2, 1500);
            // // tableContent.SetColumnWidth(3, 1000);

            // tableContent.GetRow(0).GetCell(0).SetParagraph(SetCellText(doc, tableContent, "地点"));
            // tableContent.GetRow(0).GetCell(1).SetParagraph(SetCellText(doc, tableContent, "日期"));
            // tableContent.GetRow(0).GetCell(2).SetParagraph(SetCellText(doc, tableContent, "男性"));
            // tableContent.GetRow(0).GetCell(3).SetParagraph(SetCellText(doc, tableContent, "女性"));
            // tableContent.GetRow(0).GetCell(4).SetParagraph(SetCellText(doc, tableContent, "合计"));

            // //测试数据格式
            // List<ArrayList> list = new List<ArrayList>()
            // {
            //     new ArrayList(){ "航天桥", "-", "0", "0", "0"},
            //     new ArrayList(){ "马甸", "-", "0", "0", "0" },
            //     new ArrayList(){"洋桥", "04月16日 - 05月31日", "0", "0", "0"},

            // };
            // for (int i = 0; i < list.Count; i++)//有3个数组
            // {
            //     ArrayList ls = list[i];
            //     for (int j = 0; j < ls.Count; j++)
            //     {
            //         tableContent.GetRow(i + 1).GetCell(j).SetParagraph(SetCellText(doc, tableContent, ls[j].ToString()));
            //     }
            // }

        }

        //设置字体样式
        public static XWPFParagraph SetCellText(XWPFDocument doc, XWPFTable table, string setText)
        {
            //table中的文字格式设置  
            NPOI.OpenXmlFormats.Wordprocessing.CT_P para = new NPOI.OpenXmlFormats.Wordprocessing.CT_P();
            XWPFParagraph pCell = new XWPFParagraph(para, table.Body);
            pCell.Alignment = ParagraphAlignment.CENTER;//字体居中  
            pCell.VerticalAlignment = TextAlignment.CENTER;//字体居中  

            XWPFRun r1c1 = pCell.CreateRun();
            r1c1.SetText(setText);
            r1c1.FontSize = 12;
            r1c1.FontFamily = "华文楷体";
            //r1c1.SetTextPosition(20);//设置高度  
            return pCell;
        }
    }
}
