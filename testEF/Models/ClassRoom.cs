using System;
using System.Collections.Generic;
namespace testEF.Models
{
    public partial class ClassRoom
    {
        public ClassRoom()
        {
           // Student = new HashSet<Student>();
        }
        public string RoomID { get; set; }
        public string RoomName { get; set; }

      //  public virtual ICollection<Student> Student { get; set; }
    }
}