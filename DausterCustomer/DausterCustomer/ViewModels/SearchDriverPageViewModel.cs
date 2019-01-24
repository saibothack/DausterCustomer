using DausterCustomer.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DausterCustomer.ViewModels
{
    public class SearchDriverPageViewModel : ViewModelBase, IAsyncInitialization
    {
        public INavigation Navigation { get; set; }

        #region Properties
        public Task Initialization { get; private set; }
        #endregion

        public SearchDriverPageViewModel() {

        }

        private async Task InitializeAsync() {

        }

        private async void createRoute() {

        }
    }
}
