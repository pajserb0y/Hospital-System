﻿using System;
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
        ListCollectionView collectionView = new ListCollectionView(PrescriptionStorage.getInstance().GetAll());

        public MedicationWindow(Patient selectedItem)
        {
            InitializeComponent();

            collectionView.Filter = (e) =>
            {
                Prescription temp = e as Prescription;
                if (temp.PatientID == selectedItem.Id)
                    return true;
                return false;
            };

            dgMedication.ItemsSource = collectionView;
        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }
    }
}
