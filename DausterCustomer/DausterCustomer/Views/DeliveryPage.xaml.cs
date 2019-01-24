using DausterCustomer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DausterCustomer.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DeliveryPage : ContentPage
	{
        private DeliveryPageViewModel viewModel;
        public DeliveryPage ()
		{
			InitializeComponent ();
            BindingContext = viewModel = new DeliveryPageViewModel();
            viewModel.Navigation = this.Navigation;
        }
	}
}