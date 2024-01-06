using CLI.DAO;
using CLI.Model;
using GUI.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI.View.Insert
{
    /// <summary>
    /// Interaction logic for AddProfessorToSubject.xaml
    /// </summary>
    public partial class AddProfessorToSubject : Window
    {
        HeadDao headDao;
        ProfessorDTO professorDTO;
        SubjectDTO subjectDTO;
        public ObservableCollection<SubjectDTO> Subjects { get; set; }
        public AddProfessorToSubject(HeadDao cnt, ProfessorDTO pfDTO)
        {
            InitializeComponent();
            professorDTO = new ProfessorDTO(pfDTO);
            subjectDTO = new SubjectDTO();
            headDao = cnt;
            Subjects = new ObservableCollection<SubjectDTO>();

            dataGridProfessorSubject.ItemsSource = Subjects;

            UpdateProfessorSubject(pfDTO.ToProfessor());

        }

        public void UpdateProfessorSubject(Professor pf)
        {
            Subjects.Clear();
            foreach (Subject subject in headDao.getSubjectsWithoutProfessor(pf)) Subjects.Add(new SubjectDTO(subject));
        }
    }
}
