using System;
using System.Net.NetworkInformation;

using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.DataTransfer;

using AppStudio.Services;
using AppStudio.ViewModels;

namespace AppStudio.Views
{
    public sealed partial class TYPESOFINSTRUMENTSDetail : Page
    {
        private NavigationHelper _navigationHelper;

        private DataTransferManager _dataTransferManager;

        public TYPESOFINSTRUMENTSDetail()
        {
            this.InitializeComponent();
            _navigationHelper = new NavigationHelper(this);

            TYPESOFINSTRUMENTSModel = new TYPESOFINSTRUMENTSViewModel();

            ApplicationView.GetForCurrentView().
                SetDesiredBoundsMode(ApplicationViewBoundsMode.UseVisible);
        }

        public TYPESOFINSTRUMENTSViewModel TYPESOFINSTRUMENTSModel { get; private set; }

        public NavigationHelper NavigationHelper
        {
            get { return _navigationHelper; }
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            _dataTransferManager = DataTransferManager.GetForCurrentView();
            _dataTransferManager.DataRequested += OnDataRequested;

            _navigationHelper.OnNavigatedTo(e);

            await TYPESOFINSTRUMENTSModel.LoadItemsAsync();
            TYPESOFINSTRUMENTSModel.SelectItem(e.Parameter);

            if (TYPESOFINSTRUMENTSModel != null)
            {
                TYPESOFINSTRUMENTSModel.ViewType = ViewTypes.Detail;
            }
            DataContext = this;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _navigationHelper.OnNavigatedFrom(e);
            _dataTransferManager.DataRequested -= OnDataRequested;
        }

        private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            if (TYPESOFINSTRUMENTSModel != null)
            {
                TYPESOFINSTRUMENTSModel.GetShareContent(args.Request);
            }
        }
    }
}
