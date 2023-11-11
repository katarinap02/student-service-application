using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CLI.Storage.Serialization;

namespace CLI.Model
{
    public class Grade : ISerializable
    {
        Student student {  get; set; }
        Subject subject { get; set; }
        int grade {  get; set; }
        DateOnly date {  get; set; }

        public Grade(Student student, Subject subject, int grade, DateOnly date)
        {
            this.student = student;
            this.subject = subject;
            this.grade = grade;
            this.date = date;
        }   
        public Grade()
        {
            grade = 6;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
             {
           // student.ToString(),
           // subject.ToString(),
            grade.ToString(),
            date.ToString()


        };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            grade= int.Parse(values[0]);
            date = DateOnly.Parse(values[1]); //ne znam sta cu sa studentom i profesorom

        }
    }
}
