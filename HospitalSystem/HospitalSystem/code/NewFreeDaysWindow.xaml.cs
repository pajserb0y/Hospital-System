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
    /// Interaction logic for NewFreeDaysWindow.xaml
    /// </summary>
    public partial class NewFreeDaysWindow : Window
    {
        Doctor currentDoctor;
        private object selectedItem;

        public NewFreeDaysWindow(Doctor selectedDoctor)
        {
            currentDoctor = selectedDoctor;
            InitializeComponent();
        }

        private void txbSave_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            DateTime date = (DateTime)datePickerFrom.SelectedDate;
            DateTime endDate = (DateTime)datePickerTo.SelectedDate;
            while (date <= endDate)
            {
                currentDoctor.FreeDays.Add(date);
                date = date.AddDays(1);
            }
            this.Close();
        }
    }
}
