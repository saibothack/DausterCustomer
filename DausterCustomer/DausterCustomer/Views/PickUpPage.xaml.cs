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
	public partial class PickUpPage : ContentPage
	{
        private PickUpPageViewModel viewModel;
        public PickUpPage ()
		{
			InitializeComponent();
            BindingContext = viewModel = new PickUpPageViewModel();
            viewModel.Navigation = this.Navigation;
        }
	}
}