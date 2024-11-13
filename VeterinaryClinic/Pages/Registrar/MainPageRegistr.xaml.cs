using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using VeterinaryClinic.DB;
using VeterinaryClinic.Windowws.RegistrarW;

namespace VeterinaryClinic.Pages.Registrar
{
    /// <summary>
    /// Логика взаимодействия для MainPageRegistr.xaml
    /// </summary>
    public partial class MainPageRegistr : Page
    {
        public static List<Appointment> appointments {  get; set; }
        public static List<Clients> clients { get; set; }
        public static List<Pets> pets { get; set; }
        public static List<Service> services { get; set; }

        public MainPageRegistr()
        {
            InitializeComponent();
            Rehresh();
        }

        public async void Rehresh()
        {
            while (true)
            {
                DateTime today = DateTime.Now;
                DateTime tomorrow = today.AddDays(1);

                clients = new List<Clients>(DBConnection.veterinary.Clients.ToList());
                pets = new List<Pets>(DBConnection.veterinary.Pets.ToList());
                services = new List<Service>(DBConnection.veterinary.Service.ToList());

                //appointments = new List<Appointment>(DBConnection.veterinary.Appointment.ToList());

                appointments = new List<Appointment>(DBConnection.veterinary.Appointment.
                    Where(x => (DateTime)x.DateAppointment >= today && (DateTime)x.DateAppointment <= tomorrow).OrderBy(x => (DateTime)x.DateAppointment).ToList());

                AppointmensLV.ItemsSource = appointments;
                await Task.Delay(30000);
            }
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e) //Назад
        {
            NavigationService.Navigate(new AuthPage());
        }

        private void AddClientBTN_Click(object sender, RoutedEventArgs e) //Новый клиент
        {
            AddClientReg addClientWindow = new AddClientReg();
            addClientWindow.ShowDialog();
        }

        private void AddPetBTN_Click(object sender, RoutedEventArgs e) //Новое животное
        {
            AddPetReg addPetWindow = new AddPetReg();
            addPetWindow.ShowDialog();
        }

        private void AddAppBTN_Click(object sender, RoutedEventArgs e) //Новая запись
        {
            AddAppointmentReg addAppointmentWindow = new AddAppointmentReg();
            addAppointmentWindow.ShowDialog();
        }

        private void AppointmentsBTN_Click(object sender, RoutedEventArgs e) //Все записи
        {
            AppointmentsRegW appointmentsWindow = new AppointmentsRegW();
            appointmentsWindow.Show();
        }

        private void ClientsBTN_Click(object sender, RoutedEventArgs e) //Все клиенты
        {
            ClientsRegW clientsWindow = new ClientsRegW();
            clientsWindow.Show();
        }

        private void PetsBTN_Click(object sender, RoutedEventArgs e) //Все животные
        {
            PetsRegW petsWindow = new PetsRegW();
            petsWindow.Show();
        }
    }
}
