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

namespace GUI.DTO
{
    public class GradeDTO : INotifyPropertyChanged
    {
        public GradeDTO(Grade gr)
        {
            id = gr.Id;
          //  studentDTO = new StudentDTO(gr.student);
            subjectDTO = new SubjectDTO(gr.subject);
            date = gr.date;
            grade = gr.grade;

        }

       // private StudentDTO studentDTO;
        private SubjectDTO subjectDTO;

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

        public DateOnly Birthdate
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

      //  public StudentDTO StudentDto
      //  {
      //      get { return studentDTO; }
       //     set
       //     {
        //        if (value != studentDTO)
         //       {
          //          studentDTO = value;
          //          OnPropertyChanged();
              //  }
          //  }
      //  }

        public SubjectDTO SubjectDto
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




        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
