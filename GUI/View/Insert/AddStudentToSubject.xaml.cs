﻿using CLI.DAO;
using CLI.Model;
using CLI.Observer;
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
    /// Interaction logic for AddStudentToSubject.xaml
    /// </summary>
    public partial class AddStudentToSubject : Window
    {
        HeadDao headDao;
        public ObserverSub observerSub;
        StudentDTO studentDTO;
        SubjectDTO subjectDTO;
        ObservableCollection<SubjectDTO> _failedSubs;

        public ObservableCollection<SubjectDTO> Subjects { get; set; }
        public AddStudentToSubject(HeadDao cnt, StudentDTO stdDTO, ObservableCollection<SubjectDTO> failedSubs)
        {
            InitializeComponent();
            studentDTO = new StudentDTO(stdDTO);
            subjectDTO = new SubjectDTO();
            headDao = cnt;
            Subjects = new ObservableCollection<SubjectDTO>();
            _failedSubs = failedSubs;
            observerSub = new ObserverSub();
            dataGridStudentSubject.ItemsSource = Subjects;

            UpdateStudentSubject(stdDTO.ToStudent());


        }


        public void UpdateStudentSubject(Student std)
        {
            Subjects.Clear();
            // foreach (Subject subject in headDao.GetAllSubjectsHead()) Subjects.Add(new SubjectDTO(subject));
            foreach (Subject subject in headDao.getSubjectsForStudent(std)) Subjects.Add(new SubjectDTO(subject));
        }

        private void Button_ClickAddStudentToSubject(object sender, RoutedEventArgs e)
        {
            SubjectDTO subjectDTO = dataGridStudentSubject.SelectedItem as SubjectDTO;
            if (subjectDTO != null)
            {
                
               _failedSubs.Add(subjectDTO);
                //dodajem na listu nepolozenih
                Subjects.Remove(subjectDTO); // uklanjam sa liste predmeta koje ne pohadja ili nije polozio
                StudentSubject studentSubject= new StudentSubject(studentDTO.Id, subjectDTO.Id); //dodajem vezu Student-Subject
                headDao.AddStudentSubjectHead(studentSubject); //dodajem vezu Student-Subject (sa ovim radi to da mi ostane predmet u nepolozenim i kada izadjem iz programa, ali i dalje mi ne radi brisanje)
                /***vjerovatno je problem do observera i notify)**/
                //observerSub.NotifyObservers();
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

        /*  private void Button_Exit(object sender, RoutedEventArgs e)
          {

          }*/

        /* private void Button_Exit(object sender, RoutedEventArgs e)
         {

         }*/
    

    
}
