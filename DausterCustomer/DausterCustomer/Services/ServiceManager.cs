using DausterCustomer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        #endregion

        #region User
        public Task<User> LoginAsync(User item)
        {   
            return restService.LoginAsync(item);
        }

        public Task<User> StoreUser(User user)
        {
            return restService.StoreUser(user);
        }

        public Task<User> GetUser(int iUser)
        {
            return restService.GetUser(iUser);
        }

        public Task<User> SetUser(User user)
        {
            return restService.SetUser(user);
        }

        public Task<Address> GetAddress(Address address)
        {
            return restService.GetAddress(address);
        }

        public Task<Address> SetAddress(Address address)
        {
            return restService.SetAddress(address);
        }

        public Task<Billing> GetBilling(Billing billing)
        {
            return restService.GetBilling(billing);
        }

        public Task<Billing> SetBilling(Billing billing)
        {
            return restService.SetBilling(billing);
        }
        #endregion
    }
}
