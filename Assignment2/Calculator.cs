using System;
using System.Collections.Generic;

namespace Assignment2
{
    public class Calculator
    {
        private string _inputStr;
        private List<double> _numbers;
        private List<char> _operators;

        public double Calculate(string input)
        {
            _inputStr = input;
            InitLists();

            // POWER
            var indexPower = _operators.IndexOf('^');
            while (indexPower != -1)
            {
                Power(indexPower);
                indexPower = _operators.IndexOf('^');
            }

            // DIVISION, MULTIPLICATION, MODULO
            int[] positions = { _operators.IndexOf('/'), _operators.IndexOf('%'), _operators.IndexOf('*') };
            char[] operators = { '/', '%', '*' };

            while (positions[0] != -1 || positions[1] != -1 || positions[2] != -1)
            {
                Array.Sort(positions, operators);

                for (int i = 0; i < positions.Length; i++)
                {
                    var index = positions[i];
                    if (index == -1)
                    {
                        // if index is -1 the operator does not exist in the expression.
                        // In that case, continue to the next iteration.
                        continue;
                    }
                    switch (operators[i])
                    {
                        case '/':
                            Divise(index);
                            break;
                        case '%':
                            Modulus(index);
                            break;
                        case '*':
                            Multiply(index);
                            break;
                    }

                    // Update indexes
                    for (int j = 0; j < positions.Length; j++)
                    {
                        positions[j] = _operators.IndexOf(operators[j]);
                    }
                }
            }

            // ADDITION & SUBTRACTION
            var indexAdd = _operators.IndexOf('+');
            var indexSubtract = _operators.IndexOf('-');
            while (indexAdd != -1 || indexSubtract != -1)
            {
                if (indexAdd != -1 && indexSubtract != -1) // both operaters are present
                {
                    // Check which operation comes first
                    if (indexAdd < indexSubtract)
                    {
                        Add(indexAdd);

                        // Update indexes
                        indexAdd = _operators.IndexOf('+');
                        indexSubtract = _operators.IndexOf('-');

                        Subtract(indexSubtract);
                    }
                    else
                    {
                        Subtract(indexSubtract);

                        // Update indexes
                        indexAdd = _operators.IndexOf('+');
                        indexSubtract = _operators.IndexOf('-');

                        Add(indexAdd);
                    }
                }
                else if (indexAdd != -1) // Addition is present, subtraction is not
                {
                    Add(indexAdd);
                }
                else if (indexSubtract != -1) // Subtraction is present, addition is not
                {
                    Subtract(indexSubtract);
                }

                // Update indexes
                indexAdd = _operators.IndexOf('+');
                indexSubtract = _operators.IndexOf('-');
            }


            // If we reach this point, _numbers will contain only one element; the result of the equation.
            // Perform check just in case.
            if (_numbers.Count == 1)
            {
                return _numbers[0];
            }

            throw new FormatException();
        }

        private void Divise(int index)
        {
            var num1 = _numbers[index];
            var num2 = _numbers[index + 1];
            var result = num1 / num2;

            UpdateLists(index, result);
        }

        private void Multiply(int index)
        {
            var num1 = _numbers[index];
            var num2 = _numbers[index + 1];
            var result = num1 * num2;

            UpdateLists(index, result);
        }

        private void Add(int index)
        {
            var num1 = _numbers[index];
            var num2 = _numbers[index + 1];
            var result = num1 + num2;

            UpdateLists(index, result);
        }

        private void Subtract(int index)
        {
            var num1 = _numbers[index];
            var num2 = _numbers[index + 1];
            var result = num1 - num2;

            UpdateLists(index, result);
        }

        private void Modulus(int index)
        {
            var num1 = _numbers[index];
            var num2 = _numbers[index + 1];
            var result = num1 % num2;

            UpdateLists(index, result);
        }

        private void Power(int index)
        {
            var num1 = _numbers[index];
            var num2 = _numbers[index + 1];
            var result = Math.Pow(num1, num2);

            UpdateLists(index, result);
        }

        // After a sub-calculation is made, the lists need to be updated.
        // Replace the operator and surrounding numbers with the result.
        private void UpdateLists(int index, double result)
        {
            _operators.RemoveAt(index);
            _numbers.RemoveAt(index);
            _numbers.RemoveAt(index);
            _numbers.Insert(index, result);
        }

        private void InitLists()
        {
            _numbers = new List<double>();
            _operators = new List<char>();
            string numberStr = string.Empty;
            for (int i = 0; i < _inputStr.Length; i++)
            {
                char c = _inputStr[i];
                if (c == '+' || c == '-' || c == '*' || c == '/' || c == '%' || c == '^')
                {
                    if (i != 0 && char.IsNumber(_inputStr[i - 1]) && i != _inputStr.Length - 1)
                    {
                        // Only add to _operators list if not first or last char and if previous char is a number
                        _operators.Add(c);
                    }
                    else if (i == _inputStr.Length - 1) // Equation ends in operator
                    {
                        throw new FormatException();
                    }
                    else
                    {
                        // If previous char is an operator, current char is a unary operator e.g. -7 or +7.
                        // If not, input is invalid. In any case, add to numberStr.
                        // If invalid input, FormatException will be thrown and caught when numberStr is parsed as int.
                        numberStr += c;
                    }
                }
                else
                {
                    numberStr += c;

                    if (i == _inputStr.Length - 1 || (!char.IsNumber(_inputStr[i + 1]) && !_inputStr[i + 1].Equals('.')))
                    {
                        // Once we reach the end of the number, add it to _numbers list
                        _numbers.Add(ParseDouble(numberStr));
                        numberStr = string.Empty;
                    }
                }
            }
        }

        private double ParseDouble(string input)
        {
            double number;
            if (!double.TryParse(input, out number))
            {
                throw new FormatException();
            }

            return number;
        }
    }
}
