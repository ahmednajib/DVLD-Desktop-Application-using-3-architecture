using System;
using System.Data;
using System.Windows.Forms;
using DVLD_BuisnessLayer;

namespace DVLD_Project
{
    public partial class frmListPeople : Form
    {
        public frmListPeople()
        {
            InitializeComponent();
        }

        private static DataTable _dtAllPeople = clsPerson.GetAllPeople();

        private DataTable _MyDataTable = _dtAllPeople.DefaultView.ToTable(false, "Person ID", "National No",
                                                       "First Name", "Second Name", "Third Name", "Last Name",
                                                       "Gender", "Date Of Birth", "Nationality",
                                                       "Phone", "Email");

        private void _LoadPeopleData()
        {
            _dtAllPeople = clsPerson.GetAllPeople();
            _MyDataTable = _dtAllPeople.DefaultView.ToTable(false, "Person ID", "National No",
                                                       "First Name", "Second Name", "Third Name", "Last Name",
                                                       "Gender", "Date Of Birth", "Nationality",
                                                       "Phone", "Email");

            dgvManagePeople.DataSource = _MyDataTable;
            lblNumberOfRecords.Text = dgvManagePeople.Rows.Count.ToString();
            cmbFilterBy.SelectedIndex = 0;
        }

        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            _LoadPeopleData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e) 
        {
            string filterText = txtFilter.Text.Trim(); // Escape single quotes
            string selectedFilter = cmbFilterBy.Text;

            // Show all records if no filter text or "None" is selected
            if (string.IsNullOrEmpty(filterText) || selectedFilter == "None")
            {
                _MyDataTable.DefaultView.RowFilter = "";
                lblNumberOfRecords.Text = (dgvManagePeople.Rows.Count).ToString();
                return;
            }

            // Safety: ensure the column exists
            if (!_MyDataTable.Columns.Contains(selectedFilter))
                return;

            string filterExpression;

            if (selectedFilter == "Person ID")
            {
                //integer filtering
                filterExpression = $"[{selectedFilter}] = {filterText}";
            }
            else
            {
                // String filtering
                filterExpression = $"[{selectedFilter}] LIKE '%{filterText}%'";
            }

            _MyDataTable.DefaultView.RowFilter = filterExpression;
            lblNumberOfRecords.Text = (dgvManagePeople.Rows.Count).ToString();
        }

        private void cmbFilterBy_TextChanged(object sender, EventArgs e)
        {
            if (cmbFilterBy.Text == "None")
            {
                txtFilter.Visible = false;
                _MyDataTable.DefaultView.RowFilter = ""; // Show all records again
            }
            else
            {
                txtFilter.Visible = true;

                txtFilter.Clear();
                txtFilter.Focus();
            }
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            string selectedFilter = cmbFilterBy.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedFilter))
                return;

            char keyChar = e.KeyChar;

            // Allow control characters like Backspace
            if (char.IsControl(keyChar))
                return;

            switch (selectedFilter)
            {
                case "Person ID":
                case "Phone":
                    // Allow only digits
                    if (!char.IsDigit(keyChar))
                    {
                        e.Handled = true;
                    }
                    break;

                case "First Name":
                case "Second Name":
                case "Third Name":
                case "Last Name":
                case "Nationality":
                case "Gender":
                    // Allow only letters
                    if (!char.IsLetter(keyChar))
                    {
                        e.Handled = true;
                    }
                    break;

                case "Email":
                case "National No":
                    // Allow letters, digits, and common email characters
                    if (!char.IsLetterOrDigit(keyChar) && keyChar != '@' && keyChar != '.' && keyChar != '-' && keyChar != '_')
                    {
                        e.Handled = true;
                    }
                    break;
            }
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            Form form = new frmAddUpdatePerson();
            form.ShowDialog();
            _LoadPeopleData();
        }

        //
        //Strip Menu Item Tools
        //

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int SelectedIndexID = Convert.ToInt32(dgvManagePeople.CurrentRow.Cells[0].Value);
            Form form = new frmAddUpdatePerson(SelectedIndexID);
            form.ShowDialog();
            _LoadPeopleData();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int SelectedIndexID = Convert.ToInt32(dgvManagePeople.CurrentRow.Cells[0].Value);
            Form form = new frmPersonInformation(SelectedIndexID);
            form.ShowDialog();
            _LoadPeopleData();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int SelectedIndexID = Convert.ToInt32(dgvManagePeople.CurrentRow.Cells[0].Value);
            if (MessageBox.Show("Are you sure you want to delete this person?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (clsPerson.DeletePerson(SelectedIndexID))
                {
                    MessageBox.Show("Person deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _LoadPeopleData();
                }
                else
                {
                    MessageBox.Show("Failed to delete person. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}