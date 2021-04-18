using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.code
{
    class Drug
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }

        }

        private string name;

        public Drug(int v1, string v2)
        {
            id = v1;
            name = v2;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }

        }

        public override string ToString()
        {
            return id + " - " + name;
        }
    }
}
