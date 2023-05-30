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
        }

        private void ArtistLoad(int x)
        {
            var artist = App.Context.Artists.Where(a => a.artist_id == x).ToList();
            ArtistInfo.ItemsSource = artist;
        }
    }
}
