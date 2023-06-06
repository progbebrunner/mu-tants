using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
    /// Логика взаимодействия для AdminAlbumsAddEdit.xaml
    /// </summary>
    public partial class AdminAlbumsAddEdit : Page
    {
        public int album_id;
        public AdminAlbumsAddEdit(int x)
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

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var albums = App.Context.Albums.ToList();
            var currentAlbums = albums.Where(u => u.album_id == album_id).FirstOrDefault();
            if (MessageBox.Show($"Вы точно хотите удалить альбом?",
                "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                App.Context.Albums.Remove(currentAlbums);
                App.Context.SaveChanges();
                AlbumLoad(album_id);
                MessageBox.Show("Альбом был удален", "Внимание");
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
