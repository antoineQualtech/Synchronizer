using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETLConnector.Parser
{
    /// <summary>
    /// /** Interface Parser Result. Is the result object of SimpleFieldParser object operation **/
    /// </summary>
    public interface IParserResult
    {
        object? Value { get; }
        bool Error { get; }
        string? ErrorMessage { get; }
    }
}
