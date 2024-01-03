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
    /// Interaction logic for InsertStudent.xaml
    /// </summary>
    public partial class InsertStudent : Window
    {
        StudentDTO studentDTO;
        HeadDao headDao;
        
       // GradeDTO gradeDTO;
        public static RoutedCommand NewCommand = new RoutedCommand();
        public ObservableCollection<GradeDTO> Grades { get; set; }
        public InsertStudent(HeadDao contr, StudentDTO std)
        {
            InitializeComponent();
          //  CommandBindings.Add(new CommandBinding(NewCommand, Add_Click));
            headDao = contr;
            
            studentDTO = new StudentDTO(std);
            DataContext= studentDTO;
            Grades = new ObservableCollection<GradeDTO>();
            //headDao.observerSub.Subscribe(this);

            dataGridPassed.ItemsSource = Grades;


            UpdateGrade(std.ToStudent());
            

        }

        public void UpdateGrade(Student std)
        {
            Grades.Clear();
            foreach (Grade grade in headDao.getGradesForStudent(std)) Grades.Add(new GradeDTO(grade, grade.subject));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            

            if (studentDTO.IsValid)
            {
                    headDao.UpdateStudentHead(studentDTO.ToStudent());
                    Close();
                    MessageBox.Show("Student updated!");
                }
            else
            {
                MessageBox.Show("Student can not be created. Not all fields are valid.");
            }
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Student is not updated!");
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {


            GradeDTO gradeDTO = dataGridPassed.SelectedItem as GradeDTO; //obrisemo ocenu
            if (gradeDTO != null)
            {
                 CancelGrade cancelGrade = new CancelGrade(headDao, gradeDTO);
                  cancelGrade.ShowDialog();
            }
            else
            {
                MessageBox.Show("You didnt select grade to cancel!");
            }






        }
        }
}
