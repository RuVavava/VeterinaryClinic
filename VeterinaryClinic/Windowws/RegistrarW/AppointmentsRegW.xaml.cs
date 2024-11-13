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
using VeterinaryClinic.DB;

namespace VeterinaryClinic.Windowws.RegistrarW
{
    /// <summary>
    /// Логика взаимодействия для AppointmentsRegW.xaml
    /// </summary>
    public partial class AppointmentsRegW : Window
    {
        public static List<Appointment> appointments { get; set; }
        public static List<Clients> clients { get; set; }
        public static List<Pets> pets { get; set; }
        public static List<PetType> petTypes { get; set; }
        public static List<Service> services { get; set; }
        public AppointmentsRegW()
        {
            InitializeComponent();
            clients = new List<Clients>(DBConnection.veterinary.Clients.ToList());
            pets = new List<Pets>(DBConnection.veterinary.Pets.ToList());
            services = new List<Service>(DBConnection.veterinary.Service.ToList());
            petTypes = new List<PetType>(DBConnection.veterinary.PetType.ToList());

            appointments = new List<Appointment>(DBConnection.veterinary.Appointment.ToList());

            AppoLV.ItemsSource = appointments;
        }
    }
}
