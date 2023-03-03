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

namespace HotelsProject.Pages
{
    /// <summary>
    /// Логика взаимодействия для toursPage.xaml
    /// </summary>
    public partial class toursPage : Page
    {
        public toursPage()
        {
            InitializeComponent();

            LV.ItemsSource = ClassBase.BASE.Tour.ToList();
            LV.SelectedValuePath = "Id";

            List<Type> Types = ClassBase.BASE.Type.ToList();
            Types.Insert(0, new Type()
            {
                Name = "Все типы"
            });
            typeTourBox.ItemsSource = Types;
            typeTourBox.DisplayMemberPath = "Name";
            typeTourBox.SelectedIndex = 0;
            sortBox.SelectedIndex = 0;
            searchCBox.SelectedIndex = 0;
        }

        private void TextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            int index = Convert.ToInt32((sender as TextBlock).Uid);
            Tour tour = ClassBase.BASE.Tour.FirstOrDefault(x => x.Id == index);

            (sender as TextBlock).Text = $"{Math.Round(tour.Price)} РУБ";
        }

        private void actual_Loaded(object sender, RoutedEventArgs e)
        {
            int index = Convert.ToInt32((sender as TextBlock).Uid);
            Tour tour = ClassBase.BASE.Tour.FirstOrDefault(x => x.Id == index);

            if (tour.IsActual)
            {
                (sender as TextBlock).Text = "Актуален";
                (sender as TextBlock).Foreground = new SolidColorBrush(Color.FromRgb(29, 165, 29));
            }
            else
            {
                (sender as TextBlock).Text = "Не актуален";
                (sender as TextBlock).Foreground = new SolidColorBrush(Color.FromRgb(212, 43, 43));
            }
        }

        private void ticketCount_Loaded(object sender, RoutedEventArgs e)
        {
            int index = Convert.ToInt32((sender as TextBlock).Uid);
            Tour tour = ClassBase.BASE.Tour.FirstOrDefault(x => x.Id == index);

            (sender as TextBlock).Text = $"Билетов: {tour.TicketCount}";
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void typeTourBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void onlyActualCheck_Checked(object sender, RoutedEventArgs e)
        {
            Filter();
        }

        private void onlyActualCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            Filter();
        }

        private void sortBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        void Filter()
        {
            List<Tour> tours = ClassBase.BASE.Tour.ToList();

            if (!String.IsNullOrEmpty(searchBox.Text))
            {
                switch (searchCBox.SelectedIndex)
                {
                    case 0:
                        tours = tours.Where(x => x.Name.ToString().ToLower().Contains(searchBox.Text.ToLower())).ToList();
                        break;
                    case 1:
                        tours = tours.Where(x => x.Description.ToString().ToLower().Contains(searchBox.Text.ToLower())).ToList();
                        break;
                }
            }
            if ((bool)onlyActualCheck.IsChecked)
            {
                tours = tours.Where(x => x.IsActual).ToList();
            }
            if (typeTourBox.SelectedIndex != 0)
            {
                tours = tours.Where(x => x.Type.Contains(typeTourBox.SelectedValue)).ToList();
            }
            switch (sortBox.SelectedIndex)
            {
                case 1:
                    tours = tours.OrderByDescending(x => x.Price).ToList();
                    break;
                case 2:
                    tours = tours.OrderBy(x => x.Price).ToList();
                    break;
            }

            double totalPrice = 0;

            foreach (var item in tours)
            {
                totalPrice += Convert.ToDouble(item.TicketCount * item.Price);
            }

            allPriceBox.Text = $"Общая стоимость всех отображающихся туров - {totalPrice}";

            LV.ItemsSource = tours;
        }

        private void imageTour_Loaded(object sender, RoutedEventArgs e)
        {
            int index = Convert.ToInt32((sender as Image).Uid);

            Tour tour = ClassBase.BASE.Tour.FirstOrDefault(x => x.Id == index);

            if (tour.ImagePreview != null)
            {
                string path = Environment.CurrentDirectory;
                path = path.Replace("bin\\Debug", tour.ImagePreview);
                (sender as Image).Source = BitmapFrame.Create(new Uri(path));
            }
            else
            {
                string path = Environment.CurrentDirectory;
                path = path.Replace("bin\\Debug", "Images\\picture.png");
                (sender as Image).Source = BitmapFrame.Create(new Uri(path));
            }
        }


    }
}
