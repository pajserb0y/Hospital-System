using HospitalSystem.code.Model;
using System;
using System.Windows;
using System.Windows.Threading;

namespace HospitalSystem.code
{
    /// <summary>
    /// Interaction logic for Demo.xaml
    /// </summary>
    public partial class Demo : Window
    {
        DispatcherTimer timer;
        private DoctorsWindow demoWindow;
        private int step = -1;
        private EditDoctor editDoctorDemo;
        private NewShiftWindow newShiftWindowDemo;
        Doctor currentDoctor;
        public Demo()
        {
            InitializeComponent();

            //demoWindow = new DoctorsWindow();
            //demoWindow.Show();
            //txtDemo.Focus();

            timer = new DispatcherTimer();
            timer.Tick += startDemo;

            timer.Interval = new TimeSpan(0, 0, 0, 0, 1500);
            timer.Start();
        }



        private void startDemo(object sender, EventArgs e)
        {
            if (step == -1)
            {
                demoWindow = new DoctorsWindow();
                demoWindow.Show();
                step++;
                txtDemo.Focus();
            }
            if (step == 0)
            {
                demoWindow.dataGridDoctors.SelectedIndex = 4;
                demoWindow.imageArrow.Visibility = Visibility.Visible;
                demoWindow.elipseView.Visibility = Visibility.Visible;
                step++;
                txtDemo.Focus();
            }
            else if (step == 1)
            {
                editDoctorDemo = new EditDoctor((Doctor)demoWindow.dataGridDoctors.SelectedItem);
                editDoctorDemo.Show();
                editDoctorDemo.imageArrow.Visibility = Visibility.Visible;

                demoWindow.Close();
                step++;
                txtDemo.Focus();
            }
            else if (step == 2)
            {
                currentDoctor = (Doctor)demoWindow.dataGridDoctors.SelectedItem;
                newShiftWindowDemo = new NewShiftWindow(currentDoctor.Id);
                newShiftWindowDemo.Show();

                step++;
                txtDemo.Focus();
            }
            else if (step == 3)
            {
                newShiftWindowDemo.dpStartDate.IsDropDownOpen = true;
                step++;
                txtDemo.Focus();
            }
            else if (step == 4)
            {
                newShiftWindowDemo.dpStartDate.SelectedDate = DateTime.Now.Date;
                step++;
                txtDemo.Focus();
            }
            else if (step == 5)
            {
                newShiftWindowDemo.dpStartDate.IsDropDownOpen = false;
                newShiftWindowDemo.dpEndDate.IsDropDownOpen = true;
                step++;
                txtDemo.Focus();
            }
            else if (step == 6)
            {
                newShiftWindowDemo.dpEndDate.SelectedDate = DateTime.Now.Date.AddDays(4);
                step++;
                txtDemo.Focus();
            }
            else if (step == 7)
            {
                newShiftWindowDemo.dpEndDate.IsDropDownOpen = false;
                newShiftWindowDemo.comboBoxStartTime.IsDropDownOpen = true;
                step++;
                txtDemo.Focus();
            }
            else if (step == 8)
            {
                newShiftWindowDemo.comboBoxStartTime.SelectedIndex = 5;
                step++;
                txtDemo.Focus();
            }
            else if (step == 9)
            {
                newShiftWindowDemo.comboBoxStartTime.IsDropDownOpen = false;
                newShiftWindowDemo.comboBoxEndTime.IsDropDownOpen = true;
                step++;
                txtDemo.Focus();
            }
            else if (step == 10)
            {
                newShiftWindowDemo.comboBoxEndTime.SelectedIndex = 10;
                newShiftWindowDemo.comboBoxEndTime.IsDropDownOpen = false;
                newShiftWindowDemo.elipseSave.Visibility = Visibility.Visible;
                step++;
                txtDemo.Focus();
            }
            else if (step == 11)
            {
                WorkingShiftStorage.getInstance().Add(new WorkingShift(WorkingShiftStorage.getInstance().GenerateNewID(), currentDoctor.Id,
                        (DateTime)Convert.ToDateTime(newShiftWindowDemo.comboBoxStartTime.SelectedItem.ToString()), (DateTime)Convert.ToDateTime(newShiftWindowDemo.comboBoxEndTime.SelectedItem.ToString()),
                        (DateTime)newShiftWindowDemo.dpStartDate.SelectedDate, (DateTime)newShiftWindowDemo.dpEndDate.SelectedDate));
                newShiftWindowDemo.Close();
                editDoctorDemo.elipseSave.Visibility = Visibility.Visible;
                step++;
                txtDemo.Focus();
            }
            else if (step == 12)
            {
                timer.Stop();
                DoctorStorage.getInstance().Edit(new Doctor(currentDoctor.Id, editDoctorDemo.txtFirstName.Text, editDoctorDemo.txtLastName.Text, Convert.ToInt64(editDoctorDemo.txtJmbg.Text),
                    editDoctorDemo.txtAdress.Text, Convert.ToInt64(editDoctorDemo.txtTelephone.Text), editDoctorDemo.txtSpecialization.Text, currentDoctor.FreeDays));
                DoctorStorage.getInstance().serialize();
                WorkingShiftStorage.getInstance().serialize();
                editDoctorDemo.Close();
                this.Close();
            }
        }

        public void txtDemo_Changed(object sender, EventArgs e)
        {
            try
            {
                timer.Stop();
                this.Close();
                demoWindow.Close();
                if (editDoctorDemo != null)
                    editDoctorDemo.Close();
                if (newShiftWindowDemo != null)
                    newShiftWindowDemo.Close();                
            }
            catch
            {
                MessageBoxResult rsltMessageBox = System.Windows.MessageBox.Show("Demo mode is closing", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {

            }
        }
    }
}
