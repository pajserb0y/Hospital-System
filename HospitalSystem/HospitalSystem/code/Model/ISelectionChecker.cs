using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace HospitalSystem.code.Model
{
    interface ISelectionChecker
    {
        public static bool isSelected(Object selectedItem)
        {
            if (selectedItem != null)
                return true;

            MessageBox.Show("You have to select doctor first.");
            return false;
        }
    }
}
