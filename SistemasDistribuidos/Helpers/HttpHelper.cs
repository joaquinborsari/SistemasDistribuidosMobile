using System;
using System.Collections.ObjectModel;
using System.Text;
using Newtonsoft.Json;

namespace SistemasDistribuidos.Helpers
{
    public class HttpHelper<T>
    {
        public async Task<T> GetRestServiceDataAsync(string serviceAddress)
        {
            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                using (HttpClient client = new HttpClient(clientHandler))
                {
                    client.BaseAddress = new Uri(serviceAddress);
                    //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + (string)App.Current.Properties["accessToken"]);
                    var response = await client.GetAsync(client.BaseAddress);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResult = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<T>(jsonResult);
                        return result;
                    }
                    else
                    {
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ObservableCollection<T>> GetRestServiceDataAsyncList(string serviceAddress)
        {
            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                using (HttpClient client = new HttpClient(clientHandler))
                {
                    client.BaseAddress = new Uri(serviceAddress);

                    //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + (string)App.Current.Properties["accessToken"]);

                    var response = await client.GetAsync(client.BaseAddress);
                    response.EnsureSuccessStatusCode();

                    var jsonResult = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ObservableCollection<T>>(jsonResult);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T GetRestServiceLoginAsync(string serviceAddress)
        {
            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                HttpClient client = new HttpClient(clientHandler);

                client.BaseAddress = new Uri(serviceAddress);
                var response = client.GetAsync(client.BaseAddress).Result;

                if (response.IsSuccessStatusCode)
                {
                    var jsonResult = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<T>(jsonResult);
                    return result;
                }
                else
                {
                    return default;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}