using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace HospitalSystem.code
{
    class HospitalizationService
    {
        private static Examination currExam;
        public static Examination CurrExam
        {
            get { return currExam; }
            set
            {
                if (currExam != value)
                {
                    currExam = value;
                   // OnPropertyChanged("CurrExam");
                }
            }
        }
        private static TextBox txtHospitalizationPatient;
        public static  TextBox TxtHospitalizationPatient
        { 
            get { return txtHospitalizationPatient; }
            set
            {
                if (txtHospitalizationPatient != value)
                {
                    txtHospitalizationPatient = value;
                   // OnPropertyChanged("TxtHospitalizationPatient");
                }
            }
        }

        private static DatePicker dpHospitalizationIN;
        public static DatePicker DpHospitalizationIN
        {
            get { return dpHospitalizationIN; }
            set
            {
                if (dpHospitalizationIN != value)
                {
                    dpHospitalizationIN = value;
                    //OnPropertyChanged("DpHospitalizationIN");
                }
            }
        }

        private static DatePicker dpHospitalizationOUT;
        public static DatePicker DpHospitalizationOUT
        {
            get { return dpHospitalizationOUT; }
            set
            {
                if (dpHospitalizationOUT != value)
                {
                    dpHospitalizationOUT = value;
                    //OnPropertyChanged("DpHospitalizationOUT");
                }
            }
        }

        private static ComboBox cbHospitalizationRoom;
        public static ComboBox CbHospitalizationRoom
        {
            get { return cbHospitalizationRoom; }
            set
            {
                if (cbHospitalizationRoom != value)
                {
                    cbHospitalizationRoom = value;
                    //OnPropertyChanged("CbHospitalizationRoom");
                }
            }

        }


        private static ComboBox cbHospitalizatonBed;
        public static ComboBox CbHospitalizatonBed
        {
            get { return cbHospitalizatonBed; }
            set
            {
                if (cbHospitalizatonBed != value)
                {
                    cbHospitalizatonBed = value;
                    //OnPropertyChanged("CbHospitalizatonBed");
                }
            }

        }

        private static TabItem tHospitalization;
        public static TabItem THospitalization
        {
            get { return tHospitalization; }
            set
            {
                if (tHospitalization != value)
                {
                    tHospitalization = value;
                    //OnPropertyChanged("THospitalization");
                }
            }
        }
        public static void HospitalizationServiceInitial(Examination currEx, TextBox txtHospitalizationPat, DatePicker dpHospIN, DatePicker dpHospOUT,ComboBox cbHospRoom,ComboBox cbHospBed, TabItem tHosp)
        {
            currExam = currEx;
            txtHospitalizationPatient = txtHospitalizationPat;
            dpHospitalizationIN = dpHospIN;
            dpHospitalizationOUT = dpHospOUT;
            cbHospitalizationRoom = cbHospRoom;
            cbHospitalizatonBed = cbHospBed;
            THospitalization = tHosp;
            InitializeComponents();
        }

        public static void InitializeComponents()
        {
            txtHospitalizationPatient.Text = Convert.ToString(currExam.Patient);
            txtHospitalizationPatient.IsEnabled = false;

            cbHospitalizationRoom.SelectedIndex = -1;
            cbHospitalizatonBed.SelectedIndex = -1;

            dpHospitalizationIN.SelectedDate = null;
            dpHospitalizationOUT.SelectedDate = null;
            dpHospitalizationIN.BlackoutDates.Add(new CalendarDateRange(DateTime.Now.AddYears(-1), DateTime.Now.AddDays(-1)));
            dpHospitalizationOUT.BlackoutDates.Add(new CalendarDateRange(DateTime.Now.AddYears(-1), DateTime.Now.AddDays(-1)));

            tHospitalization.Visibility = Visibility.Visible;
            tHospitalization.Focus();
        }


        public static void Save_Hospitalization(TabItem tExam)
        {

                Room selectedRoom = (Room)cbHospitalizationRoom.SelectedItem;
                Bed selectedBed = (Bed)cbHospitalizatonBed.SelectedItem;
                DateTime inTime = (DateTime)dpHospitalizationIN.SelectedDate;
                DateTime outTime = (DateTime)dpHospitalizationOUT.SelectedDate;
                Patient selectedPatient = (Patient)currExam.Patient;
                foreach (Bed b in selectedRoom.Beds)
                {
                    if (b == selectedBed)
                    {

                        b.Interval.Add(Tuple.Create(inTime, outTime, selectedPatient.Id));
                        break;
                    }
                }
                RoomStorage.getInstance().Edit(selectedRoom);
                tHospitalization.Visibility = Visibility.Collapsed;
                tExam.Focus();
        }

        public static void fillListOfAvailableRooms(List<Bed> beds)
        {
            if (dpHospitalizationIN.SelectedDate != null && dpHospitalizationOUT.SelectedDate != null)
            {
                DateTime inTime = (DateTime)dpHospitalizationIN.SelectedDate;
                DateTime outTime = (DateTime)dpHospitalizationOUT.SelectedDate;
                ListCollectionView roomCollectionView = new ListCollectionView(RoomStorage.getInstance().GetAll());
                FilterAvalablRooms(inTime, outTime, roomCollectionView);
                cbHospitalizationRoom.ItemsSource = roomCollectionView;
            }
        }

        private static void FilterAvalablRooms(DateTime inTime, DateTime outTime, ListCollectionView roomCollectionView)
        {
            roomCollectionView.Filter = (room) =>
            {
                Room tempRoom = room as Room;
                if (tempRoom.Name == "Room")
                {
                    foreach (Bed tempBed in tempRoom.Beds)
                    {
                        foreach (Tuple<DateTime, DateTime, int> val in tempBed.Interval)
                        {
                            if (val.Item1 <= inTime && val.Item2 >= inTime)
                                return false;
                            if (val.Item1 <= outTime && val.Item2 >= outTime)
                                return false;
                            if (val.Item1 >= inTime && val.Item2 <= outTime)
                                return false;
                            return true;
                        }
                    }
                }
                return false;
            };
        }

        public static void TimeIn_Changed()
        {
            if (dpHospitalizationIN.SelectedDate != null)
            {
                dpHospitalizationOUT.SelectedDate = null;
                dpHospitalizationOUT.BlackoutDates.Clear();
                dpHospitalizationOUT.BlackoutDates.Add(new CalendarDateRange(DateTime.Now.AddYears(-1), ((DateTime)dpHospitalizationIN.SelectedDate).AddDays(-1)));
            }
            else
            {
                dpHospitalizationOUT.SelectedDate = null;
                dpHospitalizationOUT.BlackoutDates.Clear();
                dpHospitalizationOUT.BlackoutDates.Add(new CalendarDateRange(DateTime.Now.AddYears(-1), DateTime.Now.AddDays(-1)));
            }
        }

        public static void fillListOfAvailableBeds()
        {
            if (cbHospitalizationRoom.SelectedIndex != -1)
            {

                DateTime inTime = (DateTime)dpHospitalizationIN.SelectedDate;
                DateTime outTime = (DateTime)dpHospitalizationOUT.SelectedDate;
                Room selectedRoom = (Room)cbHospitalizationRoom.SelectedItem;
                ListCollectionView bedCollectionView = new ListCollectionView(selectedRoom.Beds);
                NewMethod(inTime, outTime, bedCollectionView);
                cbHospitalizatonBed.ItemsSource = bedCollectionView;
            }
        }

        private static void NewMethod(DateTime inTime, DateTime outTime, ListCollectionView bedCollectionView)
        {
            bedCollectionView.Filter = (bed) =>
            {
                Bed tempBed = bed as Bed;

                foreach (Tuple<DateTime, DateTime, int> val in tempBed.Interval)
                {
                    if (val.Item1 <= inTime && val.Item2 >= inTime)
                        return false;
                    if (val.Item1 <= outTime && val.Item2 >= outTime)
                        return false;
                    if (val.Item1 >= inTime && val.Item2 <= outTime)
                        return false;
                    return true;
                }
                return false;
            };
        }

        //public  event PropertyChangedEventHandler PropertyChanged;
        //protected  virtual void OnPropertyChanged(string name)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(name));
        //    }
        //}
    }
}
