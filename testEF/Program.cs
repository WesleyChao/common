using System;
using System.Linq;
using System.Collections.Generic;
using testEF.Models;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace testEF
{
    class Program
    {

        static void Main(string[] args)
        {
            using (efCoreMVCContext DB = new efCoreMVCContext())
            {

                List<Course> c = DB.Course.ToList();
                // CourseType cy = DB.CourseTypes.Single();
                CourseType ct = DB.CourseTypes.First(u => u.CourseTypeID == "1");
                DB.CourseTypes.Remove(ct);
                Console.WriteLine(DB.Entry(ct).State);
                Console.WriteLine(DB.Entry(c.First()).State);
                Console.WriteLine(DB.Entry(c.First().CourseType).State);

                DB.SaveChanges();
                Console.WriteLine(DB.Entry(ct).State);
                Console.WriteLine(DB.Entry(c.First()).State);
                Console.WriteLine(DB.Entry(c.First().CourseType).State);



            }
            Console.WriteLine("ok");
        }



        static void Main4312(string[] args)
        {
            using (efCoreMVCContext DB = new efCoreMVCContext())
            {

                // // CourseType ct = new CourseType()
                // // {
                // //     CourseTypeID = "t1",
                // //     CourseTypeName = "type1"
                // // };

                // Course c = new Course()
                // {
                //     CourseID = "c1",
                //     CourseName = "course1",
                //     CourseTypeID = "t1"
                // };
                // DB.Set<CourseType>().Add(ct);
                // DB.Set<Course>().Attach(c);
                //目前EF Core发布的最新版本中并不支持懒加载，开发人员必须使用Include方法，才能完成导航属性的加载。   fuck. 搞了半天, 居然不支持
                Course c = DB.Set<Course>().Include(u => u.CourseType).Single(u => u.CourseID == "c1");
                // DB.SaveChanges();
                Console.WriteLine(c.CourseName);

                Console.WriteLine(c.CourseType.CourseTypeID);



            }
            Console.WriteLine("======ok======");
        }

        static void Main1()
        {
            List<List<string>> tmp = new List<List<string>>()
            {
                new List<string>(){"11","22"},
                new List<string>(){"11","22"},
                new List<string>(){"11"},

            };
            Console.WriteLine(tmp.Distinct().Count());
            Console.WriteLine(tmp.Distinct(new StringListEqualityComparer()).Count());

        }
    }

    public class StringListEqualityComparer : IEqualityComparer<List<string>>
    {
        public bool Equals(List<string> x, List<string> y)
        {
            if (x.Count != y.Count)
            {
                return false;
            }
            for (int i = 0; i < x.Count; i++)
            {
                if (x[i] != y[i])
                {
                    return false;
                }
            }
            return true;
        }

        public int GetHashCode(List<string> obj)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in obj)
            {
                stringBuilder.Append(item);
            }
            return stringBuilder.ToString().GetHashCode();
        }
    }
}