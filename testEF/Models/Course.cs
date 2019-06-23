using System;
using System.Collections.Generic;
namespace testEF.Models
{
    public partial class Course
    {

        public Course()
        {
            CourseInstance = new HashSet<CourseInstance>();
        }
        public string CourseID { get; set; }
        public string CourseName { get; set; }
        public string CourseTypeID { get; set; }
        public virtual CourseType CourseType { get; set; }
        public virtual ICollection<CourseInstance> CourseInstance { get; set; }

    }
}