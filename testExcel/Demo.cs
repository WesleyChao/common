using System;
using System.Data;
using Qx.Util.Office;
using System.Collections.Generic;
using System.IO;
using NPOI.XWPF.UserModel;
namespace Wesley.Demo
{
    public class Demo
    {
        static string docPath = $@"{ Directory.GetCurrentDirectory()}\userfile\a.docx";
        static string docToPath = $@"{ Directory.GetCurrentDirectory()}\userfile\c.docx";
        static string docToPath1 = $@"{ Directory.GetCurrentDirectory()}\userfile\d.docx";
        static string xlsPath = $@"{ Directory.GetCurrentDirectory()}\userfile\c.xlsx";
        static string xlsToPath = $@"{ Directory.GetCurrentDirectory()}\userfile\c.xlsx";
        public static void Main(string[] args)
        {


            追加表格行();
            Console.WriteLine("ok");
        }

        public static void 导入Excel()
        {
            MemoryStream ms = null;
            using (FileStream fs = new FileStream(xlsPath, FileMode.Open, FileAccess.Read))
            {
                byte[] vs = new byte[fs.Length];
                int cou = fs.Read(vs, 0, (int)vs.Length);
                ms = new MemoryStream(vs);
            }

            DataTable dt = ExcelHelper.Import(ms);
            IList<Student> st = ListDatatableMapper<Student>.DataTableToList(dt);

            foreach (var s in st)
            {
                Console.WriteLine(s.ToString());
            }

        }

        public static void 导出Excel()
        {
            List<Student> students = new List<Student>()
            {
                new Student(){StuNo = "111111", Age = 10, PhoneNum = "4321",Nationality = "Japan", Sex = 5},
                new Student(){StuNo = "222222", Age = 20, PhoneNum = "1243",Nationality = "Japan", Sex = 4},
                new Student(){StuNo = "333333", Age = 30, PhoneNum = "4321",Nationality = "Japan", Sex = 3},
                new Student(){StuNo = "444444", Age = 10, PhoneNum = "1234",Nationality = "Japan", Sex = 2},
            };

            DataTable td = ListDatatableMapper<Student>.ListToDataTable(students);


            // xlsToPath
            using (MemoryStream ms = ExcelHelper.Export(td))
            {
                using (FileStream fs = new FileStream(xlsToPath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    byte[] bytes = new byte[ms.Length];
                    ms.Read(bytes, 0, bytes.Length);
                    fs.Write(bytes, 0, bytes.Length);
                }
            }

        }

        public static void 添加表格()
        {
            List<Student> students = new List<Student>()
            {
                new Student(){StuNo = "111111", Age = 10, PhoneNum = "4321",Nationality = "Japan", Sex = 5},
                new Student(){StuNo = "222222", Age = 20, PhoneNum = "1243",Nationality = "Japan", Sex = 4},
                new Student(){StuNo = "333333", Age = 30, PhoneNum = "4321",Nationality = "Japan", Sex = 3},
                new Student(){StuNo = "444444", Age = 10, PhoneNum = "1234",Nationality = "Japan", Sex = 2},
            };

            DataTable td = ListDatatableMapper<Student>.ListToDataTable(students);

            XWPFDocument doc = new XWPFDocument();
            Func<XWPFDocument, XWPFTable, string, XWPFParagraph> ff = (x, table, setText) =>
            {
                NPOI.OpenXmlFormats.Wordprocessing.CT_P para = new NPOI.OpenXmlFormats.Wordprocessing.CT_P();
                XWPFParagraph pCell = new XWPFParagraph(para, table.Body);
                pCell.Alignment = ParagraphAlignment.CENTER;//字体居中  
                pCell.VerticalAlignment = TextAlignment.CENTER;//字体居中  

                XWPFRun r1c1 = pCell.CreateRun();
                r1c1.SetText(setText);
                r1c1.FontSize = 12;
                // r1c1.FontFamily = "华文楷体";
                //r1c1.SetTextPosition(20);//设置高度  
                return pCell;

            };
            WordHelper.AddTable(doc, td, ff);

            FileStream ms = new FileStream(docToPath, FileMode.OpenOrCreate, FileAccess.Write);
            doc.Write(ms);
            ms.Dispose();
        }


        public static void 已有的文档添加表格_添加文字_添加表格()
        {
            XWPFDocument doc = null;
            // 加载文件
            using (FileStream fs = new FileStream(Demo.docPath, FileMode.Open, FileAccess.Read))
            {
                doc = new XWPFDocument(fs);
            }
            // 添加表格
            List<Student> stus = new List<Student>()
            {
                new Student(){StuNo = "111111", Age = 10, PhoneNum = "4321",Nationality = "Japan", Sex = 5},
                new Student(){StuNo = "222222", Age = 20, PhoneNum = "1243",Nationality = "Japan", Sex = 4},
                new Student(){StuNo = "333333", Age = 30, PhoneNum = "4321",Nationality = "Japan", Sex = 3},
                new Student(){StuNo = "444444", Age = 10, PhoneNum = "1234",Nationality = "Japan", Sex = 2},
            };
            DataTable dt = ListDatatableMapper<Student>.ListToDataTable(stus);
            WordHelper.AddTable(doc, dt);

            // 添加文字
            WordHelper.AddParagrath(doc, "中间文字");

            // 添加表格
            List<Room> rooms = new List<Room>()
            {
                new Room(){ID = 1, Name="Room1", Floor="1" },
                new Room(){ID = 2, Name="Room2", Floor="3" },
                new Room(){ID = 3, Name="Room3", Floor="2" },
            };
            DataTable dt2 = ListDatatableMapper<Room>.ListToDataTable(rooms);
            WordHelper.AddTable(doc, dt2);
            // 保存

            using (FileStream fs = new FileStream(docToPath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                doc.Write(fs);
            }

        }


        public static void 追加表格行()
        {
            List<Student> students = new List<Student>()
            {
                new Student(){StuNo = "new4321", Age = 10, PhoneNum = "4321",Nationality = "Japan", Sex = 5},
                new Student(){StuNo = "new3214", Age = 20, PhoneNum = "1243",Nationality = "Japan", Sex = 4},
                new Student(){StuNo = "new2143", Age = 30, PhoneNum = "4321",Nationality = "Japan", Sex = 3},
                new Student(){StuNo = "new1432", Age = 10, PhoneNum = "1234",Nationality = "Japan", Sex = 2},
            };
            DataTable td = ListDatatableMapper<Student>.ListToDataTable(students);
            XWPFDocument doc = null;
            using (FileStream fs = new FileStream(Demo.docPath, FileMode.Open, FileAccess.Read))
            {
                doc = new XWPFDocument(fs);
            }
            XWPFTable t = doc.Tables[0];
            WordHelper.AppendTable(t, td);

            using (FileStream fs = new FileStream(docToPath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                doc.Write(fs);
            }

        }

    }

    public class Student
    {
        public string StuNo { get; set; }
        public int Age { get; set; }
        public string PhoneNum { get; set; }
        public string Nationality { get; set; }
        public int Sex { get; set; }

        public override string ToString()
        {
            return $"{this.StuNo} {this.Age}  {this.PhoneNum}  {this.Nationality}  {this.Sex}";
        }
    }
    public class Room
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Floor { get; set; }
    }
    public enum Sex
    {
        Male, Female
    }


}