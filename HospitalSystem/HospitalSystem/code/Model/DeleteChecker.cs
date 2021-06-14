using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace HospitalSystem.code.Model
{
    interface DeleteChecker
    {
        void surelyDeleting(Object selectedItem)
        {
            MessageBoxButton btnMessageBox = MessageBoxButton.YesNoCancel;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult rsltMessageBox = MessageBox.Show("Are you sure that you want to delete this item?", "Permanently deleting", btnMessageBox, icnMessageBox);
            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    {
                        deleteObject(selectedItem);                    
                        break;
                    }

                case MessageBoxResult.No:
                    /* ... */
                    break;

                case MessageBoxResult.Cancel:
                    /* ... */
                    break;
            }
        }


        void deleteObject(Object selectedItem) 
        {

        }
    }
}
