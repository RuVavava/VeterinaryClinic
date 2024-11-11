using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using VeterinaryClinic.DB;

namespace VeterinaryClinic.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public static List<Users> Users { get; set; }
        public AuthPage()
        {
            InitializeComponent();
        }

        private void PhoneTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Проверяем, что вводимые символы - это цифры
            if (!Regex.IsMatch(e.Text, @"^[0-9]+$"))
            {
                e.Handled = true; // Отменяем ввод, если это не цифра
            }
        }

        private void EntryBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string phone = (PhoneTB.Text.Trim());
                string password = (PasswordTB.Password.Trim());

                var users = DBConnection.veterinary.Users.ToList();
                var curret_user = users.FirstOrDefault(i => i.Phone == phone && i.Password == password);

                DBConnection.curret_user = curret_user;

                if (curret_user != null && curret_user.POST == 10) //Регистратор
                {
                    MessageBox.Show($"Добро пожаловать, {curret_user.FirstName} {curret_user.LastName} {curret_user.Patronymic}!", "", MessageBoxButton.OK, MessageBoxImage.Information);

                    NavigationService.Navigate(new Pages.Registrar.MainPageRegistr());

                }
                else if (curret_user != null && curret_user.POST == 2) //Ветврач
                {
                    MessageBox.Show($"Добро пожаловать, {curret_user.FirstName} {curret_user.LastName} {curret_user.Patronymic}!", "", MessageBoxButton.OK, MessageBoxImage.Information);

                    NavigationService.Navigate(new Pages.Veterinarian.MainPageVet());
                }
                else if (curret_user != null && curret_user.POST == 1) //Лаборант
                {
                    MessageBox.Show($"Добро пожаловать, {curret_user.FirstName} {curret_user.LastName} {curret_user.Patronymic}!", "", MessageBoxButton.OK, MessageBoxImage.Information);

                    //NavigationService.Navigate(new Pages.StudentsPages.OglavleniePage());
                }
                else if (phone == "" || password == "")
                    MessageBox.Show("Введите незаполненные поля", "ПРЕДУПРЕЖДЕНИЕ", MessageBoxButton.OK, MessageBoxImage.Warning);
                else if (curret_user == null)
                {
                    MessageBox.Show("Введенные данные некорректны!", "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Stop);
                    PasswordTB.Password = "";
                }

            }
            catch { MessageBox.Show("Возникла ошибка входа!", "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Stop); }
        }
    }
}
