using DausterCustomer.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms.GoogleMaps;

namespace DausterCustomer.Services
{
    public class ServiceManager
    {
        IRestService restService;

        public ServiceManager(IRestService service)
        {
            restService = service;
        }

        #region Catalogs
        public Task<List<KindPersons>> GetKindPersons()
        {
            return restService.GetKindPersons();
        }

        public Task<List<Country>> GetContry()
        {
            return restService.GetContry();
        }

        public Task<List<State>> GetStates()
        {
            return restService.GetStates();
        }

        public Task<List<TypeCard>> GetTypeCardsAsync()
        {
            return restService.GetTypeCardsAsync();
        }

        public Task<List<Vehicle>> GetVehicleAsync()
        {
            return restService.GetVehicleAsync();
        }
        #endregion

        #region User
        public Task<UserLogin> LoginAsync(User item)
        {   
            return restService.LoginAsync(item);
        }

        public Task<User> GetUser()
        {
            return restService.GetUser();
        }

        

        public Task<Address> GetAddress()
        {
            return restService.GetAddress();
        }

        

        public Task<Billing> GetBilling()
        {
            return restService.GetBilling();
        }

        
        #endregion

        #region "Alta usuarios"
        public Task<UserLogin> SetUser(User user)
        {
            return restService.SetUser(user);
        }
        public Task<UserLogin> SetAddress(Address address)
        {
            return restService.SetAddress(address);
        }
        public Task<UserLogin> SetBilling(Billing billing)
        {
            return restService.SetBilling(billing);
        }

        #endregion

        public Task<List<PaymentMethods>> GetPaymentMethodsAsync()
        {
            return restService.GetPaymentMethodsAsync();
        }

        public Task<UserLogin> SetPaymenthMethodsAsync(PaymentMethods payment)
        {
            return restService.SetPaymenthMethodsAsync(payment);
        }

        public Task<JObject> getAsyncRouteGoogle(List<Pin> lsPins)
        {
            return restService.getAsyncRouteGoogle(lsPins);
        }

        public Task<float> GetQuotation(Dictionary<string, int> valuesCoordinates)
        {
            return restService.GetQuotation(valuesCoordinates);
        }

        public Task deletePaymentMethod(PaymentMethods paymentMethods)
        {
            return restService.deletePaymentMethod(paymentMethods.id);
        }
    }
}
