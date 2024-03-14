using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karan_Project
{
    /// <summary>
    /// The main form of the application.
    /// </summary>
    public partial class Form1 : Form
    {
        Graphics g;
        CommandParser check = new CommandParser();
        List<string> commands = new List<string> { "clear", "reset" };
        List<string> paramsCommand = new List<string> { "moveto", "drawto", "rectangle", "circle", "triangle", "pen", "fill", "if", "endif", "while", "endloop", "method", "endmethod" };
        MethodsClass md = new MethodsClass();

        List<string> initialToStartHere = new List<string>();
        List<string> startToEndHere = new List<string>();
        List<string> endHereToLast = new List<string>();


        /// <summary>
        /// Initializes a new instance of the Form1 class.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            g = outputWindow.CreateGraphics();
        }

        /// <summary>
        /// Display the Success Message
        /// </summary>
        private void ShowSuccessMessage()
        {
            try
            {
                successTextBox.ForeColor = Color.Green;
                foreach (var error in check.successList)
                {
                    successTextBox.Text += error + "\n"; // Appending each error to the errorText
                }
                check.successList.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Display the Error Message
        /// </summary>
        private void ShowErrorMessage()
        {
            try
            {
                errorTextBox.ForeColor = Color.Red;
                // Add the message to the error list
                foreach (var error in check.errorList)
                {
                    errorTextBox.Text += error + "\n"; // Appending each error to the errorText
                }
                check.errorList.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Clear the Editor
        /// </summary>
        private void clearText()
        {
            successTextBox.Text = "";
            errorTextBox.Text = "";
            check.variables.Clear();
            initialToStartHere.Clear();
            startToEndHere.Clear();
            endHereToLast.Clear();
            md.methodName = "";

        }

        /// <summary>
        /// Executes a single line command based on the provided type.
        /// </summary>
        /// <param name="command">The command string.</param>
        /// <param name="type">The type of command.</param>
        private void SingleLineCommand(string command, string type)
        {
            try
            {
                string[] args = command.Split(' ');

                if (command.Contains('='))
                {
                    string[] pars = command.Split('=');
                    foreach (var item in check.operatorList)
                    {
                        if (pars[1].Contains(item))
                        {
                            check.OperatorVariable(pars[1], item);
                        }
                    }
                    try
                    {
                        if (!string.IsNullOrEmpty(pars[1]))
                        {
                            int num = int.Parse(pars[1]);
                            check.setVariables(pars[0], num);
                        }
                    }
                    catch (Exception ex)
                    {
                        check.errorList.Add(ex.Message);
                    }
                }
                else
                {
                    if (args.Length == 1)
                    {
                        ProcessSingleCommand(args[0], type, command);
                    }
                    else if (args.Length > 1 && args.Length == 2)
                    {
                        string[] argsparams = args[1].Split(',');

                        if (argsparams.Length == 1 && check.variables.ContainsKey(args[1]))
                        {
                            string newargs = check.variables[args[1]].ToString();
                            ProcessCommandWithParameters(args[0], newargs, type, command);
                        }
                        else if (argsparams.Length == 2 && check.variables.Count > 1)
                        {
                            List<string> parsargs = new List<string>(); // Added parentheses to instantiate the List

                            foreach(var line in argsparams)
                            {
                                if (check.variables.ContainsKey(line)){
                                    parsargs.Add(check.variables[line].ToString());
                                }
                                else
                                {
                                    parsargs.Add(line);
                                }
                                
                            }

                            string newargs = string.Join(",", parsargs); // Used string.Join to concatenate elements in the list

                            ProcessCommandWithParameters(args[0], newargs, type, command);
                        }
                        else
                        {
                            ProcessCommandWithParameters(args[0], args[1], type, command);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                check.errorList.Add(ex.Message);
            }

        }

        /// <summary>
        /// Processes a single command without parameters.
        /// </summary>
        /// <param name="singleCommand">The single command to process.</param>
        /// <param name="type">The type of command.</param>
        /// <param name="fullCommand">The full command string.</param>
        private void ProcessSingleCommand(string singleCommand, string type, string fullCommand)
        {
            bool isSingleCommandValid = check.CheckSingleCommand(commands, singleCommand);
            if (isSingleCommandValid)
            {
                ProcessCommandExecution(type, fullCommand);
                check.successList.Add("====" + type + " Success====.\n" + "Command:: " + fullCommand);
            }
        }

        /// <summary>f
        /// Processes a command with parameters, validating the command and its parameters.
        /// </summary>
        /// <param name="command">The command string.</param>
        /// <param name="parameters">The parameters associated with the command.</param>
        /// <param name="type">The type of command.</param>
        /// <param name="fullCommand">The full command string.</param>
        private void ProcessCommandWithParameters(string command, string parameters, string type, string fullCommand)
        {
            bool isSingleCommandValid = check.CheckSingleCommand(paramsCommand, command);

            if (isSingleCommandValid && check.CheckParameter(command, parameters))
            {
                ProcessCommandExecution(type, fullCommand);
                check.successList.Add("====" + type + " Success====.\n" + "Command:: " + fullCommand);
            }
        }

        /// <summary>
        /// Executes a command based on the specified type.
        /// </summary>
        /// <param name="type">The type of command.</param>
        /// <param name="fullCommand">The full command string.</param>
        private void ProcessCommandExecution(string type, string fullCommand)
        {
            if (type == "Run")
            {
                check.CommandExecute(fullCommand, g);
            }
        }


        /// <summary>
        /// Display Success or Error Message
        /// </summary>
        public void ShowErrors()
        {
            if (check.errorList.Count > 0)
            {
                ShowErrorMessage();
            }
            if (check.successList.Count > 0)
            {
                ShowSuccessMessage();
            }
        }

        /// <summary>
        /// Processes a single command without parameters.
        /// </summary>
        /// <param name="singleCommand">The single command to process.</param>
        /// <param name="type">The type of command.</param>
        /// <param name="fullCommand">The full command string.</param>
        private void syntax_btn_click(object sender, EventArgs e)
        {
            string multipleCommandText = multipleCommand.Text;
            string commandBox2Text = commandBox2.Text;

            Thread thread3 = new Thread(() => ExecuteCommand(multipleCommandText,"Build"));
            Thread thread4 = new Thread(() => ExecuteCommand(commandBox2Text,"Build"));

            thread3.Start();
            thread4.Start();

            ShowErrors();
        }


        /// <summary>
        /// Receives and processes a sliced command based on specified start and end items.
        /// </summary>
        /// <param name="fullcommand">The full command to be sliced.</param>
        /// <param name="startItem">The starting item for slicing.</param>
        /// <param name="endItem">The ending item for slicing.</param>
        /// <param name="type">The type of command.</param>
        public void sliceCommandReceiver(string fullcommand, string startItem, string endItem, string type)
        {
            // clear the list
            initialToStartHere.Clear();
            startToEndHere.Clear();
            endHereToLast.Clear();

            // initialization of the list
            initialToStartHere = check.SliceText(fullcommand, "", startItem);
            startToEndHere = check.SliceText(fullcommand, startItem, endItem);
            endHereToLast = check.SliceText(fullcommand, endItem, "\n");

            string[] initcmd = initialToStartHere[0].Split('\n');
            for (int i = 0; i < initcmd.Length; i++)
            {
                SingleLineCommand(initcmd[i].Trim(), type);
            }


            if (startItem == "if" && endItem == "endif")
            {
                string[] args = startToEndHere[0].Split('\n');
                if (check.CheckCondition(args[0]))
                {
                    for (int i = 0; i < args.Length; i++)
                    {
                        if (i != 0)
                        {
                            SingleLineCommand(args[i].Trim(), type);
                        }
                    }
                }
                else
                {
                    check.errorList.Add($"Invalid condition: {startToEndHere[0]}");
                }

            }
            else if (startItem == "while" && endItem == "endloop")
            {
                string[] args = startToEndHere[0].Split('\n');

                if (check.CheckCondition(args[0]))
                {
                    while (check.CheckCondition(args[0]))
                    {
                        for (int i = 0; i < args.Length; i++)
                        {
                            if (i != 0)
                            {
                                SingleLineCommand(args[i].Trim(), type);
                            }
                        }
                    }
                }
                else
                {
                    check.errorList.Add($"Invalid condition: {startToEndHere[0]}");
                }
            }
            else if (startItem == "method" && endItem == "endmethod")
            {
                string[] args = startToEndHere[0].Split('\n');
                md.methodName = args[0].ToLower().Trim();
       
                if (!string.IsNullOrEmpty(md.methodName))
                {
                    // extracting method call
                    string methodCall = md.ExtractMehtod(fullcommand,md.methodName);
                    ExecuteMethodCommand(methodCall, type);
                }
                return;
            }
            else
            {
                check.errorList.Add("Syntax Error.");
                return;
            }
        }

        /// <summary>
        /// Executes a method command based on the provided full command and type.
        /// </summary>
        /// <param name="fullCommand">The full method command to be executed.</param>
        /// <param name="type">The type of command.</param>
        public void ExecuteMethodCommand(string fullCommand,string type)
        {
            
                if (md.methodName == fullCommand.ToLower().Trim())
                {
                    string[] cmds = startToEndHere[0].Split('\n');
                    foreach (var line in cmds)
                    {
                        SingleLineCommand(line.Trim(), type);
                    }
                }
                else if (md.methodName != fullCommand.ToLower().Trim())
                {
                    int startIndex = fullCommand.IndexOf('(');
                    int endIndex = fullCommand.LastIndexOf(')');

                    if (startIndex != -1 && endIndex != -1 && endIndex > startIndex)
                    {
                        string oldArgument = md.methodName;
                        int oldStartIndex = oldArgument.IndexOf('(');
                        int oldEndIndex = oldArgument.LastIndexOf(')');

                        if (oldStartIndex != -1 && oldEndIndex != -1 && oldEndIndex > oldStartIndex)
                        {
                            string arguments = fullCommand.Substring(startIndex + 1, endIndex - startIndex - 1);
                            string oldValuesString = oldArgument.Substring(oldStartIndex + 1, oldEndIndex - oldStartIndex - 1);

                            string[] passedValues = arguments.Split(',');
                            string[] oldValues = oldValuesString.Split(',');

                            // setting the variable of the parameter
                            if (passedValues.Length == oldValues.Length)
                            {
                                for (int i = 0; i < passedValues.Length; i++)
                                {
                                    if (int.TryParse(passedValues[i], out int num))
                                    {
                                        check.setVariables(oldValues[i].Trim().ToLower(), num);
                                    }
                                    else
                                    {
                                        check.errorList.Add("Invalid Argument: " + passedValues[i]);
                                    }
                                }
                            }
                            else
                            {
                                check.errorList.Add("Number of arguments doesn't match.");
                            }

                            //running the code 
                            string[] args = startToEndHere[0].Split('\n');
                            for (int i = 0; i < args.Length; i++)
                            {
                                if (i != 0)
                                {
                                    SingleLineCommand(args[i].Trim(), type);
                                }
                            }
                        }
                        else
                        {
                            check.errorList.Add("Invalid format for old argument string.");
                        }
                    }
                    else
                    {
                        check.errorList.Add("Invalid Method Call Format.");
                    }
            }

        }

        /// <summary>
        /// Executes the provided command lines sequentially.
        /// </summary>
        /// <param name="fullCommand">The full command string containing multiple lines.</param>

        public void ExecuteCommandReveiver(string fullCommand, string type)
        {
            try
            {
                string commandInLowerCase = fullCommand.ToLower(); // Convert the command to lowercase for case insensitivity

                string startIf = "if";
                string endIf = "endif";
                string startWhile = "while";
                string endLoop = "endloop";
                string startMethod = "method";
                string endMethod = "endmethod";

                if (commandInLowerCase.Contains(startIf) && commandInLowerCase.Contains(endIf))
                {
                    sliceCommandReceiver(commandInLowerCase, startIf, endIf, type);
                }
                else if (commandInLowerCase.Contains(startWhile) && commandInLowerCase.Contains(endLoop))
                {
                    sliceCommandReceiver(commandInLowerCase, startWhile, endLoop, type);
                }
                else if (commandInLowerCase.Contains(startMethod) && commandInLowerCase.Contains(endMethod))
                {
                    sliceCommandReceiver(commandInLowerCase, startMethod, endMethod, type);
                }
                else
                {
                    Console.WriteLine("error");
                    IterateCommandParser(fullCommand, type);
                }
            }
            catch (Exception ex)
            {
                check.errorList.Add(ex.Message);
            }

            ShowErrors();
            check.variables.Clear();
        }


        /// <summary>
        /// Handles the click event of the 'Run' button, executing commands from multipleCommand and commandBox2 TextBoxes.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void run_btn_Click(object sender, EventArgs e)
        {
            string multipleCommandText = multipleCommand.Text;
            string commandBox2Text = commandBox2.Text;

            Thread thread1 = new Thread(() => ExecuteCommand(multipleCommandText,"Run"));
            Thread thread2 = new Thread(() => ExecuteCommand(commandBox2Text,"Run"));

            thread1.Start();
            thread2.Start();
           
        }


        /// <summary>
        /// Executes a command by invoking <see cref="ExecuteCommandReceiver"/> in a thread-safe manner.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        private void ExecuteCommand(string command,string type)
        {
            MethodInvoker action = delegate
            {
                ExecuteCommandReveiver(command,type);
            };
            this.BeginInvoke(action);
        }


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(saveFileDialog.FileName);
                writer.Write(multipleCommand.Text);
                writer.Close();
                MessageBox.Show("File is Saved Successfully.");
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Text Files |*.txt";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                multipleCommand.Text = File.ReadAllText(openFile.FileName);
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            check.clearFun(g);
            check.reset();
        }

        /// <summary>
        /// Handles the KeyPress event for the singleCommand TextBox.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The KeyPressEventArgs.</param>
        private void singleCommand_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                string command = singleCommand.Text.Trim();

                if (!string.IsNullOrEmpty(command))
                {
                    if (command.ToUpper() == "CLEAR" || command.ToUpper() == "RESET")
                    {
                        clearText();
                    }
                    SingleLineCommand(command, "Run");
                }
            }
            ShowErrors();
            check.variables.Clear();
        }

        /// <summary>
        /// Iterates through a series of commands and executes them.
        /// </summary>
        /// <param name="command">The string containing multiple commands separated by newline characters.</param>
        public void IterateCommandParser(string command, string type)
        {
            string[] commands = command.Split('\n');

            foreach (var line in commands)
            {
                if (line.Trim().ToUpper().Equals("CLEAR") || line.ToUpper().Equals("RESET"))
                {
                    clearText();
                }
                if (line.Contains("if") || line.Contains("while") || line.Contains("method"))
                {
                    check.errorList.Add("Invalid Commands.");
                    return;
                }
                SingleLineCommand(line.Trim(), type);
            }

        }

        /// <summary>
        /// Handles the Paint event of the outputWindow control.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The PaintEventArgs.</param>
        private void outputWindow_Paint(object sender, PaintEventArgs e)
        {
            check.InitialCursor(e.Graphics);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WindowsFormsCar windowsFormsCar = new WindowsFormsCar();
            windowsFormsCar.Show();
        }
    }
}