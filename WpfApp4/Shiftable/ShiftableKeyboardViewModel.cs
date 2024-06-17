using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmDialogs;
using WpfApp4.ViewModels;


namespace WpfApp4.Shiftable
{
    internal class ShiftableKeyboardViewModel : KeyboardViewModelBase, IModalDialogViewModel
    {
        private bool? dialogResult;

        public bool? DialogResult
        {
            get
            {
                return dialogResult;
            }
            set
            {
                if (value == dialogResult)
                {
                    return;
                }

                dialogResult = value;
                OnPropertyChanged(() => DialogResult);
            }
        }
    }
}
