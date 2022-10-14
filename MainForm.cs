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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Employees().ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Subdivisions().ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Moves().ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Journal().ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            JournalHelper.Write("Запуск программы");
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            JournalHelper.Write("Завершение работы программы");
        }
    }
}
