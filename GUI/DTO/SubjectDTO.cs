using CLI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;
using static CLI.Model.Subject;
using System.Windows.Media.Animation;
using CLI.DAO;
using System.Net;
using System.Text.RegularExpressions;
using System.Diagnostics.Eventing.Reader;

namespace GUI.DTO
{
    public class SubjectDTO: INotifyPropertyChanged
    {

        public SubjectDTO(Subject sb)
    {
            id = sb.Id;
            name = sb.Name;
            semester = sb.SemesterSb;
            year = sb.SYear;
            espb = sb.NumEspb;
            code = sb.Code;
            professorId = sb.idProf;
            Professor professor = new Professor();
            ProfessorDao professorDao = new ProfessorDao();
            professor = professorDao.GetProfessorById(professorId);
            if (professorId != -1)
            {
                professorName = professor.Name +" "+ professor.Surname; //dodala sam da uzme i prezime
            }
            else
            {
                professorName = "";
            }
            
            //professor = new ProfessorDTO(sb.ProfessorSb);

    }
        public SubjectDTO(Grade gr)
        {
            id = gr.subject.Id;
            name = gr.subject.Name;
            semester = gr.subject.SemesterSb;
            year = gr.subject.SYear;
            espb = gr.subject.NumEspb;
            code = gr.subject.Code;
            professorId = gr.subject.idProf;

            //professor = new ProfessorDTO(sb.ProfessorSb);

        }
        public SubjectDTO(SubjectDTO sb)
    {
            id = sb.Id;
            name = sb.Name;
            semester = sb.Semester;
            year = sb.Year;
            espb = sb.Espb;
            code = sb.Code;
            professorId = sb.ProfessorId;
            professorName=sb.ProfessorName;
            //professor = sb.Professor;
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

        private string code;
        public string Code
        {
            get
            {
                return code;
            }
            set
            {
                if (value != code)
                {
                    code = value;
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

        private string semesterS;
        public string SemesterS
        {
            get { return semesterS; }
            set
            {
                if (value != semesterS)
                {
                    semesterS = value;
                    OnPropertyChanged();
                }
            }
        }



        private int professorId;
        public int ProfessorId
        {
            get { return professorId; }
            set
            {
                if (value != professorId)
                {
                    professorId = value;
                    OnPropertyChanged();
                }
            }
        }

        private string professorName;
        public string ProfessorName
        {
            get { return professorName; }
            set
            {
                if (value != professorName)
                {
                    professorName = value;
                    OnPropertyChanged();
                }
            }
        }





        private CLI.Model.Subject.Semester semester;


        public CLI.Model.Subject.Semester Semester
        {
            get { return semester; }
            set
            {
                if (value != semester)
                {
                    semester= value;
                    OnPropertyChanged();
                }
            }
        }

        private int espb;
        public int Espb
        {
            get { return espb; }
            set
            {
                if (value != espb)
                {
                    espb = value;
                    OnPropertyChanged();
                }
            }
        }

       

        public string Error => null;
        private Regex _NumberRegex = new Regex("^[0-9]+$");
        private Regex _SeasonRegex = new Regex("^(Winter|Summer)$");

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Name")
                {
                    if (string.IsNullOrEmpty(Name))
                        return "Course is required";

                }
                else if (columnName == "Year")
                {
                    Match match = _NumberRegex.Match(Year.ToString());
                    if (!match.Success)
                        return "Year must be a number";
                }
                else if (columnName == "Espb")
                {
                    Match match = _NumberRegex.Match(Espb.ToString());
                    if (!match.Success)
                        return "Espb must be a number";
                }
               /* else if (columnName == "SemesterS")
                {
                    Match match = _SeasonRegex.Match(SemesterS);
                    if (!match.Success)
                        return "Semester can be only Winter or Summer";

                }*/
                return null;
            }
        }

        private readonly string[] _validatedProperties = { "Name", "Year", "Espb", "SemesterS" };

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




        public SubjectDTO()
        {

        }




        public Subject ToSubject()
    {
            
        return new Subject(id,name, semester, year, espb, code);
    }



    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
}
