namespace DVLD_Project.Controls
{
    partial class ctrlDashboardHome
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.mainPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.footerPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.lblFooter = new System.Windows.Forms.Label();
            this.actionsPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.btnRefresh = new Guna.UI2.WinForms.Guna2Button();
            this.btnDetainLicense = new Guna.UI2.WinForms.Guna2Button();
            this.btnNewInternationalLicense = new Guna.UI2.WinForms.Guna2Button();
            this.btnNewLocalApplication = new Guna.UI2.WinForms.Guna2Button();
            this.btnAddUser = new Guna.UI2.WinForms.Guna2Button();
            this.btnAddPerson = new Guna.UI2.WinForms.Guna2Button();
            this.lblActionsTitle = new System.Windows.Forms.Label();
            this.statsGrid = new System.Windows.Forms.TableLayoutPanel();
            this.cardPeople = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTotalPeople = new System.Windows.Forms.Label();
            this.lblPeopleCaption = new System.Windows.Forms.Label();
            this.cardUsers = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTotalUsers = new System.Windows.Forms.Label();
            this.lblUsersCaption = new System.Windows.Forms.Label();
            this.cardDrivers = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTotalDrivers = new System.Windows.Forms.Label();
            this.lblDriversCaption = new System.Windows.Forms.Label();
            this.cardLocalApplications = new Guna.UI2.WinForms.Guna2Panel();
            this.lblLocalApplications = new System.Windows.Forms.Label();
            this.lblLocalApplicationsCaption = new System.Windows.Forms.Label();
            this.cardInternationalLicenses = new Guna.UI2.WinForms.Guna2Panel();
            this.lblInternationalLicenses = new System.Windows.Forms.Label();
            this.lblInternationalLicensesCaption = new System.Windows.Forms.Label();
            this.cardDetainedLicenses = new Guna.UI2.WinForms.Guna2Panel();
            this.lblDetainedLicenses = new System.Windows.Forms.Label();
            this.lblDetainedLicensesCaption = new System.Windows.Forms.Label();
            this.cardPendingApplications = new Guna.UI2.WinForms.Guna2Panel();
            this.lblPendingApplications = new System.Windows.Forms.Label();
            this.lblPendingApplicationsCaption = new System.Windows.Forms.Label();
            this.cardScheduledTests = new Guna.UI2.WinForms.Guna2Panel();
            this.lblScheduledTests = new System.Windows.Forms.Label();
            this.lblScheduledTestsCaption = new System.Windows.Forms.Label();
            this.headerPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.mainPanel.SuspendLayout();
            this.footerPanel.SuspendLayout();
            this.actionsPanel.SuspendLayout();
            this.statsGrid.SuspendLayout();
            this.cardPeople.SuspendLayout();
            this.cardUsers.SuspendLayout();
            this.cardDrivers.SuspendLayout();
            this.cardLocalApplications.SuspendLayout();
            this.cardInternationalLicenses.SuspendLayout();
            this.cardDetainedLicenses.SuspendLayout();
            this.cardPendingApplications.SuspendLayout();
            this.cardScheduledTests.SuspendLayout();
            this.headerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.AutoScroll = true;
            this.mainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.mainPanel.Controls.Add(this.footerPanel);
            this.mainPanel.Controls.Add(this.actionsPanel);
            this.mainPanel.Controls.Add(this.statsGrid);
            this.mainPanel.Controls.Add(this.headerPanel);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Padding = new System.Windows.Forms.Padding(28, 24, 28, 24);
            this.mainPanel.Size = new System.Drawing.Size(1120, 680);
            this.mainPanel.TabIndex = 0;
            // 
            // footerPanel
            // 
            this.footerPanel.BackColor = System.Drawing.Color.Transparent;
            this.footerPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.footerPanel.BorderRadius = 10;
            this.footerPanel.BorderThickness = 1;
            this.footerPanel.Controls.Add(this.lblFooter);
            this.footerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.footerPanel.FillColor = System.Drawing.Color.White;
            this.footerPanel.Location = new System.Drawing.Point(28, 587);
            this.footerPanel.Margin = new System.Windows.Forms.Padding(0, 0, 0, 18);
            this.footerPanel.Name = "footerPanel";
            this.footerPanel.Padding = new System.Windows.Forms.Padding(18, 12, 18, 12);
            this.footerPanel.Size = new System.Drawing.Size(1064, 54);
            this.footerPanel.TabIndex = 3;
            // 
            // lblFooter
            // 
            this.lblFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFooter.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblFooter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblFooter.Location = new System.Drawing.Point(18, 12);
            this.lblFooter.Name = "lblFooter";
            this.lblFooter.Size = new System.Drawing.Size(1028, 30);
            this.lblFooter.TabIndex = 0;
            this.lblFooter.Text = "Updated --";
            this.lblFooter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // actionsPanel
            // 
            this.actionsPanel.BackColor = System.Drawing.Color.Transparent;
            this.actionsPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.actionsPanel.BorderRadius = 12;
            this.actionsPanel.BorderThickness = 1;
            this.actionsPanel.Controls.Add(this.btnRefresh);
            this.actionsPanel.Controls.Add(this.btnDetainLicense);
            this.actionsPanel.Controls.Add(this.btnNewInternationalLicense);
            this.actionsPanel.Controls.Add(this.btnNewLocalApplication);
            this.actionsPanel.Controls.Add(this.btnAddUser);
            this.actionsPanel.Controls.Add(this.btnAddPerson);
            this.actionsPanel.Controls.Add(this.lblActionsTitle);
            this.actionsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.actionsPanel.FillColor = System.Drawing.Color.White;
            this.actionsPanel.Location = new System.Drawing.Point(28, 405);
            this.actionsPanel.Margin = new System.Windows.Forms.Padding(0, 0, 0, 18);
            this.actionsPanel.Name = "actionsPanel";
            this.actionsPanel.Padding = new System.Windows.Forms.Padding(22, 18, 22, 18);
            this.actionsPanel.Size = new System.Drawing.Size(1064, 182);
            this.actionsPanel.TabIndex = 2;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnRefresh.BorderRadius = 8;
            this.btnRefresh.BorderThickness = 1;
            this.btnRefresh.FillColor = System.Drawing.Color.White;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnRefresh.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnRefresh.Location = new System.Drawing.Point(914, 20);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(128, 36);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnDetainLicense
            // 
            this.btnDetainLicense.BorderRadius = 10;
            this.btnDetainLicense.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.btnDetainLicense.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDetainLicense.ForeColor = System.Drawing.Color.White;
            this.btnDetainLicense.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(56)))), ((int)(((byte)(202)))));
            this.btnDetainLicense.Location = new System.Drawing.Point(774, 86);
            this.btnDetainLicense.Name = "btnDetainLicense";
            this.btnDetainLicense.Size = new System.Drawing.Size(164, 54);
            this.btnDetainLicense.TabIndex = 5;
            this.btnDetainLicense.Text = "Detain License";
            this.btnDetainLicense.Click += new System.EventHandler(this.btnDetainLicense_Click);
            // 
            // btnNewInternationalLicense
            // 
            this.btnNewInternationalLicense.BorderRadius = 10;
            this.btnNewInternationalLicense.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(165)))), ((int)(((byte)(233)))));
            this.btnNewInternationalLicense.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnNewInternationalLicense.ForeColor = System.Drawing.Color.White;
            this.btnNewInternationalLicense.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            this.btnNewInternationalLicense.Location = new System.Drawing.Point(582, 86);
            this.btnNewInternationalLicense.Name = "btnNewInternationalLicense";
            this.btnNewInternationalLicense.Size = new System.Drawing.Size(174, 54);
            this.btnNewInternationalLicense.TabIndex = 4;
            this.btnNewInternationalLicense.Text = "New International License";
            this.btnNewInternationalLicense.Click += new System.EventHandler(this.btnNewInternationalLicense_Click);
            // 
            // btnNewLocalApplication
            // 
            this.btnNewLocalApplication.BorderRadius = 10;
            this.btnNewLocalApplication.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnNewLocalApplication.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnNewLocalApplication.ForeColor = System.Drawing.Color.White;
            this.btnNewLocalApplication.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.btnNewLocalApplication.Location = new System.Drawing.Point(390, 86);
            this.btnNewLocalApplication.Name = "btnNewLocalApplication";
            this.btnNewLocalApplication.Size = new System.Drawing.Size(174, 54);
            this.btnNewLocalApplication.TabIndex = 3;
            this.btnNewLocalApplication.Text = "New Local Application";
            this.btnNewLocalApplication.Click += new System.EventHandler(this.btnNewLocalApplication_Click);
            // 
            // btnAddUser
            // 
            this.btnAddUser.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.btnAddUser.BorderRadius = 10;
            this.btnAddUser.BorderThickness = 1;
            this.btnAddUser.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.btnAddUser.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAddUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.btnAddUser.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this.btnAddUser.Location = new System.Drawing.Point(214, 86);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(158, 54);
            this.btnAddUser.TabIndex = 2;
            this.btnAddUser.Text = "Add User";
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // btnAddPerson
            // 
            this.btnAddPerson.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.btnAddPerson.BorderRadius = 10;
            this.btnAddPerson.BorderThickness = 1;
            this.btnAddPerson.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.btnAddPerson.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAddPerson.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.btnAddPerson.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this.btnAddPerson.Location = new System.Drawing.Point(38, 86);
            this.btnAddPerson.Name = "btnAddPerson";
            this.btnAddPerson.Size = new System.Drawing.Size(158, 54);
            this.btnAddPerson.TabIndex = 1;
            this.btnAddPerson.Text = "Add Person";
            this.btnAddPerson.Click += new System.EventHandler(this.btnAddPerson_Click);
            // 
            // lblActionsTitle
            // 
            this.lblActionsTitle.AutoSize = true;
            this.lblActionsTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblActionsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblActionsTitle.Location = new System.Drawing.Point(34, 26);
            this.lblActionsTitle.Name = "lblActionsTitle";
            this.lblActionsTitle.Size = new System.Drawing.Size(132, 25);
            this.lblActionsTitle.TabIndex = 0;
            this.lblActionsTitle.Text = "Quick Actions";
            // 
            // statsGrid
            // 
            this.statsGrid.ColumnCount = 4;
            this.statsGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.statsGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.statsGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.statsGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.statsGrid.Controls.Add(this.cardPeople, 0, 0);
            this.statsGrid.Controls.Add(this.cardUsers, 1, 0);
            this.statsGrid.Controls.Add(this.cardDrivers, 2, 0);
            this.statsGrid.Controls.Add(this.cardLocalApplications, 3, 0);
            this.statsGrid.Controls.Add(this.cardInternationalLicenses, 0, 1);
            this.statsGrid.Controls.Add(this.cardDetainedLicenses, 1, 1);
            this.statsGrid.Controls.Add(this.cardPendingApplications, 2, 1);
            this.statsGrid.Controls.Add(this.cardScheduledTests, 3, 1);
            this.statsGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.statsGrid.Location = new System.Drawing.Point(28, 139);
            this.statsGrid.Margin = new System.Windows.Forms.Padding(0, 0, 0, 18);
            this.statsGrid.Name = "statsGrid";
            this.statsGrid.RowCount = 2;
            this.statsGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.statsGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.statsGrid.Size = new System.Drawing.Size(1064, 266);
            this.statsGrid.TabIndex = 1;
            // 
            // cardPeople
            // 
            this.cardPeople.BackColor = System.Drawing.Color.Transparent;
            this.cardPeople.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.cardPeople.BorderRadius = 12;
            this.cardPeople.BorderThickness = 1;
            this.cardPeople.Controls.Add(this.lblTotalPeople);
            this.cardPeople.Controls.Add(this.lblPeopleCaption);
            this.cardPeople.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardPeople.FillColor = System.Drawing.Color.White;
            this.cardPeople.Location = new System.Drawing.Point(0, 0);
            this.cardPeople.Margin = new System.Windows.Forms.Padding(0, 0, 14, 14);
            this.cardPeople.Name = "cardPeople";
            this.cardPeople.Padding = new System.Windows.Forms.Padding(18);
            this.cardPeople.Size = new System.Drawing.Size(252, 119);
            this.cardPeople.TabIndex = 0;
            // 
            // lblTotalPeople
            // 
            this.lblTotalPeople.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalPeople.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTotalPeople.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblTotalPeople.Location = new System.Drawing.Point(18, 45);
            this.lblTotalPeople.Name = "lblTotalPeople";
            this.lblTotalPeople.Size = new System.Drawing.Size(216, 56);
            this.lblTotalPeople.TabIndex = 1;
            this.lblTotalPeople.Text = "--";
            this.lblTotalPeople.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPeopleCaption
            // 
            this.lblPeopleCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPeopleCaption.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblPeopleCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblPeopleCaption.Location = new System.Drawing.Point(18, 18);
            this.lblPeopleCaption.Name = "lblPeopleCaption";
            this.lblPeopleCaption.Size = new System.Drawing.Size(216, 27);
            this.lblPeopleCaption.TabIndex = 0;
            this.lblPeopleCaption.Text = "Total People";
            // 
            // cardUsers
            // 
            this.cardUsers.BackColor = System.Drawing.Color.Transparent;
            this.cardUsers.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.cardUsers.BorderRadius = 12;
            this.cardUsers.BorderThickness = 1;
            this.cardUsers.Controls.Add(this.lblTotalUsers);
            this.cardUsers.Controls.Add(this.lblUsersCaption);
            this.cardUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardUsers.FillColor = System.Drawing.Color.White;
            this.cardUsers.Location = new System.Drawing.Point(266, 0);
            this.cardUsers.Margin = new System.Windows.Forms.Padding(0, 0, 14, 14);
            this.cardUsers.Name = "cardUsers";
            this.cardUsers.Padding = new System.Windows.Forms.Padding(18);
            this.cardUsers.Size = new System.Drawing.Size(252, 119);
            this.cardUsers.TabIndex = 1;
            // 
            // lblTotalUsers
            // 
            this.lblTotalUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalUsers.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTotalUsers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(165)))), ((int)(((byte)(233)))));
            this.lblTotalUsers.Location = new System.Drawing.Point(18, 45);
            this.lblTotalUsers.Name = "lblTotalUsers";
            this.lblTotalUsers.Size = new System.Drawing.Size(216, 56);
            this.lblTotalUsers.TabIndex = 1;
            this.lblTotalUsers.Text = "--";
            this.lblTotalUsers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUsersCaption
            // 
            this.lblUsersCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblUsersCaption.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblUsersCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblUsersCaption.Location = new System.Drawing.Point(18, 18);
            this.lblUsersCaption.Name = "lblUsersCaption";
            this.lblUsersCaption.Size = new System.Drawing.Size(216, 27);
            this.lblUsersCaption.TabIndex = 0;
            this.lblUsersCaption.Text = "Total Users";
            // 
            // cardDrivers
            // 
            this.cardDrivers.BackColor = System.Drawing.Color.Transparent;
            this.cardDrivers.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.cardDrivers.BorderRadius = 12;
            this.cardDrivers.BorderThickness = 1;
            this.cardDrivers.Controls.Add(this.lblTotalDrivers);
            this.cardDrivers.Controls.Add(this.lblDriversCaption);
            this.cardDrivers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardDrivers.FillColor = System.Drawing.Color.White;
            this.cardDrivers.Location = new System.Drawing.Point(532, 0);
            this.cardDrivers.Margin = new System.Windows.Forms.Padding(0, 0, 14, 14);
            this.cardDrivers.Name = "cardDrivers";
            this.cardDrivers.Padding = new System.Windows.Forms.Padding(18);
            this.cardDrivers.Size = new System.Drawing.Size(252, 119);
            this.cardDrivers.TabIndex = 2;
            // 
            // lblTotalDrivers
            // 
            this.lblTotalDrivers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalDrivers.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTotalDrivers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.lblTotalDrivers.Location = new System.Drawing.Point(18, 45);
            this.lblTotalDrivers.Name = "lblTotalDrivers";
            this.lblTotalDrivers.Size = new System.Drawing.Size(216, 56);
            this.lblTotalDrivers.TabIndex = 1;
            this.lblTotalDrivers.Text = "--";
            this.lblTotalDrivers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDriversCaption
            // 
            this.lblDriversCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDriversCaption.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblDriversCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblDriversCaption.Location = new System.Drawing.Point(18, 18);
            this.lblDriversCaption.Name = "lblDriversCaption";
            this.lblDriversCaption.Size = new System.Drawing.Size(216, 27);
            this.lblDriversCaption.TabIndex = 0;
            this.lblDriversCaption.Text = "Total Drivers";
            // 
            // cardLocalApplications
            // 
            this.cardLocalApplications.BackColor = System.Drawing.Color.Transparent;
            this.cardLocalApplications.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.cardLocalApplications.BorderRadius = 12;
            this.cardLocalApplications.BorderThickness = 1;
            this.cardLocalApplications.Controls.Add(this.lblLocalApplications);
            this.cardLocalApplications.Controls.Add(this.lblLocalApplicationsCaption);
            this.cardLocalApplications.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardLocalApplications.FillColor = System.Drawing.Color.White;
            this.cardLocalApplications.Location = new System.Drawing.Point(798, 0);
            this.cardLocalApplications.Margin = new System.Windows.Forms.Padding(0, 0, 0, 14);
            this.cardLocalApplications.Name = "cardLocalApplications";
            this.cardLocalApplications.Padding = new System.Windows.Forms.Padding(18);
            this.cardLocalApplications.Size = new System.Drawing.Size(266, 119);
            this.cardLocalApplications.TabIndex = 3;
            // 
            // lblLocalApplications
            // 
            this.lblLocalApplications.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLocalApplications.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblLocalApplications.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.lblLocalApplications.Location = new System.Drawing.Point(18, 45);
            this.lblLocalApplications.Name = "lblLocalApplications";
            this.lblLocalApplications.Size = new System.Drawing.Size(230, 56);
            this.lblLocalApplications.TabIndex = 1;
            this.lblLocalApplications.Text = "--";
            this.lblLocalApplications.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLocalApplicationsCaption
            // 
            this.lblLocalApplicationsCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblLocalApplicationsCaption.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblLocalApplicationsCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblLocalApplicationsCaption.Location = new System.Drawing.Point(18, 18);
            this.lblLocalApplicationsCaption.Name = "lblLocalApplicationsCaption";
            this.lblLocalApplicationsCaption.Size = new System.Drawing.Size(230, 27);
            this.lblLocalApplicationsCaption.TabIndex = 0;
            this.lblLocalApplicationsCaption.Text = "Local License Applications";
            // 
            // cardInternationalLicenses
            // 
            this.cardInternationalLicenses.BackColor = System.Drawing.Color.Transparent;
            this.cardInternationalLicenses.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.cardInternationalLicenses.BorderRadius = 12;
            this.cardInternationalLicenses.BorderThickness = 1;
            this.cardInternationalLicenses.Controls.Add(this.lblInternationalLicenses);
            this.cardInternationalLicenses.Controls.Add(this.lblInternationalLicensesCaption);
            this.cardInternationalLicenses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardInternationalLicenses.FillColor = System.Drawing.Color.White;
            this.cardInternationalLicenses.Location = new System.Drawing.Point(0, 133);
            this.cardInternationalLicenses.Margin = new System.Windows.Forms.Padding(0, 0, 14, 0);
            this.cardInternationalLicenses.Name = "cardInternationalLicenses";
            this.cardInternationalLicenses.Padding = new System.Windows.Forms.Padding(18);
            this.cardInternationalLicenses.Size = new System.Drawing.Size(252, 133);
            this.cardInternationalLicenses.TabIndex = 4;
            // 
            // lblInternationalLicenses
            // 
            this.lblInternationalLicenses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInternationalLicenses.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblInternationalLicenses.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(182)))), ((int)(((byte)(212)))));
            this.lblInternationalLicenses.Location = new System.Drawing.Point(18, 45);
            this.lblInternationalLicenses.Name = "lblInternationalLicenses";
            this.lblInternationalLicenses.Size = new System.Drawing.Size(216, 70);
            this.lblInternationalLicenses.TabIndex = 1;
            this.lblInternationalLicenses.Text = "--";
            this.lblInternationalLicenses.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblInternationalLicensesCaption
            // 
            this.lblInternationalLicensesCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblInternationalLicensesCaption.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblInternationalLicensesCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblInternationalLicensesCaption.Location = new System.Drawing.Point(18, 18);
            this.lblInternationalLicensesCaption.Name = "lblInternationalLicensesCaption";
            this.lblInternationalLicensesCaption.Size = new System.Drawing.Size(216, 27);
            this.lblInternationalLicensesCaption.TabIndex = 0;
            this.lblInternationalLicensesCaption.Text = "International Licenses";
            // 
            // cardDetainedLicenses
            // 
            this.cardDetainedLicenses.BackColor = System.Drawing.Color.Transparent;
            this.cardDetainedLicenses.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.cardDetainedLicenses.BorderRadius = 12;
            this.cardDetainedLicenses.BorderThickness = 1;
            this.cardDetainedLicenses.Controls.Add(this.lblDetainedLicenses);
            this.cardDetainedLicenses.Controls.Add(this.lblDetainedLicensesCaption);
            this.cardDetainedLicenses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardDetainedLicenses.FillColor = System.Drawing.Color.White;
            this.cardDetainedLicenses.Location = new System.Drawing.Point(266, 133);
            this.cardDetainedLicenses.Margin = new System.Windows.Forms.Padding(0, 0, 14, 0);
            this.cardDetainedLicenses.Name = "cardDetainedLicenses";
            this.cardDetainedLicenses.Padding = new System.Windows.Forms.Padding(18);
            this.cardDetainedLicenses.Size = new System.Drawing.Size(252, 133);
            this.cardDetainedLicenses.TabIndex = 5;
            // 
            // lblDetainedLicenses
            // 
            this.lblDetainedLicenses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDetainedLicenses.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblDetainedLicenses.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.lblDetainedLicenses.Location = new System.Drawing.Point(18, 45);
            this.lblDetainedLicenses.Name = "lblDetainedLicenses";
            this.lblDetainedLicenses.Size = new System.Drawing.Size(216, 70);
            this.lblDetainedLicenses.TabIndex = 1;
            this.lblDetainedLicenses.Text = "--";
            this.lblDetainedLicenses.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDetainedLicensesCaption
            // 
            this.lblDetainedLicensesCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDetainedLicensesCaption.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblDetainedLicensesCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblDetainedLicensesCaption.Location = new System.Drawing.Point(18, 18);
            this.lblDetainedLicensesCaption.Name = "lblDetainedLicensesCaption";
            this.lblDetainedLicensesCaption.Size = new System.Drawing.Size(216, 27);
            this.lblDetainedLicensesCaption.TabIndex = 0;
            this.lblDetainedLicensesCaption.Text = "Active Detentions";
            // 
            // cardPendingApplications
            // 
            this.cardPendingApplications.BackColor = System.Drawing.Color.Transparent;
            this.cardPendingApplications.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.cardPendingApplications.BorderRadius = 12;
            this.cardPendingApplications.BorderThickness = 1;
            this.cardPendingApplications.Controls.Add(this.lblPendingApplications);
            this.cardPendingApplications.Controls.Add(this.lblPendingApplicationsCaption);
            this.cardPendingApplications.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardPendingApplications.FillColor = System.Drawing.Color.White;
            this.cardPendingApplications.Location = new System.Drawing.Point(532, 133);
            this.cardPendingApplications.Margin = new System.Windows.Forms.Padding(0, 0, 14, 0);
            this.cardPendingApplications.Name = "cardPendingApplications";
            this.cardPendingApplications.Padding = new System.Windows.Forms.Padding(18);
            this.cardPendingApplications.Size = new System.Drawing.Size(252, 133);
            this.cardPendingApplications.TabIndex = 6;
            // 
            // lblPendingApplications
            // 
            this.lblPendingApplications.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPendingApplications.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblPendingApplications.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(88)))), ((int)(((byte)(12)))));
            this.lblPendingApplications.Location = new System.Drawing.Point(18, 45);
            this.lblPendingApplications.Name = "lblPendingApplications";
            this.lblPendingApplications.Size = new System.Drawing.Size(216, 70);
            this.lblPendingApplications.TabIndex = 1;
            this.lblPendingApplications.Text = "--";
            this.lblPendingApplications.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPendingApplicationsCaption
            // 
            this.lblPendingApplicationsCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPendingApplicationsCaption.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblPendingApplicationsCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblPendingApplicationsCaption.Location = new System.Drawing.Point(18, 18);
            this.lblPendingApplicationsCaption.Name = "lblPendingApplicationsCaption";
            this.lblPendingApplicationsCaption.Size = new System.Drawing.Size(216, 27);
            this.lblPendingApplicationsCaption.TabIndex = 0;
            this.lblPendingApplicationsCaption.Text = "Pending Applications";
            // 
            // cardScheduledTests
            // 
            this.cardScheduledTests.BackColor = System.Drawing.Color.Transparent;
            this.cardScheduledTests.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.cardScheduledTests.BorderRadius = 12;
            this.cardScheduledTests.BorderThickness = 1;
            this.cardScheduledTests.Controls.Add(this.lblScheduledTests);
            this.cardScheduledTests.Controls.Add(this.lblScheduledTestsCaption);
            this.cardScheduledTests.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardScheduledTests.FillColor = System.Drawing.Color.White;
            this.cardScheduledTests.Location = new System.Drawing.Point(798, 133);
            this.cardScheduledTests.Margin = new System.Windows.Forms.Padding(0);
            this.cardScheduledTests.Name = "cardScheduledTests";
            this.cardScheduledTests.Padding = new System.Windows.Forms.Padding(18);
            this.cardScheduledTests.Size = new System.Drawing.Size(266, 133);
            this.cardScheduledTests.TabIndex = 7;
            // 
            // lblScheduledTests
            // 
            this.lblScheduledTests.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblScheduledTests.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblScheduledTests.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(58)))), ((int)(((byte)(237)))));
            this.lblScheduledTests.Location = new System.Drawing.Point(18, 45);
            this.lblScheduledTests.Name = "lblScheduledTests";
            this.lblScheduledTests.Size = new System.Drawing.Size(230, 70);
            this.lblScheduledTests.TabIndex = 1;
            this.lblScheduledTests.Text = "--";
            this.lblScheduledTests.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblScheduledTestsCaption
            // 
            this.lblScheduledTestsCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblScheduledTestsCaption.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblScheduledTestsCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblScheduledTestsCaption.Location = new System.Drawing.Point(18, 18);
            this.lblScheduledTestsCaption.Name = "lblScheduledTestsCaption";
            this.lblScheduledTestsCaption.Size = new System.Drawing.Size(230, 27);
            this.lblScheduledTestsCaption.TabIndex = 0;
            this.lblScheduledTestsCaption.Text = "Open Test Appointments";
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.Transparent;
            this.headerPanel.BorderRadius = 14;
            this.headerPanel.Controls.Add(this.lblSubtitle);
            this.headerPanel.Controls.Add(this.lblTitle);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.headerPanel.Location = new System.Drawing.Point(28, 24);
            this.headerPanel.Margin = new System.Windows.Forms.Padding(0, 0, 0, 18);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Padding = new System.Windows.Forms.Padding(28, 22, 28, 20);
            this.headerPanel.Size = new System.Drawing.Size(1064, 115);
            this.headerPanel.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblSubtitle.Location = new System.Drawing.Point(28, 65);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(1008, 24);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Overview of the system";
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(28, 22);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1008, 43);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "DVLD Dashboard";
            // 
            // ctrlDashboardHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.Controls.Add(this.mainPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "ctrlDashboardHome";
            this.Size = new System.Drawing.Size(1120, 680);
            this.Load += new System.EventHandler(this.ctrlDashboardHome_Load);
            this.mainPanel.ResumeLayout(false);
            this.footerPanel.ResumeLayout(false);
            this.actionsPanel.ResumeLayout(false);
            this.actionsPanel.PerformLayout();
            this.statsGrid.ResumeLayout(false);
            this.cardPeople.ResumeLayout(false);
            this.cardUsers.ResumeLayout(false);
            this.cardDrivers.ResumeLayout(false);
            this.cardLocalApplications.ResumeLayout(false);
            this.cardInternationalLicenses.ResumeLayout(false);
            this.cardDetainedLicenses.ResumeLayout(false);
            this.cardPendingApplications.ResumeLayout(false);
            this.cardScheduledTests.ResumeLayout(false);
            this.headerPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel mainPanel;
        private Guna.UI2.WinForms.Guna2Panel headerPanel;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.TableLayoutPanel statsGrid;
        private Guna.UI2.WinForms.Guna2Panel cardPeople;
        private System.Windows.Forms.Label lblTotalPeople;
        private System.Windows.Forms.Label lblPeopleCaption;
        private Guna.UI2.WinForms.Guna2Panel cardUsers;
        private System.Windows.Forms.Label lblTotalUsers;
        private System.Windows.Forms.Label lblUsersCaption;
        private Guna.UI2.WinForms.Guna2Panel cardDrivers;
        private System.Windows.Forms.Label lblTotalDrivers;
        private System.Windows.Forms.Label lblDriversCaption;
        private Guna.UI2.WinForms.Guna2Panel cardLocalApplications;
        private System.Windows.Forms.Label lblLocalApplications;
        private System.Windows.Forms.Label lblLocalApplicationsCaption;
        private Guna.UI2.WinForms.Guna2Panel cardInternationalLicenses;
        private System.Windows.Forms.Label lblInternationalLicenses;
        private System.Windows.Forms.Label lblInternationalLicensesCaption;
        private Guna.UI2.WinForms.Guna2Panel cardDetainedLicenses;
        private System.Windows.Forms.Label lblDetainedLicenses;
        private System.Windows.Forms.Label lblDetainedLicensesCaption;
        private Guna.UI2.WinForms.Guna2Panel cardPendingApplications;
        private System.Windows.Forms.Label lblPendingApplications;
        private System.Windows.Forms.Label lblPendingApplicationsCaption;
        private Guna.UI2.WinForms.Guna2Panel cardScheduledTests;
        private System.Windows.Forms.Label lblScheduledTests;
        private System.Windows.Forms.Label lblScheduledTestsCaption;
        private Guna.UI2.WinForms.Guna2Panel actionsPanel;
        private System.Windows.Forms.Label lblActionsTitle;
        private Guna.UI2.WinForms.Guna2Button btnAddPerson;
        private Guna.UI2.WinForms.Guna2Button btnAddUser;
        private Guna.UI2.WinForms.Guna2Button btnNewLocalApplication;
        private Guna.UI2.WinForms.Guna2Button btnNewInternationalLicense;
        private Guna.UI2.WinForms.Guna2Button btnDetainLicense;
        private Guna.UI2.WinForms.Guna2Button btnRefresh;
        private Guna.UI2.WinForms.Guna2Panel footerPanel;
        private System.Windows.Forms.Label lblFooter;
    }
}
