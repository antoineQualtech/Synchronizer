using ETLConnector.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ETLConnector.Connector
{
    /// <summary>
    ///  /** Class Connector. Is the configurable object of a connector **/
    /// </summary>
    public class Connector : IConnector
    {
        public string AccessToken { get; set; }

        public string InstanceUrl { get; set; }

        public string BaseURL { get; set; }

        public string ApiVersion { get; set; }

        public string ObjectApiName { get; set; }

        /// <summary>
        /// /** Connector Constructor **/
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="instanceUrl"></param>
        /// <param name="baseUrl"></param>
        /// <param name="apiVersion"></param>
        /// <param name="objectApiName"></param>
        public Connector(string accessToken, string instanceUrl, string baseUrl, string apiVersion, string objectApiName)
        {
            AccessToken = accessToken; 
            InstanceUrl = instanceUrl; 
            BaseURL = baseUrl; 
            ApiVersion = apiVersion; 
            ObjectApiName = objectApiName; 
        }



        /// <summary>
        /// SendSObjectAsync Function :  Main Connector operation for synchronising data through APIs
        /// </summary>
        /// <param name="body"></param>
        /// <param name="httpMethod"></param>
        /// <param name="salesforceIdForPatch"></param>
        /// <param name="additionalHeaders"></param>
        /// <returns> ConnectorOperationResult </returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<ConnectorOperationResult> SendSObjectAsync(
            object body,
            string httpMethod,
            string salesforceIdForPatch,
            Dictionary<string,string> additionalHeaders)
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                (message, cert, chain, errors) => true
            };

            if (object.Equals(body,null)) throw new ArgumentNullException("Exception : "+nameof(body));
            if (string.IsNullOrWhiteSpace(httpMethod)) throw new ArgumentNullException("Exception : "+nameof(httpMethod));
            //if (string.IsNullOrWhiteSpace(salesforceIdForPatch)) throw new ArgumentNullException(nameof(salesforceIdForPatch));

            var upper = httpMethod.Trim().ToUpperInvariant();
            var method = new HttpMethod(upper);
            bool isPost = upper == "POST";
            bool isPatch = upper == "PATCH";
            bool isPut = upper == "PUT";
            bool isGet = upper == "GET";
            bool isDelete = upper == "DELETE";

            // Configuration en-tête de la requête
            var baseUrl = $"{this.InstanceUrl}/{this.BaseURL}/{this.ObjectApiName}";
            var url = string.IsNullOrWhiteSpace(salesforceIdForPatch) ? baseUrl : $"{baseUrl}/{salesforceIdForPatch}";       
            using var http = new HttpClient(handler)
            {
                Timeout = TimeSpan.FromSeconds(100)
            };
            http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this.AccessToken);
            // Ajout des en-tête supplémentaires
            foreach (string key in additionalHeaders.Keys)
            {
                http.DefaultRequestHeaders.Add(key, additionalHeaders[key]);
            }
           
            Console.WriteLine(url);
            // Configuration du body de la requête
            using var req = new HttpRequestMessage(method, url);
            //si update ou création
            if(isPost || isPatch || isPut)
            {
                var json = JsonConvert.SerializeObject(body);
                req.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            try
            {   
                // Appel http
                using var resp = await http.SendAsync(req).ConfigureAwait(false);
                var raw = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);

                Dictionary<string, object> createResp = null;

                // Si un code de succès ( httpcode = 200 )
                if (resp.IsSuccessStatusCode)
                {

                    string id = salesforceIdForPatch;

                    try
                    {
                        createResp = JsonConvert.DeserializeObject<Dictionary<string, object>>(raw);
                    }
                    catch
                    {

                    }

                    //succès
                    return ConnectorOperationResult.Ok(createResp, resp.StatusCode.ToString(), method.ToString());
                }
                // Si un code d'erreur ( httpcode >= 400 )
                else
                {
     
                    string errSummary = raw;
                    return ConnectorOperationResult.Fail(createResp, resp.StatusCode.ToString(), errSummary, method.ToString());
                    
                }
            }
            // Exception
            catch (Exception ex)
            {
                return ConnectorOperationResult.Fail(new Dictionary<string, object>(),"0", ex.Message +" "+ ex.StackTrace, method.ToString());
            }
        }
    }
}
