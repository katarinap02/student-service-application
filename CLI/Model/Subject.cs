using System;
using System.Collections.Generic;
using System.Globalization;
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
        
        public Professor Professor { get; set; }

        public List<Student> StudentsP { get; set; }//spisak onih koji su polozili

        public List<Student> StudentsF { get; set; } //spisak onih koji nisu polozili






        public Subject( int id, string name, int syear, int numespb )
        {
       
            Id = id; 
            Name = name;
            SYear = syear;
            NumEspb = numespb;
            StudentsP = new List<Student>();
            StudentsF = new List<Student>();



        }

    }
}
