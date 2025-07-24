using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace calc_position
{

    public partial class Form1 : Form
    {
        TextBox displayBox;
        double firstNumber = 0;
        string operation = "";
        bool isoperationClicked = false;

        public Form1()
        {
            InitializeComponent();
            SetupFormStyle();
            CreateDisplay();
            CreateButtons();
        }

        //create form
        private void SetupFormStyle()
        {
            this.Text = "calculator"; Width = 300; Height = 300;
            FormBorderStyle = FormBorderStyle.FixedDialog; MaximizeBox = false; BackColor = Color.Gray;
            StartPosition = FormStartPosition.CenterScreen; Icon = new Icon(@"image/calc.ico");
        }

        //-----------------------------------------------

        //form textbox
        private void CreateDisplay()
        {
            displayBox = new TextBox();
            displayBox.Location = new Point(10, 10);
            //width and height
            displayBox.Size = new Size(260, 90);
            //font
            displayBox.Font = new Font("Segoe UI", 16);
            displayBox.ReadOnly = true;
            displayBox.TextAlign = HorizontalAlignment.Right;
            this.Controls.Add(displayBox);
        }

        //-----------------------------------------------

        //create buttons
        private void CreateButtons()
        {
            void StyleButton(Button btn, Color backColor, Color forColor, Font font)
            {
                btn.Font = font;
                btn.BackColor = backColor;
                btn.ForeColor = forColor;
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatStyle = FlatStyle.Flat;
            }
            void SetupButton(Button btn, string text, int width, int height, int x, int y)
            {
                btn.Text = text;
                btn.Size = new Size(width, height);
                btn.Location = new Point(x, y);
            }

            //buttons

            int startX = 10, startY = 60, buttonWidth = 40, buttonHeight = 40, padding = 10, number = 1;

            //buttons 1 to 9;

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    Button btn = new Button();
                    SetupButton(btn, number.ToString(), buttonWidth, buttonHeight, startX + col * (buttonWidth + padding)
                        , startY + row * (buttonHeight + padding));
                    StyleButton(btn, Color.DarkBlue, Color.White, new Font("Segoe UI", 14, FontStyle.Bold));

                    //click btn and send text in textbox

                    btn.Click += (sender, e) =>
                    {
                        if (isoperationClicked)
                        {
                            displayBox.Text = "";
                            isoperationClicked = false;
                        }
                        displayBox.Text += ((Button)sender).Text;
                    };
                    this.Controls.Add(btn);
                    number++;
                }
            }

            //btn 0

            Button btn0 = new Button();
            SetupButton(btn0, "0", buttonWidth, buttonHeight, (startX + (padding + 40)),
                startY + 3 * (buttonHeight + padding));
            StyleButton(btn0, Color.DarkBlue, Color.White, new Font("Segoe UI", 14, FontStyle.Bold));
            btn0.Click += (sender, e) =>
            {
                if (isoperationClicked)
                {
                    displayBox.Text = "";
                    isoperationClicked = false;
                }
                displayBox.Text += "0";
            };
            this.Controls.Add(btn0);

            //btn dot

            Button btnDot = new Button();
            SetupButton(btnDot, ".", buttonWidth, buttonHeight, (startX)
                , startY + 3 * (buttonHeight + padding));
            StyleButton(btnDot, Color.DarkBlue, Color.White, new Font("Segoe UI", 14, FontStyle.Bold));
            btnDot.Click += (sender, e) =>
            {
                if (!displayBox.Text.Contains("."))
                {
                    if (string.IsNullOrEmpty(displayBox.Text))
                        displayBox.Text = "0.";
                    else
                        displayBox.Text += ".";
                }
            };
            this.Controls.Add(btnDot);

            //btn clear

            Button btnclear = new Button();
            SetupButton(btnclear, "C", buttonWidth, buttonHeight, (startX + 200),
                startY + 3 * (buttonHeight + padding));
            StyleButton(btnclear, Color.DarkBlue, Color.White, new Font("Segoe UI", 14, FontStyle.Bold));
            btnclear.Click += (sender, e) =>
            {
                displayBox.Text = "";
            };
            this.Controls.Add(btnclear);

            //--------------------------------------------

            //the operations

            string[] ops = { "+", "-", "*", "/" };
            int opStartX = 160, opStartY = 60;
            for (int i = 0; i < ops.Length; i++)
            {
                Button btnop= new Button();
                SetupButton(btnop, ops[i], buttonWidth, buttonHeight, (opStartX)
                        , opStartY + i * 50);
                StyleButton(btnop, Color.DarkBlue, Color.White, new Font("Segoe UI", 14, FontStyle.Bold));
                btnop.Click += (sender, e) =>
                {
                    if (double.TryParse(displayBox.Text, out firstNumber))
                    {
                        operation = ((Button)sender).Text;
                        isoperationClicked = true;
                        displayBox.Text = "";
                    }

                };
                this.Controls.Add(btnop);
            }

            //btnequal

            Button btnEqual = new Button();
            SetupButton(btnEqual, "=", buttonWidth, buttonHeight, (opStartX - 50)
                , opStartY + 4 * 37);
            StyleButton(btnEqual, Color.DarkBlue, Color.White, new Font("Segoe UI", 14, FontStyle.Bold));
            btnEqual.Click += (sender, e) =>
            {
                double secondNumber;
                if (double.TryParse(displayBox.Text, out secondNumber))
                {
                    double result = 0;
                    switch (operation)
                    {
                        case "+":
                            result = firstNumber + secondNumber;
                            break;
                        case "-":
                            result = firstNumber - secondNumber;
                            break;
                        case "*":
                            result = firstNumber * secondNumber;
                            break;
                        case "/":
                            if (secondNumber != 0) result = firstNumber / secondNumber;
                            else MessageBox.Show("Cannot divide by zero!","warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                            break;
                    }
                    displayBox.Text = result.ToString();
                }

            };
            this.Controls.Add(btnEqual);
        }
    }
}
