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

namespace VeterinaryClinic.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminMainPage.xaml
    /// </summary>
    public partial class AdminMainPage : Page
    {
        public static List<AppointmentStatus> statuses { get; set; }
        public static List<Appointment> shedules { get; set; }

        public static List<Service> services { get; set; }
        public static List<Clients> clients { get; set; }
        public static List<Pets> pets { get; set; }

        public static List<Employees> employees { get; set; }
        public AdminMainPage()
        {
            InitializeComponent();
            statuses = new List<AppointmentStatus>(DBConnection.veterinary.AppointmentStatus.ToList());
            shedules = new List<Appointment>(DBConnection.veterinary.Appointment.ToList());
            services = new List<Service>(DBConnection.veterinary.Service.ToList());
            pets = new List<Pets>(DBConnection.veterinary.Pets.ToList());
            clients = new List<Clients>(DBConnection.veterinary.Clients.ToList());
            employees = new List<Employees>(DBConnection.veterinary.Employees.ToList());

            APPLV.ItemsSource = shedules;

            ApplyFilters();

            this.DataContext = this;
        }

        private void ApplyFilters()
        {
            List<Appointment> filteredShedules = new List<Appointment>(shedules);

            if (!string.IsNullOrEmpty(SearchDoctorTbx.Text))
            {
                string searchText = SearchDoctorTbx.Text.Trim().ToLower();
                filteredShedules = filteredShedules.Where(i => i.Clients.LastName_Client.ToLower().StartsWith(searchText)).ToList();
            }

            if (StatusCb.SelectedIndex > -1)
            {
                int selectedStatus = StatusCb.SelectedIndex + 1;
                filteredShedules = filteredShedules.Where(x => x.ID_AppointmentStatus == selectedStatus).ToList();
            }

            if (DisCb.SelectedIndex > -1)
            {
                int selectedDis = DisCb.SelectedIndex + 1;
                filteredShedules = filteredShedules.Where(x => x.ID_Service == selectedDis).ToList();
            }

            if (DateDp.SelectedDate.HasValue)
            {
                DateTime selectedDate = DateDp.SelectedDate.Value.Date;
                filteredShedules = filteredShedules.Where(x => x.DateAppointment.Date == selectedDate).ToList();
            }

            APPLV.ItemsSource = filteredShedules;
        }

        //private void BackBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    //NavigationService.Navigate(new AuthorizationPage());
        //}


        //private void Grathick2Btn_Click(object sender, RoutedEventArgs e)
        //{
        //    //NavigationService.Navigate(new SecondGrathickPage());
        //}

        private void DisCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }
        private void StatusCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void SearchDoctorTbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void DateDp_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void DateDp_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void Grathick1Btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminsPage());
        }
    }
}
