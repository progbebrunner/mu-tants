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
    /// Логика взаимодействия для Album.xaml
    /// </summary>
    public partial class Album : Page
    {
        public int album_id;
        public Album(int x)
        {
            InitializeComponent();
            album_id = x;
            AlbumLoad(album_id);
        }
        public void AlbumLoad(int x)
        {
            var albums = App.Context.Albums.Where(a => a.album_id == x).ToList();
            AlbumImg.ItemsSource = null;
            AlbumImg.ItemsSource = albums;
            AlbumInfo.ItemsSource = null;
            AlbumInfo.ItemsSource = albums;
        }

        private void TypeButton_Click(object sender, RoutedEventArgs e)
        {
            var albums = App.Context.Albums.Where(a => a.album_id == album_id).ToList();
            int type = Int32.Parse(albums[0].type_id.ToString());
            NavigationService.Navigate(new Recomendations(type));
        }

        private void LabelButton_Click(object sender, RoutedEventArgs e)
        {
            var albums = App.Context.Albums.Where(a => a.album_id == album_id).ToList();
            int label = Int32.Parse(albums[0].label_id.ToString());
            NavigationService.Navigate(new Labels(label));
        }

        private void ArtistButton_Click(object sender, RoutedEventArgs e)
        {
            var albums = App.Context.Albums.Where(a => a.album_id == album_id).ToList();
            int artist = Int32.Parse(albums[0].artist_id.ToString());
            NavigationService.Navigate(new Artist(artist));
        }
    }
}
