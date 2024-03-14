using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

// Importing Karan_Project namespace
using Karan_Project;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace UnitTest_Karan_Project
{
    /// <summary>
    /// Unit tests for validating CommandParser and ShapeFactory functionalities.
    /// </summary>
    [TestClass]
    public class UnitTest1

    {
        CommandParser parser = new CommandParser();
        ShapeFactory factory = new ShapeFactory();
        Form1 form = new Form1();
        MethodsClass md = new MethodsClass();
        // Lists of valid commands and parameters for testing
        List<string> commands = new List<string> { "clear", "reset" };
        List<string> paramsCommand = new List<string> { "moveto", "drawto", "rectangle", "circle", "triangle", "pen", "fill" };


        // Test methods for CommandParser class

        /// <summary>
        /// Test valid command check.
        /// </summary>
        [TestMethod]
        public void TestValidCommand()
        {
            try
            {
                string cmd = "clear";
                bool test = parser.CheckSingleCommand(commands, cmd);
                Assert.IsTrue(test);
            }
            catch(Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Test Invalid command check.
        /// </summary>
        [TestMethod]
        public void TestInvalidCommand()
        {
            try
            {
                string cmd = "invalid";
                bool test = parser.CheckSingleCommand(commands, cmd);
                Assert.IsFalse(test);
            }catch(Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Test valid Parameter check.
        /// </summary>
        [TestMethod]
        public void TestParameter()
        {
            try
            {
              
                string cmd = "rectangle 100,200";
                string[] args = cmd.Split(' ');
                List<string> errorList = new List<string>();

                bool test = parser.CheckParameter(args[0], args[1]);
                Assert.IsTrue(test);
            }catch(Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        /// <summary>
        /// Test Positive or Negative Parameter.
        /// </summary>
        [TestMethod]
        public void TestParameterPositiveAndNegative()
        {
            try
            {
                string cmd = "rectangle -100,200";
                string[] args = cmd.Split(' ');
                List<string> errorList = new List<string>();

                bool test = parser.CheckParameter(args[0], args[1]);
                Assert.IsFalse(test);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        /// <summary>
        /// Test Invalid Parameter check.
        /// </summary>
        [TestMethod]
        public void TestInvalidParameter()
        {
            try
            {
                string cmd = "rectangle 100,200,100";
                string[] args = cmd.Split(' ');
                List<string> errorList = new List<string>();

                bool test = parser.CheckParameter(args[0], args[1]);
                Assert.IsFalse(test);

            }catch(Exception e)
            {
                Assert.Fail(e.Message); 
            }
        }


        /// <summary>
        /// Test valid Color check.
        /// </summary>
        [TestMethod]
        public void TestColor()
        {
            try
            {
                string cmd = "pen red";
                string[] args = cmd.Split(' ');
                List<string> errorList = new List<string>();

                bool test = parser.CheckParameter(args[0], args[1]);
                Assert.IsTrue(test);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }


        /// <summary>
        /// Test valid Fill check.
        /// </summary>
        [TestMethod]
        public void TestFill()
        {
            try
            {
                string cmd = "fill on";
                string[] args = cmd.Split(' ');
                List<string> errorList = new List<string>();

                bool test = parser.CheckParameter(args[0], args[1]);
                Assert.IsTrue(test);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        // Test methods for ShapeFactory class

        /// <summary>
        /// Test Shape Factory getShape method for Circle shape.
        /// </summary> 
        [TestMethod]
        public void TestShapeFactoryCircle()
        {
            try
            {
                //passing the string value circle to the getShape method within the shape factory class
                factory.getShape("circle");
                Assert.IsTrue(true);


            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }


        }

        /// <summary>
        /// Test Shape Factory getShape method for Rectangle shape.
        /// </summary> 
        [TestMethod]

        public void TestShapeFactoryRectangle()
        {
            try
            {
                factory.getShape("rectangle");
                Assert.IsTrue(true);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Test Shape Factory getShape method for Triangle shape.
        /// </summary> 
        [TestMethod]

        public void TestShapeFactoryTriangle()
        {
            try
            {
                factory.getShape("triangle");
                Assert.IsTrue(true);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }


        /// <summary>
        /// Test Shape Factory getShape method for Draw Line.
        /// </summary> 
        [TestMethod]
        public void TestDrawLine()
        {
            try
            {
                factory.getShape("drawto");
                Assert.IsTrue(true);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        /// <summary>
        /// Testing error message when shape does not exist
        /// </summary>
        [TestMethod]
        public void TestInvalidShape()
        {
            //var input = "TEST"; //Fail Result
            var input = "TEST";

            try
            {
                factory.getShape(input);
                Assert.IsFalse(true);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
                var error = "Factory error: " + input + " does not exist";
                Assert.AreEqual(ex.Message, error);
            }


        }

        [TestMethod]
        public void TestSetVariable()
        {
            try
            {
                string variableKey = "x";
                int variableValue = 10;
                parser.setVariables(variableKey, variableValue);
                Assert.IsTrue(true);

            }
            catch(Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]

        public void TestGetVariable()
        {
            try
            {
                bool test = false;
                string variableKey = "10";
                int num = parser.getNum(variableKey);
                if (num != 0)
                {
                    test = true;
                }
                Assert.IsTrue(test);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestExecuteCommandReveiver()
        {
            try
            {
                string command = "clear\r\nreset\r\ncircle 20\r\nmoveTo 100,100\r\nfill on \r\npen yellow\r\ntriangle 200,50,300,100\r\npen green\r\nmoveTo 100,300\r\nrectangle 100,50\r\npen red\r\nmoveTo 200,250\r\nfill off\r\ncircle 40";
                form.ExecuteCommandReveiver(command,"Build");
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestIfandEndIf()
        {
            try
            {
                string command = "x=10\r\ncircle 20\r\nrectangle 20,30\r\nif x==10\r\ncircle 20\r\nmoveTo 100,100\r\nfill on \r\npen yellow\r\ntriangle 200,50,400,100\r\npen green\r\nmoveTo 300,100\r\nrectangle 200,50\r\nendif\r\n";
                form.ExecuteCommandReveiver(command, "Build");
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestWhileandEndLoop()
        {
            try
            {
                string command = "x=1\r\nradius=50\r\nwidth=50\r\nheight=100\r\ncircle 10\r\nrectangle 100,200\r\nwhile x<5\r\ncircle radius\r\nradius=radius+10\r\nrectangle width,height\r\nheight=height+10\r\nwidth=width+10\r\nx=x+1\r\nendloop";
                form.ExecuteCommandReveiver(command, "Build");
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }


        [TestMethod]
        public void TestMethodandEndMethod()
        {
            try
            {
                string command = "method myfunction(x,y)\r\ncircle 20\r\nmoveTo x,y\r\nfill on \r\npen yellow\r\ntriangle 200,50,300,100\r\npen green\r\nmoveTo 100,300\r\nrectangle x,y\r\npen red\r\nmoveTo 200,250\r\nfill off\r\ncircle x\r\nendmethod\r\nmyfunction(100,200)\r\n";
                form.ExecuteCommandReveiver(command, "Build");
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
