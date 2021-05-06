using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example2
{
    internal delegate void DisplayMessage(string text);

    internal class Brain
    {
        private readonly DisplayMessage _displayMessage;
        public Brain(DisplayMessage displayMessageDelegate)
        {
            _displayMessage = displayMessageDelegate;
        }

        string[] _nonZeroDigit = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        string[] _digit = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        string[] _zero = { "0" };
        string[] _operation = { "+", "-", "*", "/", "^", "log" };
        string[] equal = { "=" };
        string[] separator = { "," };

        private enum State
        {
            Zero,
            AccumulateDigits,
            AccumulateDigitsDecimal,
            ComputePending,
            Compute,
        }

        private State _currentState = State.Zero;
        private string _previousNumber = "0";
        private string _currentNumber = "";
        private string _currentOperation = "";
        public void ProcessSignal(string message)
        {
            switch (_currentState)
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

            if (message != "C") return;
            _currentState = State.Zero;
            _previousNumber = "";
            _currentNumber = "";
            _currentOperation = "";
            _displayMessage("0");
        }

        void ProcessZeroState(string msg, bool income)
        {
            if (income)
            {
                _currentState = State.Zero;
            }
            else
            {
                if (_nonZeroDigit.Contains(msg))
                {
                    ProcessAccumulateDigits(msg, true);
                }
            }
        }


        void ProcessAccumulateDigits(string msg, bool income)
        {
            if (income)
            {
                _currentState = State.AccumulateDigits;
                if (_zero.Contains(_currentNumber))
                {
                    _currentNumber = msg;
                }
                else
                {
                    _currentNumber += msg;
                }
                _displayMessage(_currentNumber);
            }
            else
            {
                if (_digit.Contains(msg))
                {
                    ProcessAccumulateDigits(msg, true);
                } else if (_operation.Contains(msg))
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
                _currentState = State.AccumulateDigitsDecimal;
                if (separator.Contains(msg))
                {
                    if (_currentNumber == "")
                    {
                        _currentNumber = _previousNumber + msg;
                    }
                    else 
                    {
                        _currentNumber += msg;
                    }
                    
                }
                else if (_digit.Contains(msg))
                {
                    _currentNumber += msg;
                }
                _displayMessage(_currentNumber);
            }
            else
            {
                if (_digit.Contains(msg))
                {
                    ProccesAccumulateDigitsDecimal(msg, true);
                }
                else if (_operation.Contains(msg))
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
                _currentState = State.ComputePending;
                _previousNumber = _currentNumber;
                _currentNumber = "";
                _currentOperation = msg;
            }
            else
            {
                if (_digit.Contains(msg))
                {
                    ProcessAccumulateDigits(msg, true);
                }
            }
        }

        void ProcessCompute(string msg, bool income)
        {   
            if (income)
            {
                _currentState = State.Compute;
                double a = double.Parse(_previousNumber);
                double b = double.Parse(_currentNumber);
                if (_currentOperation == "+")
                {
                    _currentNumber = (a + b).ToString();
                } 
                else if (_currentOperation == "-")
                {
                    _currentNumber = (a - b).ToString();
                } 
                else if (_currentOperation == "*")
                {
                    _currentNumber = (a * b).ToString();
                } 
                else if (_currentOperation == "/")
                { 
                    if (b != 0)
                    {
                        _currentNumber = (a / b).ToString();
                    } 
                    else
                    {
                        _currentNumber = ("Divide By Zero").ToString();
                        if (_zero.Contains(msg))
                        {
                            _currentNumber = "";
                            _previousNumber = "";
                            _displayMessage("0"); 
                            ProcessZeroState(msg, true);
                        }
                    }
                } 
                else if (_currentOperation == "^")
                {
                    _currentNumber = Math.Pow(a, b).ToString();
                }
                else if (_currentOperation == "log")
                {
                    _currentNumber = Math.Log(a, b).ToString();
                }
                _previousNumber = _currentNumber;
                _displayMessage(_currentNumber);
                //currentOperation = "";
            }
            else
            {
                if (_nonZeroDigit.Contains(msg))
                {
                    ProcessAccumulateDigits(msg, true);
                }
                else if (_operation.Contains(msg))
                {
                    ProcessComputePending(msg, true);
                }
                else if (_zero.Contains(msg))
                {
                    _currentNumber = "";
                    _displayMessage("0");
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
