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

namespace GUI.View.Show
{
    /// <summary>
    /// Interaction logic for AddChefToChair.xaml
    /// </summary>
    public partial class AddChefToChair : Window
    {

        HeadDao headDao;
        
        ChairDTO chairDTO;
        public ObservableCollection<ProfessorDTO> Professors { get; set; }

        public ObservableCollection<SubjectDTO> Subjects { get; set; }
        public AddChefToChair(HeadDao cnt, ChairDTO ch)
        {
            InitializeComponent();
            headDao = cnt;
            chairDTO = new ChairDTO(ch);
            Professors = new ObservableCollection<ProfessorDTO>();
            Subjects = new ObservableCollection<SubjectDTO>();

            dataGridSubjects.ItemsSource = Subjects;
            dataGridProfessorChair.ItemsSource = Professors;

            UpdateProfessorChair();
            UpdateSubject(chairDTO);
        }
        public void UpdateProfessorChair()
        {
            Professors.Clear();
            foreach (Professor professor in headDao.GetAllChairChef(chairDTO.Id)) Professors.Add(new ProfessorDTO(professor));
        }

        public void UpdateSubject(ChairDTO chairDTO)
        {
            Subjects.Clear();
            foreach (Subject subject in headDao.GetAllSubjectsForChair(chairDTO.Id)) Subjects.Add(new SubjectDTO(subject));
        }

        private void Button_ClickAddProfessor(object sender, RoutedEventArgs e)
        {
            ProfessorDTO professorDTO = dataGridProfessorChair.SelectedItem as ProfessorDTO;
            if (professorDTO != null)
            {
                
                Boolean b = headDao.AddProfessorToChair(chairDTO.Id, professorDTO.ToProfessor());
                if (!b)
                {
                    MessageBox.Show("Professor can't be Chef for this Chair!");
                }
                
                

                Close();
            }

        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {

            string searchTerm = textboxSearch.Text.ToLower();
            string[] resultArray = searchTerm.Split(',').Select(s => s.Trim()).ToArray(); //trimujem, izbacujem whitespaces
                                                                                          //treba dodati switch case da mogu da pretrazujem na razlicitim tabovima

            if (resultArray.Length > 0)
            {
                if (resultArray.Length > 2)
                {
                    MessageBox.Show("You input more than two words!");
                }
                else
                {
                    if (resultArray.Length == 1)
                    {
                        var filtered = Subjects.Where(subject => subject.Name.ToLower().Contains(resultArray[0])).ToList();
                        dataGridSubjects.ItemsSource = filtered;
                    }
                    else if (resultArray.Length == 2)
                    {
                        var filtered = Subjects.Where(subject =>
                        subject.Name.ToLower().Contains(resultArray[0]) &&
                        subject.Code.ToLower().Contains(resultArray[1])).ToList();
                        dataGridSubjects.ItemsSource = filtered;
                    }

                }

            }
            else
            {
                MessageBox.Show("Please input one or two words to search!");
            }
            
        }
    }
}
