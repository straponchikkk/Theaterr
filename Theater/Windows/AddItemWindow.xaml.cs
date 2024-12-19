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
           

                MessageBox.Show("Спектакль успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
           
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Закрытие окна без сохранения изменений
        }
    }
   
}
