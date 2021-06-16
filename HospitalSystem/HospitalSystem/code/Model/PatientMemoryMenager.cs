using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace HospitalSystem.code.Model
{
    public static class PatientMemoryMenager
    {
        private static String FileLocation = "../../../Resource/Patients.json";

        public static void serialize(ObservableCollection<Patient> patients)
        {
            var JSONresult = JsonConvert.SerializeObject(patients);
            using (StreamWriter sw = new StreamWriter(FileLocation))
            {
                sw.Write(JSONresult);
                sw.Close();
            }
        }

        public static ObservableCollection<Patient> deserialize()
        {
            ObservableCollection<Patient> list = new ObservableCollection<Patient>();
            using (StreamReader sr = new StreamReader(FileLocation))
            {
                list = JsonConvert.DeserializeObject<ObservableCollection<Patient>>(sr.ReadToEnd());
                sr.Close();
            }
            return list;
        }
    }
}
