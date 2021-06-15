using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace HospitalSystem.code
{
    class DoctorStorage
    {
        private static DoctorStorage instance = null;
        public static DoctorStorage getInstance()
        {
            if (instance == null)
            {
                instance = new DoctorStorage();
            }
            return instance;
        }


        private ObservableCollection<Doctor> doctors;
        public ObservableCollection<Doctor> Doctors
        {
            get { return doctors; }
            set { doctors = value; }
        }

        private String FileLocation = "../../../Resource/Doktori.json";


        public DoctorStorage()
        {
            this.doctors = deserialize();
        }

        public Doctor GetOne(int Id)
        {
            foreach (Doctor d in doctors)
                if (Id == d.Id)
                    return d;
            return null;
        }

        public ObservableCollection<Doctor> GetAll()
        {
            return doctors;
        }

        public void Edit(Doctor doctor)
        {
            //Patient help = null;
            //foreach (Patient P in this.Patients)
            //    if (patient.Id == P.Id)
            //       help = P;
            //int i = this.Patients.IndexOf(help);
            //this.Patients.Remove(help);
            //this.Patients.Insert(i, patient);
            foreach (Doctor d in this.doctors)
                if (doctor.Id == d.Id)
                {
                    d.Jmbg = doctor.Jmbg;
                    d.FirstName = doctor.FirstName;
                    d.LastName = doctor.LastName;
                    d.Adress = doctor.Adress;
                    d.Telephone = doctor.Telephone;
                    d.Specialization = doctor.Specialization;
                }
        }

        public void Delete(Doctor doctor)
        {
            foreach (Doctor d in doctors)
                if (doctor.Id == d.Id)
                {
                    this.doctors.Remove(d);
                    break;
                }
        }

        public void Save(Doctor doctor)
        {
            this.doctors.Add(doctor);
        }

        public int GenerateNewID()
        {
            return ((doctors.Count - 1) == -1) ? 1 : doctors[doctors.Count - 1].Id + 1;
        }

        public void serialize()
        {
            var JSONresult = JsonConvert.SerializeObject(doctors);
            using (StreamWriter sw = new StreamWriter(FileLocation))
            {
                sw.Write(JSONresult);
                sw.Close();
            }
        }

        public ObservableCollection<Doctor> deserialize()
        {
            ObservableCollection<Doctor> list = new ObservableCollection<Doctor>();
            using (StreamReader sr = new StreamReader(FileLocation))
            {
                list = JsonConvert.DeserializeObject<ObservableCollection<Doctor>>(sr.ReadToEnd());
                sr.Close();
            }
            return list;
        }
    }
}
