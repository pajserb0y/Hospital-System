using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.code
{
    public class Anamnesis
    {
        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                }
            }
        }
        private string anamnesisInfo;
        public string AnamnesisInfo
        {
            get { return anamnesisInfo; }
            set
            {
                if (anamnesisInfo != value)
                {
                    anamnesisInfo = value;
                }
            }
        }

        private string diagnosis;
        public string Diagnosis
        {
            get { return diagnosis; }
            set
            {
                if (diagnosis != value)
                {
                    diagnosis = value;
                }
            }
        }
         public Anamnesis(int idExam,string anam, string diag)
        {
            id = idExam;
            anamnesisInfo = anam;
            diagnosis = diag;
        }
    }
}
