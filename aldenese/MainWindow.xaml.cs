using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace LacuarinEWPFApplication
{
    public partial class MainWindow : Window
    {
        
        public ObservableCollection<Anime> AnimeList { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            AnimeList = new ObservableCollection<Anime>();
            dataGrid.ItemsSource = AnimeList;
        }

        
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtPrice.Text) ||
                comboBoxGenre.SelectedItem == null)
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            if (!double.TryParse(txtPrice.Text, out double price))
            {
                MessageBox.Show("Invalid price.");
                return;
            }

            double discount = 0;
            double.TryParse(txtDiscount.Text, out discount);

            string genre = (comboBoxGenre.SelectedItem as ComboBoxItem)?.Content.ToString();

            string season = GetSelectedSeason();

            double total = price - discount;

            Anime newAnime = new Anime
            {
                AnimeName = txtName.Text,
                Price = price,
                Genre = genre,
                Season = season,
                Discount = discount,
                Total = total
            };

            AnimeList.Add(newAnime);

            ClearForm();
        }

        
        private void btnDeleteData_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem is Anime selected)
            {
                AnimeList.Remove(selected);
                btnDeleteData.IsEnabled = false;
            }
        }

        
        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid.SelectedItem is Anime selected)
            {
                txtName.Text = selected.AnimeName;
                txtPrice.Text = selected.Price.ToString();
                txtDiscount.Text = selected.Discount.ToString();

                
                foreach (ComboBoxItem item in comboBoxGenre.Items)
                {
                    if (item.Content.ToString() == selected.Genre)
                    {
                        comboBoxGenre.SelectedItem = item;
                        break;
                    }
                }

                
                SetSeason(selected.Season);

                btnDeleteData.IsEnabled = true;
            }
        }

        
        private string GetSelectedSeason()
        {
            if (rbOne.IsChecked == true) return "1";
            if (rbTwo.IsChecked == true) return "2";
            if (rbThree.IsChecked == true) return "3";
            if (rbFour.IsChecked == true) return "4";
            return "";
        }

        
        private void SetSeason(string season)
        {
            rbOne.IsChecked = season == "1";
            rbTwo.IsChecked = season == "2";
            rbThree.IsChecked = season == "3";
            rbFour.IsChecked = season == "4";
        }

         
        private void ClearForm()
        {
            txtName.Clear();
            txtPrice.Clear();
            txtDiscount.Clear();
            comboBoxGenre.SelectedIndex = -1;

            rbOne.IsChecked = false;
            rbTwo.IsChecked = false;
            rbThree.IsChecked = false;
            rbFour.IsChecked = false;
        }
    }

     
    public class Anime
    {
        public string AnimeName { get; set; }
        public double Price { get; set; }
        public string Genre { get; set; }
        public string Season { get; set; }
        public double Discount { get; set; }
        public double Total { get; set; }
    }
}