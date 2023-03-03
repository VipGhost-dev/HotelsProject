using HotelsProject.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для hotelPage.xaml
    /// </summary>
    public partial class hotelPage : Page
    {
       // ClassPaginator pag = new ClassPaginator();
        
        List<Hotel> hotelsL = ClassBase.BASE.Hotel.ToList();
        public hotelPage()
        {
            InitializeComponent();

            hotelsL = ClassBase.BASE.Hotel.ToList();
            Hotels.ItemsSource = hotelsL;
            //pag.CountPage = hotelsL.Count;
            //DataContext = pag;
            //try
            //{
            //    pageCountBox.Text = "10";
            //}
            //catch
            //{

            //}
            refreshTotalRecords();
        }

        void refreshDataGrid()
        {
            hotelsL = ClassBase.BASE.Hotel.ToList();
            Hotels.ItemsSource = hotelsL;
            //pag.CountPage = hotelsL.Count;
            //DataContext = pag;
            //try
            //{
            //    pag.CountPage = Convert.ToInt32(pageCountBox.Text);
            //}
            //catch
            //{
            //    pag.CountPage = hotelsL.Count;
            //}
            //pag.Countlist = hotelsL.Count;
            //Hotels.ItemsSource = hotelsL.Skip(0).Take(pag.CountPage).ToList();
            //pag.CurrentPage = 1;
            refreshTotalRecords();
        }

        void refreshTotalRecords()
        {
            totalRecords.Text = $"Записей выведено: {Hotels.Items.Count}/{hotelsL.Count}";
        }

        private void updateHotel_Click(object sender, RoutedEventArgs e)
        {
            int index = Convert.ToInt32((sender as Button).Uid);
            Hotel h = ClassBase.BASE.Hotel.FirstOrDefault(x => x.Id == index);
            changeHotel hotel = new changeHotel(h);
            hotel.Show();

            hotel.Closing += (obj, args) =>
            {
                refreshDataGrid();
            };
        }

        private void addHotelBTN_Click(object sender, RoutedEventArgs e)
        {
            changeHotel hotel = new changeHotel();
            hotel.Show();

            hotel.Closing += (obj, args) =>
            {
                refreshDataGrid();
            };
        }

        private void deleteHotelBTN_Click(object sender, RoutedEventArgs e)
        {
            if (Hotels.SelectedItem != null)
            {
                int index = (Hotels.SelectedItem as Hotel).Id;
                Hotel hotel = ClassBase.BASE.Hotel.FirstOrDefault(x => x.Id == index);
                List<HotelOfTour> hot = ClassBase.BASE.HotelOfTour.Where(x => x.HotelId == hotel.Id).ToList();
                List<Tour> tours = new List<Tour>();
                foreach (var item in hot)
                {
                    tours.Add(ClassBase.BASE.Tour.FirstOrDefault(x => x.Id == item.TourId && x.IsActual));
                }
                if (tours.Count == 0)
                {
                    var res = MessageBox.Show($"Вы уверены, что хотите удалить {hotel.Name}?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (res == MessageBoxResult.Yes)
                    {
                        foreach (var item in ClassBase.BASE.HotelImage)
                        {
                            if (item.HotelId == hotel.Id)
                            {
                                ClassBase.BASE.HotelImage.Remove(item);
                            }
                        }

                        foreach (var item in ClassBase.BASE.HotelComment)
                        {
                            if (item.HotelId == hotel.Id)
                            {
                                ClassBase.BASE.HotelComment.Remove(item);
                            }
                        }

                        ClassBase.BASE.Hotel.Remove(Hotels.SelectedItem as Hotel);
                        ClassBase.BASE.SaveChanges();
                        refreshDataGrid();
                    }
                    else
                    {
                        MessageBox.Show("Удаление отменено!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Отель подходит под актуальный тур, поэтому его нельзя удалить", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Question);
                }
            }
            else
            {
                MessageBox.Show("Отель не выбран", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Question);
            }
        }

        private void GoPage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;

            switch (tb.Uid)
            {
                case "prev":
                    //pag.CurrentPage--;
                    break;
                case "next":
                   // pag.CurrentPage++;
                    break;
                case "firstOne":
                    //pag.CurrentPage = 1;
                    break;
                case "lastOne":
                   // pag.CurrentPage = pag.CountPages;
                    break;
                default:
                   // pag.CurrentPage = Convert.ToInt32(tb.Text);
                    break;
            }
            //Hotels.ItemsSource = hotelsL.Skip(pag.CurrentPage * pag.CountPage - pag.CountPage).Take(pag.CountPage).ToList();
            refreshTotalRecords();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Regex.IsMatch(pageCountBox.Text, @"^[0-9]*$"))
            {
                if (pageCountBox.Text.Length != 0)
                    pageCountBox.Text = pageCountBox.Text.Substring(0, pageCountBox.Text.Length - 1);
            }
            try
            {
               // pag.CountPage = Convert.ToInt32(pageCountBox.Text);
            }
            catch
            {
               // pag.CountPage = hotelsL.Count;
            }
           // pag.Countlist = hotelsL.Count;
           // Hotels.ItemsSource = hotelsL.Skip(0).Take(pag.CountPage).ToList();
           // pag.CurrentPage = 1;
            refreshTotalRecords();
        }

        private void countToursTB_Loaded(object sender, RoutedEventArgs e)
        {
            int index = Convert.ToInt32((sender as TextBlock).Uid);

            int count = ClassBase.BASE.HotelOfTour.Where(x => x.HotelId == index).Count();

            (sender as TextBlock).Text = count.ToString();
        }
    }
}
