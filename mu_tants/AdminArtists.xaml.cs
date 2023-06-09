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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace mu_tants
{
    /// <summary>
    /// Логика взаимодействия для AdminArtists.xaml
    /// </summary>
    public partial class AdminArtists : Page
    {
        public int artist_id;
        public AdminArtists()
        {
            InitializeComponent();
            ArtistsLoad();
        }
        public void ArtistsLoad()
        {
            var artists = App.Context.Artists.ToList();
            switch (ComboSortBy.SelectedIndex)
            {
                case 0:
                    artists = artists.OrderBy(a => a.artist_name).ToList();
                    break;
                case 1:
                    artists = artists.OrderByDescending(a => a.artist_name).ToList();
                    break;
            }
            artists = artists.Where(a => a.artist_name.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            LVArtists.ItemsSource = null;
            LVArtists.ItemsSource = artists;
            if (LVArtists.Items.Count == 0)
            {
                txtError.Text = "По вашему запросу ничего не найдено";
            }
            else if (ComboSortBy.Text.Trim() == null)
            {
            }
        }

        private void ComboSortBy_SelectionChanged(object sender, RoutedEventArgs e)
        {
            ArtistsLoad();
        }
        private void TBoxSearch_TextChanged(object sender, RoutedEventArgs e)
        {
            txtError.Text = null;
            ArtistsLoad();
        }

        private void ArtistButton_Click(object sender, RoutedEventArgs e)
        {
            string artist_name = (sender as Button).Content.ToString();
            var artist = App.Context.Artists.Where(x => x.artist_name == artist_name).ToList();
            artist_id = artist[0].artist_id;
            NavigationService.Navigate(new Artist(artist_id));
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var artists = App.Context.Artists.ToList();
            var currentArtist = artists.Where(u => u.artist_id == artist_id).FirstOrDefault();
            if (MessageBox.Show($"Вы точно хотите удалить исполнителя?",
                "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                App.Context.Artists.Remove(currentArtist);
                App.Context.SaveChanges();
                ArtistsLoad();
                MessageBox.Show("Исполнитель был удален", "Внимание");
            }
        }
    }
}
