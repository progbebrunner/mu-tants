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
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    public partial class Profile : Page
    {
        string login;
        public Profile(string x)
        {
            InitializeComponent();
            login = x;
            ProfileLoad(login);
        }
        public void ProfileLoad(string x)
        {
            //var albums = App.Context.Users.Where(a => a.login == x).ToList();
            //AlbumImg.ItemsSource = null;
            //AlbumImg.ItemsSource = albums;
            //AlbumInfo.ItemsSource = null;
            //AlbumInfo.ItemsSource = albums;
        }
    }
}
