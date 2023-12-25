using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Storage.Serialization;
using static CLI.Model.Student;

namespace CLI.Model
{
    public class Professor : ISerializable
    {
        public int Id { get; set; } //broj licne karte
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly Birthdate { get; set; }
        public Adress AdressPr { get; set; }
        public string PhoneNumber { get; set; }
        public string Email{ get; set; }
        public string Title { get; set; }
        public int YearS { get; set; } // godine staza

        public int bind { get; set; }

        public  List<Subject>subjects { get; set; }

        public List<Chair> chairs { get; set; }

        public Professor() 
        {
            subjects = new List<Subject>();
            chairs= new List<Chair>();
        }
        public Professor(int id, string name, string surname, DateOnly birthdate, Adress adress, string phonenumber, string email, string title, int styear)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Birthdate = birthdate;
            AdressPr = adress;
            PhoneNumber = phonenumber;
            Email = email;
            Title = title;
            YearS = styear;
            chairs = new List<Chair>();

        }

        public Professor(string name, string surname, DateOnly birthdate, Adress adress, string phonenumber, string email, string title, int styear)
        {
            Name = name;
            Surname = surname;
            Birthdate = birthdate;
            AdressPr = adress;
            PhoneNumber = phonenumber;
            Email = email;
            Title = title;
            YearS = styear;
            chairs = new List<Chair>();


        }

        
        public override string ToString()
        {
            string s1;
             s1= $"ID: {Id,2} | Name: {Name,10} | Surname: {Surname,10} | Birthdate: {Birthdate,10} |" +
                $" Adress: {AdressPr,30} | Phone number: {PhoneNumber,12} | \nEmail: {Email,20} |" +
                $" Title: {Title,14} | Years of service: {YearS,3} |" +
                $" \nSubjects: |";
            foreach (Subject sb in subjects)
            {
                s1 += sb.Name + " ";
            }

            s1 += "\nChairs: |";
            foreach (Chair ch in chairs)
            {
                s1 += ch.CName + " ";
            }
            s1 += "\n";
            return s1;
        }
        public string[] ToCSV() //ucitava u fajl
        {
            string[] csvValues =
            {
            Id.ToString(),
            Name,
            Surname,
            Birthdate.ToString(),
            AdressPr.ToCSV()[0],
            AdressPr.ToCSV()[1],
            AdressPr.ToCSV()[2],
            AdressPr.ToCSV()[3],
            AdressPr.ToCSV()[4],
            PhoneNumber.ToString(),
            Email.ToString(),
            Title,
            YearS.ToString()

        };
            return csvValues;
        }

        public string[] ToCSV1()
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
            PhoneNumber = values[4];
            Email = values[5];
            Title = values[6];
            YearS = int.Parse(values[7]);
            AdressPr = new Adress();
            AdressPr.FromCSV(new string[] { values[8], values[9], values[10], values[11], values[12] });
            
           
           
           
        }

        public void FromCSV1(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            Surname = values[2];

        }




    }
}
