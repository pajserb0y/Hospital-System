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

        private String FileLocation = "../../../Resource/Lekovi.json";


        public DrugStorage()
        {
            this.drugs = deserialize();
        }

        public Drug GetOne(int Id)
        {
            foreach (Drug d in drugs)
                if (Id == d.Id)
                    return d;
            return null;
        }

        public ObservableCollection<Drug> GetAll()
        {
            return drugs;
        }

        public void Edit(Drug drug)
        {
            foreach (Drug d in this.drugs)
                if (drug.Id == d.Id)
                {
                    d.Name = drug.Name;
                }
        }

        public void Delete(Drug drug)
        {
            foreach (Drug d in drugs)
                if (drug.Id == d.Id)
                {
                    this.drugs.Remove(d);
                    break;
                }
        }

        public void Save(Drug drug)
        {
            this.drugs.Add(drug);
        }

        public int GenerateNewID()
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
