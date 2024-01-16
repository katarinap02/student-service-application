using CLI.DAO;
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

namespace GUI.View.Show
{
    /// <summary>
    /// Interaction logic for ShowChair.xaml
    /// </summary>
    public partial class ShowChair : Window, IObserver
    {
        public static RoutedCommand NewCommand = new RoutedCommand();

        public ObservableCollection<ChairDTO> Chairs { get; set; }

        ChairDTO chairDTO;
        HeadDao headDao;
        public ShowChair(HeadDao contr)
        {
            InitializeComponent();

            headDao = contr;
            chairDTO = new ChairDTO();
            headDao.observerSub.Subscribe(this);
            Chairs = new ObservableCollection<ChairDTO>();
            

            dataGridChair.ItemsSource = Chairs;
            UpdateChair();
        }

        public void UpdateChair()
        {
            Chairs.Clear();
            foreach (Chair chair in headDao.GetAllChairsHead()) Chairs.Add(new ChairDTO(chair));
        }



        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_AddProfessortoChair(object sender, RoutedEventArgs e)
        {
            ChairDTO chairDTO = dataGridChair.SelectedItem as ChairDTO;   //obrisemo ocenu

            if (chairDTO != null)
            {

                AddChefToChair addChefToChair = new AddChefToChair(headDao, chairDTO);
                addChefToChair.ShowDialog();
            }
            else
            {
                MessageBox.Show("You didnt select chair!");
            }

            

        }

        public void Update()
        {
            UpdateChair();
        }
    }
}
