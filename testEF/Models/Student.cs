using System;
using System.Collections.Generic;

namespace testEF.Models
{
    public partial class Student
    {
        public Student()
        {
            ClassRoom = new HashSet<ClassRoom>();
        }
        public int No { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime Birthday { get; set; }

        public ICollection<ClassRoom> ClassRoom { get; set; }
    }
}
