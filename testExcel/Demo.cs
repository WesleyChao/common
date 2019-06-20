using System;
using System.Data;
using Qx.Util.Office;
using System.Collections.Generic;
namespace Wesley.Demo
{
    public class Demo
    {
        static string docPath = @"C:\Users\Wesley\source\dotnetCore\UselessProjects\testExcel\userfile\a.docx";
        static string docToPath = @"C:\Users\Wesley\source\dotnetCore\UselessProjects\testExcel\userfile\c.docx";
        static string xlsPath = @"C:\Users\Wesley\source\dotnetCore\UselessProjects\testExcel\userfile\a.xlsx";
        static string xlsToPath = @"C:\Users\Wesley\source\dotnetCore\UselessProjects\testExcel\userfile\c.xlsx";
        public static void Main(string[] args)
        {

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
        public Sex Sex { get; set; }
    }
    public enum Sex
    {
        Male, Female
    }


}