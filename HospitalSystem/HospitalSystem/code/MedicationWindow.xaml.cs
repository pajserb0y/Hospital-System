using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HospitalSystem.code
{
    /// <summary>
    /// Interaction logic for MedicationWindow.xaml
    /// </summary>
    public partial class MedicationWindow : Window
    {
        ListCollectionView collectionViewExam = new ListCollectionView(ExaminationStorage.getInstance().GetAll());

        public MedicationWindow(Patient selectedItem)
        {
            InitializeComponent();

            collectionViewExam.Filter = (e) =>
            {
                Examination temp = e as Examination;
                if (temp.Patient == selectedItem)
                    return true;
                return false;
            };

            dgMedication.ItemsSource = collectionViewExam;
        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
