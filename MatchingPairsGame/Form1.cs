using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchingPairsGame
{
    public partial class Form1 : Form
    {
        //adding reference variables to Labels
        Label firstClicked = null;
        Label secondClicked = null;

        Random random = new Random();
        List<string> icons = new List<string>()
        {
            // represents symbol or icon for all pairs for 16 grid
            "!","!","N","N",",",",","k","k",
            "b","b","v","v","w","w","z","z"
        };
        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();
        }

        private void AssignIconsToSquares()
        {
            foreach(Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if(iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;//forecolor makes the same color as background color
                    icons.RemoveAt(randomNumber);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label_click(object sender, EventArgs e)
        {
            //timer running ony after two non matching shows icons to player
            if (timer1.Enabled == true)
                return;

            Label clickedLabel = sender as Label;
            if(clickedLabel != null)
            {
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                //clickedLabel.ForeColor = Color.Black;

                // if firstclicked is null this is the first icon player clicked and it turns into black
                //after clicking this it won't shows other icons
                if(firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;

                    return;
                }
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                //for message function
                checkForWinner();

                //make matched pairs visible

                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                timer1.Start(); // timer1_tick event will fire

            }
        }
        //add timer to control form
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;
            firstClicked = null;
            secondClicked = null;
        }
        // Display winning message
        private void checkForWinner()
        {
            foreach(Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if(iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }
            MessageBox.Show("You have matched all icons!", "Congratulations! ");
            Close();
        }
        
    }
}
