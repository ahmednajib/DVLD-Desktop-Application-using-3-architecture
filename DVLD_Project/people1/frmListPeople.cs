using DVLD_BuisnessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class frmListPeople : Form
    {
        public frmListPeople()
        {
            InitializeComponent();
        }

        private static DataTable _dtAllPeople = clsPerson.GetAllPeople();

        private DataTable _MyDataTable = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                       "FirstName", "SecondName", "ThirdName", "LastName",
                                                       "GenderCaption", "DateOfBirth", "CountryName",
                                                       "Phone", "Email");

        private void _LoadPeopleData()
        {
            _dtAllPeople = clsPerson.GetAllPeople();
            _MyDataTable = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                       "FirstName", "SecondName", "ThirdName", "LastName",
                                                       "GenderCaption", "DateOfBirth", "CountryName",
                                                       "Phone", "Email");

            dgvManagePeople.DataSource = _MyDataTable;
            lblNumberOfRecords.Text = dgvManagePeople.Rows.Count.ToString();
            cmbFilterBy.SelectedIndex = 0;
        }

        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            dgvManagePeople.DataSource = _MyDataTable;
            cmbFilterBy.SelectedIndex = 0;
            lblNumberOfRecords.Text = dgvManagePeople.Rows.Count.ToString();

            if (dgvManagePeople.Rows.Count > 0)
            {

                dgvManagePeople.Columns[0].HeaderText = "Person ID";
                dgvManagePeople.Columns[0].Width = 80;

                dgvManagePeople.Columns[1].HeaderText = "National No";
                dgvManagePeople.Columns[1].Width = 70;


                dgvManagePeople.Columns[2].HeaderText = "First Name";
                dgvManagePeople.Columns[2].Width = 100;

                dgvManagePeople.Columns[3].HeaderText = "Second Name";
                dgvManagePeople.Columns[3].Width = 100;


                dgvManagePeople.Columns[4].HeaderText = "Third Name";
                dgvManagePeople.Columns[4].Width = 100;

                dgvManagePeople.Columns[5].HeaderText = "Last Name";
                dgvManagePeople.Columns[5].Width = 100;

                dgvManagePeople.Columns[6].HeaderText = "Gender";
                dgvManagePeople.Columns[6].Width = 80;

                dgvManagePeople.Columns[7].HeaderText = "Date Of Birth";
                dgvManagePeople.Columns[7].Width = 120;

                dgvManagePeople.Columns[8].HeaderText = "Nationality";
                dgvManagePeople.Columns[8].Width = 80;


                dgvManagePeople.Columns[9].HeaderText = "Phone";
                dgvManagePeople.Columns[9].Width = 100;


                dgvManagePeople.Columns[10].HeaderText = "Email";
                dgvManagePeople.Columns[10].Width = 157;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e) 
        {
            string FilterColumn = "";

            switch (cmbFilterBy.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No":
                    FilterColumn = "NationalNo";
                    break;

                case "First Name":
                    FilterColumn = "FirstName";
                    break;

                case "Second Name":
                    FilterColumn = "SecondName";
                    break;

                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;

                case "Last Name":
                    FilterColumn = "LastName";
                    break;

                case "Nationality":
                    FilterColumn = "CountryName";
                    break;

                case "Gender":
                    FilterColumn = "GenderCaption";
                    break;

                case "Phone":
                    FilterColumn = "Phone";
                    break;

                case "Email":
                    FilterColumn = "Email";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilter.Text.Trim() == "" || FilterColumn == "None")
            {
                _MyDataTable.DefaultView.RowFilter = "";
                lblNumberOfRecords.Text = dgvManagePeople.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "PersonID")
                //in this case we deal with numbers not string.
                _MyDataTable.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilter.Text.Trim());
            else
                _MyDataTable.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilter.Text.Trim());

            lblNumberOfRecords.Text = dgvManagePeople.Rows.Count.ToString();
        }

        private void cmbFilterBy_TextChanged(object sender, EventArgs e)
        {
            txtFilter.Visible = (cmbFilterBy.Text != "None");

            if (txtFilter.Visible)
            {
                txtFilter.Text = "";
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