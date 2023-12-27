using CLI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;
using Index = CLI.Model.Index;

namespace GUI.DTO
{
    public class IndexDTO : INotifyPropertyChanged
    {

        public IndexDTO(Index ind)
        {
            course = ind.Course;
            number = ind.Number;
            year = ind.YearE;


        }

        public IndexDTO(IndexDTO ind)
        {
            course = ind.Course;
            number = ind.Number;
            year = ind.Year;


        }

        public IndexDTO(String c, int n, int y)
        {
            this.course = c;
            this.number = n;
            this.year = y;
        }

        private string course;
        public string Course
        {
            get
            { return course; }
            set
            {
                if (value != course)
                {
                    course = value;
                    OnPropertyChanged();
                }
            }
        }

        private int year;

        public int Year
        {
            get { return year; }
            set
            {
                if (value != year)
                {
                    year = value;
                    OnPropertyChanged();
                }
            }
        }

        private int number;

        public int Number
        {
            get { return number; }
            set
            {
                if (value != number)
                {
                    number = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Error => null;
        private Regex _NumberRegex = new Regex("^[0-9]+$");

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Course")
                {
                    if (string.IsNullOrEmpty(Course))
                        return "Course is required";

                }
                else if (columnName == "Year")
                {
                    Match match = _NumberRegex.Match(Year.ToString());
                    if (!match.Success)
                        return "Year must be a number";
                }
                else if (columnName == "Number")
                {
                    Match match = _NumberRegex.Match(Number.ToString());
                    if (!match.Success)
                        return "Year must be a number";
                }
                return null;
            }
        }

        private readonly string[] _validatedProperties = { "Course", "Year", "Number"};

        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }





        public IndexDTO() { }

        public Index ToIndex()
        {
            return new Index(course, number, year);
        }

        public String ToString()
        {
            return course + "" + number + "-" + year;
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }




    }
}

