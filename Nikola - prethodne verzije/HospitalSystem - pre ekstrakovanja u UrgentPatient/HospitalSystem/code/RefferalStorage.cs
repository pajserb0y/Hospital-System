using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace HospitalSystem.code
{
    class RefferalStorage
    {
        private static RefferalStorage instance = null;
        public static RefferalStorage getInstance()
        {
            if (instance == null)
            {
                instance = new RefferalStorage();
            }
            return instance;
        }

        private ObservableCollection<Refferal> refferals;
        public ObservableCollection<Refferal> Refferals
        {
            get { return refferals; }
            set { refferals = value; }
        }

        private String FileLocation = "../../../Resource/Refferals.json";

        public RefferalStorage()
        {
            this.refferals = deserialize();
        }

        public Refferal GetOne(int refferalID)
        {
            foreach (Refferal r in refferals)
                if (refferalID == r.Id)
                    return r;
            return null;
        }

        public ObservableCollection<Refferal> GetAll()
        {
            return refferals;
        }

        public void Edit(Refferal refferal)
        {
            foreach (Refferal r in this.refferals)
                if (refferal.Id == r.Id)
                {
                    r.Id = refferal.Id;
                    r.Note = refferal.Note;
                    r.Specialization = refferal.Specialization;
                    r.PatientId = refferal.PatientId;
                    r.DoctorId = refferal.DoctorId;
                }
        }

        public void Delete(Refferal refferal)
        {
            foreach (Refferal r in refferals)
                if (refferal.Id == r.Id)
                {
                    this.refferals.Remove(r);
                    break;
                }
        }

        public void Add(Refferal refferal)
        {
            this.refferals.Add(refferal);
        }

        public int GenerateNewID()
        {
            return (refferals.Count == 0) ? 1 : refferals[refferals.Count - 1].Id + 1;
        }

        public void serialize()
        {
            var JSONresult = JsonConvert.SerializeObject(refferals);
            using (StreamWriter sw = new StreamWriter(FileLocation))
            {
                sw.Write(JSONresult);
                sw.Close();
            }
        }

        public ObservableCollection<Refferal> deserialize()
        {
            ObservableCollection<Refferal> list = new ObservableCollection<Refferal>();
            using (StreamReader sr = new StreamReader(FileLocation))
            {
                list = JsonConvert.DeserializeObject<ObservableCollection<Refferal>>(sr.ReadToEnd());
                sr.Close();
            }
            return list;
        }
    }
}
