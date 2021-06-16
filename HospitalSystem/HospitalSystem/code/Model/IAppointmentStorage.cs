using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HospitalSystem.code.Model
{
    interface IAppointmentStorage
    {

        public AppointmentViewModel copyAVM(Appointment a);

        public void serialize();

        public ObservableCollection<Appointment> deserialize();

        public ObservableCollection<Appointment> convertAvmToAppt(List<AppointmentViewModel> avms);

        public Appointment GetOne(int Id);

        public ObservableCollection<Appointment> GetAll();

        public void Edit(Appointment appointment);

        public void Delete(Appointment appointment);

        public void Add(Appointment a);

        public int GenerateNewID();

    }
}