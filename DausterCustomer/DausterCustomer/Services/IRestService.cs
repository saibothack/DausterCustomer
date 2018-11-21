using DausterCustomer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DausterCustomer.Services
{
    public interface IRestService
    {
        #region Catalogs
        Task<List<KindPersons>> GetKindPersons();
        Task<List<Country>> GetContry();
        Task<List<State>> GetStates();
        #endregion

        #region User
        Task<User> LoginAsync(User item);
        Task<User> StoreUser(User user);
        Task<User> GetUser(int iUser);
        Task<User> SetUser(User user);
        Task<Address> GetAddress(Address address);
        Task<Address> SetAddress(Address address);
        Task<Billing> GetBilling(Billing billing);
        Task<Billing> SetBilling(Billing billing);
        #endregion

    }
}   
