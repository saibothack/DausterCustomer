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
	public partial class QuotationPage : ContentPage 
	{
        private QuotationPageViewModel viewModel;

        public QuotationPage ()
		{
			InitializeComponent ();
            BindingContext = viewModel = new QuotationPageViewModel();
            viewModel.Navigation = this.Navigation;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.callPopOption();
        }
    }
}