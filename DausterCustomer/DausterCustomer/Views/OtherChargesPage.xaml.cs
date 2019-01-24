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
	public partial class OtherChargesPage : ContentPage
	{
        private OtherChargesPageViewModel viewModel;

        public OtherChargesPage ()
		{
			InitializeComponent();
            BindingContext = viewModel = new OtherChargesPageViewModel();
            viewModel.Navigation = this.Navigation;
        }
	}
}