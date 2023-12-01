using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CLI.Storage.Serialization;
using static CLI.Model.Student;


namespace CLI.Model
{
    public class Subject : ISerializable
    {
        public enum Semester { Summer, Winter };
        public int Id { get; set; }
        public string Name { get; set; }

        public Semester SemesterSb { get; set; }
        public int SYear {  get; set; }
        public int NumEspb { get; set; }

        public int bind { get; set; }

        public Professor ProfessorSb { get; set; }
       


         public List<Student> StudentsP { get; set; }//spisak onih koji su polozili


         public List<Student> StudentsF { get; set; } //spisak onih koji nisu polozili




        public Subject() 
        {
               StudentsP = new List<Student>();
               StudentsF = new List<Student>();
        }

        public Subject( int id, string name, Semester s, int syear,  int numespb, Professor p)
        {
       
            Id = id; 
            Name = name;
            SemesterSb = s; //postavljen je inicijalno na letnji
            SYear = syear;
            NumEspb = numespb;
            ProfessorSb = p;



        }

        public Subject(string name, Semester s, int syear,  int numespb, Professor p)
        {
            Name = name;
            SemesterSb = s; //postavljen je inicijalno na letnji
            SYear = syear;
            NumEspb = numespb;
            ProfessorSb = p;



        }
        public override string ToString()
        {
            string s;
            s= $"ID: {Id,6} | Name: {Name,10} | Semester: {SemesterSb,15} | Year: {SYear,10} | ESPB: {NumEspb,5} | Proffesors's Name: {ProfessorSb.Name,10}| Professor's Surname: {ProfessorSb.Surname,10} |"+
            $" \nStudents:  |";

            foreach (Student st in StudentsF)
            {
                s += st.Name;
                s += " ";
                s += st.Surname;
                s+= "   ";
            }
            s += "\n";
            return s;
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Name,
                SemesterSb.ToString(),
                SYear.ToString(),
                NumEspb.ToString(),
                ProfessorSb.ToCSV1()[0],
                ProfessorSb.ToCSV1()[1],
            ProfessorSb.ToCSV1()[2],


        };
            return csvValues;

        }
        public string[] ToCSV1()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Name
                


        };
            return csvValues;

        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            SemesterSb = Enum.Parse<Semester>(values[2]);
            SYear = int.Parse(values[3]);
            NumEspb = int.Parse(values[4]);
            ProfessorSb = new Professor();
            ProfessorSb.FromCSV1(new string[] { values[5], values[6], values[7] });

        }
        public void FromCSV1(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            

        }

        
    }
}
