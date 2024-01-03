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

            idSubject = sb.Id;
            espb = sb.NumEspb;
            subName = sb.Name;

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

            stId = gr.StId;
            sumEspb = gr.SumEspb;

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


        public GradeDTO()
        { }




        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
