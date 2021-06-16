using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HospitalSystem.code.Model
{
    class UnderagedPatient : Patient
    {
        private long parentJmbg;
        public long ParentJmbg
        {
            get { return parentJmbg; }
            set
            {
                if (parentJmbg != value)
                {
                    parentJmbg = value;
                    OnPropertyChanged("ParentJmbg");
                }
            }
        }

        public UnderagedPatient(long parentJmbg)
        {
            this.parentJmbg = parentJmbg;
        }
    }
}
