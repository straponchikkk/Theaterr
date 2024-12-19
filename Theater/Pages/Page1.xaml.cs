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
        private ObservableCollection<Perfomances> perfomances;
        public Page1()
        {
            InitializeComponent();
            LoadPerfomances();
   

        }
            public void LoadPerfomances()
        {
            try
            {
                using (var theaterEntities = new Context())
                {
                    perfomances = new ObservableCollection <Perfomances> (theaterEntities.Perfomances); 
                    PerfomanceslistView.ItemsSource = perfomances;
              
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ByTickets byTickets = new ByTickets();
            byTickets.ShowDialog();

        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddItemWindow addItemWindow = new AddItemWindow();
            addItemWindow.ShowDialog();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (PerfomanceslistView.SelectedItem == null)
            {
                MessageBox.Show("Не выбран объект для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Получаем выбранный элемент
            var selectedPerformance = (Perfomances)PerfomanceslistView.SelectedItem;

            
            using (var theaterEntities = new Context())
            {
                var performanceToRemove = theaterEntities.Perfomances
                    .Include(b => b.Theaters) 
                    .FirstOrDefault(p => p.PerfomanceID == selectedPerformance.PerfomanceID);

                if (performanceToRemove != null)
                {
                    theaterEntities.Perfomances.Remove(performanceToRemove);
                    theaterEntities.SaveChanges();

                 
                    LoadPerfomances();
                }
                else
                {
                    MessageBox.Show("Представление не найдено", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            var selectedPerformance = PerfomanceslistView.SelectedItem as Perfomances;
            if (selectedPerformance != null )
            {
        
                ChangeWindow changeWindow = new ChangeWindow (selectedPerformance);

        
                AuthWindow authWindow = new AuthWindow
                {
                    Title = "Спектакль"
                };

              
                changeWindow.Show();
                authWindow.Closed += (s, args) =>
                {
                    LoadPerfomances();
                };
            }
            else
            {
                System.Windows.MessageBox.Show("Пожалуйста, выберите спектакль для обновления.");
            }
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
            AuthWindow win = new AuthWindow();
            win.Show();
        }
        private void Find_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = find.Text; if (string.IsNullOrEmpty(search))
            {
                LoadPerfomances();
            }
        }
        private void Search_Button(object sender, RoutedEventArgs e)
        {
           
            string search = find.Text;

            var searchResult = (from p in Context.DB.Perfomances
                                where p.Genre.Contains(search) || p.Title.Contains(search) || p.Duration.Contains(search) // изменил на ||
                                select new { p.Title, p.Duration, p.Genre });

            PerfomanceslistView.ItemsSource = searchResult.ToList();
        }
    }
}
