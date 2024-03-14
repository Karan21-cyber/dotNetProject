using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karan_Project
{
    /// <summary>
    /// Represents a class containing methods and related functionality.
    /// </summary>
    public class MethodsClass
    {
     
        /// <summary>
        /// List of method commands.
        /// </summary>
        public List<string> methodCommands = new List<string>();

        /// <summary>
        /// The name of the method.
        /// </summary>
        public string methodName = "";

        /// <summary>
        /// Instance of the <see cref="CommandParser"/> class for parsing commands.
        /// </summary>
        public CommandParser parse = new CommandParser();

        /// <summary>
        /// Dictionary to store method parameters and their values.
        /// </summary>
        public Dictionary<string, int> methodParams = new Dictionary<string, int>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodsClass"/> class.
        /// </summary>
        public MethodsClass() 
        {
        }


        /// <summary>
        /// Checks if a method exists or is contained within the class.
        /// </summary>
        /// <param name="method">The method to check.</param>
        /// <returns>True if the method exists, otherwise false.</returns>

        public bool checkMehtod(string method)
        {

            if (methodName == method)
            {
                return true;
            }
            else if (methodName.Contains(method))
            {
              //setMethodCommand variable
                return true;
            }
            else
            {
                parse.errorList.Add($"Method didn't Exist {method}");
                return false;
            }
           
        }

        /// <summary>
        /// Extracts the method call from the input string based on the provided function name.
        /// </summary>
        /// <param name="input">The input string containing the method call.</param>
        /// <param name="functionName">The name of the function.</param>
        /// <returns>The extracted method call.</returns>

        public string ExtractMehtod(string input,string functionName)
        {
            // Use regex to extract the method call only if it contains numbers
            int startIndex = functionName.IndexOf('(');
            int endIndex = functionName.LastIndexOf(')');
            //slicing the function
            string arguments = functionName.Substring(startIndex + 1, endIndex - startIndex - 1);
            
            string[] newMethod = functionName.Split('(');            
            string pattern;

            if(string.IsNullOrEmpty(arguments))
            {
                //without parameter
                 string[] cmdargs = input.Split('\n');
                 int len = cmdargs.Length-1;
                 return cmdargs[len];
            }
            else
            {
                // if there is parameterized method
                 pattern = $@"\b{Regex.Escape(newMethod[0])}\b\s*\(\s*(.*\d.*)\s*\)";    
            }
            Match match = Regex.Match(input, pattern);

            if (match.Success)
            {
                return match.Groups[0].Value.Trim();
            }

            parse.errorList.Add("Invalid Method Call.");
            return "";
        }
    }
}
