using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Model
{
    public class Student
    {
        enum Status {B, S};
        public string name { get; set; }
        public string surname { get; set; }

        public string birthdate { get; set; }
        public string adress {  get; set; }
        public int phoneNumber { get; set; }
        public string email { get; set; }
        public string indexNm { get; set; }
        public int stYear { get; set; }
        public double avGrade { get; set; }


    }
}
