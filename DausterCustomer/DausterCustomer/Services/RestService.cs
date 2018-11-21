using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DausterCustomer.Models;
using Newtonsoft.Json;

namespace DausterCustomer.Services
{
    public class RestService : IRestService
    {
        HttpClient client;

        public RestService()
        {
            var authData = string.Format("{0}:{1}", Constants.sUserName, Constants.sPassword);
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

            client = new HttpClient();
            //client.MaxResponseContentBufferSize = 256000;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        }

        #region Catalogs

        public async Task<List<KindPersons>> GetKindPersons()
        {
            List<KindPersons> itemKindPersons = new List<KindPersons>();

            try
            {
                HttpResponseMessage response = null;
                String sUrl = Constants.RestUrl + "kind-persons";
                var uri = new Uri(String.Format(sUrl, string.Empty));
                response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var request = await response.Content.ReadAsStringAsync();
                    itemKindPersons = JsonConvert.DeserializeObject<List<KindPersons>>(request);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return itemKindPersons;
        }

        public async Task<List<Country>> GetContry()
        {
            List<Country> itemCountries = new List<Country>();

            try
            {
                HttpResponseMessage response = null;
                string sUrl = Constants.RestUrl + "countries";
                var uri = new Uri(string.Format(sUrl, string.Empty));
                response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var request = await response.Content.ReadAsStringAsync();
                    itemCountries = JsonConvert.DeserializeObject<List<Country>>(request);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return itemCountries;
        }

        public async Task<List<State>> GetStates()
        {
            List<State> itemStates = new List<State>();

            try
            {
                HttpResponseMessage response = null;
                string sUrl = Constants.RestUrl + "states";
                var uri = new Uri(string.Format(sUrl, string.Empty));
                response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var request = await response.Content.ReadAsStringAsync();
                    itemStates = JsonConvert.DeserializeObject<List<State>>(request);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return itemStates;
        }

        #endregion

        #region User

        public async Task<User> LoginAsync(User item)
        {
            var uri = new Uri(string.Format(Constants.RestUrl + "auth/user/login", string.Empty));
            User itemUser = new User();

            try
            {
                var postDriver = JsonConvert.SerializeObject(item);
                var content = new StringContent(postDriver, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var request = await response.Content.ReadAsStringAsync();
                    itemUser = JsonConvert.DeserializeObject<User>(request);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return itemUser;
        }

        public async Task<User> StoreUser(User user)
        {
            var uri = new Uri(string.Format(Constants.RestUrl + "auth/user/register", string.Empty));
            User itemUser = new User();

            try
            {
                var postDriver = JsonConvert.SerializeObject(user);
                var content = new StringContent(postDriver, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var request = await response.Content.ReadAsStringAsync();
                    itemUser = JsonConvert.DeserializeObject<User>(request);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return itemUser;
        }
        public async Task<User> GetUser(int iUser)
        {
            var uri = new Uri(string.Format(Constants.RestUrl + "user/{0}/edit", iUser));
            User itemUser = new User();

            try
            {
                HttpResponseMessage response = null;
                response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var request = await response.Content.ReadAsStringAsync();
                    itemUser = JsonConvert.DeserializeObject<User>(request);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return itemUser;
        }

        public async Task<User> SetUser(User user)
        {
            var uri = new Uri(string.Format(Constants.RestUrl + "user/{0}", user.id));
            User itemUser = new User();

            try
            {
                var postDriver = JsonConvert.SerializeObject(user);
                var content = new StringContent(postDriver, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var request = await response.Content.ReadAsStringAsync();
                    itemUser = JsonConvert.DeserializeObject<User>(request);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return itemUser;
        }

        public async Task<Address> GetAddress(Address address)
        {
            var uri = new Uri(string.Format(Constants.RestUrl + "address/{0}", address.users_id));
            Address itemAddress = new Address();

            try
            {
                HttpResponseMessage response = null;
                response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var request = await response.Content.ReadAsStringAsync();
                    itemAddress = JsonConvert.DeserializeObject<Address>(request);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return itemAddress;
        }

        public async Task<Address> SetAddress(Address address)
        {
            var uri = new Uri(string.Format(Constants.RestUrl + "address/edit", string.Empty));
            Address itemAddress = new Address();

            try
            {
                var postDriver = JsonConvert.SerializeObject(address);
                var content = new StringContent(postDriver, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var request = await response.Content.ReadAsStringAsync();
                    itemAddress = JsonConvert.DeserializeObject<Address>(request);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return itemAddress;
        }

        public async Task<Billing> GetBilling(Billing billing)
        {
            var uri = new Uri(string.Format(Constants.RestUrl + "address/{0}", billing.users_id));
            Billing itemBilling = new Billing();

            try
            {
                HttpResponseMessage response = null;
                response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var request = await response.Content.ReadAsStringAsync();
                    itemBilling = JsonConvert.DeserializeObject<Billing>(request);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return itemBilling;
        }

        public async Task<Billing> SetBilling(Billing billing)
        {
            var uri = new Uri(string.Format(Constants.RestUrl + "billing/edit", string.Empty));
            Billing itemBilling = new Billing();

            try
            {
                var postDriver = JsonConvert.SerializeObject(billing);
                var content = new StringContent(postDriver, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var request = await response.Content.ReadAsStringAsync();
                    itemBilling = JsonConvert.DeserializeObject<Billing>(request);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return itemBilling;
        }

        #endregion
    }
}
