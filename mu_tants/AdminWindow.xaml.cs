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
using System.IO;
using System.Net.NetworkInformation;

namespace mu_tants
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public string path = Path.Combine(Directory.GetParent(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName)).FullName, @"Resources\Users\");
        object frameContent = null;
        byte[] _mainImageData;
        string user_login;
        public AdminWindow(string x)
        {
            InitializeComponent();
            user_login = x;
            FrameAdmin.Navigate(new Homepage());
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if (FrameAdmin.CanGoBack)
                FrameAdmin.GoBack();
        }

        private void FrameMain_ContentRendered(object sender, EventArgs e)
        {
            if (FrameAdmin.Content != frameContent)
            {
                if (App.CurrentUser != null)
                    TBlockUsername.Text = App.CurrentUser.login;
                else TBlockUsername.Text = "Гость";
            }
            else
            {
                TBlockUsername.Text = String.Empty;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var users = App.Context.Users.ToList();
            var currentUser = users.Where(u => u.login == user_login).FirstOrDefault();
            var img = currentUser.user_img;
            if (currentUser.user_img == null)
            {
                img = "just_img.png";
            }
            var profilePic = new BitmapImage(new Uri(path + img, UriKind.Relative));
            (UserPhoto.Fill as ImageBrush).ImageSource = profilePic;
            TBlockUsername.Text = currentUser.login;            
        }

        private void Image_Click(object sender, RoutedEventArgs e)
        {
            FrameAdmin.Navigate(new Homepage());
        }
        private void Ellipse_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            //FrameAdmin.Navigate(new MainPage(currentUserId));
        }
        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Window.GetWindow(this).Close();
        }
        private void profileBtn_Click(object sender, RoutedEventArgs e)
        {
            FrameAdmin.Navigate(new Homepage());
        }


        private void BtnLabels_Click(object sender, RoutedEventArgs e)
        {
            FrameAdmin.Navigate(new AdminLabels());
        }

        private void BtnAlbums_Click(object sender, RoutedEventArgs e)
        {
            FrameAdmin.Navigate(new AdminAlbums());
        }

        private void BtnArtists_Click(object sender, RoutedEventArgs e)
        {
            FrameAdmin.Navigate(new AdminArtists());
        }
    }
}