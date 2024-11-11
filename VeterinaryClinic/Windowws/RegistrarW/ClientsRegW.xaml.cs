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
    /// Логика взаимодействия для ClientsRegW.xaml
    /// </summary>
    public partial class ClientsRegW : Window
    {
        public static List<Clients> clients { get; set; }
        public ClientsRegW()
        {
            InitializeComponent();
            clients = new List<Clients>(DBConnection.veterinary.Clients.ToList());

            ClientsLV.ItemsSource = clients;
        }
    }
}
