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
    /// Interaction logic for NewExam.xaml
    /// </summary>
    public partial class NewExam : Window
    {
        public NewExam()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Examination exam = new Examination("tempExam");
            exam.Patient = new Patient(txtPatId.Text, txtPatName.Text, txtPatLastname.Text);
            exam.Doctor = new Doctor(txtDocId.Text, txtDocName.Text, txtDocLastname.Text);
            exam.Time = Convert.ToDateTime(txtDateTime.Text);
            ExaminationStorage.getInstance().Save(exam);
            List<Examination> ExamList = ExaminationStorage.getInstance().GetAll();
          // foreach (Examination exam in ExamList)
           //     DataGridXAML.Items.Add(exam);
        }
    }
}
