﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using CLI.Model;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace GUI.DTO
{
    public class StudentDTO : INotifyPropertyChanged
    {

        public StudentDTO(Student std)
        {
            id = std.Id;
            indexDto = new IndexDTO(std.IndexNm);
            name = std.Name;
            surname = std.Surname;
            studentYear = std.StYear;
            status = std.StudentStatus;
            average = std.Average(std.Grades); //jos nema ocena

        }

        private IndexDTO indexDto;
        private AdressDTO adressDto;

        public StudentDTO(AdressDTO adr, IndexDTO ind)
        {
            adressDto = adr;
            indexDto = ind;
        }

        public int id { get; set; }

        public int Id
        {
            get { return id; }
            set
            {
                if (value != id)
                {
                    id = value;
                    OnPropertyChanged();
                }
            }
        }

        private string name;
        public string Name
        {
            get
            { return name; }
            set
            {
                if (value != name)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }




        private String statusS;
        public String StatusS
        {
            get { return status.ToString(); }
            set
            {
                if (value != statusS)
                {
                    statusS = value;
                    OnPropertyChanged();
                }
            }
        }



        private CLI.Model.Student.Status status;
        

        public CLI.Model.Student.Status StudentStatus
        {
            get { return status; }
            set
            {
                if (statusS == "S")
                {
                    status = Student.Status.S;
                }
                else
                {
                    // Postavite drugu vrednost enuma, ako je potrebno
                    status = Student.Status.B;
                }
            }
        }

        private double average;
        public double Average
        {
            get { return average; }
            set
            {
                if (value != average)
                {
                    average = value;
                    OnPropertyChanged();
                }
            }
        }

        private string surname;
        public string Surname
        {
            get
            { return surname; }
            set
            {
                if (value != surname)
                {
                    surname = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime birthdate;

        public DateTime Birthdate
        {
            get { return birthdate; }
            set
            {
                if (value != birthdate)
                {
                    birthdate = value;
                    OnPropertyChanged();
                }
            }
        }
       

        public AdressDTO AdressDto
        {
            get { return adressDto; }
            set
            {
                if (value != adressDto)
                {
                    adressDto = value;
                    OnPropertyChanged();
                }
            }
        }

        

        

        public IndexDTO IndexDto
        {
            get { return indexDto; }
            set
            {
                if (value != indexDto)
                {
                    indexDto = value;
                    OnPropertyChanged();
                }
            }
        }

        public String IndexS
        {
            get { return indexDto.ToString(); }
        }



    private string phoneNumber;

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                if (value != phoneNumber)
                {
                    phoneNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set
            {
                if (value != email)
                {
                    email = value;
                    OnPropertyChanged();
                }
            }
        }

        private int studentYear;

        public int StYear
        {
            get { return studentYear; }
            set
            {
                if (value != studentYear)
                {
                    studentYear = value;
                    OnPropertyChanged();
                }
            }
        }
        /*
        public string Error => null;

        private Regex _indexRegex = new Regex(@"([a-zA-Z]{2,3})(\\d{2,3})-(\\d{4})\"); //regex za indeks
        private Regex _adressRegex = new Regex(@"(\d+\s[a-zA-Z\s]+),\s(\d+[a-zA-Z]?),\s([a-zA-Z]+),\s([a-zA-Z]+)"); // regex za adresu
        string adrReg = @"(\d+\s[a-zA-Z\s]+),\s(\d+[a-zA-Z]?),\s([a-zA-Z]+),\s([a-zA-Z]+)";
        private Regex _NameRegex = new Regex("[A-Za-z0-9-]+ [A-Za-z0-9-]+");

        Match matchAdress = _adressRegex.Match(Adress);

        if(Regex.)
            {
                
            }

    public string this[string columnName]
    {
        get
        {
            if (columnName == "Name")
            {
                if (string.IsNullOrEmpty(Name))
                    return "Name is required";

                Match match = _NameRegex.Match(Name);
                if (!match.Success)
                    return "Format not good. Try again.";

            }
            else if (columnName == "NumberOfWheels")
            {
                if (NumberOfWheels <= 0)
                    return "Number of wheels must be a positive value";
            }
            return null;
        }
    }

   
    */



        

        public Student ToStudent()
        { 
            return new Student( name, surname, DateOnly.FromDateTime(birthdate), adressDto.ToAdress(), phoneNumber, email, indexDto.ToIndex(), studentYear, status);
        }


       


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
