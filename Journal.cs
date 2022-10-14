using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HumanResources
{
    public partial class Journal : Form
    {
        public Journal()
        {
            InitializeComponent();
        }

        private void Journal_Load(object sender, EventArgs e)
        {
            if (File.Exists("journal.txt"))
                textBox1.Text = File.ReadAllText("journal.txt");
        }
    }
}
