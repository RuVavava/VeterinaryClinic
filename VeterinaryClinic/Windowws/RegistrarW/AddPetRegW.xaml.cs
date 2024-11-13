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
    /// Логика взаимодействия для AddPetReg.xaml
    /// </summary>
    public partial class AddPetReg : Window
    {
        public static List<Clients> clients { get; set; }
        public static List<Pets> pets { get; set; }
        public static List<PetType> petsType { get; set; }
        public static Pets pet = new Pets();
        public AddPetReg()
        {
            InitializeComponent();
            petsType = new List<PetType>(DB.DBConnection.veterinary.PetType.ToList());
            clients = new List<Clients>(DB.DBConnection.veterinary.Clients.ToList());

            TypePetCB.ItemsSource = petsType;
            ClientsCB.ItemsSource = clients;

            this.DataContext = this;
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
            try
            {
                StringBuilder error = new StringBuilder();
                if (string.IsNullOrWhiteSpace(NamePetTB.Text) || TypePetCB.Text.Trim() == "" || GenderCB.Text.Trim() == "" || BreedTB.Text.Trim() == "" || ColorTB.Text.Trim() == "" 
                    || AgeTB.Text.Trim() == "" || WeightTB.Text.Trim() == "" || ClientsCB.Text.Trim() == "")

                {
                    error.AppendLine("Заполните все поля!");
                    return;
                }

                if (error.Length > 0)
                {
                    MessageBox.Show(error.ToString());
                }

                var a = TypePetCB.SelectedItem as PetType;

                var b = ClientsCB.SelectedItem as Clients;


                var result = MessageBox.Show($"Проверьте верность введенных данных:\nКличка животного: {NamePetTB.Text}, \nВид животного: {a.Name_PetType} {GenderCB.Text}, " +
                    $"\nПорода и окрас:, {BreedTB.Text}, {ColorTB.Text} , \nКлиент: {b.LastName_Client} {b.FirstName_Client} {b.Patronymic_Client}", "",
                    MessageBoxButton.YesNo, MessageBoxImage.Asterisk);

                if (result == MessageBoxResult.Yes)
                {

                    pet.Name_Pet = NamePetTB.Text.Trim();

                    pet.ID_PetType = a.ID_PetType;

                    pet.ID_Client = b.ID_Client;

                    pet.Breed = BreedTB.Text.Trim();
                    pet.Age = Convert.ToInt32(AgeTB.Text.Trim());

                    if (GenderCB.SelectedIndex == 0)
                        pet.ID_Gender = 1;
                    else
                        pet.ID_Gender = 2;

                    pet.Color = ColorTB.Text.Trim();
                    pet.Weight = Convert.ToInt32(WeightTB.Text.Trim());



                    DBConnection.veterinary.Pets.Add(pet);
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
