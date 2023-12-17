using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using CLI.Model;
using System.IO;

namespace GUI.DTO
{
    class StudentDTO : INotifyPropertyChanged
    {
        public int Id { get; set; }

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

        private CLI.Model.Student.Status status;

        public CLI.Model.Student.Status StudentStatus
        {
            get { return status; }
            set
            {
                if (value != status)
                {
                    status = value;
                    OnPropertyChanged();
                }
            }
        }

        private int average;
        public int Average
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

        private DateOnly birthdate;

        public DateOnly Birthdate
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
        private Adress adress;

        public Adress AdressSt
        {
            get { return adress; }
            set
            {
                if (value != adress)
                {
                    adress = value;
                    OnPropertyChanged();
                }
            }
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

        private CLI.Model.Index index;

        public CLI.Model.Index IndexNm
        {
            get { return index; }
            set
            {

                index = value;
                OnPropertyChanged();

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

        public StudentDTO() { }

        public Student ToStudent()
        { 
            return new Student(name, surname, birthdate, adress, phoneNumber, email, index, studentYear, status);
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
