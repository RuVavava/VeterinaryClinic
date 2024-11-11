using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using VeterinaryClinic.DB;

namespace VeterinaryClinic.Windowws.RegistrarW
{
    /// <summary>
    /// Логика взаимодействия для AddClientReg.xaml
    /// </summary>
    public partial class AddClientReg : Window
    {
        public static List<Clients> clients { get; set; }
        public static Clients client = new Clients();

        public AddClientReg()
        {
            InitializeComponent();

        }

        private void NumericOnly(System.Object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = IsTextNumeric(e.Text);

        }

        private static bool IsTextNumeric(string str)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^0-9]");
            return reg.IsMatch(str);

        }

        private void CancelBTN_Click(object sender, RoutedEventArgs e) //Отмена
        {
            var result = MessageBox.Show("Вы действительно хотите выйти?", "", MessageBoxButton.YesNo, MessageBoxImage.Asterisk);

            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void OKBTN_Click(object sender, RoutedEventArgs e) //Подтвердить
        {
            try
            {
                StringBuilder error = new StringBuilder();
                if (string.IsNullOrWhiteSpace(LastNameTB.Text) || FirstNameTB.Text.Trim() == "" || PatronymicTB.Text.Trim() == "" || PhoneTB.Text.Trim() == "" || AdressPrTB.Text.Trim() == "" || DateClientBDDP.SelectedDate == null)

                {
                    error.AppendLine("Заполните все поля!");
                    return;
                }

                if (error.Length > 0)
                {
                    MessageBox.Show(error.ToString());
                }

                if (DateClientBDDP.SelectedDate != null && (DateTime.Now - (DateTime)DateClientBDDP.SelectedDate).TotalDays < 365 * 18 + 4)
                {
                    MessageBox.Show("Клиент не может быть младше 18 лет!", "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                
                var result = MessageBox.Show($"Проверьте верность введенных данных:\nФИО: {FirstNameTB.Text} {LastNameTB.Text} {PatronymicTB.Text}, \nДата рождения: {DateClientBDDP.SelectedDate}, " +
                    $"Телефон:, {PhoneTB.Text}, Почта: {EmailTB.Text} , \nАдрес проживания: {AdressPrTB.Text}", "",
                    MessageBoxButton.YesNo, MessageBoxImage.Asterisk);

                if (result == MessageBoxResult.Yes)
                {

                    client.LastName_Client = LastNameTB.Text.Trim();
                    client.FirstName_Client = FirstNameTB.Text.Trim();
                    client.Patronymic_Client = PatronymicTB.Text.Trim();
                    client.DB_Client = DateClientBDDP.SelectedDate.Value;

                    if (GenderCB.SelectedIndex == 0)
                        client.ID_Gender = 1;
                    else
                        client.ID_Gender = 2;

                    client.Phone_Client = PhoneTB.Text.Trim();
                    client.Email_Client = EmailTB.Text.Trim();
                    client.ResidentialAddress_Client = AdressPrTB.Text.Trim();
                    client.Password = "";

                    DBConnection.veterinary.Clients.Add(client);
                    DBConnection.veterinary.SaveChanges();

                    this.Close();
                } 


            }
            catch
            {
                MessageBoxResult result = MessageBox.Show("Произошла ошибка!\nНеобходимо перезагрузить программу!", "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);

                if (result == MessageBoxResult.OK)
                {
                    //ПЕРЕЗАПУСК ПРОГРАММЫ
                    string exePath = Process.GetCurrentProcess().MainModule.FileName;
                    Process.Start(exePath);
                    Application.Current.Shutdown();
                }
            }
        }
    }
}
