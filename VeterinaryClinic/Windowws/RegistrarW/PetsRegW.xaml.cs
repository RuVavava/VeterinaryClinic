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
    /// Логика взаимодействия для PetsRegW.xaml
    /// </summary>
    public partial class PetsRegW : Window
    {
        public static List<Pets> pets {  get; set; }
        public static List<Clients> clients { get; set; }
        public static List<PetType> petTypes { get; set; }
        public static List<Gender> genders { get; set; }
        public PetsRegW()
        {
            InitializeComponent();
            pets = new List<Pets>(DBConnection.veterinary.Pets.ToList());
            clients = new List<Clients>(DBConnection.veterinary.Clients.ToList());
            petTypes = new List<PetType>(DBConnection.veterinary.PetType.ToList());
            genders = new List<Gender>(DBConnection.veterinary.Gender.ToList());

            PetsLV.ItemsSource = pets;
        }
    }
}
