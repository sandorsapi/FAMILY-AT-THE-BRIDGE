using Bridge_App.Interface;
using GalaSoft.MvvmLight;

namespace Bridge_App.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private MenuViewModel MenuViewModel;
        private OperatorViewModel OperatorViewModel;
        private InformationInterfaceViewModel InformationInterfaceViewModel;

        public MainViewModel()
        {           
            this.OperatorViewModel = new OperatorViewModel();
            this.InformationInterfaceViewModel = new InformationInterfaceViewModel();
            this.MenuViewModel = new MenuViewModel(this.OperatorViewModel, this.InformationInterfaceViewModel);          
        }

        public MenuViewModel ContentMenuViewModel
        {
            get { return MenuViewModel; }
            set { Set(nameof(ContentMenuViewModel), ref MenuViewModel, value); }
        }

        public OperatorViewModel ContentOperatorViewModel
        {
            get { return OperatorViewModel; }
            set { Set(nameof(ContentOperatorViewModel), ref OperatorViewModel, value); }
        }

        public InformationInterfaceViewModel ContentInformationInterfaceViewModel
        {
            get { return InformationInterfaceViewModel; }
            set { Set(nameof(ContentInformationInterfaceViewModel), ref InformationInterfaceViewModel, value); }
        }
    }
}