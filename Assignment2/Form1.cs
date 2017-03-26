using System;
using System.Windows.Forms;

namespace Assignment2
{
    public partial class Form1 : Form
    {
        private Calculator _calc;
        private bool _isEqualsBtnClicked;

        public Form1()
        {
            InitializeComponent();
            _calc = new Calculator();
            textbox.Text = "0";
            _isEqualsBtnClicked = false;
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            numberClick("0");
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            numberClick("1");
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            numberClick("2");
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            numberClick("3");
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            numberClick("4");
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            numberClick("5");
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            numberClick("6");
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            numberClick("7");
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            numberClick("8");
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            numberClick("9");
        }

        private void btnModulus_Click(object sender, EventArgs e)
        {
            operatorClick("%");
        }

        private void btnPower_Click(object sender, EventArgs e)
        {
            operatorClick("^");
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            operatorClick("-");
        }

        private void btnDivide_Click(object sender, EventArgs e)
        {
            operatorClick("/");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            operatorClick("+");
        }

        private void btnMultiply_Click(object sender, EventArgs e)
        {
            operatorClick("*");
        }

        private void btnDecimal_Click(object sender, EventArgs e)
        {
            if (_isEqualsBtnClicked)
            {
                textbox.Text = ".";
                _isEqualsBtnClicked = false;
                return;
            }

            textbox.Text = textbox.Text + ".";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            textbox.Text = "0";
            _isEqualsBtnClicked = false;
        }

        private void btnClearEntry_Click(object sender, EventArgs e)
        {
            string input = textbox.Text;
            if(input.Length == 1)
            {
                textbox.Text = "0";
            }
            else
            {
                textbox.Text = input.Remove(input.Length - 1);
            }
            _isEqualsBtnClicked = false;
        }

        private void btnEquals_Click(object sender, EventArgs e)
        {
            _isEqualsBtnClicked = true;

            try
            {
                var input = textbox.Text;
                var result = _calc.Calculate(input);
                textbox.Text = result.ToString();
            }
            catch (FormatException)
            {
                textbox.Text = "Invalid double";
            }
        }


        // HELPER METHODS
        private void numberClick(string num)
        {
            if (_isEqualsBtnClicked) // check if a calculation was just made
            {
                textbox.Text = num;
                _isEqualsBtnClicked = false; // reset the flag
                return;
            }

            if (textbox.Text == "0")
            {
                textbox.Text = num;
                return;
            }
            
            textbox.Text = textbox.Text + num;
        }

        private void operatorClick(string op)
        {
            _isEqualsBtnClicked = false;
            textbox.Text = textbox.Text + op;
        }
    }
}
