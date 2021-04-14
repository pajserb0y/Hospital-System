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
    /// Interaction logic for DoctorInitialWindow.xaml
    /// </summary>
    public partial class DoctorInitialWindow : Window
    {
        public DoctorInitialWindow()
        {
            this.Closed += new EventHandler(Window_Closed);
            InitializeComponent();
   
            ObservableCollection<Examination> exams = ExaminationStorage.getInstance().GetAll();
            DataGridXAML.ItemsSource = exams;
            //exam1.Time = new DateTime(2012, 12, 25, 10, 30, 50);
            //if(exams != null)
            //   foreach (Examination exam in exams)
            //       DataGridXAML.Items.Add(exam);

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ExaminationStorage.getInstance().serialize();
            this.Close();
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            NewExam ne = new NewExam();
           
            ne.Show();
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            var selectedItem = DataGridXAML.SelectedItem;
            if (selectedItem != null)
            {
                ExaminationStorage.getInstance().Delete((Examination)selectedItem);
                //DataGridXAML.Items.Remove(selectedItem);
            }
        }

        private void Button_Edit(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Examination> ExamList = ExaminationStorage.getInstance().GetAll();
            var selectedItem = DataGridXAML.SelectedItem;
            if (selectedItem != null)
            {
                EditExam ee = new EditExam((Examination)selectedItem);
                ee.Show();
                //DataGridXAML.Items.Remove(selectedItem);
            }
        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            ExaminationStorage.getInstance().serialize();
            mw.Show();
            this.Close();
        }
    }
}
