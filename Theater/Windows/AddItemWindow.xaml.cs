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
            // Валидация полей ввода
            //if (string.IsNullOrWhiteSpace(titleTextBox.Text) ||
            //    string.IsNullOrWhiteSpace(durationTextBox.Text) ||
            //    genreTextBox.SelectedItem == null ||
                
            //{
            //    MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            //    return;
            //}

            //// Создание нового спектакля
            //var performance = new Perfomances
            //{
            //    Title = TextBox.Text,
            //    Duration = TextBox.Text,
            //    Genre = ComboBox.Text,
            //};

            //try
            //{
            //    // Сохранение спектакля в базу данных
            //    Context.DB.Performances.Add(performance);
            //    Context.DB.SaveChanges();

            //    MessageBox.Show("Спектакль успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            //    this.Close(); // Закрытие окна после успешного добавления
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Ошибка при добавлении спектакля: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Закрытие окна без сохранения изменений
        }
    }
   
}
