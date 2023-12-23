using CLI.DAO;
using GUI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUI.View
{
    class AddStudent: Window
    {
        public StudentDTO StudentDto { get; set; }

        private StudentDao studentsDAO;
    }
}
