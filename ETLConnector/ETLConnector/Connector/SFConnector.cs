using ETLConnector.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETLConnector.Connector
{
    /// <summary>
    ///  /** SalesForce Class Connector. Is the configurable object of a SalesForce connector **/
    /// </summary>
    internal class SFConnector : Connector
    {

        /// <summary>
        /// /** SalesForce Class Constructor **/
        /// </summary>
        /// <param name="instanceUrl"></param>
        /// <param name="baseUrl"></param>
        /// <param name="apiVersion"></param>
        public SFConnector(string instanceUrl, string baseUrl, string apiVersion) : base(instanceUrl, baseUrl, apiVersion)
        {
            InstanceUrl = instanceUrl;
            BaseURL = baseUrl;
            ApiVersion = apiVersion;
        }

        public async Task<ConnectorOperationResult> AuthenticateAsync(
            Dictionary<string, string> body,
            string httpMethod,
            Dictionary<string, string> additionalHeaders )
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                (message, cert, chain, errors) => true
            };

            if (object.Equals(body, null)) throw new ArgumentNullException("Exception : " + nameof(body));
            if (string.IsNullOrWhiteSpace(httpMethod)) throw new ArgumentNullException("Exception : " + nameof(httpMethod));

            var upper = httpMethod.Trim().ToUpperInvariant();
            var method = new HttpMethod(upper);
            bool isPost = upper == "POST";
            bool isPatch = upper == "PATCH";
            bool isPut = upper == "PUT";
            bool isGet = upper == "GET";
            bool isDelete = upper == "DELETE";

            // Configuration en-tête de la requête
            var url = $"{this.InstanceUrl}/{this.BaseURL}";


            using var http = new HttpClient(handler)
            {
                Timeout = TimeSpan.FromSeconds(100)
            };


            // Ajout des en-tête supplémentaires
            foreach (string key in additionalHeaders.Keys)
            {
                http.DefaultRequestHeaders.Add(key, additionalHeaders[key]);
            }

            Console.WriteLine(url);

            // Configuration du body de la requête
            using var req = new HttpRequestMessage(method, url);


            //si update ou création
            if (isPost || isPatch || isPut)
            {
                var content = new FormUrlEncodedContent(body);
                req.Content = content;
            }

            int value = 0;

            try
            {
                // Appel http
                using var resp = await http.SendAsync(req).ConfigureAwait(false);
                var raw = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);

                Dictionary<string, object> createResp = null;

                value = (int)resp.StatusCode;

                // Si un code de succès ( httpcode = 200 )
                if (resp.IsSuccessStatusCode)
                {


                    try
                    {
                        createResp = JsonConvert.DeserializeObject<Dictionary<string, object>>(raw);
                    }
                    catch
                    {

                    }

                    //succès
                    return ConnectorOperationResult.Ok(createResp, resp.StatusCode.ToString(), method.ToString(), value.ToString(), url);
                }
                // Si un code d'erreur ( httpcode >= 400 )
                else
                {

                    string errSummary = raw;
                    return ConnectorOperationResult.Fail(createResp, resp.StatusCode.ToString(), errSummary, method.ToString(), value.ToString(), url);

                }
            }
            // Exception
            catch (Exception ex)
            {
                return ConnectorOperationResult.Fail(new Dictionary<string, object>(), "0", ex.Message + " " + ex.StackTrace, method.ToString(), value.ToString(), url);
            }

        }


        /// <summary>
        /// SendSObjectAsync Function :  Main Connector operation for synchronising data through APIs
        /// </summary>
        /// <param name="body"></param>
        /// <param name="httpMethod"></param>
        /// <param name="objectApiName"></param>
        /// <param name="objectId"></param>
        /// <param name="additionalHeaders"></param>
        /// <param name="maxTimeoutInSeconds"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public override async Task<ConnectorOperationResult> SendObjectAsync(
            object body,
            string httpMethod,
            string objectApiName,
            string objectId,
            Dictionary<string, string> additionalHeaders,
            int maxTimeoutInSeconds)
        {
            //eviter les erreurs de certificat ssl
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                (message, cert, chain, errors) => true
            };

            if (object.Equals(body, null)) throw new ArgumentNullException("Exception : " + nameof(body));
            if (string.IsNullOrWhiteSpace(httpMethod)) throw new ArgumentNullException("Exception : " + nameof(httpMethod));

            var upper = httpMethod.Trim().ToUpperInvariant();
            var method = new HttpMethod(upper);
            bool isPost = upper == "POST";
            bool isPatch = upper == "PATCH";
            bool isPut = upper == "PUT";
            bool isGet = upper == "GET";
            bool isDelete = upper == "DELETE";

            // Configuration en-tête de la requête
            var baseUrl = $"{this.InstanceUrl}/{this.BaseURL}";
            var url = string.IsNullOrWhiteSpace(objectApiName) ? baseUrl : $"/{objectApiName}";
            url = string.IsNullOrWhiteSpace(objectId) ? baseUrl : $"{url}/{objectId}";


            using var http = new HttpClient(handler)
            {
                Timeout = TimeSpan.FromSeconds(maxTimeoutInSeconds)
            };

            //ajout bearer token si pas null
            if (string.IsNullOrEmpty(this.AccessToken) == false)
            {
                http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this.AccessToken);
            }

            // Ajout des en-tête supplémentaires
            foreach (string key in additionalHeaders.Keys)
            {
                http.DefaultRequestHeaders.Add(key, additionalHeaders[key]);
            }

            Console.WriteLine(url);
            // Configuration du body de la requête
            using var req = new HttpRequestMessage(method, url);
            //si update ou création
            if (isPost || isPatch || isPut)
            {
                var json = JsonConvert.SerializeObject(body);
                req.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }
            int value = 0;
            try
            {
                // Appel http
                using var resp = await http.SendAsync(req).ConfigureAwait(false);
                var raw = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);

                Dictionary<string, object> createResp = null;

                value = (int)resp.StatusCode;

                // Si un code de succès ( httpcode = 200 )
                if (resp.IsSuccessStatusCode)
                {


                    try
                    {
                        createResp = JsonConvert.DeserializeObject<Dictionary<string, object>>(raw);
                    }
                    catch
                    {

                    }

                    //succès
                    return ConnectorOperationResult.Ok(createResp, resp.StatusCode.ToString(), method.ToString(), value.ToString(), url);
                }
                // Si un code d'erreur ( httpcode >= 400 )
                else
                {

                    string errSummary = raw;
                    return ConnectorOperationResult.Fail(createResp, resp.StatusCode.ToString(), errSummary, method.ToString(), value.ToString(), url);

                }
            }
            // Exception
            catch (Exception ex)
            {
                return ConnectorOperationResult.Fail(new Dictionary<string, object>(), "0", ex.Message + " " + ex.StackTrace, method.ToString(), value.ToString(), url);
            }
        }
    }
}
