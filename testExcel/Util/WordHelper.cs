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
        // todo

        // word 添加表格, 表格的行宽怎么确定
        private static void AddTable(XWPFDocument doc, DataTable content, Dictionary<int, string> coloWidth, Func<XWPFDocument, XWPFTable, string, XWPFParagraph> SetCellText)
        {
            // XWPFDocument doc = new XWPFDocument();
            //创建表格-提前创建好表格后填数
            XWPFTable tableContent = doc.CreateTable(content.Rows.Count, content.Columns.Count);//4行5列

            // 设置content的标题
            tableContent.GetRow(0).GetCell(0).SetParagraph(SetCellText(doc, tableContent, "地点"));
            tableContent.GetRow(0).GetCell(1).SetParagraph(SetCellText(doc, tableContent, "日期"));
            tableContent.GetRow(0).GetCell(2).SetParagraph(SetCellText(doc, tableContent, "男性"));
            tableContent.GetRow(0).GetCell(3).SetParagraph(SetCellText(doc, tableContent, "女性"));
            tableContent.GetRow(0).GetCell(4).SetParagraph(SetCellText(doc, tableContent, "合计"));


            for (int i = 0; i < content.Rows.Count; i++)//有3个数组
            {
                DataRow dr = content.Rows[i];
                //  ArrayList ls = list[i];
                for (int j = 0; j < dr.ItemArray.Length; j++)
                {
                    tableContent.GetRow(i + 1).GetCell(j).SetParagraph(SetCellText(doc, tableContent, dr[j].ToString()));
                }
            }

        }
        // word 给表格追加行, 下标对应
        private static void UpdateTable(XWPFTable table, DataTable newDatas)
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
        }

    }



}