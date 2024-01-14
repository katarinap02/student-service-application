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
        public AddChefToChair(HeadDao cnt, ChairDTO ch)
        {
            InitializeComponent();
            headDao = cnt;
            chairDTO = new ChairDTO(ch);
            Professors = new ObservableCollection<ProfessorDTO>();
            dataGridProfessorChair.ItemsSource = Professors;

            UpdateProfessorChair();
        }
        public void UpdateProfessorChair()
        {
            Professors.Clear();
            foreach (Professor professor in headDao.GetAllChairChef(chairDTO.Id)) Professors.Add(new ProfessorDTO(professor));
        }

        private void Button_ClickAddProfessor(object sender, RoutedEventArgs e)
        {
            ProfessorDTO professorDTO = dataGridProfessorChair.SelectedItem as ProfessorDTO;
            if (professorDTO != null)
            {

                headDao.AddProfessorToChair(chairDTO.Id, professorDTO.ToProfessor());
                
                

                Close();
            }

        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
