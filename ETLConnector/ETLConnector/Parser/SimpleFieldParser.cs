using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETLConnector.Parser
{
    /// <summary>
    /// Class SimpleFieldParser. Object used for parsing rawvalues.
    /// </summary>
    public class SimpleFieldParser
    {

        /// <summary>
        /// Parse Function : Main SimpleFieldParser operation for parsing rawvalues
        /// </summary>
        /// <param name="rawValue"></param>
        /// <param name="targetType"></param>
        /// <param name="size"></param>
        /// <param name="nullable"></param>
        /// <returns></returns>
        public ParserResult Parse(object? rawValue, Type targetType, int? size)
        {
            try
            {
                // null / DBNull
                if (rawValue == null || rawValue is DBNull)
                {
                    
                    return ParserResult.Fail($"Parameter {nameof(rawValue)} input is null.");
                       
                }

                // type est déjà ok?
                if (rawValue.GetType() == targetType) {
                    return ParserResult.Ok(rawValue);
                }

                // ajust size des strings (truncate)
                if (rawValue.GetType() == typeof(string))
                {
                    var s = Convert.ToString(rawValue, CultureInfo.InvariantCulture) ?? string.Empty;
                    if (size is int max && s.Length > max)
                    {
                        s = s.Substring(0, max);
                    }
                    return ParserResult.Ok(s);
                }

                // utiliser Convert.ChangeType avec InvariantCulture
                return ParserResult.Ok(Convert.ChangeType(rawValue, targetType, System.Globalization.CultureInfo.InvariantCulture));
            }
            catch (Exception ex) {
                return ParserResult.Fail($"Exception thrown while converting value. rawValue {rawValue} to targetType {targetType.Name}");
            }
        }
    }
}
