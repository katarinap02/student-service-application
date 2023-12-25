﻿using CLI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace GUI.DTO
{
    public  class ProfessorDTO: INotifyPropertyChanged
    {
        //FALI NAM BROJ LICNE KARTE!    
        
        public ProfessorDTO(Professor prof)
        {
            id = prof.Id;
            name = prof.Name;
            surname = prof.Surname;
            birthdate = prof.Birthdate;
            phoneNumber = prof.PhoneNumber; 
            email = prof.Email; 
            title = prof.Title;
            year = prof.YearS;
        }
        public ProfessorDTO(ProfessorDTO prof)
        {
            id = prof.id;
            name = prof.name;
            surname = prof.surname;
            birthdate = prof.birthdate;
            phoneNumber = prof.phoneNumber;
            email = prof.email;
            title = prof.title;
            year = prof.year;
        }


        private int id;
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
            {
                return name;
            }
            set
            {
                if (value != name)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }


        private string surname;
        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                if (value != surname)
                {
                    surname = value;
                    OnPropertyChanged();
                }
            }
        }


        private DateOnly birthdate;

        public DateOnly Birthdate
        {
            get 
            { 
                return birthdate;
            }
            set
            {
                if (value != birthdate)
                {
                    birthdate = value;
                    OnPropertyChanged();
                }
            }
        }


        private string title;

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                if (value != title)
                {
                    title = value;
                    OnPropertyChanged();
                }
            }
        }

        private string phoneNumber;

        public string Phonenumber
        {
            get
            {
                return phoneNumber;
            }
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
            get
            {
                return email;
            }
            set
            {
                if (value != email)
                {
                    email = value;
                    OnPropertyChanged();
                }
            }
        }

        private int year;

        public int Year
        {
            get
            {
                return year;
            }
            set
            {
                if (value != year)
                {
                    year = value;
                    OnPropertyChanged();
                }
            }
        }

        private AdressDTO adressDto;

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
        // public Professor(string name, string surname, DateOnly birthdate, Adress adress, string phonenumber, string email,
        // string title, int styear)
       

        public ProfessorDTO(AdressDTO adress)
        {
            adressDto = adress;
        }

        public Professor ToProfessor()
        {
            return new Professor(name, surname, birthdate, adressDto.ToAdress(), phoneNumber, email, title, year);
        }



        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
