using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        SqlConnection myConnection = new SqlConnection("Server = (localdb)\\MSSQLLocaldb; database = mu-tants; Integrated Security=True; TrustServerCertificate = True");      


        public Authorization()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //string login = txtLogin.Text.ToString();
                //string password = txtPwd.Password.ToString();
                //myConnection.Open();
                //string loginquery = $"select login, password, role from Users where Login = '{login}';";
                //SqlDataAdapter adpt = new SqlDataAdapter(loginquery, myConnection);
                //DataTable table = new DataTable();
                //adpt.Fill(table);
                //myConnection.Close();

                var user_log = App.Context.Users.Where(u => u.login == txtLogin.Text.Trim()).ToList();

                if (user_log.Count == 1)
                {
                    //var user_pass = App.Context.Users.Where(u => u.password == PasswordBox.ToString().Trim());
                    if (user_log[0].password.ToString() == txtPwd.Password.ToString())
                    {

                        if (user_log[0].role == 1)
                        {
                            AdminWindow adminWindow = new AdminWindow(user_log[0].login);
                            adminWindow.Show();
                        }
                        else
                        {
                            if (user_log[0].role.ToString().Trim() == "")
                            {
                                HomeWindow homeWindow = new HomeWindow("guest");
                                homeWindow.Show();
                            }
                            else
                            {
                                HomeWindow homeWindow = new HomeWindow(user_log[0].login);
                                homeWindow.Show();
                            }
                        }
                        Window.GetWindow(this).Close();
                        NavigationService.Navigate(new Homepage());
                    }
                    else
                    {
                        txtError.Text = "Неправильный пароль. Попробуйте снова.";
                    }
                }
                else
                {
                    txtError.Text = "Неправильный логин. Попробуйте снова.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Registration());
        }

        private void txtPwd_PasswordChanged(object sender, RoutedEventArgs e)
        {
            txtError.Text = "";
        }
    }
}
