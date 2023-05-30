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
    /// Логика взаимодействия для Recomendations.xaml
    /// </summary>
    public partial class Recomendations : Page
    {
        public int type ;
        public Recomendations(int x)
        {
            InitializeComponent();
            type = x;
            AlbumsLoad(x);
            ComboSortBy.SelectedIndex= 0;
        }

        public void AlbumsLoad(int x)
        {
            var albums = App.Context.Albums.Where(a => a.type_id == x).ToList();
            switch (ComboSortBy.SelectedIndex)
            {
                case 0:
                    albums = albums.OrderBy(a => a.artist_name).ToList();
                    break;
                case 1:
                    albums = albums.OrderByDescending(a => a.artist_name).ToList();
                    break;
                case 2:
                    albums = albums.OrderBy(a => a.album_name).ToList();
                    break;
                case 3:
                    albums = albums.OrderByDescending(a => a.album_name).ToList();
                    break;                
            }
            albums = albums.Where(a => a.artist_name.ToLower().Contains(TBoxSearch.Text.ToLower()) || a.album_name.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            LVAlbums.ItemsSource = null;
            LVAlbums.ItemsSource = albums;
            if (LVAlbums.Items.Count == 0)
            {
                txtError.Text = "По вашему запросу ничего не найдено";
            }
            else if(ComboSortBy.Text.Trim() == null) {
            }
        }

        private void ComboSortBy_SelectionChanged(object sender, RoutedEventArgs e)
        {
            AlbumsLoad(type);
        }
        private void TBoxSearch_TextChanged(object sender, RoutedEventArgs e)
        {
            txtError.Text = null;
            AlbumsLoad(type);
        }

        private void ArtistButton_Click(object sender, RoutedEventArgs e)
        {
            string artist_name = (sender as Button).Content.ToString();
            var artist = App.Context.Artists.Where(x => x.artist_name == artist_name).ToList();
            int artist_id = artist[0].artist_id;
            NavigationService.Navigate(new Artist(artist_id));
        }
    }
}
