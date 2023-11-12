using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Storage.Serialization;

namespace CLI.Model
{
    public class Adress : ISerializable
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public int StrNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public Adress() { }


        public Adress(int i, string s, int n, string c, string co)
        {
            Id = i;
            Street = s;
            StrNumber = n;
            City = c;
            Country = co;

        }
        public Adress(string s, int n, string c, string co)
        {
            Street = s;
            StrNumber = n;
            City = c;
            Country = co;

        }

        public override string ToString()
        {
            return $"{Id} {Street} {StrNumber} {City} {Country}";
        }

        public string[] ToCSV() //ucitava u fajl
        {
            string[] csvValues =
            {
            Id.ToString(),
            Street,
            StrNumber.ToString(),
            City,
            Country
        };
            return csvValues;
        }

        public void FromCSV(string[] values) //cita iz fajla
        {
            Id = int.Parse(values[0]);  
            Street = values[1];
            StrNumber = int.Parse(values[2]);
            City = values[3];
            Country = values[4];
        }

    }
   
}
