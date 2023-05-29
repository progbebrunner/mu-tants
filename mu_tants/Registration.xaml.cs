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
//using System.Windows.Shapes;
using System.IO;
using System.Text.RegularExpressions;
using System.Data.Odbc;
using Microsoft.Win32;
using System.Data.SqlClient;
using System.Data;
using System.Xml.Linq;

namespace mu_tants
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        //cтрока подключения к бд
        SqlConnection myConnection = new SqlConnection("Server = (localdb)\\MSSQLLocalDB; database = mu-tants; Integrated Security=True; TrustServerCertificate = True");

        //переменные для вставки изображения
        public byte[] _mainImageData = null;
        public string justimg = "just_img.png";
        public string path = Path.Combine(Directory.GetParent(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName)).FullName, @"Resources\Users\");
        public string selectedFile;
        public string extension = ".png";

        //Регулярные выражения для проверки вводимых данных на соответствие 
        Regex namecheck = new Regex(@"^[А-ЯЁ][а-яё]+$");
        //Regex namecheck = new Regex(@"^[А-ЯЁ][а-яё]+\-?[А-ЯЁ][а-яё]+$");
        Regex logcheck = new Regex(@"^\w{5,30}$");
        Regex passcheck = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{5,15}$");
        Regex mailcheck = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");

        public Registration()
        {
            InitializeComponent();
        }

        private void AddImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog imgch = new OpenFileDialog();
            imgch.Multiselect= false;
            imgch.Filter = "Image | *.png; *.jpg; *.jpeg";
            if (imgch.ShowDialog() == true)
            {
                justimg = Path.GetFileName(imgch.FileName);
                extension = Path.GetExtension(justimg);
                selectedFile= imgch.FileName;
                _mainImageData= File.ReadAllBytes(imgch.FileName);
                ImageService.Source = new ImageSourceConverter().ConvertFrom(_mainImageData) as ImageSource;
            }
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            MatchCollection logch = logcheck.Matches(txtLogin.Text);
            MatchCollection passch = passcheck.Matches(txtPassword.Password);
            MatchCollection namech = namecheck.Matches(txtName.Text);
            MatchCollection surnamech = namecheck.Matches(txtSurname.Text);
            MatchCollection emailch = mailcheck.Matches(txtEmail.Text);

            myConnection.Open();
            string loginquery = $"select login, password from Users where Login = '{txtLogin.Text}';";
            SqlDataAdapter adpt = new SqlDataAdapter(loginquery, myConnection);
            DataTable table = new DataTable();
            adpt.Fill(table);
            myConnection.Close();

            if (table.Rows.Count == 0)
            {
                if (logch.Count > 0)
                {
                    if (passch.Count > 0)
                    {
                        if (namech.Count > 0)
                        {
                            if (surnamech.Count > 0)
                            {
                                if (emailch.Count > 0)
                                {
                                    if (justimg != null)
                                    {
                                        justimg = txtLogin.Text + extension;
                                        int x = 0;
                                        while (File.Exists(path + justimg))
                                        {
                                            x++;
                                            justimg = txtLogin.Text + $" {x}" + extension;
                                        }
                                        if (selectedFile != null)
                                        {
                                            File.Copy(selectedFile, path + justimg);
                                        }
                                        else
                                        {
                                            justimg = null;
                                        }
                                    }

                                    myConnection.Open();
                                    string addquery = $"insert into Users([user_img],[login],[password],[role],[name],[surname],[email],[birthday]) values ('{justimg}', Trim('{txtLogin.Text}'), Trim('{txtPassword.Password}'), '2', Trim('{txtName.Text}'), Trim('{txtSurname.Text}'), Trim('{txtEmail.Text}'), '{dtBirthday.Text}')";
                                    SqlDataAdapter addadpt = new SqlDataAdapter(addquery, myConnection);
                                    DataTable addtable = new DataTable();
                                    addadpt.Fill(addtable);
                                    myConnection.Close();
                                    //MessageBox.Show("Проверки пройдены");
                                    NavigationService.Navigate(new Authorization());
                                    MessageBox.Show("Поздравляю, вы успешно зарегестрировались. Чтобы продолжить, войдите в систему");
                                }
                                else
                                {
                                    txtError.Text = " Почта не соответсвует шаблону: email@domain.com ";
                                }
                            }
                            else
                            {
                                txtError.Text = " Фамилия должна начинаться с заглавной буквы \n и содержать в себе только кириллицу ";
                            }
                        }
                        else
                        {
                            txtError.Text = " Имя должно начинаться с заглавной буквы \n и содержать в себе только кириллицу ";
                        }
                    }
                    else
                    {
                        txtError.Text = " Пароль должен содержать от 5 до 15 символов \n хотя бы одну строчную букву, одну заглавную букву, \n одну цифру и один специальный символ ";
                    }
                }
                else
                {                    
                    txtError.Text = " Логин должен содержать от 5 до 30 алфавитно-цифровых символов ";
                }
            }
            else
            {
                txtError.Text = " Такой логин уже занят. Придумайте уникальный ";
            }
        }
    }
}

