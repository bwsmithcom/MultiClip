using System;
using System.Windows.Forms;

namespace MultiClip
{
    public partial class HowToUse : Form
    {
        private static string howToUse = @"How To Use

MultiClip allows you to store multiple clipboard options 
in slots 1 - 12. 

When you are using another application, simple select 
""Ctrl-F1"" to put Clip #1 into your clipboard.

You can then paste this clip into your application using the
standard ""Ctrl-v"" command.

It's that's easy!";

        public HowToUse()
        {
            InitializeComponent();
            label1.Text = howToUse;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
