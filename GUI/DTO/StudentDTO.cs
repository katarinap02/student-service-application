using System;
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
            adressDto = new AdressDTO(std.AdressSt);
            name = std.Name;
            surname = std.Surname;
            birthdate = std.Birthdate;
            studentYear = std.StYear;
            status = std.StudentStatus;
            phoneNumber = std.PhoneNumber;
            email=std.Email;
            subjectList = std.Subjects; //ovo su nepolozeni predmeti


            average = std.Average(std.Grades); //jos nema ocena

        }
        public StudentDTO(StudentDTO std)
        {
            id = std.Id;
            indexDto = std.IndexDto;
            adressDto = std.AdressDto;
            name = std.Name;
            surname = std.Surname;
            studentYear = std.StYear;
            status = std.StudentStatus;
            birthdate=std.Birthdate;
            phoneNumber = std.PhoneNumber;
            email = std.Email;
           // subjectList = std.SubjectList;
            average = std.Average; //jos nema ocena

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
        

        public Student.Status StudentStatus
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

        private List<Subject> subjectList = new List<Subject>();

        public List<Subject> SubjectList
        {
            get { return subjectList; }
            set
            {
                if (!EqualityComparer<List<Subject>>.Default.Equals(value, subjectList))
                {
                    subjectList = value;
                    OnPropertyChanged();
                }
            }
        }

        public StudentDTO()
        {

        }
        public string Error => null;

        private Regex _NameRegex = new Regex("[A-Za-z0-9-]+ [A-Za-z0-9-]+");
        private Regex _PhoneNumberRegex = new Regex("^06\\d{8}$");
        private Regex _NumberRegex = new Regex("^[0-9]+$");
        public string this[string columnName]
        {
            get
            {
                if (columnName == "Name")
                {
                    if (string.IsNullOrEmpty(Name))
                        return "Name is required";

                }
                else if (columnName == "Surname")
                {
                    if (string.IsNullOrEmpty(Surname))
                        return "Surname is required";

                }
                else if (columnName == "PhoneNumber")
                {
                    if (string.IsNullOrEmpty(PhoneNumber))
                        return "Phonenumber is required";

                    Match match = _PhoneNumberRegex.Match(PhoneNumber);
                    if (!match.Success)
                    return "Phonenumber must start with 06 and have 10 digits";

                }
                else if (columnName == "Email")
                {
                    if (string.IsNullOrEmpty(Email))
                        return "Email is required";

                }
                else if (columnName == "StYear")
                {
                    Match match = _NumberRegex.Match(StYear.ToString());
                    if (!match.Success)
                        return "Student Year must be a number";
                }
                return null;
            }
        }//cao Kaca : )

        private readonly string[] _validatedProperties = { "Surname", "Name", "PhoneNumber", "Email", "StYear" };

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





        public Student ToStudent()
        { 
            return new Student( id, name, surname, birthdate, adressDto.ToAdress(), phoneNumber, email, indexDto.ToIndex(), studentYear, status);
        }


       


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
