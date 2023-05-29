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
    /// Логика взаимодействия для Homepage.xaml
    /// </summary>
    public partial class Homepage : Page
    {
        public Homepage()
        {
            InitializeComponent();
        }

        private void RecButton_Click(object sender, RoutedEventArgs e)
        {
            string name = (sender as Button).Name.ToString();
            switch (name)
            {
                case "CoreButton":
                    NavigationService.Navigate(new Recomendations(1));
                    break;
                case "SubCoreButton":
                    NavigationService.Navigate(new Recomendations(2));
                    break;
                case "HipHopButton":
                    NavigationService.Navigate(new Recomendations(4));
                    break;
                case "JazzButton":
                    NavigationService.Navigate(new Recomendations(6));
                    break;
                case "ClassiscButton":
                    NavigationService.Navigate(new Recomendations(3));
                    break;
                case "ElectronicButton":
                    NavigationService.Navigate(new Recomendations(5));
                    break;
                case "MetalButton":
                        NavigationService.Navigate(new Recomendations(7));
                    break;
            }
        }        
    }
}
