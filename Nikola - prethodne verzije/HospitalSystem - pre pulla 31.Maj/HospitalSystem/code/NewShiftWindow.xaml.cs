using HospitalSystem.code.Model;
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
    /// Interaction logic for NewShiftWindow.xaml
    /// </summary>
    public partial class NewShiftWindow : Window
    {
        List<string> terms = new List<string> { "07:00", "07:30", "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30",
                                                "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00", "16:30", "17:00", "17:30", "18:00", "18:30"};
        private int currentDoctorID;
        private int calledConstructor;
        private WorkingShift currentShift;

        public NewShiftWindow(int selectedDoctorID)
        {
            currentDoctorID = selectedDoctorID;
            InitializeComponent();
            comboBoxStartTime.ItemsSource = terms;
            comboBoxEndTime.ItemsSource = terms;
            calledConstructor = 1;
        }

        public NewShiftWindow(WorkingShift selectedShift)
        {
            currentShift = selectedShift;
            InitializeComponent();
            comboBoxStartTime.ItemsSource = terms;
            comboBoxEndTime.ItemsSource = terms;
            dpStartDate.SelectedDate = selectedShift.StartDate;
            dpEndDate.SelectedDate = selectedShift.EndDate;
            comboBoxStartTime.SelectedItem = selectedShift.StartTime.ToString("HH:mm");
            comboBoxEndTime.SelectedItem = selectedShift.EndTime.ToString("HH:mm");
            calledConstructor = 2;
        }

        private void txbSave_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (calledConstructor == 1)
                WorkingShiftStorage.getInstance().Add(new WorkingShift(WorkingShiftStorage.getInstance().GenerateNewID(), currentDoctorID, 
                    (DateTime)Convert.ToDateTime(comboBoxStartTime.SelectedItem.ToString()), (DateTime)Convert.ToDateTime(comboBoxEndTime.SelectedItem.ToString()),
                    (DateTime)dpStartDate.SelectedDate, (DateTime)dpEndDate.SelectedDate));
            else
                WorkingShiftStorage.getInstance().Edit(new WorkingShift(currentShift.Id, currentShift.DoctorId, (DateTime)Convert.ToDateTime(comboBoxStartTime.SelectedItem.ToString()), 
                    (DateTime)Convert.ToDateTime(comboBoxEndTime.SelectedItem.ToString()), (DateTime)dpStartDate.SelectedDate, (DateTime)dpEndDate.SelectedDate));

            WorkingShiftStorage.getInstance().serialize();
            this.Close();
        }

        private void startTimeChanged(object sender, System.EventArgs e)
        {
            int k = 0;
            List<string> endTimeTerms = new List<string>();
            foreach (string term in terms)
            {
                if (k == 1)
                    endTimeTerms.Add(term);
                if (term == comboBoxStartTime.SelectedItem.ToString())
                    k = 1;
            }
            comboBoxEndTime.ItemsSource = endTimeTerms;
        }
    }
}
