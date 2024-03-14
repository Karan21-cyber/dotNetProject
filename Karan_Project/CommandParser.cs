using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Karan_Project
{

    /// <summary>
    /// Parses commands and executes drawing operations.
    /// </summary>
    public class CommandParser
    {
        // Define a List to store error messages

        private static int x, y;
        private static Color colour = Color.Black;
        private static bool fill;
        public List<string> errorList = new List<string>();
        public List<string> successList = new List<string>();
        public Dictionary<string, int> variables = new Dictionary<string, int>();
        public List<string> conditionList = new List<string> { "==", "<=", ">=", "<", ">" };
        public List<string> operatorList = new List<string> { "=", "+=", "-=", "+", "-", "/" };

        //commandlist befor sliced
        public List<string> firstCommandList = new List<string>();
        //list of commands between sliced 
        public List<string> slicedCommandList = new List<string>();
        // lis of commands after sliced
        public List<string> afterSlicedCommandList = new List<string>();


        // Instance of ShapeFactory
        ShapeFactory shape = new ShapeFactory();

        /// <summary>
        /// Initializes a new instance of the CommandParser class.
        /// </summary>
        public CommandParser() { }


        // Methods for resetting drawing parameters and handling the drawing cursor

        /// <summary>
        /// Resets the drawing parameters.
        /// </summary>
        public void reset()
        {
            x = 0;
            y = 0;
            colour = Color.Black;
            fill = false;
        }

        // Methods for resetting drawing parameters and handling the drawing cursor

        /// <summary>
        /// Resets the drawing parameters.
        /// </summary>
        public void DrawCursor(Graphics g)
        {
            Shape dot = shape.getShape("circle");
            dot.set(colour, true, x, y, 2);
            dot.draw(g);
        }

        /// <summary>
        /// Resets the drawing parameters.
        /// </summary>
        public void InitialCursor(Graphics g)
        {
            Shape dot = shape.getShape("rectangle");
            dot.set(Color.Red, true, 10, 10, 3, 3);
            dot.draw(g);
        }

        /// <summary>
        /// Sets the value of a variable in the variables dictionary.
        /// If the variable already exists, its value is updated; otherwise, a new key-value pair is added.
        /// </summary>
        /// <param name="cmd">The name of the variable.</param>
        /// <param name="num">The value to set for the variable.</param>
        public void setVariables(string cmd, int num)
        {
            if (variables.ContainsKey(cmd))
            {
                variables.Remove(cmd); // Remove the old key-value pair
                variables.Add(cmd, num);
            }
            else
            {
                variables.Add(cmd, num);
            }
            successList.Add($"Variable is set to {cmd} = {num}");
        }

        // Methods for validating commands and parameters
        /// <summary>
        /// Checks if a single command is valid.
        /// </summary>
        /// <param name="commandList">List of valid commands.</param>
        /// <param name="command">Command to validate.</param>
        /// <returns>True if the command is valid; otherwise, false.</returns>

        public bool CheckSingleCommand(List<String> commandList, string command)
        {
            try
            {
                if (commandList.Contains(command.ToLower().Trim()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("check singleCommand:" + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Checks if a number is positive or negative.
        /// </summary>
        /// <param name="num">Number to check.</param>
        /// <returns>True if the number is positive; otherwise, false.</returns>

        public bool CheckPositiveAndNegative(string param)
        {
            if (variables.ContainsKey(param))
            {
                if (variables[param] < 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (int.Parse(param) < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Checks if the given parameter is a valid integer or if it refers to a variable holding an integer value.
        /// </summary>
        /// <param name="param">The parameter to be checked for being an integer or a variable.</param>
        /// <returns>
        /// True if the parameter is a valid integer or a variable holding an integer value; otherwise, false.
        /// </returns>
        public bool CheckIntegerOrNot(string param)
        {
            int parsedInt;
            if (int.TryParse(param, out parsedInt))
            {
                // The input parameter is a valid integer
                return true;
            }
            else
            {
                // Check if the input is a variable stored in 'variables' dictionary
                if (variables.ContainsKey(param))
                {
                    // If it's a variable, check if it's holding an integer value
                    return variables[param] is int;
                }
                else
                {
                    // If neither an integer nor a variable holding an integer value
                    return false;
                }
            }
        }


        /// <summary>
        /// Checks if a given condition, in the form of a comparison between a variable and a value,
        /// is true or false based on the specified comparison item.
        /// </summary>
        /// <param name="condition">The condition to be evaluated.</param>
        /// <param name="item">The comparison item (e.g., "==", "<=", ">=").</param>
        /// <returns>
        /// True if the condition is true; otherwise, false.
        /// </returns>
        public bool isTrueCondition(string condition, string item)
        {
            string[] args = condition.Split(new string[] { item }, StringSplitOptions.None);

            if (args.Length == 2)
            {
                string variable = args[0].Trim();
                int comparisonValue;

                if (variables.ContainsKey(variable) && int.TryParse(args[1].Trim(), out comparisonValue))
                {
                    int variableValue = variables[variable];

                    switch (item)
                    {
                        case "==":
                            return variableValue == comparisonValue;
                        case "<=":
                            return variableValue <= comparisonValue;
                        case ">=":
                            return variableValue >= comparisonValue;
                        case "<":
                            return variableValue < comparisonValue;
                        case ">":
                            return variableValue > comparisonValue;
                        default:
                            return false;
                    }
                }
            }

            return false;
        }


        /// <summary>
        /// Checks if a given condition contains a valid comparison operator from the <see cref="conditionList"/>.
        /// If found, evaluates the condition using the <see cref="isTrueCondition"/> method.
        /// </summary>
        /// <param name="condition">The condition to be checked and evaluated.</param>
        /// <returns>
        /// True if the condition is true based on the comparison operator; otherwise, false.
        /// </returns>
        public bool CheckCondition(string condition)
        {
            foreach (var item in conditionList)
            {
                if (condition.Contains(item))
                {
                    return isTrueCondition(condition, item);
                }
            }
            // If no valid comparison operator is found, return false
            return false;
        }

        /// <summary>
        /// Applies arithmetic operations on a variable based on the specified arithmetic operator.
        /// </summary>
        /// <param name="condition">The condition containing the variable and value.</param>
        /// <param name="item">The arithmetic operator (e.g., "+", "+=", "-", "-=").</param>

        public void OperatorVariable(string condition, string item)
        {
            string[] args = condition.Split(new string[] { item }, StringSplitOptions.None);

            if (args.Length == 2)
            {
                string variable = args[0].Trim();
                int comparisonValue;

                if (variables.ContainsKey(variable) && int.TryParse(args[1].Trim(), out comparisonValue))
                {
                    int variableValue = variables[variable];

                    if (item.Equals("+") || item.Equals("+="))
                    {
                        int newVariable = variableValue + comparisonValue;
                        setVariables(variable, newVariable);
                    }
                    else if (item.Equals("-")|| item.Equals("-="))
                    {
                        int newValue = variableValue - comparisonValue;
                        setVariables(variable, newValue);
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Validates a command and its parameters.
        /// </summary>
        /// <param name="command">Command to validate.</param>
        /// <param name="parameters">Parameters associated with the command.</param>
        /// <param name="errorList">List to store error messages.</param>
        /// <returns>True if the command and its parameters are valid; otherwise, false.</returns>

        public bool CheckParameter(string command, string parameters)
        {
            command = command.ToUpper().Trim();
            string[] param = parameters.Split(',');

            try
            {
                switch (command)
                {
                    case "CIRCLE":
                        return ValidateParameterCountAndType(param, 1, "integer");

                    case "RECTANGLE":
                        return ValidateParameterCountAndType(param, 2, "integer");

                    case "TRIANGLE":
                        return ValidateParameterCountAndType(param, 4, "integer");

                    case "MOVETO":
                    case "DRAWTO":
                        return ValidateParameterCountAndType(param, 2, "integer");

                    case "PEN":
                        return ValidateParameterCountAndType(param, 1, "color", "red", "blue", "green", "yellow");

                    case "FILL":
                        return ValidateFillParameter(param);

                    default:
                        errorList.Add("===Invalid Command===\n" + parameters);
                        return false;
                }
            }
            catch (Exception ex)
            {
                errorList.Add("Exception occurred: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Slices the input text between the specified start and end words, returning a list of sliced texts.
        /// </summary>
        /// <param name="input">The input text to be sliced.</param>
        /// <param name="startWord">The starting word or substring for slicing (exclusive).</param>
        /// <param name="endWord">The ending word or substring for slicing (exclusive).</param>
        /// <returns>
        /// A list of sliced texts between the specified start and end words.
        /// </returns>

        public List<string> SliceText(string input, string startWord, string endWord)
        {
            List<string> result = new List<string>();

            string pattern = $@"\b{startWord}\b(.*?){endWord}\b";
            MatchCollection matches = Regex.Matches(input, pattern, RegexOptions.Singleline);

            foreach (Match match in matches)
            {
                Group capturedGroup = match.Groups[1];
                string slicedText = capturedGroup.Value.Trim();
                result.Add(slicedText);
            }

            return result;
        }

        /// <summary>
        /// Validates the count and type of parameters.
        /// </summary>
        /// <param name="param">An array of parameters.</param>
        /// <param name="expectedCount">The expected count of parameters.</param>
        /// <param name="expectedType">The expected type of parameters.</param>
        /// <param name="errorList">The list to store error messages.</param>
        /// <param name="validColors">Optional valid colors for color type parameters.</param>
        /// <returns>True if parameters are valid; otherwise, false.</returns>

        private bool ValidateParameterCountAndType(string[] param, int expectedCount, string expectedType, params string[] validColors)
        {
            if (param.Length != expectedCount)
            {
                errorList.Add($"===Parameter count should be {expectedCount}===");
                return false;
            }

            foreach (string p in param)
            {
                if (expectedType == "integer" && !CheckPositiveAndNegative(p))
                {
                    errorList.Add("====Parameter should be positive :: ===" + p);
                    return false;
                }
                else if (expectedType == "integer" && !CheckIntegerOrNot(p))
                {
                    errorList.Add("===Parameter should be an integer===");
                    return false;
                }

                if (expectedType == "color" && !validColors.Contains(p.Trim().ToLower()))
                {
                    errorList.Add("===Parameter for color should be red, blue, green, or yellow===");
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Validates the fill parameter.
        /// </summary>
        /// <param name="param">An array of parameters.</param>
        /// <param name="errorList">The list to store error messages.</param>
        /// <returns>True if the fill parameter is valid; otherwise, false.</returns>

        private bool ValidateFillParameter(string[] param)
        {
            if (param.Length != 1)
            {
                errorList.Add("===Parameter should be exactly 1===");
                return false;
            }

            string status = param[0].Trim().ToLower();
            if (status != "on" && status != "off")
            {
                errorList.Add("===Parameter should be 'on' or 'off'===");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Sets the pen color based on the provided color string.
        /// </summary>
        /// <param name="color">The color string to set the pen color.</param>

        public void SetPenColor(string color)
        {
            Dictionary<string, Color> colorMap = new Dictionary<string, Color>
            {
                { "RED", Color.Red },
                { "GREEN", Color.Green },
                { "YELLOW", Color.Yellow },
                { "BLUE", Color.Blue }
                    // Add more colors as needed
             };

            if (colorMap.ContainsKey(color.ToUpper()))
            {
                colour = colorMap[color.ToUpper()];
            }
        }

        /// <summary>
        /// Clears the graphics area with a specified color.
        /// </summary>
        public void clearFun(Graphics g)
        {
            g.Clear(Color.MintCream);
            errorList.Clear();
            successList.Clear();
            variables.Clear();
        }

        /// <summary>
        /// Retrieves the numeric value associated with the specified parameter.
        /// If the parameter is a key in the 'variables' dictionary, the corresponding value is returned.
        /// Otherwise, the parameter is parsed as an integer and returned.
        /// </summary>
        /// <param name="param">The parameter for which to retrieve the numeric value.</param>
        /// <returns>
        /// The numeric value associated with the parameter.
        /// </returns>
        public int getNum(string param)
        {
            int num;
            if (variables.ContainsKey(param))
            {
                num = variables[param];
            }
            else
            {
                num = int.Parse(param);
            }
            return num;
        }

        /// <summary>
        /// Executes a specific command based on the input provided.
        /// </summary>
        /// <param name="commands">The command to execute.</param>
        /// <param name="g">The graphics object used for drawing.</param>

        public void CommandExecute(string commands, Graphics g)
        {
            //split commands and parameter
            try
            {
                string[] args = commands.Split(' ');
                string command = args[0].Trim().ToUpper();
                switch (command)
                {
                    case "RESET":
                        reset();
                        break;
                    case "CLEAR":
                        clearFun(g);
                        break;
                    case "MOVETO":
                        x = getNum(args[1].Split(',')[0]);
                        y = getNum(args[1].Split(',')[1]);
                        DrawCursor(g);
                        break;
                    case "DRAWTO":
                        int xPoint = getNum(args[1].Split(',')[0]);
                        int yPoint = getNum(args[1].Split(',')[1]);
                        Shape line = shape.getShape("drawto");
                        line.set(colour, true, x, y, xPoint, yPoint);
                        line.draw(g);
                        break;
                    case "RECTANGLE":
                        int width = getNum(args[1].Split(',')[0]);
                        int height = getNum(args[1].Split(',')[1]);
                        Shape rectangle = shape.getShape("rectangle");
                        rectangle.set(colour, fill, x, y, width, height);
                        rectangle.draw(g);
                        break;
                    case "TRIANGLE":
                        int x2 = getNum(args[1].Split(',')[0]);
                        int y2 = getNum(args[1].Split(',')[1]);
                        int x3 = getNum(args[1].Split(',')[2]);
                        int y3 = getNum(args[1].Split(',')[3]);
                        Shape triangle = shape.getShape("triangle");
                        triangle.set(colour, fill, x, y, x2, y2, x3, y3);
                        triangle.draw(g);
                        break;
                    case "CIRCLE":
                        int radius = getNum(args[1].Split(',')[0]);
                        Shape circle = shape.getShape("circle");
                        circle.set(colour, fill, x, y, radius);
                        circle.draw(g);
                        break;
                    case "PEN":
                        string color = args[1].Trim().ToUpper();
                        SetPenColor(color);
                        break;
                    case "FILL":
                        string status = args[1].Trim().ToUpper();
                        if (status == "ON")
                        {
                            fill = true;
                        }
                        else
                        {
                            fill = false;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}