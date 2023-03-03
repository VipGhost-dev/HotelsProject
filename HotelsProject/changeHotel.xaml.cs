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
using System.Windows.Shapes;

namespace HotelsProject
{
    /// <summary>
    /// Логика взаимодействия для changeHotel.xaml
    /// </summary>
    public partial class changeHotel : Window
    {
        bool update = false;
        Hotel hotel;
        void uploadFields()
        {
            countryBox.ItemsSource = ClassBase.BASE.Country.ToList();
            countryBox.SelectedValuePath = "Id";
            countryBox.DisplayMemberPath = "Name";
        }

        public changeHotel()
        {
            InitializeComponent();
            uploadFields();
            changeHotelBTN.Content = "Добавить";
        }

        public changeHotel(Hotel hotel)
        {
            InitializeComponent();
            uploadFields();
            this.hotel = hotel;

            hotelNameBox.Text = hotel.Name;
            countStars.Text = hotel.CountOfStars.ToString();
            hotelDescriptionBox.Text = hotel.Description;
            countryBox.SelectedItem = hotel.Country;

            update = true;

            changeHotelBTN.Content = "Изменить";
        }

        private void changeHotelBTN_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(hotelNameBox.Text) && !String.IsNullOrEmpty(hotelDescriptionBox.Text) && !String.IsNullOrEmpty(countStars.Text) && countryBox.SelectedIndex != -1)
            {
                if (update)
                {
                    hotel.Name = hotelNameBox.Text;
                    hotel.Description = hotelDescriptionBox.Text;
                    hotel.CountOfStars = Convert.ToInt32(countStars.Text);
                    hotel.Country = countryBox.SelectedItem as Country;

                    ClassBase.BASE.SaveChanges();
                    MessageBox.Show("Изменения внесены!", "Уведомление");
                    this.Close();
                }
                else
                {
                    Hotel hotel = new Hotel()
                    {
                        Name = hotelNameBox.Text,
                        Description = hotelDescriptionBox.Text,
                        CountOfStars = Convert.ToInt32(countStars.Text),
                        Country = countryBox.SelectedItem as Country
                    };
                    ClassBase.BASE.Hotel.Add(hotel);
                    ClassBase.BASE.SaveChanges();

                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Заполнены не все поля!", "Ошибка");
            }
        }

        private void countStars_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Regex.IsMatch(countStars.Text, @"[0-5]") || countStars.Text.Length > 1)
            {
                if (countStars.Text.Length != 0)
                    countStars.Text = countStars.Text.Substring(0, countStars.Text.Length - 1);
            }
        }
    }
}
