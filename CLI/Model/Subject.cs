using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Storage.Serialization;


namespace CLI.Model
{
    public class Subject : ISerializable
    {
        public enum Semester { Summer, Winter };
        public int Id { get; set; }
        public string Name { get; set; }

        public Semester Semester1 { get; set; }
        public int SYear {  get; set; }
        public int NumEspb { get; set; }
        
        public Professor Professor { get; set; }

      //  public List<Student> StudentsP { get; set; }//spisak onih koji su polozili

       // public List<Student> StudentsF { get; set; } //spisak onih koji nisu polozili




        public Subject() { }

        public Subject( int id, string name, Semester s, int syear,  int numespb)
        {
       
            Id = id; 
            Name = name;
            Semester1 = s; //postavljen je inicijalno na letnji
            SYear = syear;
            NumEspb = numespb;
            //StudentsP = new List<Student>();
           // StudentsF = new List<Student>();



        }

        public Subject(string name, Semester s, int syear,  int numespb)
        {
            Name = name;
            Semester1 = s; //postavljen je inicijalno na letnji
            SYear = syear;
            NumEspb = numespb;
            //StudentsP = new List<Student>();
            // StudentsF = new List<Student>();



        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
            Id.ToString(),
            Name,
            SYear.ToString(),
            NumEspb.ToString()


        };
            return csvValues;

        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            SYear = int.Parse(values[2]);
            NumEspb = int.Parse(values[3]);

        }
    }
}
