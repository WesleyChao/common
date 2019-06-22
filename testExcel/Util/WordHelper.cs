using System;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using NPOI.Util;
using NPOI.XWPF.UserModel;
using System.Reflection;
using System.Collections.Generic;
using System.Data;
using System.IO;
namespace Qx.Util.Office
{
    public class WordHelper
    {
        // word 文字替换
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

        // word 添加文字
        public static void AddParagrath(XWPFDocument doc, string content)
        {
            XWPFParagraph para = doc.CreateParagraph();
            XWPFRun run = para.CreateRun();
            run.SetText(content);
        }
        // todo

        // word 添加表格, 表格的行宽怎么确定
        public static void AddTable(XWPFDocument doc, DataTable dt,/* Dictionary<int, string> coloWidth,  */Func<XWPFDocument, XWPFTable, string, XWPFParagraph> SetCellText)
        {
            // XWPFDocument doc = new XWPFDocument();
            //创建表格-提前创建好表格后填数
            XWPFTable table = doc.CreateTable();//4行5列

            int rowCount = dt.Rows.Count + 1;
            int colCount = dt.Columns.Count;

            XWPFTableRow titles = table.GetRow(0);
            // table.AddRow(titles);
            for (int i = 0; i < colCount; i++)
            {
                string title = dt.Columns[i].ColumnName;
                XWPFTableCell cell = titles.GetCell(i);
                if (cell == null)
                {
                    cell = titles.CreateCell();
                }
                cell.SetParagraph(SetCellText(doc, table, title));
                // cell.SetText(title);
            }

            for (int i = 1; i < rowCount; i++)//有3个数组
            {
                DataRow dr = dt.Rows[i - 1];
                XWPFTableRow tablerow = table.CreateRow();
                // XWPFTableRow tablerow = table.GetRow(i);
                for (int j = 0; j < colCount; j++)
                {
                    string content = dr[j].ToString();
                    tablerow.GetCell(j).SetParagraph(SetCellText(doc, table, content));

                    // tablerow.CreateCell().SetText(content);

                }
                //    table.AddRow(tablerow);
            }
        }


        // word 添加表格, 表格的行宽怎么确定
        public static void AddTable(XWPFDocument doc, DataTable dt)
        {
            // XWPFDocument doc = new XWPFDocument();
            //创建表格-提前创建好表格后填数
            XWPFTable table = doc.CreateTable();//4行5列

            int rowCount = dt.Rows.Count + 1;
            int colCount = dt.Columns.Count;

            XWPFTableRow titles = table.GetRow(0);
            // table.AddRow(titles);
            for (int i = 0; i < colCount; i++)
            {
                string title = dt.Columns[i].ColumnName;
                XWPFTableCell cell = titles.GetCell(i);
                if (cell == null)
                {
                    cell = titles.CreateCell();
                }
                // cell.SetParagraph(SetCellText(doc, table, title));
                cell.SetText(title);
            }

            for (int i = 1; i < rowCount; i++)//有3个数组
            {
                DataRow dr = dt.Rows[i - 1];
                XWPFTableRow tablerow = table.CreateRow();
                // XWPFTableRow tablerow = table.GetRow(i);
                for (int j = 0; j < colCount; j++)
                {
                    string content = dr[j].ToString();
                    // tablerow.GetCell(j).SetParagraph(SetCellText(doc, table, content));

                    tablerow.GetCell(j).SetText(content);

                }
                //    table.AddRow(tablerow);
            }
        }



        // word 给表格追加行, 下标对应
        public static void AppendTable(XWPFTable table, DataTable newDatas)
        {
            //创建表格-提前创建好表格后填数

            int rowCount = newDatas.Rows.Count;
            int colCount = newDatas.Columns.Count;

            for (int i = 0; i < rowCount; i++)
            {
                XWPFTableRow row = table.CreateRow();

                for (int j = 0; j < colCount; j++)
                {
                    XWPFTableCell cell = row.GetCell(j);
                    if (cell == null)
                    {
                        cell = row.CreateCell();
                    }
                    string content = newDatas.Rows[i][j]?.ToString();
                    cell.SetText(content);
                }
            }
        }

    }



}