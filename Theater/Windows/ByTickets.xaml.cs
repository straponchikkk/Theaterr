using System;
using System.Collections.Generic;
using System.Data.Entity;
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
          
                Context.DB.Perfomances.Load();
                Context.DB.Tickets.Load();
                Context.DB.Theaters.Load();
            var perfomances = Context.DB.Perfomances.Local.ToList();
            var tickets = Context.DB.Tickets.Local.ToList();
            var theaters = Context.DB.Theaters.Local.ToList();
            var result = from per in perfomances
                         join tick in tickets on per.PerfomanceID equals tick.PerfomanceID
                         join theat in theaters on per.TheaterID equals theat.TheaterID
                         select new
                             {
                                 per.Title,
                                 tick.Row,
                                 tick.Price,
                                 tick.Place,
                                 tick.DateOfStart,
                                 theat.Name
                             };
            TicketListView.ItemsSource = result.ToList();
          
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

}


