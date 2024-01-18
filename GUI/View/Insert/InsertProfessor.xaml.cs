﻿using CLI.DAO;
using CLI.Model;
using CLI.Observer;
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
    public partial class InsertProfessor : Window, IObserver
    {
        ProfessorDTO professorDTO;
        AdressDTO adressDTO;
        HeadDao headDao;
        SubjectDTO subjectDTO;

        public static RoutedCommand NewCommand = new RoutedCommand();
        public ObservableCollection<SubjectDTO> Subjects { get; set; }
        public ObservableCollection<StudentDTO> Students { get; set; }

        public InsertProfessor(HeadDao contr, ProfessorDTO prof)
        {
            InitializeComponent();

            headDao = contr;

           
            professorDTO = new ProfessorDTO(prof);
            subjectDTO = new SubjectDTO();
            DataContext = professorDTO;

            Subjects = new ObservableCollection<SubjectDTO>();
            Students = new ObservableCollection<StudentDTO>();
            headDao.observerSub.Subscribe(this);
            dataGridSubjects.ItemsSource = Subjects;
            dataGridStudents.ItemsSource = Students;

            UpdateSubject(prof.ToProfessor());
            UpdateStudent(prof.ToProfessor());

        }

        public void UpdateSubject(Professor prof)
        {
            Subjects.Clear();
            foreach (Subject subject in headDao.getSubjectsForProfessor(prof)) Subjects.Add(new SubjectDTO(subject));
        }

        public void UpdateStudent(Professor prof)
        {
            Students.Clear();
            foreach (Student student in headDao.ProfessorTeachesStudents(prof)) Students.Add(new StudentDTO(student));
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
            if (subjectDto != null) 
            {
                DeleteProfessorFromSubject deleteProfessorFromSubject = new DeleteProfessorFromSubject(headDao, subjectDto, professorDTO);
                deleteProfessorFromSubject.ShowDialog();
            }
            else
            {
                MessageBox.Show("You didn't select subuject to remove!");
            }
            
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            Close();
            MessageBox.Show("Professor is not updated!");
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
           
            string searchTerm = textboxSearch.Text.ToLower();
            string[] resultArray = searchTerm.Split(',').Select(s => s.Trim()).ToArray(); //trimujem, izbacujem whitespaces
            //treba dodati switch case da mogu da pretrazujem na razlicitim tabovima

               
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
                                dataGridStudents.ItemsSource = filtered;
                            }
                            else if (resultArray.Length == 2)
                            {
                                var filtered = Students.Where(student =>
                                student.Surname.ToLower().Contains(resultArray[0]) &&
                                student.Name.ToLower().Contains(resultArray[1])).ToList();
                                dataGridStudents.ItemsSource = filtered;
                            }
                            else if (resultArray.Length == 3)
                            {
                                var filtered = Students.Where(student =>
                                student.IndexS.ToLower().Contains(resultArray[0]) &&
                                student.Name.ToLower().Contains(resultArray[1]) &&
                                student.Surname.ToLower().Contains(resultArray[2])).ToList();
                                dataGridStudents.ItemsSource = filtered;
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("Please input one, two or three words to search!");
                    }
            }

        public void Update()
        {
            UpdateSubject(professorDTO.ToProfessor());
            UpdateStudent(professorDTO.ToProfessor());
        }
    }
}
