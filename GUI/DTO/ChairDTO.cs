using CLI.DAO;
using CLI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GUI.DTO
{
    public class ChairDTO: INotifyPropertyChanged
    {

        public ChairDTO(Chair ch)
        {

            id = ch.Id;
            name = ch.CName;
            professorId = ch.IdChef;
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

        }

        public ChairDTO(ChairDTO ch)
        {
            id = ch.Id;
            name = ch.Name;
            professorId = ch.ProfessorId;
            professorName = ch.ProfessorName;

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

        public ChairDTO() { }





        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
