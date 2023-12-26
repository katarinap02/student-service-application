using CLI.DAO;
using CLI.Model;
using CLI.Observer;
using GUI.DTO;
using GUI.View;
using GUI.View.Add;
using GUI.View.Insert;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Collections.ObjectModel;

namespace GUI 
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IObserver
    {

        public ObservableCollection<StudentDTO> Students { get; }
        public ObservableCollection<ProfessorDTO> Professors { get; }
        public ObservableCollection<SubjectDTO> Subjects { get; set; }

        List<ProfessorDTO> professorDtos;
        List<StudentDTO> studentDtos;
        List<SubjectDTO> subjectDtos;
        private HeadDao headDao;
        private DispatcherTimer timer;
        public MainWindow()
        {
                
            InitializeComponent();
            SetWindowSize();
            Students = new ObservableCollection<StudentDTO>();
            Professors = new ObservableCollection<ProfessorDTO>();
            Subjects = new ObservableCollection<SubjectDTO>();
            headDao = new HeadDao();
            headDao.observerSub.Subscribe(this);
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += UpdateDateTime;
            timer.Start();

            dataGridProfessor.ItemsSource = Professors;
            dataGridStudent.ItemsSource = Students;
            dataGridSubject.ItemsSource = Subjects;
           
            
            Update();
         
        }
        public void Update()
        {
            UpdateProfessor();
            UpdateStudent();
            UpdateSubject();
        }

        private void SetWindowSize()
        {
            double screenW = SystemParameters.PrimaryScreenWidth;
            double screenH = SystemParameters.PrimaryScreenHeight;
            double desiredW = screenW * 0.75;
            double desiredH = screenH * 0.75;

            Width = desiredW;
            Height = desiredH;
        }

         private void UpdateDateTime(object sender, EventArgs e)
         {
             dateTimeTextBlock.Text = DateTime.Now.ToString("HH:mm yyyy-MM-dd");
         }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

       


        private void Add_Click(object sender, RoutedEventArgs e)
        {
            TabItem currentTab = tabControl.SelectedItem as TabItem;  //  kastujem u objekat tipa TabItem 

            if (currentTab != null)
            {
                if (currentTab.Header.Equals("Student"))
                {
                    AddStudent addStudent = new AddStudent(headDao);
                    addStudent.ShowDialog();
                }
               
                else if(currentTab.Header.Equals("Professor"))
                {
                    //MessageBox.Show("Selected tab: " + (tabControl.SelectedItem as TabItem).Header);
                    AddProfessor addProfessor = new AddProfessor(headDao);
                    addProfessor.ShowDialog();
                    
                }
                else
                {
                    AddSubject addSubject = new AddSubject();
                    addSubject.ShowDialog();
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
                   ProfessorDTO professorDTO = dataGridProfessor.SelectedItem as ProfessorDTO;
                    if (professorDTO != null)
                    {
                        InsertProfessor insertProfessor = new InsertProfessor(headDao, professorDTO);
                        insertProfessor.Show();
                    }
                    else
                    {
                        MessageBox.Show("You didnt select professor to edit!");
                    }
                }
                else
                {
                   InsertSubject insertSubject = new InsertSubject();   
                   insertSubject.Show();
                }
            }
        }




        public void UpdateStudent()
        {
            Students.Clear();
            foreach (Student student in headDao.GetAllStudentsHead()) Students.Add(new StudentDTO(student));
        }

        public void UpdateProfessor()
        {
            Professors.Clear();
            foreach (Professor professor in headDao.GetAllProfessorsHead()) Professors.Add(new ProfessorDTO(professor));
        }

       public void UpdateSubject()
       {
            Subjects.Clear();
            foreach (Subject subject in headDao.GetAllSubjectsHead()) Subjects.Add(new SubjectDTO(subject));
       }

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
                        deleteStudent.ShowDialog();
                        break;
                    case "Professor":
                        ProfessorDTO professorDTO = dataGridProfessor.SelectedItem as ProfessorDTO;
                        DeleteProfessor deleteProfessor = new DeleteProfessor(headDao, professorDTO);
                        deleteProfessor.ShowDialog();
                        break;
                    case "Subject":
                        break;
                }
                
            }
            
        }
        
       
    }
}
