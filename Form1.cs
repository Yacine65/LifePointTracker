using System;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace LifePointTracker
{
    public partial class Form1 : Form
    {
        int selectedPlayer = 1;
        string numberToSubtractOrAdd = "";
        string selectedOperation = "";
        int lifePointsPlayer1 = 8000;
        int lifePointsPlayer2 = 8000;
        ArrayList tabLastOperation = new ArrayList();
        string lastOperation = "";

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            lblPlayer1.ForeColor = System.Drawing.Color.Crimson;
        }

        private void lblPlayer1_Click(object sender, EventArgs e)
        {
            selectPlayer1();
        }
        private void lblPlayer2_Click(object sender, EventArgs e)
        {
            selectPlayer2();
        }
        private void selectPlayer1()
        {
            lblPlayer1.ForeColor = System.Drawing.Color.Crimson;
            lblPlayer2.ForeColor = System.Drawing.Color.White;
            selectedPlayer = 1;
            refreshLifePointLabels();
        }
        private void selectPlayer2()
        {
            lblPlayer2.ForeColor = System.Drawing.Color.Crimson;
            lblPlayer1.ForeColor = System.Drawing.Color.White;
            selectedPlayer = 2;
            refreshLifePointLabels();
        }
        private void refreshLifePointLabels()
        {
            numberToSubtractOrAdd = "";
            selectedOperation = "";
            lblPlayer1.Text = lifePointsPlayer1.ToString();
            lblPlayer2.Text = lifePointsPlayer2.ToString();
        }
        private void resetScore()
        {
            lifePointsPlayer1 = 8000;
            lifePointsPlayer2 = 8000;
            lastOperation = "";
            refreshLifePointLabels();
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Reset Life Points?", "Confirm", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                resetScore();
            }
        }
        private void btn0_Click(object sender, EventArgs e)
        {
            addOperand("0");
        }
        private void btn00_Click(object sender, EventArgs e)
        {
            addOperand("00");
        }

        private void btn000_Click(object sender, EventArgs e)
        {
            addOperand("000");
        }
        private void btn1_Click(object sender, EventArgs e)
        {
            addOperand("1");
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            addOperand("2");
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            addOperand("3");
        }
        private void btn4_Click(object sender, EventArgs e)
        {
            addOperand("4");
        }
        private void btn5_Click(object sender, EventArgs e)
        {
            addOperand("5");
        }
        private void btn6_Click(object sender, EventArgs e)
        {
            addOperand("6");
        }
        private void btn7_Click(object sender, EventArgs e)
        {
            addOperand("7");    
        }
        private void btn8_Click(object sender, EventArgs e)
        {
            addOperand("8");
        }
        private void btn9_Click(object sender, EventArgs e)
        {
            addOperand("9");
        }
        private void addOperand(string number)
        {
            numberToSubtractOrAdd += number;
            if (numberToSubtractOrAdd.Length>4)
                numberToSubtractOrAdd = numberToSubtractOrAdd.Substring(0, 4);

            if (selectedOperation == "+" || selectedOperation == "-")
            {
                if (selectedPlayer == 1)
                    lblPlayer1.Text = selectedOperation + numberToSubtractOrAdd;
                else
                    lblPlayer2.Text = selectedOperation + numberToSubtractOrAdd;
            }
        }
        private void btnPlus_Click(object sender, EventArgs e)
        {
            selectedOperation = "+";
            numberToSubtractOrAdd = "";
            if (selectedPlayer == 1)
                lblPlayer1.Text = selectedOperation ;
            else
                lblPlayer2.Text = selectedOperation;
        }
        private void btnMinus_Click(object sender, EventArgs e)
        {
            selectedOperation = "-";
            numberToSubtractOrAdd = "";
            if (selectedPlayer == 1)
                lblPlayer1.Text = selectedOperation;
            else
                lblPlayer2.Text = selectedOperation;
        }
        private void btnHalf_Click(object sender, EventArgs e)
        {
            numberToSubtractOrAdd = "";
            if (selectedPlayer == 1)
            {
                if (lifePointsPlayer1 > 1)
                    lifePointsPlayer1 = lifePointsPlayer1 / 2;
            }  
            else
            {
                if (lifePointsPlayer2 > 1)
                    lifePointsPlayer2 = lifePointsPlayer2 / 2;
            }
            lastOperation = selectedPlayer + "," + "half" + ",0";
            tabLastOperation.Add(lastOperation);
            refreshLifePointLabels();
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            if (numberToSubtractOrAdd.Length > 0)
            {
                numberToSubtractOrAdd = numberToSubtractOrAdd.Substring(0,numberToSubtractOrAdd.Length-1);
                if (selectedPlayer == 1)
                    lblPlayer1.Text = selectedOperation + numberToSubtractOrAdd;
                else
                    lblPlayer2.Text = selectedOperation + numberToSubtractOrAdd;
            }
        }
        private void btnUndo_Click(object sender, EventArgs e)
        {
            if (tabLastOperation.Count > 0)
            {
                lastOperation = tabLastOperation[tabLastOperation.Count - 1].ToString();
                if (lastOperation != "")
                {
                    string[] tabResults = lastOperation.Split(',');
                    if (tabResults[1] == "+")
                    {
                        if (tabResults[0] == "1") //PLAYER 1
                            lifePointsPlayer1 = lifePointsPlayer1 - Convert.ToInt32(tabResults[2]);
                        else if (tabResults[0] == "2") //PLAYER 2
                            lifePointsPlayer2 = lifePointsPlayer2 - Convert.ToInt32(tabResults[2]);
                    }
                    else if (tabResults[1] == "-")
                    {
                        if (tabResults[0] == "1") //PLAYER 1
                            lifePointsPlayer1 = lifePointsPlayer1 + Convert.ToInt32(tabResults[2]);
                        else if (tabResults[0] == "2") //PLAYER 2
                            lifePointsPlayer2 = lifePointsPlayer2 + Convert.ToInt32(tabResults[2]);
                    }
                    else if (tabResults[1] == "half")
                    {
                        if (tabResults[0] == "1") //PLAYER 1
                            lifePointsPlayer1 = lifePointsPlayer1 * 2;
                        else if (tabResults[0] == "2") //PLAYER 2
                            lifePointsPlayer2 = lifePointsPlayer2 * 2;
                    }
                    lastOperation = "";
                    tabLastOperation.RemoveAt(tabLastOperation.Count - 1);
                    refreshLifePointLabels();
                    if (tabResults[0] == "1") //PLAYER 1
                        selectPlayer1();
                    else if (tabResults[0] == "2") //PLAYER 2
                        selectPlayer2();
                }
            }
        }
        private void btnEquals_Click(object sender, EventArgs e)
        {
            if ((selectedOperation == "+" || selectedOperation == "-") && numberToSubtractOrAdd != ""){
                if (selectedPlayer == 1)
                {
                    if (selectedOperation == "+")
                        lifePointsPlayer1 = (lifePointsPlayer1 + Convert.ToInt32(numberToSubtractOrAdd));
                    else if (selectedOperation == "-")
                        lifePointsPlayer1 = (lifePointsPlayer1 - Convert.ToInt32(numberToSubtractOrAdd));

                    if (lifePointsPlayer1 <= 0)
                        playerLost(1);
                    else
                        lblPlayer1.Text = lifePointsPlayer1.ToString();
                }

                else
                {
                    if (selectedOperation == "+")
                        lifePointsPlayer2 = (lifePointsPlayer2 + Convert.ToInt32(numberToSubtractOrAdd));
                    else if (selectedOperation == "-")
                        lifePointsPlayer2 = (lifePointsPlayer2 - Convert.ToInt32(numberToSubtractOrAdd));

                    if (lifePointsPlayer2 <= 0)
                        playerLost(2);                  
                    else
                        lblPlayer2.Text = lifePointsPlayer2.ToString();
                }
                lastOperation = selectedPlayer + "," + selectedOperation + ","+ numberToSubtractOrAdd;
                tabLastOperation.Add(lastOperation);
                refreshLifePointLabels();
            }     
        }
        private void playerLost(int player)
        {
            if (player == 1)
            {
                lifePointsPlayer1 = 0;
                lblPlayer1.Text = lifePointsPlayer1.ToString();
            }
            else
            {
                lifePointsPlayer2 = 0;
                lblPlayer2.Text = lifePointsPlayer2.ToString();
            }

            DialogResult dialogResult = MessageBox.Show("Player " + player + " Lost. Reset Life Points?", "Confirm", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                resetScore();
            }
        }
    }
    public class RoundButton : Button
    {
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            GraphicsPath grPath = new GraphicsPath();
            grPath.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height);
            this.Region = new System.Drawing.Region(grPath);
            base.OnPaint(e);
        }
    }
}
