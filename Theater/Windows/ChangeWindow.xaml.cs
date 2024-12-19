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
    /// Логика взаимодействия для ChangeWindow.xaml 
    /// </summary> 
    public partial class ChangeWindow : Window
    {
        public int PerfomanceID { get; set; }

        public event Action<Perfomances> PerformanceChanged;
        public ChangeWindow(Perfomances perfomances)
        {
            InitializeComponent();
            LoadGenres();
            PerfomanceID = perfomances.PerfomanceID;
        }
        private void LoadGenres()
        {
            var uniqueGenres = Context.DB.Perfomances
                .Select(p => p.Genre)
                .Distinct()
                .ToList();

            genre.ItemsSource = uniqueGenres;
        }
        private void Change_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Check that all required fields are filled 
                if (string.IsNullOrWhiteSpace(title.Text) ||
                    genre.SelectedItem == null ||
                    string.IsNullOrWhiteSpace(duration.Text) ||
                    DatePicker.SelectedDate == null ||
                    string.IsNullOrWhiteSpace(theater.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Assuming PerformanceID has been properly initialized 
                using (var context = new Context())
                {
                    // Find performance by ID 
                    var performanceToUpdate = context.Perfomances.Find(PerfomanceID);
                    if (performanceToUpdate == null)
                    {
                        MessageBox.Show("Спектакль не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Update performance details 
                    performanceToUpdate.Title = title.Text.Trim();
                    performanceToUpdate.Genre = genre.SelectedItem.ToString(); // Assuming genre is simple strings 
                    performanceToUpdate.Duration = duration.Text.Trim();
                    performanceToUpdate.DateOfStart = DatePicker.SelectedDate.Value;
                    performanceToUpdate.TheaterName = theater.Text.Trim();

                    context.SaveChanges();
                }

                // Notify of successful update 
                MessageBox.Show("Запись успешно обновлена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                // Close the current window 
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}\nДополнительная информация: {ex.InnerException?.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}