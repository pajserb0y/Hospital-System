using HospitalSystem.code.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    /// Interaction logic for EditDoctor.xaml
    /// </summary>
    public partial class EditDoctor : Window
    {
        private Doctor currentDoctor;
        public EditDoctor(Doctor selectedDoctor)
        {
            this.Closed += new EventHandler(Window_Closed);
            InitializeComponent();

            currentDoctor = selectedDoctor;
            initializeSelectedDoctorDetails(selectedDoctor);
            setButtonImages();
        }

        private void initializeSelectedDoctorDetails(Doctor selectedDoctor)
        {
            txtID.Text = selectedDoctor.Id.ToString();
            txtFirstName.Text = selectedDoctor.FirstName;
            txtLastName.Text = selectedDoctor.LastName;
            txtJmbg.Text = selectedDoctor.Jmbg.ToString();
            txtAdress.Text = selectedDoctor.Adress;
            txtTelephone.Text = selectedDoctor.Telephone.ToString();
            txtSpecialization.Text = selectedDoctor.Specialization;
            //txtUsername.Text = selectedDoctor.Username;
            //txtPassword.Text = selectedDoctor.Password;

            fillFreeDaysList(selectedDoctor);
            filterShiftsAndFillList();
        }

        private void fillFreeDaysList(Doctor selectedDoctor)
        {
            if (selectedDoctor.FreeDays == null)
                selectedDoctor.FreeDays = new ObservableCollection<DateTime>();
            else
            {
                List<DateTime> freeDays = new List<DateTime>(selectedDoctor.FreeDays);
                foreach (DateTime freeDay in freeDays)
                    if (freeDay.Year != DateTime.Now.Year)
                        selectedDoctor.FreeDays.Remove(freeDay);
            }
            freeDaysList.ItemsSource = selectedDoctor.FreeDays;
        }

        private void filterShiftsAndFillList()
        {
            ListCollectionView shiftsCollection = new ListCollectionView(WorkingShiftStorage.getInstance().GetAll());
            shiftsCollection.Filter = (shift) =>
            {
                WorkingShift workingShift = shift as WorkingShift;
                if (workingShift.DoctorId == currentDoctor.Id)
                    return true;
                return false;
            };
            workingShiftsList.ItemsSource = shiftsCollection;
        }

        private void setButtonImages()
        {
            buttonAddFreeDays.Background = new ImageBrush(new BitmapImage(new Uri(System.IO.Path.GetFullPath("../../../Images/add.jpg"))));
            buttonDeleteFreeDays.Background = new ImageBrush(new BitmapImage(new Uri(System.IO.Path.GetFullPath("../../../Images/delete.jpg"))));
            buttonAddShift.Background = new ImageBrush(new BitmapImage(new Uri(System.IO.Path.GetFullPath("../../../Images/add.jpg"))));
            buttonEditShift.Background = new ImageBrush(new BitmapImage(new Uri(System.IO.Path.GetFullPath("../../../Images/edit.jpg"))));
            buttonDeleteShift.Background = new ImageBrush(new BitmapImage(new Uri(System.IO.Path.GetFullPath("../../../Images/delete.jpg"))));
        }

        private void txbSave_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            DoctorStorage.getInstance().Edit(new Doctor(currentDoctor.Id, txtFirstName.Text, txtLastName.Text, Convert.ToInt64(txtJmbg.Text), txtAdress.Text,
                Convert.ToInt64(txtTelephone.Text), txtSpecialization.Text, currentDoctor.FreeDays));
            DoctorStorage.getInstance().serialize();
            this.Close();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            //DoctorStorage.getInstance().serialize();
            this.Close();
        }

        private void buttonAddFreeDays_Click(object sender, RoutedEventArgs e)
        {
            if (datePickerStart.SelectedDate != null && datePickerEnd.SelectedDate != null)
            {
                DateTime date = (DateTime)datePickerStart.SelectedDate;
                DateTime endDate = (DateTime)datePickerEnd.SelectedDate;
                checkFreeDaysLengthAndFillList(date, endDate);
                datePickerStart.SelectedDate = null;
                datePickerEnd.SelectedDate = null;
            }
        }

        private void checkFreeDaysLengthAndFillList(DateTime date, DateTime endDate)
        {
            while (date <= endDate)
            {
                if (currentDoctor.FreeDays.Contains(date) || date < DateTime.Now.Date)
                {
                    date = date.AddDays(1);
                    continue;
                }
                else if (currentDoctor.FreeDays.Count <= 30)
                {
                    currentDoctor.FreeDays.Add(date);
                    date = date.AddDays(1);
                }
                else
                {
                    MessageBox.Show("Nije moguce imati vise od 30 slobodnih dana godisnje!");
                    break;
                }
            }
        }

        private void buttonDeleteFreeDays_Click(object sender, RoutedEventArgs e)
        {
            List<DateTime> tempList = new List<DateTime>();
            foreach (DateTime selectedDate in freeDaysList.SelectedItems)
                tempList.Add(selectedDate);

            foreach (DateTime date in tempList)
                currentDoctor.FreeDays.Remove(date);
        }

        private void buttonAddShift_Click(object sender, RoutedEventArgs e)
        {
            NewShiftWindow newShiftWindow = new NewShiftWindow(currentDoctor.Id);
            newShiftWindow.Show();
        }

        private void buttonEditShift_Click(object sender, RoutedEventArgs e)
        {
            NewShiftWindow newShiftWindow = new NewShiftWindow((WorkingShift)workingShiftsList.SelectedItem);
            newShiftWindow.Show();
        }

        private void buttonDeleteShift_Click(object sender, RoutedEventArgs e)
        {
            WorkingShiftStorage.getInstance().Delete((WorkingShift)workingShiftsList.SelectedItem);
        }     
    }
}
