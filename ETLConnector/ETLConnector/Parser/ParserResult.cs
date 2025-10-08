using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETLConnector.Parser
{
    /// <summary>
    /// /** Class Parser Result. Is the result object of SimpleFieldParser object operation **/
    /// </summary>
    public class ParserResult : IParserResult
    {
        public object? Value { get; init; }
        public bool Error { get; init; }
        public string? ErrorMessage { get; init; }

        /// <summary>
        /// /** Returns Successful Parsing Operation Result **/
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ParserResult Ok(object? value)
        {
            return new()
            {
                Value = value,
                Error = false,
                ErrorMessage = ""
            };
        }

        /// <summary>
        /// /** Returns Unsuccessful Parsing Operation Result **/
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ParserResult Fail(string message)
        {
            return new()
            {
                Value = null,
                Error = true,
                ErrorMessage = message
            };
        }
    }
}
