using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HumanResources.AddEditForms
{
    public partial class AddEditSubdivisionChange : Form
    {
        public AddEditSubdivisionChange()
        {
            InitializeComponent();
            employeeBox.Items.AddRange(Entities.Employee.Employees.Where(t => {
                var state = t.CurrentState();
                return state.Position != null && state.Position != Entities.Dismissal.DISMISSALED && state.Subdivision != null;
            }).ToArray());

            subdivisionBox.Items.AddRange(Entities.Subdivision.Subdivisions.ToArray());
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            if (employeeBox.SelectedItem == null ||
                subdivisionBox.SelectedItem == null)
            {
                MessageBox.Show("Все поля должны быть заполнены", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult = DialogResult.OK;

        }
    }
}
