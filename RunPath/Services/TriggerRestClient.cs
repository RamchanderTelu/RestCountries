using RestSharp;
using RestSharp.Deserializers;
using System;


namespace RestCountries
{
 public class TriggerRestClient: RestClient
    {
        public RestRequest Request;
        public DynamicRestClient Client;
        public TriggerRestClient()
        {
            this.AddHandler("application/json", new JsonDeserializer());
        }

        public TriggerRestClient(Uri baseUri) : base(baseUri)
        {
            this.AddHandler("application/json", new JsonDeserializer());
        }

        public IRestResponse<T> RestRequestExecute<T>(Method method, string url) where T : new()
        {
            try
            {
                this.Request = new RestSharp.RestRequest(method);
                this.Request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
                this.Request.RequestFormat = DataFormat.Json;
                this.Client = new DynamicRestClient(new Uri(url));
                return this.Client.Execute<T>(this.Request);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
