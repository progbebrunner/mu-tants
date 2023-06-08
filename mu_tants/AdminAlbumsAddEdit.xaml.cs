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
    /// Логика взаимодействия для AdminAlbumsAddEdit.xaml
    /// </summary>
    public partial class AdminAlbumsAddEdit : Page
    {
        public Albums album = null;
        public byte[] _mainImageData = null;
        public string justimg = null;
        public string path = Path.Combine(Directory.GetParent(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName)).FullName, @"Resources\Albums\");
        public string selectedFile;
        public string extension = ".png";
        public string just_path = Path.Combine(Directory.GetParent(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName)).FullName, @"Resources\");
        Regex rusnamecheck = new Regex(@"^[А-ЯЁ][а-яё]+$");
        Regex engnamecheck = new Regex(@"^[A-Z][a-zA-Z\s]*$");
        Regex lengthcheck = new Regex(@"\d");

        public AdminAlbumsAddEdit(Albums x)
        {
            InitializeComponent();
            album = x;
            AlbumLoad(album);
        }
        public AdminAlbumsAddEdit()
        {
            InitializeComponent();
            InfoLoad();
        }

        public void AlbumLoad(Albums album)
        {
            var albums = App.Context.Albums.ToList();

            ArtistName.ItemsSource = App.Context.Artists.Select(x => x.artist_name).ToList();
            Genre.ItemsSource = App.Context.Genres.Select(x => x.name).ToList();
            LabelName.ItemsSource = App.Context.Labels.Select(x => x.label_name).ToList();
            Type.ItemsSource = App.Context.Types.Select(x => x.name).ToList();

            AlbumName.Text = album.album_name;
            ArtistName.Text = album.artist_name;
            ReleaseDate.Text = album.new_release_date;
            Length.Text = album.new_length;
            Genre.Text = album.genre;
            LabelName.Text = album.label_name;
            Type.Text = album.type_name;
            AlbumImg.Source = new ImageSourceConverter().ConvertFrom(album.new_img) as ImageSource;


        }

        public void InfoLoad()
        {
            AlbumImg.Source = new ImageSourceConverter().ConvertFrom(just_path + "just_img.png") as ImageSource;

            ArtistName.ItemsSource = App.Context.Artists.Select(x => x.artist_name).ToList();
            Genre.ItemsSource = App.Context.Genres.Select(x => x.name).ToList();
            LabelName.ItemsSource = App.Context.Labels.Select(x => x.label_name).ToList();
            Type.ItemsSource = App.Context.Types.Select(x => x.name).ToList();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            int id_album = album.album_id;
            var albums = App.Context.Albums.ToList();
            var currentAlbums = albums.Where(u => u.album_id == id_album).FirstOrDefault();
            if (MessageBox.Show($"Вы точно хотите удалить альбом?",
                "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                App.Context.Albums.Remove(currentAlbums);
                App.Context.SaveChanges();
                NavigationService.Navigate(new AdminAlbums());
                MessageBox.Show("Альбом был удален");
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {              
                MatchCollection rusnamech = rusnamecheck.Matches(AlbumName.Text);
                MatchCollection engnamech = engnamecheck.Matches(AlbumName.Text);
                MatchCollection lengthch = lengthcheck.Matches(Length.Text);

                
                if (AlbumName.Text.Trim() != null && ArtistName.Text.Trim() != null && ReleaseDate.Text.Trim() != "" && Length.Text.Trim() != "" && Genre.Text.Trim() != "" && LabelName.Text.Trim() != "" && Type.Text.Trim() != "")
                {
                    if (rusnamech.Count > 0 || engnamech.Count > 0)
                    {
                        if (lengthch.Count > 0)
                        {
                            var artist = App.Context.Artists.Where(a => a.artist_name == ArtistName.Text.Trim()).ToList();
                            int id_artist = artist[0].artist_id;
                            string name_artist = artist[0].artist_name.ToLower().Trim();
                            var genres = App.Context.Genres.Where(a => a.name == Genre.Text.Trim()).ToList();
                            int id_genre = genres[0].genre_id;
                            var labels = App.Context.Labels.Where(a => a.label_name == LabelName.Text.Trim()).ToList();
                            int id_label = labels[0].label_id;
                            var types = App.Context.Types.Where(a => a.name == Type.Text.Trim()).ToList();
                            int id_type = types[0].type_id;
                            string release_date = ReleaseDate.Text;

                            var albumch = App.Context.Albums.Where(a => a.artist_id == id_artist && a.album_name == AlbumName.Text.ToLower()).ToList();

                            if (albumch.Count == 0)
                            {
                                if (justimg != null)
                                {
                                    justimg = name_artist + " " + AlbumName.Text.ToLower() + extension;
                                    int x = 0;
                                    while (File.Exists(path + justimg))
                                    {
                                        x++;
                                        justimg = AlbumName.Text + $" ({x})" + extension;
                                    }
                                    path = path + justimg;
                                    File.Copy(selectedFile, path);
                                }
                                var new_album = new Albums
                                {
                                    album_img = name_artist + " " + AlbumName.Text.ToLower().Trim() + ".png",
                                    album_name = AlbumName.Text.Trim(),
                                    artist_id = id_artist,
                                    length = Int32.Parse(Length.Text.Trim()),
                                    release_date = DateTime.Parse(ReleaseDate.Text.Trim()),
                                    genre_id = id_genre,
                                    label_id = id_label,
                                    type_id = id_type
                                };

                                App.Context.Albums.Add(new_album);
                                App.Context.SaveChanges();
                                MessageBox.Show("Альбом успешно добавлен");
                            }
                            else
                            {
                                txtError.Text = " Альбом с таким названием и таким исполнителем уже существует ";
                            }
                        }
                        else
                        {
                            txtError.Text = " Длительность должна быть цифровым значением ";
                        }
                    }
                    else
                    {
                        txtError.Text = " Название должно содержать символы русского или английского алфавитов и начинаться с заглавной буквы ";
                    }
                }
                else
                {
                    txtError.Text = "Заполните все поля";
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                       

            
        }

        private void AddImgButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog imgch = new OpenFileDialog();
            imgch.Multiselect = false;
            imgch.Filter = "Image | *.png; *.jpg; *.jpeg";
            if (imgch.ShowDialog() == true)
            {
                justimg = Path.GetFileName(imgch.FileName);
                extension = Path.GetExtension(justimg);
                selectedFile = imgch.FileName;
                _mainImageData = File.ReadAllBytes(imgch.FileName);
                AlbumImg.Source = new ImageSourceConverter().ConvertFrom(_mainImageData) as ImageSource;
            }
        }
    }
}
