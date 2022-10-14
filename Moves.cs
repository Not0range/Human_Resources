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
    public partial class Moves : Form
    {
        public Moves()
        {
            InitializeComponent();
        }

        private void Moves_Load(object sender, EventArgs e)
        {
            LoadHiring();
            LoadPositions();
            LoadSubdivisions();
            LoadTrips();
            LoadVacations();
            LoadDismissals();
        }

        private void LoadHiring()
        {
            hiringDataGrid.SuspendLayout();
            hiringDataGrid.Rows.Clear();
            foreach (var h in Entities.Hiring.Hirings)
            {
                hiringDataGrid.Rows.Add(h.Id, h.Employee.Name, h.Date.ToString("dd.MM.yyyy"), h.Subdivision, h.Position);
            }
            hiringDataGrid.ResumeLayout();
        }

        private void LoadPositions()
        {
            positionDataGrid.SuspendLayout();
            positionDataGrid.Rows.Clear();
            foreach (var p in Entities.PositionChange.PositionChanges)
            {
                positionDataGrid.Rows.Add(p.Id, p.Employee.Name, p.Date.ToString("dd.MM.yyyy"), p.Position);
            }
            positionDataGrid.ResumeLayout();
        }

        private void LoadSubdivisions()
        {
            subdivisionDataGrid.SuspendLayout();
            subdivisionDataGrid.Rows.Clear();
            foreach (var s in Entities.SubdivisionChange.SubdivisionChanges)
            {
                subdivisionDataGrid.Rows.Add(s.Id, s.Employee.Name, s.Date.ToString("dd.MM.yyyy"), s.Subdivision);
            }
            subdivisionDataGrid.ResumeLayout();
        }

        private void LoadTrips()
        {
            tripDataGrid.SuspendLayout();
            tripDataGrid.Rows.Clear();
            foreach (var b in Entities.BusinessTrip.BusinessTrips)
            {
                tripDataGrid.Rows.Add(b.Id, b.Employee.Name, b.Date.ToString("dd.MM.yyyy"), b.Duration, b.Place);
            }
            tripDataGrid.ResumeLayout();
        }

        private void LoadVacations()
        {
            vacationDataGrid.SuspendLayout();
            vacationDataGrid.Rows.Clear();
            foreach (var v in Entities.Vacation.Vacations)
            {
                vacationDataGrid.Rows.Add(v.Id, v.Employee.Name, v.Date.ToString("dd.MM.yyyy"), v.Duration);
            }
            vacationDataGrid.ResumeLayout();
        }

        private void LoadDismissals()
        {
            dismisalsDataGrid.SuspendLayout();
            dismisalsDataGrid.Rows.Clear();
            foreach (var d in Entities.Dismissal.Dismissals)
            {
                dismisalsDataGrid.Rows.Add(d.Id, d.Employee.Name, d.Date.ToString("dd.MM.yyyy"));
            }
            dismisalsDataGrid.ResumeLayout();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form = new AddEditForms.AddEditHiring
            {
                Text = "Найм сотрудника"
            };

            if (form.ShowDialog() == DialogResult.OK)
            {
                int id = Entities.Hiring.Hirings.Any() ? Entities.Hiring.Hirings.Max(t => t.Id) + 1 : 0;
                Entities.Hiring.Hirings.Add(
                    new Entities.Hiring(id, form.employeeBox.SelectedItem as Entities.Employee, form.dateBox.Value,
                    form.subdivisionBox.SelectedItem as Entities.Subdivision, form.positionBox.Text)
                );
                JournalHelper.Write("Добавлена новая информация (приём на работу)");
                LoadHiring();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (hiringDataGrid.SelectedRows.Count == 0)
                return;
            var form = new AddEditForms.AddEditHiring
            {
                Text = "Изменение информации о найме"
            };
            form.applyButton.Text = "Применить";
            var hiring = Entities.Hiring.Hirings.First(t => t.Id == int.Parse(hiringDataGrid.SelectedRows[0].Cells[0].Value.ToString()));
            form.employeeBox.Items.Insert(0, hiring.Employee);

            form.employeeBox.SelectedItem = hiring.Employee;
            form.dateBox.Value = hiring.Date;
            form.subdivisionBox.SelectedItem = hiring.Subdivision;
            form.positionBox.Text = hiring.Position;

            if (form.ShowDialog() == DialogResult.OK)
            {
                hiring.Employee = form.employeeBox.SelectedItem as Entities.Employee;
                hiring.Date = form.dateBox.Value;
                hiring.Subdivision = form.subdivisionBox.SelectedItem as Entities.Subdivision;
                hiring.Position = form.positionBox.Text;
                JournalHelper.Write("Изменена информация (приём на работу)");
                LoadHiring();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (hiringDataGrid.SelectedRows.Count == 0)
                return;
            var hiring = Entities.Hiring.Hirings.First(t => t.Id == int.Parse(hiringDataGrid.SelectedRows[0].Cells[0].Value.ToString()));
            if (MessageBox.Show("Вы действительно желаете удалить выбранную запись?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Entities.Hiring.Hirings.Remove(hiring);
                JournalHelper.Write("Удалена информация (приём на работу)");
                LoadHiring();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var form = new AddEditForms.AddEditPositionChange
            {
                Text = "Перевод на должность"
            };

            if (form.ShowDialog() == DialogResult.OK)
            {
                int id = Entities.PositionChange.PositionChanges.Any() ? Entities.PositionChange.PositionChanges.Max(t => t.Id) + 1 : 0;
                Entities.PositionChange.PositionChanges.Add(
                    new Entities.PositionChange(id, form.employeeBox.SelectedItem as Entities.Employee, form.dateBox.Value,
                    form.positionBox.Text)
                );
                JournalHelper.Write("Добавлена новая информация (перевод на другую должность)");
                LoadSubdivisions();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (positionDataGrid.SelectedRows.Count == 0)
                return;
            var form = new AddEditForms.AddEditPositionChange
            {
                Text = "Изменение информации о переводе на должность"
            };
            form.applyButton.Text = "Применить";
            var pos = Entities.PositionChange.PositionChanges.First(t => t.Id == int.Parse(positionDataGrid.SelectedRows[0].Cells[0].Value.ToString()));

            form.employeeBox.SelectedItem = pos.Employee;
            form.dateBox.Value = pos.Date;
            form.positionBox.Text = pos.Position;

            if (form.ShowDialog() == DialogResult.OK)
            {
                pos.Employee = form.employeeBox.SelectedItem as Entities.Employee;
                pos.Date = form.dateBox.Value;
                pos.Position = form.positionBox.Text;
                JournalHelper.Write("Изменена информация (перевод на другую должность)");
                LoadPositions();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (positionDataGrid.SelectedRows.Count == 0)
                return;
            var pos = Entities.PositionChange.PositionChanges.First(t => t.Id == int.Parse(positionDataGrid.SelectedRows[0].Cells[0].Value.ToString()));
            if (MessageBox.Show("Вы действительно желаете удалить выбранную запись?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Entities.PositionChange.PositionChanges.Remove(pos);
                JournalHelper.Write("Удалена информация (перевод на другую должность)");
                LoadPositions();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var form = new AddEditForms.AddEditSubdivisionChange
            {
                Text = "Перевод в подразделение"
            };

            if (form.ShowDialog() == DialogResult.OK)
            {
                int id = Entities.SubdivisionChange.SubdivisionChanges.Any() ? Entities.SubdivisionChange.SubdivisionChanges.Max(t => t.Id) + 1 : 0;
                Entities.SubdivisionChange.SubdivisionChanges.Add(
                    new Entities.SubdivisionChange(id, form.employeeBox.SelectedItem as Entities.Employee, form.dateBox.Value,
                    form.subdivisionBox.SelectedItem as Entities.Subdivision)
                );
                JournalHelper.Write("Добавлена новая информация (перевод в другое подразделение)");
                LoadPositions();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (subdivisionDataGrid.SelectedRows.Count == 0)
                return;
            var form = new AddEditForms.AddEditSubdivisionChange
            {
                Text = "Изменение информации о переводе в подразделение"
            };
            form.applyButton.Text = "Применить";
            var sub = Entities.SubdivisionChange.SubdivisionChanges.First(t => t.Id == int.Parse(subdivisionDataGrid.SelectedRows[0].Cells[0].Value.ToString()));

            form.employeeBox.SelectedItem = sub.Employee;
            form.dateBox.Value = sub.Date;
            form.subdivisionBox.SelectedItem = sub.Subdivision;

            if (form.ShowDialog() == DialogResult.OK)
            {
                sub.Employee = form.employeeBox.SelectedItem as Entities.Employee;
                sub.Date = form.dateBox.Value;
                sub.Subdivision = form.subdivisionBox.SelectedItem as Entities.Subdivision;
                JournalHelper.Write("Изменена информация (перевод в другое подразделение)");
                LoadPositions();
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (subdivisionDataGrid.SelectedRows.Count == 0)
                return;
            var sub = Entities.SubdivisionChange.SubdivisionChanges.First(t => t.Id == int.Parse(subdivisionDataGrid.SelectedRows[0].Cells[0].Value.ToString()));
            if (MessageBox.Show("Вы действительно желаете удалить выбранную запись?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Entities.SubdivisionChange.SubdivisionChanges.Remove(sub);
                JournalHelper.Write("Удалена информация (перевод в другое подразделение)");
                LoadPositions();
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            var form = new AddEditForms.AddEditBusinessTrip
            {
                Text = "Добавить командировку"
            };

            if (form.ShowDialog() == DialogResult.OK)
            {
                int id = Entities.BusinessTrip.BusinessTrips.Any() ? Entities.BusinessTrip.BusinessTrips.Max(t => t.Id) + 1 : 0;
                Entities.BusinessTrip.BusinessTrips.Add(
                    new Entities.BusinessTrip(id, form.employeeBox.SelectedItem as Entities.Employee, form.dateBox.Value,
                    (int)form.durationBox.Value, form.placeBox.Text)
                );
                JournalHelper.Write("Добавлена новая информация (командировка)");
                LoadTrips();
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (tripDataGrid.SelectedRows.Count == 0)
                return;
            var form = new AddEditForms.AddEditBusinessTrip
            {
                Text = "Изменение информации о командировке"
            };
            form.applyButton.Text = "Применить";
            var tr = Entities.BusinessTrip.BusinessTrips.First(t => t.Id == int.Parse(tripDataGrid.SelectedRows[0].Cells[0].Value.ToString()));

            form.employeeBox.SelectedItem = tr.Employee;
            form.dateBox.Value = tr.Date;
            form.durationBox.Value = tr.Duration;
            form.placeBox.Text = tr.Place;

            if (form.ShowDialog() == DialogResult.OK)
            {
                tr.Employee = form.employeeBox.SelectedItem as Entities.Employee;
                tr.Date = form.dateBox.Value;
                tr.Duration = (int)form.durationBox.Value;
                tr.Place = form.placeBox.Text;
                JournalHelper.Write("Изменена информация (командировка)");
                LoadTrips();
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (tripDataGrid.SelectedRows.Count == 0)
                return;
            var tr = Entities.BusinessTrip.BusinessTrips.First(t => t.Id == int.Parse(tripDataGrid.SelectedRows[0].Cells[0].Value.ToString()));
            if (MessageBox.Show("Вы действительно желаете удалить выбранную запись?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Entities.BusinessTrip.BusinessTrips.Remove(tr);
                JournalHelper.Write("Удалена информация (командировка)");
                LoadTrips();
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            var form = new AddEditForms.AddEditVacation
            {
                Text = "Добавить отпуск"
            };

            if (form.ShowDialog() == DialogResult.OK)
            {
                int id = Entities.Vacation.Vacations.Any() ? Entities.Vacation.Vacations.Max(t => t.Id) + 1 : 0;
                Entities.Vacation.Vacations.Add(
                    new Entities.Vacation(id, form.employeeBox.SelectedItem as Entities.Employee, form.dateBox.Value,
                    (int)form.durationBox.Value)
                );
                JournalHelper.Write("Добавлена новая информация (отпуск)");
                LoadVacations();
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (vacationDataGrid.SelectedRows.Count == 0)
                return;
            var form = new AddEditForms.AddEditVacation
            {
                Text = "Изменение информации об отпуске"
            };
            form.applyButton.Text = "Применить";
            var v = Entities.Vacation.Vacations.First(t => t.Id == int.Parse(vacationDataGrid.SelectedRows[0].Cells[0].Value.ToString()));

            form.employeeBox.SelectedItem = v.Employee;
            form.dateBox.Value = v.Date;
            form.durationBox.Value = v.Duration;

            if (form.ShowDialog() == DialogResult.OK)
            {
                v.Employee = form.employeeBox.SelectedItem as Entities.Employee;
                v.Date = form.dateBox.Value;
                v.Duration = (int)form.durationBox.Value;
                JournalHelper.Write("Изменена информация (отпуск)");
                LoadVacations();
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (vacationDataGrid.SelectedRows.Count == 0)
                return;
            var v = Entities.Vacation.Vacations.First(t => t.Id == int.Parse(vacationDataGrid.SelectedRows[0].Cells[0].Value.ToString()));
            if (MessageBox.Show("Вы действительно желаете удалить выбранную запись?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Entities.Vacation.Vacations.Remove(v);
                JournalHelper.Write("Удалена информация (отпуск)");
                LoadVacations();
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            var form = new AddEditForms.AddEditDismissal
            {
                Text = "Добавить увольнение"
            };

            if (form.ShowDialog() == DialogResult.OK)
            {
                int id = Entities.Dismissal.Dismissals.Any() ? Entities.Dismissal.Dismissals.Max(t => t.Id) + 1 : 0;
                Entities.Dismissal.Dismissals.Add(
                    new Entities.Dismissal(id, form.employeeBox.SelectedItem as Entities.Employee, form.dateBox.Value)
                );
                JournalHelper.Write("Добавлена новая информация (увольнение)");
                LoadDismissals();
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (dismisalsDataGrid.SelectedRows.Count == 0)
                return;
            var form = new AddEditForms.AddEditDismissal
            {
                Text = "Изменение информации об увольнении"
            };
            form.applyButton.Text = "Применить";
            var d = Entities.Dismissal.Dismissals.First(t => t.Id == int.Parse(dismisalsDataGrid.SelectedRows[0].Cells[0].Value.ToString()));
            form.employeeBox.Items.Insert(0, d.Employee);

            form.employeeBox.SelectedItem = d.Employee;
            form.dateBox.Value = d.Date;

            if (form.ShowDialog() == DialogResult.OK)
            {
                d.Employee = form.employeeBox.SelectedItem as Entities.Employee;
                d.Date = form.dateBox.Value;
                JournalHelper.Write("Изменена информация (увольнение)");
                LoadDismissals();
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (dismisalsDataGrid.SelectedRows.Count == 0)
                return;
            var d = Entities.Dismissal.Dismissals.First(t => t.Id == int.Parse(dismisalsDataGrid.SelectedRows[0].Cells[0].Value.ToString()));
            if (MessageBox.Show("Вы действительно желаете удалить выбранную запись?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Entities.Dismissal.Dismissals.Remove(d);
                JournalHelper.Write("Удалена информация (увольнение)");
                LoadDismissals();
            }
        }
    }
}
