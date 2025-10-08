using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ETLConnector.Model
{
    /// <summary>
    /// /** Interface Connector Operation Result. Is the result object of a connector operation **/
    /// </summary>
    public interface IConnectorOperationResult
    {
        public bool Success { get; }
        public string Method { get; }  
        public string ExternalId { get; }     
        public Dictionary<string, object> RawResponse { get; }
        public string StatusCode { get; }
        public string ErrorSummary { get; }
    }
}
