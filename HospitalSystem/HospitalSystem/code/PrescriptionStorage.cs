using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace HospitalSystem.code
{
    class PrescriptionStorage
    {
        private static PrescriptionStorage instance = null;
        public static PrescriptionStorage getInstance()
        {
            if (instance == null)
            {
                instance = new PrescriptionStorage();
            }
            return instance;
        }

        private ObservableCollection<Prescription> prescriptions;
        public ObservableCollection<Prescription> Prescriptions
        {
            get { return prescriptions; }
            set { prescriptions = value; }
        }

        private String FileLocation = "../../../Resource/Recepti.json";

        public PrescriptionStorage()
        {
            this.prescriptions = deserialize();
        }

        public Prescription GetOne(int prescriptionID)
        {
            foreach (Prescription p in prescriptions)
                if (prescriptionID == p.Id)
                    return p;
            return null;
        }

        public ObservableCollection<Prescription> GetAll()
        {
            return prescriptions;
        }

        public void Edit(Prescription prescription)
        {
            foreach (Prescription P in this.prescriptions)
                if (prescription.Id == P.Id)
                {
                    P.Id = prescription.Id;
                    P.ExaminationID = prescription.ExaminationID;
                    P.DateOfPrescription = prescription.DateOfPrescription;
                    P.Taking = prescription.Taking;
                    P.Drug = prescription.Drug;
                }
        }

        public void Delete(Prescription prescription)
        {
            foreach (Prescription p in prescriptions)
                if (prescription.Id == p.Id)
                {
                    this.prescriptions.Remove(p);
                    break;
                }
        }

        public void Add(Prescription prescription)
        {
            this.prescriptions.Add(prescription);
        }

        public int GenerateNewID()
        {
            return (prescriptions.Count == 0) ? 1 : prescriptions[prescriptions.Count - 1].Id + 1;
        }

        public void serialize()
        {
            var JSONresult = JsonConvert.SerializeObject(prescriptions);
            using (StreamWriter sw = new StreamWriter(FileLocation))
            {
                sw.Write(JSONresult);
                sw.Close();
            }
        }

        public ObservableCollection<Prescription> deserialize()
        {
            ObservableCollection<Prescription> list = new ObservableCollection<Prescription>();
            using (StreamReader sr = new StreamReader(FileLocation))
            {
                list = JsonConvert.DeserializeObject<ObservableCollection<Prescription>>(sr.ReadToEnd());
                sr.Close();
            }
            return list;
        }

    }
}
