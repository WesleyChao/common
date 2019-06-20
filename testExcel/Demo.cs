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
        static string xlsPath = $@"{ Directory.GetCurrentDirectory()}\userfile\a.xlsx";
        static string xlsToPath = $@"{ Directory.GetCurrentDirectory()}\userfile\c.xlsx";
        public static void Main(string[] args)
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


            Console.WriteLine("ok");
        }

        public static void 导出Excel()
        {
            List<Student> students = new List<Student>()
            {
                new Student(){StuNo = "", Age = 10},


            };


            // xlsToPath


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