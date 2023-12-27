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
            //professor = new ProfessorDTO(sb.ProfessorSb);

    }
    public SubjectDTO(SubjectDTO sb)
    {
            id = sb.Id;
            name = sb.Name;
            semester = sb.Semester;
            year = sb.Year;
            espb = sb.Espb;
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
                if (value != semester)
                {
                    semester = value;
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

       private ProfessorDTO professor;
       public ProfessorDTO Professor
       {
          get { return professor; }
            set
            {
                if (value != professor)
                {
                    professor = value;
                    OnPropertyChanged();
                }
            }
        }






        public SubjectDTO()
        {

        }




        public Subject ToSubject()
    {
        return new Subject(name, semester, year, espb);
    }



    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
}
