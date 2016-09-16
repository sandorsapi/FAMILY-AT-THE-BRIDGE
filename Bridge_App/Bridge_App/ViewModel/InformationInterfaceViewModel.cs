using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Text;

namespace Bridge_App.ViewModel
{
    public class InformationInterfaceViewModel : ViewModelBase
    {
        private string solutionText;

        public InformationInterfaceViewModel()
        {
            this.ClearCommand = new RelayCommand(ClearText);
        }

        public string SolutionText
        {
            get { return solutionText; }
            set { Set(nameof(SolutionText), ref solutionText, value); }
        }

        public RelayCommand ClearCommand { get; private set; }

        //TextBox containt clear
        private void ClearText()
        {
            this.SolutionText = null;
        }
    }
}