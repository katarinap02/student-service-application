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
        public int Adress { get; set; }
        public int PhoneNumber { get; set; }
        public string Email{ get; set; }
        public string Title { get; set; }
        public int YearS { get; set; } // godine staza

       public  List<string>subjects { get; set; }

        public Professor() 
        {
            subjects = new List<string>();
        }
        public Professor(int id, string name, string surname, DateOnly birthdate, int adress, int phonenumber, string email, string title, int styear)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Birthdate = birthdate;
            Adress = adress;
            PhoneNumber = phonenumber;
            Email = email;
            Title = title;
            YearS = styear;
            //subjects = new List<Subject>();

        }

        public Professor(string name, string surname, DateOnly birthdate, int adress, int phonenumber, string email, string title, int styear)
        {
            Name = name;
            Surname = surname;
            Birthdate = birthdate;
            Adress = adress;
            PhoneNumber = phonenumber;
            Email = email;
            Title = title;
            YearS = styear;



        }

        public void AddElementToSubject(string s)
        {
            subjects.Add(s);
        }
        public override string ToString()
        {
            return $"ID: {Id,6} | Name: {Name,21} | Surname: {Surname,21} | Birthdate: {Birthdate,10} | Adress: {Adress,21} | Phone number: {PhoneNumber,12} | Email: {Email,30} | Title: {Title,14} | Years of service: {YearS,3} |";
        }
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
            Adress = int.Parse(values[4]);
            PhoneNumber = int.Parse(values[5]);
            Email = values[6];
            Title = values[7];
            YearS = int.Parse(values[8]);
        }

        public void FromCSV1(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            Surname = values[2];

        }




    }
}
