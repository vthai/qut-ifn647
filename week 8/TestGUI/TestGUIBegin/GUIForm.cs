using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestGUI
{
    public partial class GUIForm : Form
    {
        Poem p;
        public GUIForm()
        {
            InitializeComponent();
            p = new Poem();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            NextLabel.Text = p.getNextLine();
           // Poem GetText = new Poem()
          
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void TextButton_Click(object sender, EventArgs e)
        {
            TextShowEnter.Text = TextEnter.Text;
        }

        private void TextEnter_TextChanged(object sender, EventArgs e)
        {
            TextShowChange.Text = TextEnter.Text;
        }

        private void ClickLable1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You click 1");
        }

        private void ClickLable2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You click 2");
        }

        private void ClickLable3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You click 3");
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            MyfolderBrowserDialog.ShowDialog();
        }

        private void Savebutton_Click(object sender, EventArgs e)
        {

            saveFileDialog.ShowDialog();
        }
    }
}
