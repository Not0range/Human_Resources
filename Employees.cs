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
    public partial class Employees : Form
    {
        public Employees()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Employees_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            dataGridView1.SuspendLayout();
            dataGridView1.Rows.Clear();
            foreach (var e in Entities.Employee.Employees)
            {
                var state = e.CurrentState();
                dataGridView1.Rows.Add(e.Id, e.Name, e.BirthDate.ToString("dd.MM.yyyy"), e.Passport, e.Address, e.Phone, state.Subdivision, state.Position);
            }
            dataGridView1.ResumeLayout();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form = new AddEditForms.AddEditEmployee
            {
                Text = "Добавить сотрудника"
            };

            if (form.ShowDialog() == DialogResult.OK)
            {
                int id = Entities.Employee.Employees.Any() ? Entities.Employee.Employees.Max(t => t.Id) + 1 : 0;
                Entities.Employee.Employees.Add(
                    new Entities.Employee(id, form.nameBox.Text, form.birthBox.Value, form.passportBox.Text,
                    form.addressBox.Text, form.phoneBox.Text)
                );
                JournalHelper.Write("Добавлен новый сотрудник");
                LoadData();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;
            var form = new AddEditForms.AddEditEmployee
            {
                Text = "Изменить сотрудника"
            };
            var emp = Entities.Employee.Employees.First(t => t.Id == int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
            form.applyButton.Text = "Применить";
            form.nameBox.Text = emp.Name;
            form.birthBox.Value = emp.BirthDate;
            form.passportBox.Text = emp.Passport;
            form.addressBox.Text = emp.Address;
            form.phoneBox.Text = emp.Phone;
            if (form.ShowDialog() == DialogResult.OK)
            {
                emp.Name = form.nameBox.Text;
                emp.BirthDate = form.birthBox.Value;
                emp.Passport = form.passportBox.Text;
                emp.Address = form.addressBox.Text;
                emp.Phone = form.phoneBox.Text;
                JournalHelper.Write("Изменена информация о сотруднике");
                LoadData();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;
            var emp = Entities.Employee.Employees.First(t => t.Id == int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
            if (emp.Check())
            {
                MessageBox.Show("Данную запись невозможно удалить, так как от неё зависима как минимум одна запись", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("Вы действительно желаете удалить выбранную запись?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Entities.Employee.Employees.Remove(emp);
                JournalHelper.Write("Сотрудник удалён");
                LoadData();
            }
        }
    }
}
