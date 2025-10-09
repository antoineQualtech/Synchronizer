using ETLConnector.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ETLConnector.Connector
{
    internal class EpicorConnector : Connector
    {
        /// <summary>
        /// /** Epicor Class Constructor **/
        /// </summary>
        /// <param name="instanceUrl"></param>
        /// <param name="baseUrl"></param>
        /// <param name="apiVersion"></param>
        public EpicorConnector(string instanceUrl, string baseUrl, string apiVersion) : base(instanceUrl, baseUrl, apiVersion)
        {
            InstanceUrl = instanceUrl;
            BaseURL = baseUrl;
            ApiVersion = apiVersion;
        }


        /// <summary>
        /// /** Epicor Class Constructor **/
        /// </summary>
        /// <param name="body"></param>
        /// <param name="httpMethod"></param>
        /// <param name="additionalHeaders"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<ConnectorOperationResult> AuthenticateAsync(
            Dictionary<string, string> body,
            string httpMethod,
            Dictionary<string, string> additionalHeaders)
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
            http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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
    }
}
