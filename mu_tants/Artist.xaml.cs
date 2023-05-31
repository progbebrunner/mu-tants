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

namespace mu_tants
{
    /// <summary>
    /// Логика взаимодействия для Artist.xaml
    /// </summary>
    public partial class Artist : Page
    {
        public int artist_id;
        public Artist(int x)
        {
            InitializeComponent();
            artist_id = x;
            ArtistLoad(x);
            AlbumsLoad(x);
            ComboSortBy.SelectedIndex = 0;
        }

        private void ArtistLoad(int x)
        {
            var artist = App.Context.Artists.Where(a => a.artist_id == x).ToList();
            ArtistInfo.ItemsSource = artist;
        }

        public void AlbumsLoad(int x)
        {
            var albums = App.Context.Albums.Where(a => a.artist_id == x).ToList();
            switch (ComboSortBy.SelectedIndex)
            {
                case 0:
                    albums = albums.OrderBy(a => a.album_name).ToList();
                    break;
                case 1:
                    albums = albums.OrderByDescending(a => a.album_name).ToList();
                    break;
            }
            albums = albums.Where(a => a.album_name.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            ArtistAlbums.ItemsSource = null;
            ArtistAlbums.ItemsSource = albums;
            if (ArtistAlbums.Items.Count == 0)
            {
                txtError.Text = "По вашему запросу ничего не найдено";
            }
        }

        private void ComboSortBy_SelectionChanged(object sender, RoutedEventArgs e)
        {
            AlbumsLoad(artist_id);
        }
        private void TBoxSearch_TextChanged(object sender, RoutedEventArgs e)
        {
            txtError.Text = null;
            AlbumsLoad(artist_id);
        }
    }
}