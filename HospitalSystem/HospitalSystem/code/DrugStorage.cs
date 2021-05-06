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


        private ObservableCollection<Drug> verifiedDrugs;
        public ObservableCollection<Drug> VerifiedDrugs
        {
            get { return verifiedDrugs; }
            set { verifiedDrugs = value; }
        }

        private ObservableCollection<Drug> unverifiedDrugs;
        public ObservableCollection<Drug> UnverifiedDrugs
        {
            get { return unverifiedDrugs; }
            set { unverifiedDrugs = value; }
        }

        private String FileLocationUnverifiedDrugs = "../../../Resource/Neverifikovani_Lekovi.json";
        private String FileLocationVerifiedDrugs = "../../../Resource/Verifikovani_Lekovi.json";


        public DrugStorage()
        {
            this.verifiedDrugs = deserialize(FileLocationVerifiedDrugs);
            this.unverifiedDrugs = deserialize(FileLocationUnverifiedDrugs);
        }

        public Drug GetOneVerifiedDrug(int Id)
        {
            foreach (Drug d in verifiedDrugs)
                if (Id == d.Id)
                    return d;
            return null;
        }

        public Drug GetOneUnverifiedDrug(int Id)
        {
            foreach (Drug d in unverifiedDrugs)
                if (Id == d.Id)
                    return d;
            return null;
        }

        public ObservableCollection<Drug> GetAllVerifiedDrugs()
        {
            return verifiedDrugs;
        }

        public ObservableCollection<Drug> GetAllUnverifiedDrugs()
        {
            return unverifiedDrugs;
        }

        public void EditVerifiedDrug(Drug drug)
        {
            foreach (Drug d in this.verifiedDrugs)
                if (drug.Id == d.Id)
                {
                    d.Name = drug.Name;
                    d.Amount = drug.Amount;
                }
        }

        public void EditUnverifiedDrug(Drug drug)
        {
            foreach (Drug d in this.unverifiedDrugs)
                if (drug.Id == d.Id)
                {
                    d.Name = drug.Name;
                    d.Amount = drug.Amount;
                }
        }

        public void DeleteVerifiedDrug(Drug drug)
        {
            foreach (Drug d in verifiedDrugs)
                if (drug.Id == d.Id)
                {
                    this.verifiedDrugs.Remove(d);
                    break;
                }
        }

        public void DeleteUnverifiedDrug(Drug drug)
        {
            foreach (Drug d in unverifiedDrugs)
                if (drug.Id == d.Id)
                {
                    this.unverifiedDrugs.Remove(d);
                    break;
                }
        }

        public void SaveVerifiedDrug(Drug drug)
        {
            this.verifiedDrugs.Add(drug);
        }

        public void SaveUnverifiedDrug(Drug drug)
        {
            this.unverifiedDrugs.Add(drug);
        }

        public int GenerateNewVerifiedDrugID()
        {
            return ((verifiedDrugs.Count - 1) == -1) ? 1 : verifiedDrugs[verifiedDrugs.Count - 1].Id + 1;
        }

        public int GenerateNewUnverifiedDrugID()
        {
            return ((unverifiedDrugs.Count - 1) == -1) ? 1 : unverifiedDrugs[unverifiedDrugs.Count - 1].Id + 1;
        }

        public void serialize(string FileLocation,ObservableCollection<Drug> drugs)
        {
            var JSONresult = JsonConvert.SerializeObject(drugs);
            using (StreamWriter sw = new StreamWriter(FileLocation))
            {
                sw.Write(JSONresult);
                sw.Close();
            }
        }

        public ObservableCollection<Drug> deserialize(string FileLocation)
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
