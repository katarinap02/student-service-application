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

namespace GUI.View.Insert
{
    /// <summary>
    /// Interaction logic for InsertProfessor.xaml
    /// </summary>
    public partial class InsertProfessor : Window
    {
        public InsertProfessor()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Professor updated!");
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Professor is not updated!");
        }
    }
}
