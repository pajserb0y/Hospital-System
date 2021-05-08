using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;

namespace HospitalSystem.code
{
    class AlergenStorage
    {
        private static AlergenStorage instance = null;
        public static AlergenStorage getInstance()
        {
            if (instance == null)
            {
                instance = new AlergenStorage();
            }
            return instance;
        }

        private ObservableCollection<Alergen> alergens = new ObservableCollection<Alergen>();
        public ObservableCollection<Alergen> Alergens
        {
            get { return alergens; }
            set { alergens = value; }
        }

        private string FileLocation = "../../../Resource/Alergens.json";

        public AlergenStorage()
        {
            this.alergens = deserialize();
        }

        public ObservableCollection<Alergen> GetAll()
        {
            return alergens;
        }
        public void Edit(Alergen alergen)
        {
            foreach (Alergen a in this.alergens)
                if (alergen.No == a.No)
                {
                    a.Substance = alergen.Substance;
                }
        }

        public void Delete(Alergen alergen)
        {
            foreach (Alergen a in alergens)
                if (alergen.No == a.No && alergen.PatientID == a.PatientID)
                {
                    this.alergens.Remove(a);
                    break;
                }
        }

        public void Save(Alergen alergen)
        {
            this.alergens.Add(alergen);
            serialize();
        }

        public int GenerateNewID(int patientID)
        {
            int max = 0;
            foreach (Alergen a in alergens)
                if (a.PatientID == patientID && a.No > max)
                    max = a.No;
            return max + 1;
        }

        public void serialize()
        {
            var JSONresult = JsonConvert.SerializeObject(alergens);
            using (StreamWriter sw = new StreamWriter(FileLocation))
            {
                sw.Write(JSONresult);
                sw.Close();
            }
        }

        public ObservableCollection<Alergen> deserialize()
        {
            ObservableCollection<Alergen> list = new ObservableCollection<Alergen>();
            using (StreamReader sr = new StreamReader(FileLocation))
            {
                list = JsonConvert.DeserializeObject<ObservableCollection<Alergen>>(sr.ReadToEnd());
                sr.Close();
            }
            return list;
        }
    }
}