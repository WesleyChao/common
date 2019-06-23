using System;
using System.Collections.Generic;
namespace testEF.Models
{
    public partial class CourseType
    {
        public CourseType()
        {
            this.Course = new HashSet<Course>();
        }
        public string CourseTypeID { get; set; }
        public string CourseTypeName { get; set; }
        public virtual ICollection<Course> Course { get; set; }

    }
}