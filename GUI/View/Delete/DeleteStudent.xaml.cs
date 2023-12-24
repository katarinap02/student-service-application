﻿using CLI.DAO;
using GUI.DTO;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for DeleteStudent.xaml
    /// </summary>
    public partial class DeleteStudent : Window
    {
        StudentDTO _studentDTO;
        HeadDao _headDao;
        public bool IsClosed { get; set; } = false;
        public DeleteStudent(HeadDao cnt, StudentDTO studentDto)
        {
            InitializeComponent();
            _headDao= cnt;
            _studentDTO = studentDto;

        }


        
        private void YesButton_Click(object sender, RoutedEventArgs e)
        {


            _headDao.RemoveStudentHead(_studentDTO.Id);
            IsClosed= true;
            Close();
            //MessageBox.Show("Professor is not deleted!");
          //  YesButton.Background = Brushes.Blue; 
           // NoButton.Background = Brushes.Gray;  
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            IsClosed = true;
            Close();

           // MessageBox.Show("Professor is not deleted!");
            // Change color when No button is clicked
          //  NoButton.Background = Brushes.Blue;   
           // YesButton.Background = Brushes.Gray; 
          
        }

    }
}
