using DausterCustomer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DausterCustomer.Views.PaymentMethods
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PaymenthMethodsCreatePage : ContentPage
	{
        private PaymenthMethodsCreatePageViewModel viewModel;

        public PaymenthMethodsCreatePage (int id)
		{
			InitializeComponent ();
            BindingContext = viewModel = new PaymenthMethodsCreatePageViewModel(id);
            viewModel.Navigation = this.Navigation;
        }
	}
}