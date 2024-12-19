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
    /// Логика взаимодействия для ByTickets.xaml
    /// </summary>
    public partial class ByTickets : Window
    {
        public ByTickets()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadTicketData();
        }

        private void LoadTicketData()
        {
            try
            {
                // Загружаем данные из базы данных
                var performances = Context.DB.Perfomances.ToList();
                var theaters = Context.DB.Theaters.ToList();
                var tickets = Context.DB.Tickets.ToList();

                // Создаем список для хранения результатов
                var result = new List<TicketInfo>();

                // Проходим по каждому представлению (перфомансу)
                foreach (var performance in performances)
                {
                    // Находим соответствующий театр
                    var theater = theaters.FirstOrDefault(t => t.TheaterID == performance.TheaterID);
                    if (theater != null)
                    {
                        // Находим все билеты для текущего представления
                        var performanceTickets = tickets.Where(t => t.PerfomanceID == performance.PerfomanceID);

                        foreach (var ticket in performanceTickets)
                        {
                            // Добавляем информацию о представлении, театре и билете в результат
                            result.Add(new TicketInfo
                            {
                                Title = performance.Title,
                                Name = theater.Name,
                                DateOfStart = ticket.DateOfStart,
                                Row = (int)ticket.Row,
                                Place = (int)ticket.Place,
                                Price = (decimal)ticket.Price
                            });
                        }
                    }
                }

                // Устанавливаем источник данных для ListView
                TicketListView.ItemsSource = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CompletePurchase_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем количество билетов
            if (int.TryParse(TicketCountTextBox.Text, out int ticketCount) && ticketCount > 0)
            {
                MessageBox.Show($"Вы купили {ticketCount} билет(ов)!", "Покупка завершена", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректное количество билетов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TicketCountTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var textBox = sender as System.Windows.Controls.TextBox;
            if (textBox != null)
            {
                string input = textBox.Text;

                // Проверяем, является ли введенное значение числом
                if (int.TryParse(input, out int ticketCount))
                {
                    // Проверяем, что значение не отрицательное
                    if (ticketCount < 0)
                    {
                        MessageBox.Show("Количество билетов не может быть отрицательным.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        textBox.Text = string.Empty; 
                    }
                    else
                    {
                       
                      Close();
                    }
                }
                else if (!string.IsNullOrEmpty(input))
                {
                    MessageBox.Show("Пожалуйста, введите корректное число.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    textBox.Text = string.Empty; 
                }
            }
        }
    }

    // Класс для хранения информации о билете
    public class TicketInfo
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public DateTime DateOfStart { get; set; }
        public int Row { get; set; }
        public int Place { get; set; }
        public decimal Price { get; set; }
    }
}


