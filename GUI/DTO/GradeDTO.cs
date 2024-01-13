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
using System.Security.Cryptography.X509Certificates;

namespace GUI.DTO
{
    public class GradeDTO : INotifyPropertyChanged
    {
        public GradeDTO(Grade gr, Subject sb)
        {
            id = gr.Id;
            date = gr.date;
            grade = gr.grade;

            subject = sb;

            idSubject = sb.Id;
            espb = sb.NumEspb;
            subName = sb.Name;
            subYear = sb.SYear;
            subSemester = sb.SemesterSb.ToString();

            stId = gr.student.Id;
            sumEspb = sumEspb + sb.NumEspb;


        }

        public GradeDTO(GradeDTO gr)
        {
            id = gr.Id;
            date = gr.Date;
            grade = gr.Grade;

            idSubject = gr.IdSubject;
            espb = gr.Espb;
            subName = gr.SubName;
            subSemester = gr.SubSemester;
            subYear = gr.subYear;

            stId = gr.StId;
            sumEspb = gr.SumEspb;

        }

        private int subYear;
        public int SubYear
        {
            get { return subYear; }
            set
            {
                if (value != subYear)
                {
                    subYear = value;
                    OnPropertyChanged();
                }
            }
        }

        private string subSemester;
        public string SubSemester
        {
            get { return subSemester; }
            set
            {
                if (value != subSemester)
                {
                   subSemester = value;
                    OnPropertyChanged();
                }
            }
        }






        private int sumEspb = 0;
        public int SumEspb
        {
            get { return sumEspb; }
            set
            {
                if (value != sumEspb)
                {
                    sumEspb = value;
                    OnPropertyChanged();
                }
            }
        }


        private int stId;
        public int StId
        {
            get { return stId; }
            set
            {
                if (value != stId)
                {
                    stId = value;
                    OnPropertyChanged();
                }
            }
        }


        private int idSubject;
        public int IdSubject
        {
            get { return idSubject; }
            set
            {
                if (value != idSubject)
                {
                    idSubject = value;
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

        private string subName;
        public string SubName
        {
            get { return subName; }
            set
            {
                if (value != subName)
                {
                    subName = value;
                    OnPropertyChanged();
                }
            }
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

        private DateOnly date;

        public DateOnly Date
        {
            get { return date; }
            set
            {
                if (value != date)
                {
                    date = value;
                    OnPropertyChanged();
                }
            }
        }


        private int grade;

        public int Grade
        {
            get { return grade; }
            set
            {
                if (value != grade)
                {
                    grade = value;
                    OnPropertyChanged();
                }
            }
        }

        private Subject subject;

        public Subject Subject
        {
            get { return subject; }
            set
            {
                if (value != subject)
                {
                    subject = value;
                    OnPropertyChanged();
                }
            }
        }


        private Student student;

        public Student Student
        {
            get { return student; }
            set
            {
                if (value != student)
                {
                    student = value;
                    OnPropertyChanged();
                }
            }
        }


        private StudentDTO studentDTO;


        public StudentDTO StudentDTO
        {
            get { return studentDTO; }
            set
            {
                if (value != studentDTO)
                {
                    studentDTO = value;
                    OnPropertyChanged();
                }
            }
        }



        private SubjectDTO subjectDTO;

        public SubjectDTO SubjectDTO
        { 
            get { return subjectDTO; }
            set
            {
                if (value != subjectDTO)
                {
                    subjectDTO = value;
                    OnPropertyChanged();
                }
            }
        }



        public GradeDTO(StudentDTO st, SubjectDTO sb)
        {

            studentDTO = st;
            subjectDTO = sb;
        }
        public GradeDTO()
        {

            ;
        }
        public Grade ToGrade()
        {
            return new Grade(id,student, subject, grade, date);
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
