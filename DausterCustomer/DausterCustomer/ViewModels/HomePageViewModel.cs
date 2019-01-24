using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DausterCustomer.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        #region "Commandos"
        public INavigation Navigation { get; set; }
        public ImageSource imageSorceMenu { get; set; }
        public ImageSource imgSourceMarker { get; set; }
        public ImageSource imgSourceMarkerButton { get; set; }
        public ICommand ShowMenuCommand { get; set; }
        #endregion

        public HomePageViewModel() {
            imageSorceMenu = ImageSource.FromResource("DausterCustomer.Images.ic_menu.png");
            imgSourceMarker = ImageSource.FromResource("DausterCustomer.Images.ic_marker.png");
            imgSourceMarkerButton = ImageSource.FromResource("DausterCustomer.Images.ic_marker.png");

            ShowMenuCommand = new Command(showMenu);
        }


    }
}
