using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for AnnouncementWindow.xaml
    /// </summary>
    public partial class AnnouncementWindow : Window
    {
        public AnnouncementWindow()
        {
            InitializeComponent();
            tabDoctors.Visibility = Visibility.Collapsed;
            tabPatients.Visibility = Visibility.Collapsed;
            txtTime.Text = DateTime.Now.ToString("HH:mm");

            listViewDoctors.ItemsSource = DoctorStorage.getInstance().GetAll();
            listViewPatients.ItemsSource = PatientCRUDMenager.getInstance().GetAll();
        }
        public AnnouncementWindow(Announcement selectedAnnouncement)
        {
            InitializeComponent();
            initializeSelectedAnnouncementDetails(selectedAnnouncement);

            hideUnimportantDetails();
        }

        private void initializeSelectedAnnouncementDetails(Announcement selectedAnnouncement)
        {
            dpDate.SelectedDate = selectedAnnouncement.Date;
            txtTime.Text = selectedAnnouncement.Time.ToString("HH:mm");
            txtTitle.Text = selectedAnnouncement.Title;
            richTextBoxMessage.Document.Blocks.Clear();
            richTextBoxMessage.Document.Blocks.Add(new Paragraph(new Run(selectedAnnouncement.Content)));

            txtTitle.IsReadOnly = true;
            richTextBoxMessage.IsReadOnly = true;
        }

        private void hideUnimportantDetails()
        {
            checkBoxDoctors.Visibility = Visibility.Collapsed;
            checkBoxPatients.Visibility = Visibility.Collapsed;
            buttonChooseDoctors.Visibility = Visibility.Collapsed;
            buttonChoosePatients.Visibility = Visibility.Collapsed;
            textBlockSend.Visibility = Visibility.Collapsed;
            tabDoctors.Visibility = Visibility.Collapsed;
            tabPatients.Visibility = Visibility.Collapsed;
        }

        private void buttonChooseDoctors_Click(object sender, RoutedEventArgs e)
        {
            tabDoctors.Visibility = Visibility.Visible;            
            tabDoctors.Focus();
        }
        private void buttonChoosePatients_Click(object sender, RoutedEventArgs e)
        {
            tabPatients.Visibility = Visibility.Visible;           
            tabPatients.Focus();
        }
        private void CheckBoxDoctorsChanged(object sender, RoutedEventArgs e)
        {
            if (checkBoxDoctors.IsChecked == true)
                listViewDoctors.SelectAll();
            else
                listViewDoctors.SelectedItems.Clear();
        }
        private void CheckBoxPatientsChanged(object sender, RoutedEventArgs e)
        {
            if (checkBoxPatients.IsChecked == true)
                listViewPatients.SelectAll();
            else
                listViewPatients.SelectedItems.Clear();
        }
        private void txbSend_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            TextRange content = new TextRange(richTextBoxMessage.Document.ContentStart, richTextBoxMessage.Document.ContentEnd);

            List<Doctor> selectedDoctors = new List<Doctor>(castFromcollectionToIListDoctors());
            List<Patient> selectedPatients = new List<Patient>(castFromCollectionToIListPatients());

            List<int> selectedDoctorIDs = new List<int>();
            getIDsFromDoctors(selectedDoctors, selectedDoctorIDs);
            List<int> selectedPatientIDs = new List<int>();
            getIDsFromPatients(selectedPatients, selectedPatientIDs);

            Announcement announcement = new Announcement(selectedDoctorIDs, selectedPatientIDs, (DateTime)dpDate.SelectedDate, Convert.ToDateTime(txtTime.Text), txtTitle.Text, content.Text);
            AnnouncementStorage.getInstance().Add(announcement);
            this.Close();
        }             

        private static void getIDsFromDoctors(List<Doctor> selectedDoctors, List<int> selectedDoctorIDs)
        {
            foreach (Doctor dr in selectedDoctors)
                selectedDoctorIDs.Add(dr.Id);
        }
        private static void getIDsFromPatients(List<Patient> selectedPatients, List<int> selectedPatientIDs)
        {
            foreach (Patient pt in selectedPatients)
                selectedPatientIDs.Add(pt.Id);
        }

        private IEnumerable<Doctor> castFromcollectionToIListDoctors()
        {
            System.Collections.IList items = (System.Collections.IList)listViewDoctors.SelectedItems;
            var castedItems = items.Cast<Doctor>();
            return castedItems;
        }
        private IEnumerable<Patient> castFromCollectionToIListPatients()
        {
            System.Collections.IList items1 = (System.Collections.IList)listViewPatients.SelectedItems;
            var itemss1 = items1.Cast<Patient>();
            return itemss1;
        }
    }
}
