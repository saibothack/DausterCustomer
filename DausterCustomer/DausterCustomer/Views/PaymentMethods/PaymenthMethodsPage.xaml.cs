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
	public partial class PaymenthMethodsPage : ContentPage
	{
        private PaymentMethodsPageViewModels viewModel;

        public PaymenthMethodsPage ()
		{
			InitializeComponent ();
            BindingContext = viewModel = new PaymentMethodsPageViewModels();
            viewModel.Navigation = this.Navigation;
        }
	}
}