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
using System.Windows.Navigation;
using System.Windows.Shapes;
using VeterinaryClinic.DB;
using VeterinaryClinic.Windowws.RegistrarW;
using VeterinaryClinic.Windowws.VeterW;

namespace VeterinaryClinic.Pages.Veterinarian
{
    /// <summary>
    /// Логика взаимодействия для MainPageVet.xaml
    /// </summary>
    public partial class MainPageVet : Page
    {
        public static List<Appointment> appointments { get; set; }
        public static List<Clients> clients { get; set; }
        public static List<Pets> pets { get; set; }
        public static List<Service> services { get; set; }

        public MainPageVet()
        {
            InitializeComponent();
            Rehresh();

            this.DataContext = this;
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
                Clients_PetsLV.ItemsSource = pets;
                await Task.Delay(30000);
            }
        }

        private void AppointmensLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = AppointmensLV.SelectedItem as Appointment;
            Clients_PetsLV.ItemsSource = new List<Pets>(DBConnection.veterinary.Pets.Where(x => x.ID_Client == a.ID_Client).ToList());

            this.DataContext = this;
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthPage());
        }

        //private void Clients_PetsLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var context_pet = Clients_PetsLV.SelectedItem as Pets;
        //    MedicalRecordsWin addMedicalRecordWindow = new MedicalRecordsWin(context_pet);
        //    addMedicalRecordWindow.ShowDialog();

        //    Rehresh();
        //}

        private void MedicalRBTN_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Pets context_pet)
            {
                Pets selectedpet = context_pet;

                MedicalRecordsWin addMedicalRecordWindow = new MedicalRecordsWin(selectedpet);
                addMedicalRecordWindow.ShowDialog();
            }
        }
    }
}
