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

namespace VeterinaryClinic.Windowws.VeterW
{
    /// <summary>
    /// Логика взаимодействия для MedicalRecordsWin.xaml
    /// </summary>
    public partial class MedicalRecordsWin : Window
    {
        public static List<Pets> pets {  get; set; }
        public static List<MedicalRecord> medicalRecords { get; set; }
        Pets context_pets;
        public MedicalRecordsWin(Pets pet)
        {
            InitializeComponent();
            context_pets = pet;
            NamePetTBX.Text = context_pets.Name_Pet;
            medicalRecords = new List<MedicalRecord>(DBConnection.veterinary.MedicalRecord.Where(x => x.ID_Pet == context_pets.ID_Pet).ToList());
            MedicalRecordLV.ItemsSource = medicalRecords;
            this.DataContext = this;
        }

        private void CancelBTN_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите выйти?", "", MessageBoxButton.YesNo, MessageBoxImage.Asterisk);

            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void OKBTN_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
