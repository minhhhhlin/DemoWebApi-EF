using System.Collections;
using System.Collections.Generic;

namespace DemoWebAPI_2.Entity
{
    public class Class
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Student> Students { get; set; }

    }
}
