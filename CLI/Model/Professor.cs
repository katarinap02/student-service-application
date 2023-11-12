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
        public DateTime Birthdate { get; set; }
        public int Adress { get; set; }
        public int PhoneNumber { get; set; }
        public string Email{ get; set; }
        public string Title { get; set; }
        public int YearS { get; set; } // godine staza

      //public  List<Subject>subjects { get; set; }

        public Professor() { }
        public Professor(int id, string name, string surname, DateTime birthdate, int adress, int phonenumber, string email, string title, int styear)
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
          //  subjects = new List<Subject>();

        }

        public Professor(string name, string surname, DateTime birthdate, int adress, int phonenumber, string email, string title, int styear)
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
        public override string ToString()
        {
            return $"ID: {"",6} | Name: {"",21} | Surname: {"",21} | Birthdate: {"",10} | Adress: {"",21} | Phone number: {"",12} | Email: {"",30} | Title: {"",14} | Years of service: {"",3} |";
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

        public void FromCSV(string[] values) //cita iz fajla
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            Surname = values[2];
            Birthdate = DateTime.Parse(values[3]);
            Adress = int.Parse(values[4]);
            PhoneNumber = int.Parse(values[5]);
            Email = values[6];
            Title = values[7];
            YearS = int.Parse(values[8]);
        }




    }
}
