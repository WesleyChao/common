using System;
using System.Collections.Generic;
namespace testEF.Models
{
    public partial class CourseInstance
    {
        public string InstanceID { get; set; }
        public string InstanceName { get; set; }
        // public string CourseID { get; set; }
        public virtual Course Course { get; set; }
    }
}