using CLI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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

