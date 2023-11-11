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

        public DateTime Birthdate { get; set; } 
        public int  Adress {  get; set; } //povezacemo kasnije preko id
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public Index IndexNm { get; set; }
        public int StYear { get; set; }
       // public double AvGrade { get; set; }

       // List<int> Grades = new List<int>();
       // List<string> Subjects = new List<string>();

        public Student ()
        { }

        public Student ( int id,  string name, string surname, DateTime birthdate, int adress, int phonenumber, string email,Index indexnm, int styear ) //fali avggrade parametar
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

        public Student(string name, string surname, DateTime birthdate, int adress, int phonenumber, string email, Index indexnm, int styear) //fali avggrade parametar
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
            Birthdate.ToString(),
            Adress.ToString(),
            PhoneNumber.ToString(),
            Email.ToString(),
            IndexNm.ToCSV()[0],
            IndexNm.ToCSV()[1],
            IndexNm.ToCSV()[2],
            StYear.ToString()

        };
            return csvValues;
        }

        public void FromCSV(string[] values) //cita iz fajla
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            Surname = values[2];
            Birthdate = DateTime.Parse(values[3]);
            Adress = int.Parse(values[4]);
            PhoneNumber = int.Parse(values[5]);
            Email = values[6];
            IndexNm = new Index();
            IndexNm.FromCSV(new string[] {  values[7], values[8], values[9] });
            StYear = int.Parse(values[10]);  
        }
    }
}
