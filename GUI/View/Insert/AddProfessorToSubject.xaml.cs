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
        ObservableCollection<SubjectDTO> profSubs;
        public AddProfessorToSubject(HeadDao cnt, ProfessorDTO pfDTO, ObservableCollection<SubjectDTO> failedSubs)
        {
            InitializeComponent();
            professorDTO = new ProfessorDTO(pfDTO);
            subjectDTO = new SubjectDTO();
            headDao = cnt;
            Subjects = new ObservableCollection<SubjectDTO>();
            profSubs = failedSubs;

            dataGridProfessorSubject.ItemsSource = Subjects;

            UpdateProfessorSubject(pfDTO.ToProfessor());

        }

        public void UpdateProfessorSubject(Professor pf)
        {
            Subjects.Clear();
            foreach (Subject subject in headDao.getSubjectsWithoutProfessor(pf)) Subjects.Add(new SubjectDTO(subject));
        }

        private void Button_ClickAddProfessorToSubject(object sender, RoutedEventArgs e)
        {
            SubjectDTO subjectDTO = dataGridProfessorSubject.SelectedItem as SubjectDTO;
            if (subjectDTO != null)
            {
                profSubs.Add(subjectDTO); //dodajem na listu sa profesorom
                Subjects.Remove(subjectDTO); // uklanjam sa liste predmeta koje ne pohadja ili nije polozio
                headDao.AddProfessorToSubject(subjectDTO.ToSubject(), professorDTO.ToProfessor());
                Close();
            }
            else
            {
                MessageBox.Show("You didnt select subject to add!");
            }


        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Subject not added to student!");
            Close();
        }


    }
}
