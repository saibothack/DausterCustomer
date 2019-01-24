using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DausterCustomer.Helpers;
using DausterCustomer.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms.GoogleMaps;

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

        public async Task<List<TypeCard>> GetTypeCardsAsync()
        {
            var uri = new Uri(string.Format(Constants.RestUrl + "type-cards"));
            List<TypeCard> items = new List<TypeCard>();

            try
            {
                HttpResponseMessage response = null;
                client.DefaultRequestHeaders
                    .Authorization = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);
                client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = await client.GetAsync(uri);

                var request = await response.Content.ReadAsStringAsync();
                items = JsonConvert.DeserializeObject<List<TypeCard>>(request);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return items;
        }

        public async Task<List<Vehicle>> GetVehicleAsync()
        {
            var uri = new Uri(string.Format(Constants.RestUrl + "vehicles"));
            List<Vehicle> items = new List<Vehicle>();

            try
            {
                HttpResponseMessage response = null;
                response = await client.GetAsync(uri);

                var request = await response.Content.ReadAsStringAsync();
                items = JsonConvert.DeserializeObject<List<Vehicle>>(request);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return items;
        }
        #endregion

        #region User

        public async Task<UserLogin> LoginAsync(User item)
        {
            var uri = new Uri(string.Format(Constants.RestUrl + "login", string.Empty));
            UserLogin itemUser = new UserLogin();

            try
            {
                var postDriver = JsonConvert.SerializeObject(item);
                var content = new StringContent(postDriver, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var request = await response.Content.ReadAsStringAsync();
                    itemUser = JsonConvert.DeserializeObject<UserLogin>(request);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return itemUser;
        }


        public async Task<User> GetUser()
        {
            var uri = new Uri(string.Format(Constants.RestUrl + "users/details"));
            User itemUser = new User();

            try
            {
                HttpResponseMessage response = null;
                client.DefaultRequestHeaders
                    .Authorization = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);
                client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = await client.GetAsync(uri);

                var request = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    itemUser = JsonConvert.DeserializeObject<User>(request);
                }
                else {
                    itemUser = JsonConvert.DeserializeObject<User>(request);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return itemUser;
        }



        #region "Alta usuarios"

        public async Task<UserLogin> SetUser(User user)
        {
            var uri = new Uri(string.Format(Constants.RestUrl));

            if (Settings.IsLoggedIn)
                uri = new Uri(string.Format(Constants.RestUrl + "users/edit"));
            else
                uri = new Uri(string.Format(Constants.RestUrl + "register"));

            UserLogin itemUser = new UserLogin();
            
            try
            {
                var postDriver = JsonConvert.SerializeObject(user);
                var content = new StringContent(postDriver, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;

                if (Settings.IsLoggedIn) {
                    client.DefaultRequestHeaders
                    .Authorization = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);
                    client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }

                response = await client.PostAsync(uri, content);

                var request = await response.Content.ReadAsStringAsync();
                itemUser = JsonConvert.DeserializeObject<UserLogin>(request);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return itemUser;
        }

        public async Task<UserLogin> SetAddress(Address address)
        {
            var uri = new Uri(string.Format(Constants.RestUrl + "address/set", string.Empty));
            UserLogin itemAddress = new UserLogin();

            try
            {
                var postDriver = JsonConvert.SerializeObject(address);
                var content = new StringContent(postDriver, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                client.DefaultRequestHeaders
                    .Authorization = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);
                client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = await client.PostAsync(uri, content);

                var request = await response.Content.ReadAsStringAsync();
                itemAddress = JsonConvert.DeserializeObject<UserLogin>(request);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return itemAddress;
        }

        public async Task<UserLogin> SetBilling(Billing billing)
        {
            var uri = new Uri(string.Format(Constants.RestUrl + "billing/set", string.Empty));
            UserLogin itemBilling = new UserLogin();

            try
            {
                var postDriver = JsonConvert.SerializeObject(billing);
                var content = new StringContent(postDriver, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                client.DefaultRequestHeaders
                    .Authorization = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);
                client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = await client.PostAsync(uri, content);

                var request = await response.Content.ReadAsStringAsync();
                itemBilling = JsonConvert.DeserializeObject<UserLogin>(request);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return itemBilling;
        }

        #endregion

        public async Task<Address> GetAddress()
        {
            var uri = new Uri(string.Format(Constants.RestUrl + "address/get"));
            Address itemAddress = new Address();

            try
            {
                HttpResponseMessage response = null;
                client.DefaultRequestHeaders
                    .Authorization = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);
                client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = await client.GetAsync(uri);

                var request = await response.Content.ReadAsStringAsync();
                itemAddress = JsonConvert.DeserializeObject<Address>(request);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return itemAddress;
        }

        public async Task<Billing> GetBilling()
        {
            var uri = new Uri(string.Format(Constants.RestUrl + "billing/get"));
            Billing itemBilling = new Billing();

            try
            {
                HttpResponseMessage response = null;
                client.DefaultRequestHeaders
                    .Authorization = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);
                client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = await client.GetAsync(uri);

                var request = await response.Content.ReadAsStringAsync();
                itemBilling = JsonConvert.DeserializeObject<Billing>(request);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return itemBilling;
        }

        public async Task<List<PaymentMethods>> GetPaymentMethodsAsync() {
            var uri = new Uri(string.Format(Constants.RestUrl + "payment-methods/get"));
            List<PaymentMethods> items = new List<PaymentMethods>();

            try
            {
                HttpResponseMessage response = null;
                client.DefaultRequestHeaders
                    .Authorization = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);
                client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = await client.GetAsync(uri);

                var request = await response.Content.ReadAsStringAsync();
                items = JsonConvert.DeserializeObject<List<PaymentMethods>>(request);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return items;
        }

        public async Task<UserLogin> SetPaymenthMethodsAsync(PaymentMethods payment) {
            var uri = new Uri(string.Format(Constants.RestUrl + "payment-methods/set", string.Empty));
            UserLogin itemBilling = new UserLogin();

            try
            {
                var postDriver = JsonConvert.SerializeObject(payment);
                var content = new StringContent(postDriver, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                client.DefaultRequestHeaders
                    .Authorization = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);
                client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = await client.PostAsync(uri, content);

                var request = await response.Content.ReadAsStringAsync();
                itemBilling = JsonConvert.DeserializeObject<UserLogin>(request);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return itemBilling;
        }

        public async Task<JObject> getAsyncRouteGoogle(List<Pin> lsPins)
        {
            string origin = lsPins[0].Position.Latitude.ToString() + "," + lsPins[0].Position.Longitude.ToString();
            string destination = lsPins[lsPins.Count - 1].Position.Latitude.ToString() + "," + lsPins[lsPins.Count - 1].Position.Longitude.ToString();
            string waypoints = string.Empty;

            if (lsPins.Count > 2)
            {
                for (int i = 1; i < (lsPins.Count - 1); i++) {
                    waypoints += (!string.IsNullOrEmpty(waypoints) ? "|" : string.Empty) + lsPins[i].Position.Latitude.ToString() + 
                        "," + lsPins[i].Position.Longitude.ToString();
                }
            }

            string baseUrl = "https://maps.googleapis.com/maps/api/directions/json?";

            string urlComplete = baseUrl + "origin=" + origin + "&destination=" + destination +
                (!string.IsNullOrEmpty(waypoints) ? "&waypoints=" + destination : string.Empty) +
                "&key=" + Constants.googleKey;

            var uri = new Uri(urlComplete);

            JObject routes = new JObject();

            try
            {
                HttpResponseMessage response = null;
                response = await client.GetAsync(uri);

                var request = await response.Content.ReadAsStringAsync();
                routes = (JObject)JsonConvert.DeserializeObject(request);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return routes;
        }

        #endregion

        public async Task<float> GetQuotation(Dictionary<string, int> valuesCoordinates)
        {
            var uri = new Uri(string.Format(Constants.RestUrl + "services/quotation", string.Empty));
            float itemBilling = 0;

            try
            {
                var postDriver = JsonConvert.SerializeObject(valuesCoordinates);
                var content = new StringContent(postDriver, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                client.DefaultRequestHeaders
                    .Authorization = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);
                client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = await client.PostAsync(uri, content);

                var request = await response.Content.ReadAsStringAsync();
                itemBilling = JsonConvert.DeserializeObject<float>(request);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return itemBilling;
        }

        public async Task deletePaymentMethod(int id)
        {
            var uri = new Uri(string.Format(Constants.RestUrl + "payment-methods/{0}", id));

            try
            {
                HttpResponseMessage response = null;
                client.DefaultRequestHeaders
                    .Authorization = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);
                client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = await client.DeleteAsync(uri);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
        }
    }
}
