using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Model
{
    public class Subject
    {
        enum Semester { Summer, Winter };
        public int Id { get; set; }
        public string Name { get; set; }
        public int SYear {  get; set; }
        public int NumEspb { get; set; }

    }
}
