using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Annotations;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Theater.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddItemWindow.xaml
    /// </summary>
    public partial class AddItemWindow : Window
    {
        

        public AddItemWindow()
        {
            InitializeComponent();
            LoadGenres();

        }
   

        private void LoadGenres()
        {
            var uniqueGenres = Context.DB.Perfomances
                .Select(p => p.Genre) 
                .Distinct() 
                .ToList(); 

            genre.ItemsSource = uniqueGenres; 
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string titleP = title.Text.Trim();
                string genreSelected = genre.SelectedItem as string;
                string durationText = duration.Text.Trim();
                DateTime? selectedDate = DatePicker.SelectedDate;
                string theaterName = theater.Text.Trim();

                if (string.IsNullOrEmpty(titleP) || string.IsNullOrEmpty(genreSelected) ||
                    string.IsNullOrEmpty(durationText) || !selectedDate.HasValue ||
                    string.IsNullOrEmpty(theaterName))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var performances = new Perfomances
                {
                    Title = titleP,
                    Genre = genreSelected,
                    Duration =  durationText,
                    DateOfStart = selectedDate.Value,
                    TheaterName = theaterName
                };

                Context.DB.Perfomances.Add(performances);
                Context.DB.SaveChanges();
                Context.DB.SaveChanges();
           

                MessageBox.Show("Запись успешно сохранена.");
                Window parentWindow = Window.GetWindow(this);
                parentWindow?.Close();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении спектакля: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

     
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Закрытие окна без сохранения изменений
        }
    }
   
}
