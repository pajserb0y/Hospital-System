using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.code
{
    public class Anamnesis
    {
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
         public Anamnesis(string anam, string diag)
        {
            anamnesisInfo = anam;
            diagnosis = diag;
        }
    }
}
