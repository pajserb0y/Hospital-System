using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace HospitalSystem.code
{
    class DrugStorage
    {
        private static DrugStorage instance = null;
        public static DrugStorage getInstance()
        {
            if (instance == null)
            {
                instance = new DrugStorage();
            }
            return instance;
        }

        private ObservableCollection<Drug> drugs;
        public ObservableCollection<Drug> Drugs
        {
            get { return drugs; }
            set { drugs = value; }
        }
        private String FileLocation = "../../../Resource/Drugs.json";

        public DrugStorage()
        {
            this.drugs = deserialize();
        }

        public Drug GetOneDrug(int Id)
        {
            foreach (Drug d in drugs)
                if (Id == d.Id)
                    return d;
            return null;
        }

        public ObservableCollection<Drug> GetAllDrugs()
        {
            return drugs;
        }
        public ObservableCollection<Drug> GetAllVerifiedDrugs()
        {
            ObservableCollection<Drug> verifiedDrugs = new ObservableCollection<Drug>();
            foreach (Drug d in drugs)
                if (d.Status == Drug.STATUS.Verified)
                    verifiedDrugs.Add(d);
            return verifiedDrugs;
        }

        public ObservableCollection<Drug> GetAllUnverifiedDrugs()
        {
            ObservableCollection<Drug> unverifiedDrugs = new ObservableCollection<Drug>();
            foreach (Drug d in drugs)
                if (d.Status == Drug.STATUS.Unverified)
                    unverifiedDrugs.Add(d);
            return unverifiedDrugs;
        }

        public void EditDrug(Drug drug)
        {
            foreach (Drug d in this.drugs)
                if (drug.Id == d.Id)
                {
                    d.Name = drug.Name;
                    d.Amount = drug.Amount;
                    d.Ingridients = drug.Ingridients;
                    d.Status = drug.Status;
                    d.Report = drug.Report;
                }
            serialize();
        }

        public void DeleteDrug(Drug drug)
        {
            foreach (Drug d in drugs)
                if (drug.Id == d.Id)
                {
                    this.drugs.Remove(d);
                    break;
                }
            serialize();
        }
        public void AddDrug(Drug drug)
        {
            this.drugs.Add(drug);
            serialize();
        }

        public int GenerateNewDrugID()
        {
            return ((drugs.Count - 1) == -1) ? 1 : drugs[drugs.Count - 1].Id + 1;
        }

        public void serialize()
        {
            var JSONresult = JsonConvert.SerializeObject(drugs);
            using (StreamWriter sw = new StreamWriter(FileLocation))
            {
                sw.Write(JSONresult);
                sw.Close();
            }
        }

        public ObservableCollection<Drug> deserialize()
        {
            ObservableCollection<Drug> list = new ObservableCollection<Drug>();
            using (StreamReader sr = new StreamReader(FileLocation))
            {
                list = JsonConvert.DeserializeObject<ObservableCollection<Drug>>(sr.ReadToEnd());
                sr.Close();
            }
            return list;
        }
    }
}
