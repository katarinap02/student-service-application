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

        public int Id { get;  set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public string Birthdate { get; set; }
        public string Adress {  get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public string IndexNm { get; set; }
        public int StYear { get; set; }
        public double AvGrade { get; set; }

        List<int> Grades = new List<int>();
        List<string> Subjects = new List<string>();

        public Student ( int id,  string name, string surname, string birthdate, string adress, int phonenumber, string email,string indexnm, int styear, double avgrade  )
        {
            Id = id;
            Name = name;
            Surname = surname;
            Birthdate = birthdate;
            Adress = adress;
            PhoneNumber = phonenumber;
            Email = email;
            IndexNm = indexnm;
            StYear = styear;
            AvGrade = avgrade;

        }



    }
}
