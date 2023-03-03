using HotelsProject.Classes;
using HotelsProject.Pages;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HotelsProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ClassBase.BASE = new Entities();
            ClassFrame.Mfrm = mainFrame;
            ClassFrame.Mfrm.Navigate(new toursPage());
        }

        private void openHotelPageBTN_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.Mfrm.Navigate(new hotelPage());
        }

        private void openToursPageBTN_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.Mfrm.Navigate(new toursPage());
        }
    }
}
