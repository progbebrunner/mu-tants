﻿using System;
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
    /// Логика взаимодействия для Recomendations.xaml
    /// </summary>
    public partial class Recomendations : Page
    {
        public Recomendations()
        {
            InitializeComponent();
            AlbunsLoad();
        }

        public void AlbunsLoad()
        {
            var albums = App.Context.Albums.ToList();
        }

        private void ArtistButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void AlbumButton_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}