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

        private void CompletePurchase_Click(object sender, RoutedEventArgs e)
        {
            string itemName = ByTick.Name;
            this.Close();
            MessageBox.Show("Билет куплен!");
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TicketCountTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
