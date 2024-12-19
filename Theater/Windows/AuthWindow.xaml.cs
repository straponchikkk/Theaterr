using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Windows;
using System.Windows.Controls;

namespace Theater.Windows
{
    /// <summary>    
    /// Логика взаимодействия для AuthWindow.xaml    
    /// </summary>    
    public partial class AuthWindow : Window
    {
        private  List<Audiences> audiences;

        public AuthWindow()
        {
            InitializeComponent();
            InitializeAudiences();
        }

        private void InitializeAudiences()
        {
            using (var context = new Context())
            {
                audiences = context.Audiences.ToList(); // Предполагается, что Audiences - это DbSet в контексте 
            }
        }

        private void AuthInput(object sender, RoutedEventArgs e)
        {
            string login = Login.Text;
            string password = Password.Password;

            // Проверка наличия введенных логина и пароля.    
            if (string.IsNullOrWhiteSpace(login) && string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Логин и пароль не могут быть пустыми строками!");
                return;
            }
            if (string.IsNullOrWhiteSpace(login))
            {
                MessageBox.Show("Логин не может быть пустой строкой!");
                return;
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Пароль не может быть пустой строкой!");
                return;
            }

            // Поиск пользователя в списке 
            var user1 = audiences.FirstOrDefault(x => x.Email == login && x.Password == password);

            if (user1 != null) // Обработка результатов поиска и установка роли текущего пользователя.    
            {
                var window = Window.GetWindow(this);
                if (window != null)
                {
                    window.Hide();
                }

                MessageBox.Show("Вы успешно авторизованы!");
                Window1 mainWindow = new Window1();
                mainWindow.Show();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
                return;
            }
        }

        private void GuestLogin_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вход как гость.");
            Window1 mainWindow = new Window1();
            mainWindow.Show();
     
        }
    }
}