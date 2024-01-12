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
    /// Interaction logic for AddProfessorInSubject.xaml
    /// </summary>
    public partial class AddProfessorInSubject : Window
    {
        HeadDao headDao;
       // ProfessorDTO professorDTO;
        SubjectDTO subjectDTO;
        public ObservableCollection<ProfessorDTO> Professors { get; set; }
        public AddProfessorInSubject(HeadDao cnt, SubjectDTO subjDTO)
        {
            InitializeComponent();

            headDao = cnt;
            subjectDTO = new SubjectDTO(subjDTO);
            Professors = new ObservableCollection<ProfessorDTO>();
            dataGridProfessorSubject.ItemsSource = Professors;

            UpdateProfessorSubject();
        }
        public void UpdateProfessorSubject()
        {
            Professors.Clear();
            foreach (Professor professor in headDao.GetAllProfessorsHead()) Professors.Add(new ProfessorDTO(professor));
        }

        private void Button_ClickAddProfessor(object sender, RoutedEventArgs e)
        {
            ProfessorDTO professorDTO = dataGridProfessorSubject.SelectedItem as ProfessorDTO;
            if (professorDTO != null)
            {
               
                headDao.AddProfessorToSubject(subjectDTO.ToSubject(), professorDTO.ToProfessor());
                SubjectDTO subjectDTOPRACENJE = subjectDTO;
               // subjectDTO.ProfessorId = professorDTO.Id;

                TextBox textBoxProfessor = (TextBox)Application.Current.MainWindow.FindName("textBoxProfessor");

                // Force update on the binding  POSLJEDNJI POKUSAJ DA PRORADI AUTOMATSKI EDIT, ALI OPET NE RADI
                var bindingExpression = textBoxProfessor?.GetBindingExpression(TextBox.TextProperty);
                bindingExpression?.UpdateTarget();

               /* TextBox buttonAddProfessor = (TextBox)Application.Current.MainWindow.FindName("buttonAddProfessor");
                buttonAddProfessor.IsEnabled = false;*/

                Close();
            }

        }

        private void Button_ClickRemoveProfessor(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Professor is not added!");
        }
    }
}
