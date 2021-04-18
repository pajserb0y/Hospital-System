using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.code
{
    class Prescription
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }

        }

        private int examinationID;
        public int ExaminationID
        {
            get { return examinationID; }
            set { examinationID = value; }

        }

        private DateTime dateOfPrescription;
        public DateTime DateOfPrescription
        {
            get { return dateOfPrescription; }
            set { dateOfPrescription = value; }

        }

        private string taking;
        public string Taking
        {
            get { return taking; }
            set { taking = value; }

        }

        public Prescription(int prescID, int examID, Drug drug, string taking, DateTime date)
        {
            this.id = prescID;
            this.examinationID = examID;
            this.drug = drug;
            this.taking = taking;
            this.dateOfPrescription = date;
        }

        private Drug drug;
        public Drug Drug
        {
            get { return drug; }
            set { drug = value; }
        }

    }
}