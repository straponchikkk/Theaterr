using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Theater.Pages;

namespace Theater.Windows
{
    /// <summary>
    /// Логика взаимодействия для UserList.xaml
    /// </summary>
    public partial class UserList : Window
    {
        private List<Audiences> usersList;
        public UserList()
        {
            InitializeComponent();
            LoadUsers();
        }
        private void LoadUsers()
        {
            using (var context = new Context())
            {
                // Получаем список пользователей из базы данных
                usersList = context.Audiences.ToList();
            }

            // Привязываем список пользователей к DataGrid
            UsersDataGrid.ItemsSource = usersList;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
           this.Close();

        }
    }
}
