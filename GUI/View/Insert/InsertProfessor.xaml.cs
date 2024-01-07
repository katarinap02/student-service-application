using CLI.DAO;
using CLI.Model;
using GUI.DTO;
using GUI.View.Insert;
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

namespace GUI
{
    /// <summary>
    /// Interaction logic for InsertProfessor.xaml
    /// </summary>
    public partial class InsertProfessor : Window
    {
        ProfessorDTO professorDTO;
        AdressDTO adressDTO;
        HeadDao headDao;
        SubjectDTO subjectDTO;

        public static RoutedCommand NewCommand = new RoutedCommand();
        public ObservableCollection<SubjectDTO> Subjects { get; set; }

        public InsertProfessor(HeadDao contr, ProfessorDTO prof)
        {
            InitializeComponent();

            headDao = contr;

           
            professorDTO = new ProfessorDTO(prof);
            subjectDTO = new SubjectDTO();
            DataContext = professorDTO;

            Subjects = new ObservableCollection<SubjectDTO>();

            dataGridSubjects.ItemsSource = Subjects;

            UpdateSubject(prof.ToProfessor());

        }

        public void UpdateSubject(Professor prof)
        {
            Subjects.Clear();
            foreach (Subject subject in headDao.getSubjectsForProfessor(prof)) Subjects.Add(new SubjectDTO(subject));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (professorDTO.IsValid)
            {
                headDao.UpdateProfessorHead(professorDTO.ToProfessor());
                Close();
                MessageBox.Show("Professor updated!");
            }
            else
            {
                MessageBox.Show("Professor can not be created. Not all fields are valid.");
            }
        }

        private void AddProfessor_Click(object sender, RoutedEventArgs e)
        {
            AddProfessorToSubject addProfessorToSubject = new AddProfessorToSubject(headDao, professorDTO, Subjects);
            addProfessorToSubject.ShowDialog();
        }


        private void RemoveProfessor_Click(object sender, RoutedEventArgs e)
        {
            SubjectDTO subjectDto = dataGridSubjects.SelectedItem as SubjectDTO;
            DeleteProfessorFromSubject deleteProfessorFromSubject = new DeleteProfessorFromSubject(headDao, subjectDto, professorDTO);
            deleteProfessorFromSubject.ShowDialog();
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            Close();
            MessageBox.Show("Professor is not updated!");
        }
    }
}
