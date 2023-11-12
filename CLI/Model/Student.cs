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
        public enum Status { B, S };

        public int Id { get;  set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public DateTime Birthdate { get; set; } 
        public Adress  Adress1 {  get; set; } //povezacemo kasnije preko id
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public Index IndexNm { get; set; }
        public int StYear { get; set; }

        public List<Subject> Subjects { get; set; } // lista nepolozenih predmeta

        public Status Status1 { get; set; }
       // public double AvGrade { get; set; }

       // List<int> Grades = new List<int>();
       // List<string> Subjects = new List<string>();

        public Student ()
        {
            Subjects = new List<Subject>();
        }

        public Student ( int id,  string name, string surname, DateTime birthdate, Adress adress, int phonenumber, string email,Index indexnm, int styear, Status s ) //fali avggrade parametar
        {
            Id = id;
            Name = name;
            Surname = surname;
            Birthdate = birthdate;
            Adress1 = adress;
            PhoneNumber = phonenumber;
            Email = email;
            IndexNm = indexnm;
            StYear = styear;
            Status1 = s;
            Subjects = new List<Subject>();
            

        }

        public Student(string name, string surname, DateTime birthdate, Adress adress, int phonenumber, string email, Index indexnm, int styear, Status s) //fali avggrade parametar
        {
            Name = name;
            Surname = surname;
            Birthdate = birthdate;
            Adress1 = adress;
            PhoneNumber = phonenumber;
            Email = email;
            IndexNm = indexnm;
            StYear = styear;
            Status1 = s;
            Subjects = new List<Subject>();
            

        } //trebace nam jer se id generise

        public override string ToString()
        {
            return $"ID: {Id,5} | Name: {Name,15} | Surname: {Surname,15} | Birthdate: {Birthdate,10} | Adress: {Adress1, 30} | Phone number: {PhoneNumber, 12} | Email: {Email,20} | Index: {IndexNm, 12} | Current school year: {StYear, 4} | Current student's status: {Status1, 2} |";
        }
        
        public string[] ToCSV() //ucitava u fajl
        {
            string[] csvValues =
            {
            Id.ToString(),
            Name,
            Surname,
            Birthdate.ToString("yyyy-MM-dd"),
            Adress1.ToCSV()[0],
            Adress1.ToCSV()[1],
            Adress1.ToCSV()[2],
            Adress1.ToCSV()[3],
            Adress1.ToCSV()[4],
            PhoneNumber.ToString(),
            Email.ToString(),
            IndexNm.ToCSV()[0],
            IndexNm.ToCSV()[1],
            IndexNm.ToCSV()[2],
            StYear.ToString(),
            Status1.ToString()

        };
            return csvValues;
        }

        public void FromCSV(string[] values) //cita iz fajla
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            Surname = values[2];
            Birthdate = DateTime.Parse(values[3]);
            Adress1 = new Adress();
            Adress1.FromCSV(new string[] { values[4], values[5], values[6], values[7], values[8] });
            PhoneNumber = int.Parse(values[9]);
            Email = values[10];
            IndexNm = new Index();
            IndexNm.FromCSV(new string[] {  values[11], values[12], values[13] });
            StYear = int.Parse(values[14]);
            Status1= Enum.Parse<Status>(values[15]);   
           
        }
    }
}
