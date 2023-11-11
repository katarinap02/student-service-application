using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CLI.Model
{
    public class Index
    {
        public string Course { get; set; }
        public int Number { get; set; }
        public int YearE { get; set; } //godina upisa

        public Index() { }

        public Index(string c, int n, int y) 
        {
            Course = c;
            Number = n;
            YearE = y;

        }

        public string[] ToCSV() //ucitava u fajl
        {
            string[] csvValues =
            {
            Course,
            Number.ToString(),
            YearE.ToString()
        };
            return csvValues;
        }

        public void FromCSV(string[] values) //cita iz fajla
        {
            Course = values[0];
            Number = int.Parse(values[1]);
            YearE = int.Parse(values[2]);

        }

    }
}
