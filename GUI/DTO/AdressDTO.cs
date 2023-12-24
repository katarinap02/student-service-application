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
    internal class AdressDTO
    {
        public AdressDTO(Adress adr)
        {
            street = adr.Street;
            streetNm = adr.StrNumber;
            city = adr.City;
            country = adr.Country;

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
}
