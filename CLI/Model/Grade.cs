using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CLI.Storage.Serialization;
using static CLI.Model.Student;

namespace CLI.Model
{
    public class Grade : ISerializable
    {
       // Student student {  get; set; }
        public int Id { get; set; }
        public Student student {  get; set; }
        public  Subject subject { get; set; }
        public int grade {  get; set; }
        public DateOnly date {  get; set; }

        
        public Grade()
        {
           
        }
        public Grade(int id, Student st, Subject subj, int gr, DateOnly d)
        {
            Id = id;
            student = st;
            subject = subj; 
            grade = gr;
            date = d;
        }
        public Grade(int id, int gr, DateOnly d)
        {
            Id = id;
            grade = gr;
            date = d;
        }
        public Grade( Student st, Subject subj, int gr, DateOnly d)
        {
            
            student = st;
            subject = subj;
            grade = gr;
            date = d;
        }
        public override string ToString()
        {
            return $"ID: {Id,6} | Student's Name: {student.Name,10}| Student's Surname: {student.Surname,10}|Subject's Name{subject.Name,10}| Grade: {grade,2} | Date: {date,12}| ";
        }

     


        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                student.ToCSV1()[0],
                student.ToCSV1()[1],
            student.ToCSV1()[2],
            subject.ToCSV1()[0],
            subject.ToCSV1()[1],
            grade.ToString(),
            date.ToString()


        };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id= int.Parse(values[0]);
            student = new Student();
            student.FromCSV1(new string[] { values[1], values[2], values[3] });
            subject = new Subject();
            subject.FromCSV1(new string[] { values[4], values[5] });
            grade = int.Parse(values[6]);
            date = DateOnly.Parse(values[7]); 

        }
    }
}
