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

namespace GUI.View.Insert
{
    /// <summary>
    /// Interaction logic for InsertProfessor.xaml
    /// </summary>
    public partial class InsertProfessor : Window
    {
        ProfessorDTO professorDTO;
        HeadDao headDao;
        public InsertProfessor(HeadDao contr, ProfessorDTO prof)
        {
            InitializeComponent();

            headDao = contr;
            professorDTO = new ProfessorDTO(prof);
            DataContext = professorDTO;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           // headDao.
            Close();
            MessageBox.Show("Professor updated!");
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            Close();
            MessageBox.Show("Professor is not updated!");
        }
    }
}
