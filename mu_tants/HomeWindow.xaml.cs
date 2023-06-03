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
using System.Windows.Shapes;

namespace mu_tants
{
    /// <summary>
    /// Логика взаимодействия для HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        object frameContent = null;

        public HomeWindow()
        {
            InitializeComponent();
            FrameHome.Navigate(new Homepage());
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if (FrameHome.CanGoBack)
                FrameHome.GoBack();
        }

        private void FrameMain_ContentRendered(object sender, EventArgs e)
        {
            if (FrameHome.Content != frameContent)
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
            frameContent = FrameHome.Content;
        }

        private void Image_Click(object sender, RoutedEventArgs e)
        {
            FrameHome.Navigate(new Homepage());
        }
    }
}
