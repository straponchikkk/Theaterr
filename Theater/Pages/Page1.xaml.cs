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
using Theater.Windows;
using System.Collections.ObjectModel;
using System.Data.Entity;


namespace Theater.Pages
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        private List<Perfomances> perfomances;
        public Page1()
        {
            InitializeComponent();
            LoadPerfomances();

        }


        public void LoadPerfomances()
        {
            try
            {
                using (var theaterEntities = new TheaterEntities())
                {
                    perfomances = theaterEntities.Perfomances.ToList(); // Загружаем все отзывы из базы данных
                    PerfomanceslistView.ItemsSource = perfomances; // Устанавливаем источник данных для ListView
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Билет куплен!");
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddItemWindow addItemWindow = new AddItemWindow();
            addItemWindow.ShowDialog();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
           ChangeWindow changeWindow = new ChangeWindow();
            changeWindow.ShowDialog();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
            AuthWindow win = new AuthWindow();
            win.Show();
        }

        private void Search_Button(object sender, RoutedEventArgs e)
        {

        }

       
    }
}
