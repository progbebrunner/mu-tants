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
    /// Логика взаимодействия для Labels.xaml
    /// </summary>
    public partial class Labels : Page
    {
        public int label;
        public Labels(int x)
        {
            InitializeComponent();
            label = x;
            LabelLoad(x);
            AlbumsLoad(x);
            ComboSortBy.SelectedIndex = 0;
        }

        private void LabelLoad(int x)
        {
            var labels = App.Context.Labels.Where(a => a.label_id == x).ToList();
            ArtistInfo.ItemsSource = labels;
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
            AlbumsLoad(label);
        }
        private void TBoxSearch_TextChanged(object sender, RoutedEventArgs e)
        {
            txtError.Text = null;
            AlbumsLoad(label);
        }

        private void AlbumButton_Click(object sender, RoutedEventArgs e)
        {
            string album_name = (sender as Button).Content.ToString();
            var albums = App.Context.Albums.Where(x => x.album_name == album_name).ToList();
            int album_id = albums[0].album_id;
            NavigationService.Navigate(new Album(album_id));
        }
    }
}

