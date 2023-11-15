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
        public Adress Adress1 { get; set; }
        public int PhoneNumber { get; set; }
        public string Email{ get; set; }
        public string Title { get; set; }
        public int YearS { get; set; } // godine staza

        public  List<Subject>subjects { get; set; }

        public List<Chair> chairs { get; set; }

        public Professor() 
        {
            subjects = new List<Subject>();
            chairs= new List<Chair>();
        }
        public Professor(int id, string name, string surname, DateOnly birthdate, Adress adress, int phonenumber, string email, string title, int styear)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Birthdate = birthdate;
            Adress1 = adress;
            PhoneNumber = phonenumber;
            Email = email;
            Title = title;
            YearS = styear;
            chairs = new List<Chair>();

        }

        public Professor(string name, string surname, DateOnly birthdate, Adress adress, int phonenumber, string email, string title, int styear)
        {
            Name = name;
            Surname = surname;
            Birthdate = birthdate;
            Adress1 = adress;
            PhoneNumber = phonenumber;
            Email = email;
            Title = title;
            YearS = styear;
            chairs = new List<Chair>();


        }

        
        public override string ToString()
        {
            string s1;
             s1= $"ID: {Id,6} | Name: {Name,21} | Surname: {Surname,21} | Birthdate: {Birthdate,10} | Adress: {Adress1,30} | Phone number: {PhoneNumber,12} | Email: {Email,30} | Title: {Title,14} | Years of service: {YearS,3} |";
            foreach (Subject sb in subjects)
            {
                s1 += sb.ToString();
            }

            /*foreach (Chair ch in chairs)
            {
                s1 += ch.ToString();
            }*/
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
            Adress1.ToCSV()[0],
            Adress1.ToCSV()[1],
            Adress1.ToCSV()[2],
            Adress1.ToCSV()[3],
            Adress1.ToCSV()[4],
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
            Adress1 = new Adress();
            Adress1.FromCSV(new string[] { values[4], values[5], values[6], values[7], values[8] });
            PhoneNumber = int.Parse(values[9]);
            Email = values[10];
            Title = values[11];
            YearS = int.Parse(values[12]);
        }

        public void FromCSV1(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            Surname = values[2];

        }




    }
}
