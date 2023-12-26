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

namespace GUI.DTO
{
    public class SubjectDTO: INotifyPropertyChanged
    {

        private CLI.DAO.HeadDao controller;

        public SubjectDTO(CLI.DAO.HeadDao cnt)
        {
            controller = cnt;
        }


        public SubjectDTO(Subject sb)
    {
            id = sb.Id;
            name = sb.Name;
            semester = sb.SemesterSb;
            year = sb.SYear;
            espb = sb.NumEspb;
            professorsId = sb.ProfessorSb.Id;

    }
    public SubjectDTO(SubjectDTO sb)
    {
            id = sb.Id;
            name = sb.Name;
            semester = sb.Semester;
            year = sb.Year;
            espb = sb.Espb;
            professorsId = sb.ProfessorsId;
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

        private String semesterS;
        public String SemesterS
        {
            get { return semester.ToString(); }
            set
            {
                if (value != semesterS)
                {
                    semesterS = value;
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
                if (semesterS == "Summer")
                {
                    semester = Subject.Semester.Summer;
                }
                else
                {
                    // Postavite drugu vrednost enuma, ako je potrebno
                    semester = Subject.Semester.Winter;
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

        private int professorsId;
        public int ProfessorsId
        {
            get { return professorsId; }
            set
            {
                if (value != professorsId)
                {
                    professorsId = value;
                    OnPropertyChanged();
                }
            }
        }

        private Professor professors;
        public Professor Professors
        {
            get { return professors; }
            set
            {
                if (controller.getProfessorById(professorsId) != professors)
                {
                    professors = controller.getProfessorById(professorsId);
                    OnPropertyChanged();
                }
            }
        }

        
        public String ProfessorString
        {
            get { return professors.Name + " " + professors.Surname; }
        }

        




        

    public Subject ToSubject()
    {
        return new Subject(name, semester, year, espb, professors);
    }



    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
}
