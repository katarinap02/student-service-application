﻿using CLI.DAO;
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
using GUI.Localization;
using System.Collections.ObjectModel;

using GUI.View.Show;

namespace GUI 
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IObserver
    {
        private App app;

        private const string SRB = "sr-RS";

        private const string ENG = "en-US";

        public static RoutedCommand NewCommand = new RoutedCommand();
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
            this.DataContext = this;
            app = (App)Application.Current;
            app.ChangeLanguage(SRB);
         
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
        private void About_Click(object sender, RoutedEventArgs e) //kratak tekst kad se klikne na help + about
        {
            
            Window newWindow = new Window
            {
                Title = "New Text Window",
                Width = 550,
                Height = 400,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };


            TextBlock textBlock = new TextBlock
            {
                Text = "Application Version: Visual Studio 2022 \n" +
                "Description: An application for student services, providing insight into necessary information essential for more efficient operation to every faculty. \n\n" +
                "Students who worked on it: Katarina Petrovic, RA17-2021, and Milena-Mima Sekaric, RA57-2021.\n" +
                " Both are third-year students at the Faculty of Technical Sciences in Novi Sad",
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(10)
            };


            newWindow.Content = textBlock;

   
            newWindow.ShowDialog();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.N && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                
                Add_Click(sender, e);
            }
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

       
        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void St_Click(object sender, RoutedEventArgs e)
        {
            TabItem currentTab = tabControl.SelectedItem as TabItem;

            if (!currentTab.Header.Equals("Student"))
            {
                TabItem studentTabItem = tabControl.Items.OfType<TabItem>().FirstOrDefault(tab => tab.Header.Equals("Student"));

                if (studentTabItem == null)
                {
                    
                    studentTabItem = new TabItem { Header = "Student" };
                    
                    tabControl.Items.Add(studentTabItem);
                }

               
                tabControl.SelectedItem = studentTabItem;
            }
        }

        private void Pf_Click(object sender, RoutedEventArgs e) //sluzi da bi radilo add i na vise nacina
        {
            TabItem currentTab = tabControl.SelectedItem as TabItem;

            if (!currentTab.Header.Equals("Professor") || !currentTab.Header.Equals("Profesor"))
            {
                TabItem studentTabItem = tabControl.Items.OfType<TabItem>().FirstOrDefault(tab => tab.Header.Equals("Professor"));

                if (studentTabItem == null)
                {
                    
                    studentTabItem = new TabItem { Header = "Professor" };
                    
                    tabControl.Items.Add(studentTabItem);
                }

               
                tabControl.SelectedItem = studentTabItem;
            }
        }

        private void Sb_Click(object sender, RoutedEventArgs e)
        {
            TabItem currentTab = tabControl.SelectedItem as TabItem;

            if (!currentTab.Header.Equals("Subject") || !currentTab.Header.Equals("Predmet"))
            {
                TabItem studentTabItem = tabControl.Items.OfType<TabItem>().FirstOrDefault(tab => tab.Header.Equals("Subject"));

                if (studentTabItem == null)
                {
                    
                    studentTabItem = new TabItem { Header = "Subject" };
            
                    tabControl.Items.Add(studentTabItem);
                }


                tabControl.SelectedItem = studentTabItem;
            }
        }

        private void Ch_Click(object sender, RoutedEventArgs e)
        {
            TabItem currentTab = tabControl.SelectedItem as TabItem;

            if (currentTab != null)
            {
                ShowChair showChair = new ShowChair(headDao);
                showChair.ShowDialog();
            }
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
               
                else if(currentTab.Header.Equals("Professor") || currentTab.Header.Equals("Profesor"))
                {
                    //MessageBox.Show("Selected tab: " + (tabControl.SelectedItem as TabItem).Header);
                    AddProfessor addProfessor = new AddProfessor(headDao);
                    addProfessor.ShowDialog();
                    
                }
                else
                {
                    AddSubject addSubject = new AddSubject(headDao);
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
                    StudentDTO studentDTO = dataGridStudent.SelectedItem as StudentDTO;

                    if (studentDTO != null)
                    {
                        InsertStudent insertStudent = new InsertStudent(headDao, studentDTO);
                        insertStudent.Show();
                    }
                    else
                    {
                        MessageBox.Show("You didnt select student to edit!");
                    }
                }

                else if (currentTab.Header.Equals("Professor") || currentTab.Header.Equals("Profesor"))
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
                    SubjectDTO subjectDTO = dataGridSubject.SelectedItem as SubjectDTO;
                    if (subjectDTO != null)
                    {
                        InsertSubject insertSubject = new InsertSubject(headDao, subjectDTO);
                        insertSubject.Show();
                    }
                    else
                    {
                        MessageBox.Show("You didnt select subject to edit!");
                    }
                    
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
          //  List<Subject> subjects = headDao.GetAllSubjectsHead();
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
                        if (studentDTO != null)
                        {
                            DeleteStudent deleteStudent = new DeleteStudent(headDao, studentDTO);
                            deleteStudent.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("You didnt select student to delete!");
                        }
                        break;
                    case "Professor":
                        ProfessorDTO professorDTO = dataGridProfessor.SelectedItem as ProfessorDTO;
                        if (professorDTO != null)
                        {
                            DeleteProfessor deleteProfessor = new DeleteProfessor(headDao, professorDTO);
                            deleteProfessor.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("You didnt select professor to delete!");
                        }
                        break;
                    case "Profesor":
                        ProfessorDTO professorDTO1 = dataGridProfessor.SelectedItem as ProfessorDTO;
                        if (professorDTO1 != null)
                        {
                            DeleteProfessor deleteProfessor = new DeleteProfessor(headDao, professorDTO1);
                            deleteProfessor.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("You didnt select professor to delete!");
                        }
                        break;
                    case "Subject":
                        SubjectDTO subjectDTO = dataGridSubject.SelectedItem as SubjectDTO;
                        if (subjectDTO != null)
                        {
                            DeleteSubject deleteSubject = new DeleteSubject(headDao, subjectDTO);
                            deleteSubject.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("You didnt select subject to delete!");
                        }
                        break;
                    case "Predmet":
                        SubjectDTO subjectDTO1 = dataGridSubject.SelectedItem as SubjectDTO;
                        if (subjectDTO1 != null)
                        {
                            DeleteSubject deleteSubject = new DeleteSubject(headDao, subjectDTO1);
                            deleteSubject.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("You didnt select subject to delete!");
                        }
                        break;
                }
                
            }
          
            
        }



        private void Search_Click(object sender, RoutedEventArgs e)
        {
            TabItem selectedTab = tabControl.SelectedItem as TabItem;
            string searchTerm = textboxSearch.Text.ToLower();
            string[] resultArray = searchTerm.Split(',').Select(s => s.Trim()).ToArray(); //trimujem, izbacujem whitespaces
            //treba dodati switch case da mogu da pretrazujem na razlicitim tabovima

            switch (selectedTab.Header)
            {
                case "Student":
                    if (resultArray.Length > 0)
                    {
                        if (resultArray.Length > 3)
                        {
                            MessageBox.Show("You input more than three words!");
                        }
                        else
                        {
                            if (resultArray.Length == 1)
                            {
                                var filtered = Students.Where(student => student.Surname.ToLower().Contains(resultArray[0])).ToList();
                                dataGridStudent.ItemsSource = filtered;
                            }
                            else if (resultArray.Length == 2)
                            {
                                var filtered = Students.Where(student =>
                                student.Surname.ToLower().Contains(resultArray[0]) &&
                                student.Name.ToLower().Contains(resultArray[1])).ToList();
                                dataGridStudent.ItemsSource = filtered;
                            }
                            else if (resultArray.Length == 3)
                            {
                                var filtered = Students.Where(student =>
                                student.IndexS.ToLower().Contains(resultArray[0]) &&
                                student.Name.ToLower().Contains(resultArray[1]) &&
                                student.Surname.ToLower().Contains(resultArray[2])).ToList();
                                dataGridStudent.ItemsSource = filtered;
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("Please input one, two or three words to search!");
                    }
                    break;
                case "Professor":
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
                                var filtered = Professors.Where(professor => professor.Surname.ToLower().Contains(resultArray[0])).ToList();
                                dataGridProfessor.ItemsSource = filtered;
                            }
                            else if (resultArray.Length == 2)
                            {
                                var filtered = Professors.Where(professor =>
                                professor.Surname.ToLower().Contains(resultArray[0]) &&
                                professor.Name.ToLower().Contains(resultArray[1])).ToList();
                                dataGridProfessor.ItemsSource = filtered;
                            }

                        }

                    }
                    else
                    {
                        MessageBox.Show("Please input one or two words to search!");
                    }
                    break;
                case "Profesor":
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
                                var filtered = Professors.Where(professor => professor.Surname.ToLower().Contains(resultArray[0])).ToList();
                                dataGridProfessor.ItemsSource = filtered;
                            }
                            else if (resultArray.Length == 2)
                            {
                                var filtered = Professors.Where(professor =>
                                professor.Surname.ToLower().Contains(resultArray[0]) &&
                                professor.Name.ToLower().Contains(resultArray[1])).ToList();
                                dataGridProfessor.ItemsSource = filtered;
                            }

                        }

                    }
                    else
                    {
                        MessageBox.Show("Please input one or two words to search!");
                    }
                    break;
                case "Subject":
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
                                dataGridSubject.ItemsSource = filtered;
                            }
                            else if (resultArray.Length == 2)
                            {
                                var filtered = Subjects.Where(subject =>
                                subject.Name.ToLower().Contains(resultArray[0]) &&
                                subject.Code.ToString().ToLower().Contains(resultArray[1])).ToList();
                                dataGridSubject.ItemsSource = filtered;
                            }

                        }

                    }
                    else
                    {
                        MessageBox.Show("Please input one or two words to search!");
                    }
                    break;

                case "Predmet":
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
                                dataGridSubject.ItemsSource = filtered;
                            }
                            else if (resultArray.Length == 2)
                            {
                                var filtered = Subjects.Where(subject =>
                                subject.Name.ToLower().Contains(resultArray[0]) &&
                                subject.Code.ToString().ToLower().Contains(resultArray[1])).ToList();
                                dataGridSubject.ItemsSource = filtered;
                            }

                        }

                    }
                    else
                    {
                        MessageBox.Show("Please input one or two words to search!");
                    }
                    break;


            }
        }

        private void ChooseSubject_Click(object sender, RoutedEventArgs e)

        {
            SubjectDTO subjectDTO = dataGridSubject.SelectedItem as SubjectDTO;
            if (subjectDTO != null)
            {
                ShowOtherSubjects showOtherSubjects = new ShowOtherSubjects(headDao, subjectDTO);
                showOtherSubjects.ShowDialog();
            }
            else
            {
                MessageBox.Show("You didn't select subject!");
            }
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void MenuItem_Click_Serbian(object sender, RoutedEventArgs e)
        {
            app.ChangeLanguage(SRB);
        }

        private void MenuItem_Click_English(object sender, RoutedEventArgs e)
        {
            app.ChangeLanguage(ENG);
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            /*if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.N))
                MenuItem_Click_New(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.D))
                MenuItem_Click_Delete(sender, e);*/
             if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.X))
               Close_Click(sender, e);
             else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.R))
                MenuItem_Click_Serbian(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.G))
                MenuItem_Click_English(sender, e);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
