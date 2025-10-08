using ETLConnector.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ETLConnector.Model
{
    /// <summary>
    /// /** Class Connector Operation Result. Is the result object of a connector operation **/
    /// </summary>
    public class ConnectorOperationResult : IConnectorOperationResult
    {
        public bool Success { get; set; }

        public string Method { get; set; }

        public string ExternalId { get; set; }

        public Dictionary<string,object> RawResponse { get; set; }

        public string StatusCode { get; set; }

        public string ErrorSummary { get; set; }

        /// <summary>
        /// /** Returns Successful Connector Operation Result **/
        /// </summary>
        /// <param name="rawResponse"></param>
        /// <param name="statusCode"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ConnectorOperationResult Ok(Dictionary<string, object> rawResponse, string statusCode, string method)
        {
            return new() {
                Success = true,
                RawResponse = rawResponse,
                ErrorSummary = "",
                StatusCode = statusCode,
                Method = method
            };
        }

        /// <summary>
        /// /** Returns Unsuccessful Connector Operation Result **/
        /// </summary>
        /// <param name="rawResponse"></param>
        /// <param name="statusCode"></param>
        /// <param name="errorSummary"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ConnectorOperationResult Fail(Dictionary<string, object> rawResponse, string statusCode, string errorSummary, string method)
        {
            return new() { 
                Success = false,
                RawResponse = rawResponse,
                ErrorSummary = errorSummary, 
                StatusCode = statusCode, 
                Method = method 
            };
        } 
    }
}
