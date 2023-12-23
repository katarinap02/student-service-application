using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Storage.Serialization;

namespace CLI.Model
{
    public class Student : ISerializable
    {
        public enum Status { B, S };

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public DateOnly Birthdate { get; set; }
        public Adress AdressSt { get; set; } //povezacemo kasnije preko id
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Index IndexNm { get; set; }
        public int StYear { get; set; }

        

        public List<Subject> Subjects { get; set; } // lista nepolozenih predmeta

        public List<Subject> SubjectsP {  get; set; } //pomocna lista polozenih predmeta
       
        public Status StudentStatus { get; set; }

        public List<Grade> Grades = new List<Grade>();
       
        public Student()
        {
            Grades = new List<Grade>();
            Subjects = new List<Subject>();
            SubjectsP = new List<Subject>();    
        }


          public double Average(List<Grade> grades)
       {
           if (grades == null || grades.Count == 0)
           {

               return 0;
           }

           int sum = 0;

           foreach (Grade g in grades)
           {
               sum += g.grade;
           }

           double av = (double)sum / Grades.Count;
           return av;


       }




        public Student ( int id,  string name, string surname, DateOnly birthdate, Adress adress, string phonenumber, string email,Index indexnm, int styear, Status s )
        {
            Id = id;
            Name = name;
            Surname = surname;
            Birthdate = birthdate;
            AdressSt = adress;
            PhoneNumber = phonenumber;
            Email = email;
            IndexNm = indexnm;
            StYear = styear;
            StudentStatus = s;
            Subjects = new List<Subject>();
            

        }

        public Student(string name, string surname, DateOnly birthdate, Adress adress, string phonenumber, string email, Index indexnm, int styear, Status s) //fali avggrade parametar, samo ispisi
        {
            Name = name;
            Surname = surname;
            Birthdate = birthdate;
            AdressSt = adress;
            PhoneNumber = phonenumber;
            Email = email;
            IndexNm = indexnm;
            StYear = styear;
            StudentStatus = s;
            Subjects = new List<Subject>();
            

        } //trebace nam jer se id generise

        

        public override string ToString()
        {
            string s;
            s = $"ID: {Id,2} | Name: {Name,10} | Surname: {Surname,10} | Birthdate: {Birthdate,10} " + 
                $"| Adress: {AdressSt, 30} | Phone number: {PhoneNumber, 12} | \nEmail: {Email,20} | Index: {IndexNm, 12} " +
                $"| Current school year: {StYear, 4} | Current student's status: {StudentStatus, 2} | Average Grade {Average(Grades), 2} |" +
                $" \nSubjectNames: |";
            foreach(Subject sub in Subjects)
            {
                s += sub.Name + " ";
            }

            s += $" \nGrades: |";

            foreach (Grade gra in Grades)
            {
                s +=gra.subject.Name + " " + gra.grade.ToString() + " ";
            }
            s+= "\n";
            return s;
            

        }
        public string[] ToCSV() //ucitava u fajl
        {
            string[] csvValues =
            {
            Id.ToString(),
            Name,
            Surname,
            Birthdate.ToString("yyyy-MM-dd"),
            AdressSt.ToCSV()[0],
            AdressSt.ToCSV()[1],
            AdressSt.ToCSV()[2],
            AdressSt.ToCSV()[3],
            AdressSt.ToCSV()[4],
            PhoneNumber.ToString(),
            Email.ToString(),
            IndexNm.ToCSV()[0],
            IndexNm.ToCSV()[1],
            IndexNm.ToCSV()[2],
            StYear.ToString(),
            StudentStatus.ToString()

        };
            return csvValues;
        }
        public string[] ToCSV1() //ucitava u fajl
        {
            string[] csvValues =
            {
            Id.ToString(),
            Name,
            Surname
           

        };
            return csvValues;
        }
        public void FromCSV(string[] values) //cita iz fajla
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            Surname = values[2];
            Birthdate = DateOnly.Parse(values[3]);
            AdressSt = new Adress();
            AdressSt.FromCSV(new string[] { values[4], values[5], values[6], values[7], values[8] });
            PhoneNumber = values[9];
            Email = values[10];
            IndexNm = new Index();
            IndexNm.FromCSV(new string[] {  values[11], values[12], values[13] });
            StYear = int.Parse(values[14]);
            StudentStatus = Enum.Parse<Status>(values[15]);   
           
        }

        public void FromCSV1(string[] values) //cita iz fajla
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            Surname = values[2];
           
        }
        
    }
}
