﻿using CLI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GUI.DTO
{
    public class AdressDTO : INotifyPropertyChanged
    {
        public AdressDTO(Adress adr)
        {
            street = adr.Street;
            streetNm = adr.StrNumber;
            city = adr.City;
            country = adr.Country;

        }

        public AdressDTO(AdressDTO adr)
        {
            street = adr.Street;
            streetNm = adr.StreetNm;
            city = adr.City;
            country = adr.Country;

        }

        public AdressDTO(String s, String n, String ci, String co)
        {
            this.street = s;
            this.streetNm = n;
            this.city = ci;
            this.country = co;
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

        private string street;
        public string Street
        {
            get
            { return street; }
            set
            {
                if (value != street)
                {
                    street = value;
                    OnPropertyChanged();
                }
            }
        }

 

        private string streetNm;
        public string StreetNm
        {
            get
            { return streetNm; }
            set
            {
                if (value != streetNm)
                {
                    streetNm = value;
                    OnPropertyChanged();
                }
            }
        }

       

        private string city;

        public string City
        {
            get { return city; }
            set
            {
                if (value != city)
                {
                    city = value;
                    OnPropertyChanged();
                }
            }
        }

        private string country;

        public string Country
        {
            get { return country; }
            set
            {
                if (value != country)
                {
                    country = value;
                    OnPropertyChanged();
                }
            }
        }
        public String ToString()
        {
            return street + " " + streetNm + " city and country: " + city + " " + country;
        }


        public AdressDTO() { }

        public Adress ToAdress()
        {
            return new Adress(street, streetNm, city, country);
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

       



         
    }
}

