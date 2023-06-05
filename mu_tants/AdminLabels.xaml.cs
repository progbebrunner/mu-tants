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
    /// Логика взаимодействия для AdminLabels.xaml
    /// </summary>
    public partial class AdminLabels : Page
    {
        public AdminLabels()
        {
            InitializeComponent();
            LabelsLoad();
        }
        public void LabelsLoad()
        {
            var labels = App.Context.Labels.ToList();
            switch (ComboSortBy.SelectedIndex)
            {
                case 0:
                    labels = labels.OrderBy(a => a.label_name).ToList();
                    break;
                case 1:
                    labels = labels.OrderByDescending(a => a.label_name).ToList();
                    break;
            }
            labels = labels.Where(a => a.label_name.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            LVLabels.ItemsSource = null;
            LVLabels.ItemsSource = labels;
            if (LVLabels.Items.Count == 0)
            {
                txtError.Text = "По вашему запросу ничего не найдено";
            }
            else if (ComboSortBy.Text.Trim() == null)
            {
            }
        }

        private void ComboSortBy_SelectionChanged(object sender, RoutedEventArgs e)
        {
            LabelsLoad();
        }
        private void TBoxSearch_TextChanged(object sender, RoutedEventArgs e)
        {
            txtError.Text = null;
            LabelsLoad();
        }

        private void LabelButton_Click(object sender, RoutedEventArgs e)
        {
            string label_name = (sender as Button).Content.ToString();
            var artist = App.Context.Labels.Where(x => x.label_name == label_name).ToList();
            int label_id = artist[0].label_id;
            NavigationService.Navigate(new Labels(label_id));
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
