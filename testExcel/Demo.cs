using System;
using System.Data;
using Qx.Util.Office;
using System.Collections.Generic;
using System.IO;
namespace Wesley.Demo
{
    public class Demo
    {
        static string docPath = $@"{ Directory.GetCurrentDirectory()}\userfile\a.docx";
        static string docToPath = $@"{ Directory.GetCurrentDirectory()}\userfile\c.docx";
        static string xlsPath = $@"{ Directory.GetCurrentDirectory()}\userfile\c.xlsx";
        static string xlsToPath = $@"{ Directory.GetCurrentDirectory()}\userfile\c.xlsx";
        public static void Main(string[] args)
        {


            导入Excel();
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
    public enum Sex
    {
        Male, Female
    }


}