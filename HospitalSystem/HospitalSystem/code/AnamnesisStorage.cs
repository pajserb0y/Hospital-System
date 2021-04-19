using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace HospitalSystem.code
{
    class AnamnesisStorage
    {
        private static AnamnesisStorage instance = null;
        public static AnamnesisStorage getInstance()
        {
            if (instance == null)
            {
                instance = new AnamnesisStorage();
            }
            return instance;
        }


        private ObservableCollection<Anamnesis> anamnesis;
        public ObservableCollection<Anamnesis> Anamnesis
        {
            get { return anamnesis; }
            set { anamnesis = value; }
        }

        private String FileLocation = "../../../Resource/Anamneze.json";


        public AnamnesisStorage()
        {
            this.anamnesis = deserialize();
        }

        public Anamnesis GetOne(int Id)
        {
            foreach (Anamnesis a in anamnesis)
                if (Id == a.Id)
                    return a;
            return null;
        }

        public ObservableCollection<Anamnesis> GetAll()
        {
            return anamnesis;
        }

        public void Edit(Anamnesis anam)
        {
            foreach (Anamnesis a in anamnesis)
                if (anam.Id == a.Id)
                {
                    a.AnamnesisInfo = anam.AnamnesisInfo;
                    a.Diagnosis = anam.Diagnosis;
                }
        }

        public void Delete(Anamnesis anam)
        {
            foreach (Anamnesis a in anamnesis)
                if (anam.Id == a.Id)
                {
                    this.anamnesis.Remove(a);
                    break;
                }
        }

        public void Add(Anamnesis anam)
        {
            this.anamnesis.Add(anam);
        }


        public void serialize()
        {
            var JSONresult = JsonConvert.SerializeObject(anamnesis);
            using (StreamWriter sw = new StreamWriter(FileLocation))
            {
                sw.Write(JSONresult);
                sw.Close();
            }
        }

        public ObservableCollection<Anamnesis> deserialize()
        {
            ObservableCollection<Anamnesis> anams = new ObservableCollection<Anamnesis>();
            using (StreamReader sr = new StreamReader(FileLocation))
            {

                anams = JsonConvert.DeserializeObject<ObservableCollection<Anamnesis>>(sr.ReadToEnd());
                if (anams == null)
                    anams = new ObservableCollection<Anamnesis>();
                sr.Close();
            }
            return anams;
        }
    }
}
