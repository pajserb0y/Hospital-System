using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.code
{
    class Drug 
    { 
         public Drug(int v1, string v2)
         {
             id = v1;
            name = v2;
         }
    
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }

        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }

        }

        private int amount;

        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }
        List<string> ingredients;
        public List<string> Ingridients
        {
            get { return ingredients; }
            set { ingredients = value; }
        }
        public override string ToString()
        {
            return id + " - " + name;
        }
    }
}
