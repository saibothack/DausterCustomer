using DausterCustomer.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms.GoogleMaps;

namespace DausterCustomer.Services
{
    public interface IRestService
    {
        #region Catalogs
        Task<List<KindPersons>> GetKindPersons();
        Task<List<Country>> GetContry();
        Task<List<State>> GetStates();
        Task<List<TypeCard>> GetTypeCardsAsync();
        Task<List<Vehicle>> GetVehicleAsync();
        #endregion

        #region User
        Task<UserLogin> LoginAsync(User item);
        Task<User> GetUser();
        Task<Address> GetAddress();
        Task<Billing> GetBilling();
        #endregion

        #region "Alta usuarios"
        Task<UserLogin> SetUser(User user);
        Task<UserLogin> SetAddress(Address address);
        Task<UserLogin> SetBilling(Billing billing);
        #endregion

        Task<List<PaymentMethods>> GetPaymentMethodsAsync();
        Task<UserLogin> SetPaymenthMethodsAsync(PaymentMethods payment);

        Task<JObject> getAsyncRouteGoogle(List<Pin> lsPins);
        Task<float> GetQuotation(Dictionary<string, int> valuesCoordinates);

        Task deletePaymentMethod(int id);

    }
}   
