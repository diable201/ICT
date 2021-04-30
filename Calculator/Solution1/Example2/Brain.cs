using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example2
{

    delegate void DisplayMessage(string text);

    class Brain
    {
        DisplayMessage displayMessage;
        public Brain(DisplayMessage displayMessageDelegate)
        {
            displayMessage = displayMessageDelegate;
        }

        string[] nonZeroDigit = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        string[] digit = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        string[] zero = { "0" };
        string[] operation = { "+", "-", "*", "/", "^", "log" };
        string[] equal = { "=" };
        string[] separator = { "," };

        enum State
        {
            Zero,
            AccumulateDigits,
            AccumulateDigitsDecimal,
            ComputePending,
            Compute,
        }

        State currentState = State.Zero;
        string previousNumber = "0";
        string currentNumber = "";
        string currentOperation = "";
        public void ProcessSignal(string message)
        {
            switch (currentState)
            {
                case State.Zero:
                    ProcessZeroState(message, false);
                    break;
                case State.AccumulateDigits:
                    ProcessAccumulateDigits(message, false);
                    break;
                case State.AccumulateDigitsDecimal:
                    ProccesAccumulateDigitsDecimal(message, false);
                    break;
                case State.ComputePending:
                    ProcessComputePending(message, false);
                    break;
                case State.Compute:
                    ProcessCompute(message, false);
                    break;
                default:
                    break;
            }

            if (message == "C")
            {
                currentState = State.Zero;
                previousNumber = "";
                currentNumber = "";
                currentOperation = "";
                displayMessage("0");
                return;
            }
        }

        void ProcessZeroState(string msg, bool income)
        {
            if (income)
            {
                currentState = State.Zero;
            }
            else
            {
                if (nonZeroDigit.Contains(msg))
                {
                    ProcessAccumulateDigits(msg, true);
                }
            }
        }


        void ProcessAccumulateDigits(string msg, bool income)
        {
            if (income)
            {
                currentState = State.AccumulateDigits;
                if (zero.Contains(currentNumber))
                {
                    currentNumber = msg;
                }
                else
                {
                    currentNumber += msg;
                }
                displayMessage(currentNumber);
            }
            else
            {
                if (digit.Contains(msg))
                {
                    ProcessAccumulateDigits(msg, true);
                } else if (operation.Contains(msg))
                {
                    ProcessComputePending(msg, true);
                } 
                else if (equal.Contains(msg))
                {
                    ProcessCompute(msg, true);
                }
                else if (separator.Contains(msg))
                {
                    ProccesAccumulateDigitsDecimal(msg, true);
                }
            }
            
        }

        void ProccesAccumulateDigitsDecimal(string msg, bool income)
        {
            if (income)
            {
                currentState = State.AccumulateDigitsDecimal;
                if (separator.Contains(msg))
                {
                    if (currentNumber == "")
                    {
                        currentNumber = previousNumber + msg;
                    }
                    else 
                    {
                        currentNumber += msg;
                    }
                    
                }
                else if (digit.Contains(msg))
                {
                    currentNumber += msg;
                }
                displayMessage(currentNumber);
            }
            else
            {
                if (digit.Contains(msg))
                {
                    ProccesAccumulateDigitsDecimal(msg, true);
                }
                else if (operation.Contains(msg))
                {
                    ProcessComputePending(msg, true);
                }
                else if (equal.Contains(msg))
                {
                    ProcessCompute(msg, true);
                }
            }
        }

        void ProcessComputePending(string msg, bool income)
        {
            if (income)
            {
                currentState = State.ComputePending;
                previousNumber = currentNumber;
                currentNumber = "";
                currentOperation = msg;
            }
            else
            {
                if (digit.Contains(msg))
                {
                    ProcessAccumulateDigits(msg, true);
                }
            }
        }

        void ProcessCompute(string msg, bool income)
        {   
            if (income)
            {
                currentState = State.Compute;

                double a = double.Parse(previousNumber);
                
                double b = double.Parse(currentNumber);
                if (currentOperation == "+")
                {
                    currentNumber = (a + b).ToString();
                } 
                else if (currentOperation == "-")
                {
                    currentNumber = (a - b).ToString();
                } 
                else if (currentOperation == "*")
                {
                    currentNumber = (a * b).ToString();
                } 
                else if (currentOperation == "/")
                { 
                    if (b != 0)
                    {
                        currentNumber = (a / b).ToString();
                    } 
                    else
                    {
                        currentNumber = ("Divide By Zero").ToString();
                        if (zero.Contains(msg))
                        {
                            currentNumber = "";
                            previousNumber = "";
                            displayMessage("0"); 
                            ProcessZeroState(msg, true);
                        }
                    }

                } 
                else if (currentOperation == "^")
                {
                    currentNumber = (Math.Pow(a, b)).ToString();
                }
                else if (currentOperation == "log")
                {
                    currentNumber = (Math.Log(a, b)).ToString();
                }

                previousNumber = currentNumber;
                displayMessage(currentNumber);
                //currentOperation = "";
            }
            else
            {
                if (nonZeroDigit.Contains(msg))
                {
                    ProcessAccumulateDigits(msg, true);
                }
                else if (operation.Contains(msg))
                {
                    ProcessComputePending(msg, true);
                }
                else if (zero.Contains(msg))
                {
                    currentNumber = "";
                    displayMessage("0");
                    ProcessZeroState(msg, true);
                }
                else if (separator.Contains(msg))
                {
                    ProccesAccumulateDigitsDecimal(msg, true);
                }
            }
        }
    }
}
