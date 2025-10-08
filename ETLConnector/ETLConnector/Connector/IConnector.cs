using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETLConnector.Connector
{
    /// <summary>
    /// /** Interface Connector. Is the configurable object of a connector **/
    /// </summary>
    public interface IConnector
    {
        public string AccessToken { get; }
        public string InstanceUrl { get; }
        public string ApiVersion { get; }
        public string ObjectApiName { get; }
        public string BaseURL { get; }
    }
}
