using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HumanResources
{
    public partial class Subdivisions : Form
    {
        public Subdivisions()
        {
            InitializeComponent();
        }

        private void Subdivisions_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            dataGridView1.SuspendLayout();
            dataGridView1.Rows.Clear();
            foreach (var s in Entities.Subdivision.Subdivisions)
            {
                dataGridView1.Rows.Add(s.Id, s.Name, 
                    Entities.Employee.Employees.Select(t => t.CurrentState()).Count(t => t.Subdivision == s));
            }
            dataGridView1.ResumeLayout();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form = new AddEditForms.AddEditSubdivision
            {
                Text = "Добавить подразделение"
            };

            if (form.ShowDialog() == DialogResult.OK)
            {
                int id = Entities.Subdivision.Subdivisions.Any() ? Entities.Subdivision.Subdivisions.Max(t => t.Id) + 1 : 0;
                Entities.Subdivision.Subdivisions.Add(
                    new Entities.Subdivision(id, form.nameBox.Text)
                );
                JournalHelper.Write("Добавлено новое подразделение");
                LoadData();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;
            var form = new AddEditForms.AddEditSubdivision
            {
                Text = "Изменить подразделение"
            };

            var sub = Entities.Subdivision.Subdivisions.First(t => t.Id == int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
            form.applyButton.Text = "Применить";
            form.nameBox.Text = sub.Name;

            if (form.ShowDialog() == DialogResult.OK)
            {
                sub.Name = form.nameBox.Text;
                JournalHelper.Write("Изменена информация о подразделении");
                LoadData();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;
            var sub = Entities.Subdivision.Subdivisions.First(t => t.Id == int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
            if (sub.Check())
            {
                MessageBox.Show("Данную запись невозможно удалить, так как от неё зависима как минимум одна запись", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("Вы действительно желаете удалить выбранную запись?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Entities.Subdivision.Subdivisions.Remove(sub);
                JournalHelper.Write("Подразделение удалено");
                LoadData();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
