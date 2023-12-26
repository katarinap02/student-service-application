﻿using System;
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

namespace GUI.View.Add
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    /// 
    
    public partial class AddSubject : Window
    {

        public DTO.SubjectDTO subjectDTO {  get; set; }
        public AddSubject()
        {
       
        InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Subject added!");
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Subject not added!");
        }
    }
}
