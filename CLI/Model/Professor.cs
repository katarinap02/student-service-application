using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Model
{
    public class Professor
    {
        public int Id { get; set; } //broj licne karte
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Birthdate { get; set; }
        public int Adress { get; set; }
        public int PhoneNumber { get; set; }
        public string Email{ get; set; }
        public string Title { get; set; }
        public int YearS { get; set; } // godine staza

        public Professor(int id, string name, string surname, string birthdate, int adress, int phonenumber, string email, string title, int styear)
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
            

        }

        public Professor(string name, string surname, string birthdate, int adress, int phonenumber, string email, string title, int styear)
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

        public string[] ToCSV() //ucitava u fajl
        {
            string[] csvValues =
            {
            Id.ToString(),
            Name,
            Surname,
            Birthdate,
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
            Birthdate = values[3];
            Adress = int.Parse(values[4]);
            PhoneNumber = int.Parse(values[5]);
            Email = values[6];
            Title = values[7];
            YearS = int.Parse(values[8]);
        }




    }
}
