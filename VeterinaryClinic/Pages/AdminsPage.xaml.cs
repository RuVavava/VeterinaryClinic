using LiveCharts.Wpf;
using LiveCharts;
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
    /// Логика взаимодействия для AdminsPage.xaml
    /// </summary>
    public partial class AdminsPage : Page
    {
        public static List<Service> services { get; set; }
        public SeriesCollection ServicePopularitySeriesCollection { get; set; }
        public List<string> ServiceNames { get; set; }
        public AdminsPage()
        {
            InitializeComponent();
            DataContext = this;

            services = DBConnection.veterinary.Service.ToList();
            if (services == null || !services.Any())
            {
                services = new List<Service>();
                MessageBox.Show("Services list is empty or null.");
            }
            else
            {
                MessageBox.Show($"Loaded {services.Count} services.");
            }

            var filteredShedules = DBConnection.veterinary.Appointment.ToList();
            if (filteredShedules != null && filteredShedules.Any())
            {
                UpdateServicePopularityChart(filteredShedules);
            }
            else
            {
                MessageBox.Show("No filtered schedules available.");
            }
        }

        private void UpdateServicePopularityChart(List<Appointment> filteredShedules)
        {
            var serviceCounts = services.Select(service => new
            {
                Service = service.Name_Service,
                Count = filteredShedules.Count(s => s.ID_Service == service.ID_Service)
            }).ToList();

            ServiceNames = serviceCounts.Select(x => x.Service).ToList();
            var values = serviceCounts.Select(x => (double)x.Count).ToArray();

            var hexColor = "#CC05A0FF";
            var color = (Color)ColorConverter.ConvertFromString(hexColor);

            ServicePopularitySeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Service Popularity",
                    Values = new ChartValues<double>(values),
                    Fill = new SolidColorBrush(color)
                }
            };

            // Обновляем график
            ServicePopularityChart.Series = ServicePopularitySeriesCollection;
            ServicePopularityChart.AxisX[0].Labels = ServiceNames;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new AdminPage());
        }
    }
}
