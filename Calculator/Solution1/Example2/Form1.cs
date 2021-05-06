using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Example2
{
    public partial class Form1 : Form
    {
        Brain brain;
        public Form1()
        {
            InitializeComponent();
            DisplayMessage displayMessage = new DisplayMessage(SetDisplayMessage);
            brain = new Brain(displayMessage);
        }


        void SetDisplayMessage(string text)
        {
            textBox1.Text = text;
        }


        void ButtonClicked(object sender, EventArgs e)
        {
            if (sender is Button btn) brain.ProcessSignal(btn.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
