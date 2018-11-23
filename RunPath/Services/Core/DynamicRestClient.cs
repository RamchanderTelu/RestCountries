using System;
 
namespace RestCountries
{
    using System.Diagnostics.CodeAnalysis;
    using RestSharp;
    using Newtonsoft.Json;
    using RestSharp.Deserializers;

    public class DynamicRestClient : RestClient
    {
        public DynamicRestClient()
        {
            this.AddHandler("application/json", new  JsonDeserializer());
        }

        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#")]
        public DynamicRestClient(Uri baseUrl)
            : base(baseUrl)
        {
            this.AddHandler("application/json", new  JsonDeserializer());       
        }

        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#")]
        public DynamicRestClient(Uri baseUrl, string id)
            : base(baseUrl)
        {
            this.AddHandler("application/json", new JsonDeserializer());
            this.AddDefaultHeader("Authorization", "Bearer "+id);
        }

       public class DynamicJsonDeserializer : IDeserializer
        {
            #region Public Properties

            public string DateFormat { get; set; }

            public string Namespace { get; set; }

            public string RootElement { get; set; }

            #endregion

            #region Public Methods and Operators

            public T Deserialize<T>(IRestResponse response)
            {
                if (response == null)
                {
                    throw new ArgumentNullException("response");
                }

                return JsonConvert.DeserializeObject<T>(response.Content);
            }



            #endregion
        }
     
    }
}
