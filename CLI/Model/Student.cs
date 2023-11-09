using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Storage.Serialization;

namespace CLI.Model
{
    public class Student : ISerializable
    {
        enum Status {B, S};

        public int Id { get;  set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public string Birthdate { get; set; } 
        public int  Adress {  get; set; } //povezacemo kasnije preko id
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public string IndexNm { get; set; }
        public int StYear { get; set; }
       // public double AvGrade { get; set; }

       // List<int> Grades = new List<int>();
       // List<string> Subjects = new List<string>();

        public Student ( int id,  string name, string surname, string birthdate, int adress, int phonenumber, string email,string indexnm, int styear, double avgrade  )
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
           // AvGrade = avgrade;

        }

        public Student(string name, string surname, string birthdate, int adress, int phonenumber, string email, string indexnm, int styear, double avgrade)
        {
            Name = name;
            Surname = surname;
            Birthdate = birthdate;
            Adress = adress;
            PhoneNumber = phonenumber;
            Email = email;
            IndexNm = indexnm;
            StYear = styear;
            // AvGrade = avgrade;

        } //trebace nam jer se id generise

        public string[] ToCSV() //ucitava u fajl
        {
            string[] csvValues =
            {
            Id.ToString(),
            Name,
            Surname,
            Birthdate,
            Adress.ToString(),
            PhoneNumber.ToString(),
            Email.ToString(),
            IndexNm.ToString(),
            StYear.ToString()

        };
            return csvValues;
        }

        public void FromCSV(string[] values) //cita iz fajla
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            Surname = values[2];
            Birthdate = values[3];
            Adress = int.Parse(values[4]);
            PhoneNumber = int.Parse(values[5]);
            Email = values[6];
            IndexNm = values[7];
            StYear = int.Parse(values[8]);  
        }
    }
}
