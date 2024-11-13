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
    /// Логика взаимодействия для AddAppointmentReg.xaml
    /// </summary>
    public partial class AddAppointmentReg : Window
    {
        public static List<Appointment> appointments {  get; set; }
        public static List<Clients> clients { get; set; }
        public static List<Pets> pets { get; set; }
        public static List<Service> service { get; set; }
        public static Appointment appointment = new Appointment();
        public AddAppointmentReg()
        {
            InitializeComponent();

            clients = new List<Clients>(DBConnection.veterinary.Clients.ToList());
            pets = new List<Pets>(DBConnection.veterinary.Pets.ToList());
            service = new List<Service>(DBConnection.veterinary.Service.ToList());

            ClientsCB.ItemsSource = clients;
            PetsCB.ItemsSource = pets;
            ServiceCB.ItemsSource = service;

            this.DataContext = this;

        }

        private void OKBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder error = new StringBuilder();
                if (string.IsNullOrWhiteSpace(TimeClientServiceTB.Text) || PetsCB.Text.Trim() == "" || ServiceCB.Text.Trim() == "" || ClientsCB.Text.Trim() == "" || DateClientServiceDP.SelectedDate == null)
                {
                    error.AppendLine("Заполните все поля!");
                    return;
                }
                
                if (error.Length > 0)
                {
                    MessageBox.Show(error.ToString());
                }

                if (DateClientServiceDP.SelectedDate != null && (DateTime.Now > (DateTime)DateClientServiceDP.SelectedDate))
                {
                    MessageBox.Show("Невозможно записаться уже в прошедшее время", "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var a = PetsCB.SelectedItem as Pets;

                var b = ClientsCB.SelectedItem as Clients;

                var c = ServiceCB.SelectedItem as Service;

                var result = MessageBox.Show($"Проверьте верность введенных данных:\nФИО: {b.LastName_Client} {b.FirstName_Client} {b.Patronymic_Client}, Животное {a.Name_Pet}" +
                    $"\nДата записи: {DateClientServiceDP.SelectedDate}, {TimeClientServiceTB.Text}" +
                    $"\nУслуга:, {c.Name_Service}, " +
                    $"\nЖалобы: {ReasonTB.Text}", "",
                    MessageBoxButton.YesNo, MessageBoxImage.Asterisk);

                if (result == MessageBoxResult.Yes)
                {
                    DateTime selectedDate = DateClientServiceDP.SelectedDate ?? DateTime.Now;
                    string timeText = TimeClientServiceTB.Text;
                    TimeSpan timeSpan;

                    if (!TimeSpan.TryParse(timeText, out timeSpan))
                    {
                        MessageBox.Show("Введите корректное время в формате HH:mm.");
                        return;
                    }

                    // Объединяем дату и время
                    DateTime dateTimeToSave = selectedDate.Date + timeSpan;

                    appointment.DateAppointment = dateTimeToSave;
                    appointment.ID_Client = b.ID_Client;
                    appointment.ID_Pet = a.ID_Pet;
                    appointment.ID_Service = c.ID_Service;
                    appointment.Reason = ReasonTB.Text;
                    appointment.ID_AppointmentStatus = 1;

                    DBConnection.veterinary.Appointment.Add(appointment);
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

        private void ClientsCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Получаем выбранного клиента
            var selectedClient = ClientsCB.SelectedItem as Clients; // Предполагаем, что Client - это ваш класс клиента

            if (selectedClient != null)
            {
                // Получаем список животных, принадлежащих выбранному клиенту
                var petsForSelectedClient = GetPetsForClient(selectedClient.ID_Client); // Замените на вашу логику получения животных

                // Устанавливаем источник данных для ComboBox с животными
                PetsCB.ItemsSource = petsForSelectedClient;
            }
            else
            {
                // Если клиент не выбран, очищаем ComboBox
                PetsCB.ItemsSource = null;
            }
        }

        // Метод для получения животных по ID клиента
        private List<Pets> GetPetsForClient(int clientId)
        {
            // Здесь должна быть ваша логика для получения животных из базы данных или другой структуры данных
            return pets.Where(pet => pet.ID_Client == clientId).ToList(); // petsList - это ваша коллекция животных
        }
    }
}
