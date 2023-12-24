using CLI.DAO;
using CLI.Model;
using GUI.DTO;
using GUI.View;
using GUI.View.Add;
using GUI.View.Insert;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<ProfessorDTO> professorDtos;
        List<StudentDTO> studentDtos;
        private HeadDao headDao;
        private DispatcherTimer timer;
        public MainWindow()
        {
                
            InitializeComponent();
            headDao = new HeadDao();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
           // timer.Tick += UpdateDateTime;
            timer.Start();

            makeStudentList();
            makeProfessorList();
        }

       /* private void UpdateDateTime(object sender, EventArgs e)
        /*{
            dateTimeTextBlock.Text = DateTime.Now.ToString("HH:mm yyyy-MM-dd");
        }*/

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public void makeStudentList()
        { 
            studentDtos = new List<StudentDTO>();
            foreach (Student std in headDao.GetAllStudentsHead())
            { 
                studentDtos.Add(new StudentDTO(std));
            }
            dataGridStudent.ItemsSource = studentDtos;
        }
        
        public void makeProfessorList()
        {
            professorDtos = new List<ProfessorDTO>();
            foreach (Professor prof in headDao.GetAllProfessorsHead())
            {
                professorDtos.Add(new ProfessorDTO(prof));

            }
            dataGridProfessor.ItemsSource= professorDtos; 
        }


        private void Add_Click(object sender, RoutedEventArgs e)
        {
            TabItem currentTab = tabControl.SelectedItem as TabItem;  //  kastujem u objekat tipa TabItem 

            if (currentTab != null)
            {
                if (currentTab.Header.Equals("Student"))
                {
                    AddStudent addStudent = new AddStudent(headDao);
                    addStudent.Show();

                }
               
                else if(currentTab.Header.Equals("Professor"))
                {
                    //MessageBox.Show("Selected tab: " + (tabControl.SelectedItem as TabItem).Header);
                    AddProfessor addProfessor = new AddProfessor(headDao);
                    addProfessor.Show();
                    
                }
                else
                {
                    AddSubject addSubject = new AddSubject();
                    addSubject.Show();
                }    
            }
            
            
            
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            TabItem currentTab = tabControl.SelectedItem as TabItem;  //  kastujem u objekat tipa TabItem 

            if (currentTab != null)
            {
                if (currentTab.Header.Equals("Student"))
                {
                   InsertStudent insertStudent = new InsertStudent();
                   insertStudent.Show();

                }

                else if (currentTab.Header.Equals("Professor"))
                {
                   InsertProfessor insertProfessor= new InsertProfessor();
                   insertProfessor.Show();

                }
                else
                {
                   InsertSubject insertSubject = new InsertSubject();   
                   insertSubject.Show();
                }
            }
        }




        /*  private void ShowStudentsGrid(object sender, RoutedEventArgs e)
          { StudentDTO studentDTO = dataGridStudent.SelectedItem as StudentDTO;
                        DeleteStudent deleteStudent = new DeleteStudent(headDao, studentDTO);
                        deleteStudent.Show();
              //studentGrid.Visibility = ((ToggleButton)sender).IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
          }*/

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            TabItem selectedTab = tabControl.SelectedItem as TabItem;

            if (selectedTab != null)
            {
                switch (selectedTab.Header)
                {
                    case "Student":
                        StudentDTO studentDTO = dataGridStudent.SelectedItem as StudentDTO;
                        DeleteStudent deleteStudent = new DeleteStudent(headDao, studentDTO);
                        deleteStudent.Show();
                        break;
                    case "Professors":
                        //  deleteProfessor();
                        break;
                    case "Subjects":
                        // deleteSubject();
                        break;
                }

            }
             

        }
        
       
    }
}
