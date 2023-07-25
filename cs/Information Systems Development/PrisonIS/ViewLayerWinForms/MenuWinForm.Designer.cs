
namespace ViewLayerWinForms
{
    partial class MenuWinForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.cellPrisonerVisitTabPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.editCellButton = new System.Windows.Forms.Button();
            this.addCellButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.releasePrisonerButton = new System.Windows.Forms.Button();
            this.editPrisonerButton = new System.Windows.Forms.Button();
            this.addPrisonerButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cellDataGrid = new System.Windows.Forms.DataGridView();
            this.visitDataGrid = new System.Windows.Forms.DataGridView();
            this.prisonerDataGrid = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.editVisitButton = new System.Windows.Forms.Button();
            this.addVisitButton = new System.Windows.Forms.Button();
            this.visitorTabPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.editVisitorButton = new System.Windows.Forms.Button();
            this.forbidVisitorButton = new System.Windows.Forms.Button();
            this.addVisitorButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.visitorDataGrid = new System.Windows.Forms.DataGridView();
            this.employeeTabPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.editEmployeeButton = new System.Windows.Forms.Button();
            this.fireEmployeeButton = new System.Windows.Forms.Button();
            this.addEmployeeButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.employeeDataGrid = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.cellPrisonerVisitTabPage.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cellDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.visitDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.prisonerDataGrid)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.visitorTabPage.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.visitorDataGrid)).BeginInit();
            this.employeeTabPage.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.employeeDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.cellPrisonerVisitTabPage);
            this.tabControl1.Controls.Add(this.visitorTabPage);
            this.tabControl1.Controls.Add(this.employeeTabPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1584, 881);
            this.tabControl1.TabIndex = 0;
            // 
            // cellPrisonerVisitTabPage
            // 
            this.cellPrisonerVisitTabPage.Controls.Add(this.tableLayoutPanel1);
            this.cellPrisonerVisitTabPage.Location = new System.Drawing.Point(4, 22);
            this.cellPrisonerVisitTabPage.Name = "cellPrisonerVisitTabPage";
            this.cellPrisonerVisitTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.cellPrisonerVisitTabPage.Size = new System.Drawing.Size(1576, 855);
            this.cellPrisonerVisitTabPage.TabIndex = 0;
            this.cellPrisonerVisitTabPage.Text = "Správa cel, vězňů a návštěv";
            this.cellPrisonerVisitTabPage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cellDataGrid, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.visitDataGrid, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.prisonerDataGrid, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(8);
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1570, 849);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.editCellButton, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.addCellButton, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(11, 71);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(227, 34);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // editCellButton
            // 
            this.editCellButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editCellButton.Enabled = false;
            this.editCellButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.editCellButton.Location = new System.Drawing.Point(117, 3);
            this.editCellButton.Name = "editCellButton";
            this.editCellButton.Size = new System.Drawing.Size(108, 28);
            this.editCellButton.TabIndex = 1;
            this.editCellButton.Text = "Upravit celu";
            this.editCellButton.UseVisualStyleBackColor = true;
            this.editCellButton.Click += new System.EventHandler(this.editCellButton_Click);
            // 
            // addCellButton
            // 
            this.addCellButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addCellButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.addCellButton.Location = new System.Drawing.Point(3, 3);
            this.addCellButton.Name = "addCellButton";
            this.addCellButton.Size = new System.Drawing.Size(108, 28);
            this.addCellButton.TabIndex = 0;
            this.addCellButton.Text = "Přidat celu";
            this.addCellButton.UseVisualStyleBackColor = true;
            this.addCellButton.Click += new System.EventHandler(this.addCellButton_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.releasePrisonerButton, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.editPrisonerButton, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.addPrisonerButton, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(244, 71);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(693, 34);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // releasePrisonerButton
            // 
            this.releasePrisonerButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.releasePrisonerButton.Enabled = false;
            this.releasePrisonerButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.releasePrisonerButton.Location = new System.Drawing.Point(231, 3);
            this.releasePrisonerButton.Name = "releasePrisonerButton";
            this.releasePrisonerButton.Size = new System.Drawing.Size(108, 28);
            this.releasePrisonerButton.TabIndex = 2;
            this.releasePrisonerButton.Text = "Propustit vězně";
            this.releasePrisonerButton.UseVisualStyleBackColor = true;
            this.releasePrisonerButton.Click += new System.EventHandler(this.releasePrisonerButton_Click);
            // 
            // editPrisonerButton
            // 
            this.editPrisonerButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editPrisonerButton.Enabled = false;
            this.editPrisonerButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.editPrisonerButton.Location = new System.Drawing.Point(117, 3);
            this.editPrisonerButton.Name = "editPrisonerButton";
            this.editPrisonerButton.Size = new System.Drawing.Size(108, 28);
            this.editPrisonerButton.TabIndex = 1;
            this.editPrisonerButton.Text = "Upravit vězně";
            this.editPrisonerButton.UseVisualStyleBackColor = true;
            this.editPrisonerButton.Click += new System.EventHandler(this.editPrisonerButton_Click);
            // 
            // addPrisonerButton
            // 
            this.addPrisonerButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addPrisonerButton.Enabled = false;
            this.addPrisonerButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.addPrisonerButton.Location = new System.Drawing.Point(3, 3);
            this.addPrisonerButton.Name = "addPrisonerButton";
            this.addPrisonerButton.Size = new System.Drawing.Size(108, 28);
            this.addPrisonerButton.TabIndex = 0;
            this.addPrisonerButton.Text = "Přidat vězně";
            this.addPrisonerButton.UseVisualStyleBackColor = true;
            this.addPrisonerButton.Click += new System.EventHandler(this.addPrisonerButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 3);
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1540, 46);
            this.label1.TabIndex = 5;
            this.label1.Text = "Správa cel, vězňů a návštěv";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cellDataGrid
            // 
            this.cellDataGrid.AllowUserToAddRows = false;
            this.cellDataGrid.AllowUserToDeleteRows = false;
            this.cellDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cellDataGrid.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.cellDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.cellDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.cellDataGrid.Location = new System.Drawing.Point(15, 115);
            this.cellDataGrid.Margin = new System.Windows.Forms.Padding(7);
            this.cellDataGrid.MultiSelect = false;
            this.cellDataGrid.Name = "cellDataGrid";
            this.cellDataGrid.ReadOnly = true;
            this.cellDataGrid.RowHeadersVisible = false;
            this.cellDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.cellDataGrid.Size = new System.Drawing.Size(219, 719);
            this.cellDataGrid.TabIndex = 6;
            this.cellDataGrid.SelectionChanged += new System.EventHandler(this.cellDataGrid_SelectionChanged);
            // 
            // visitDataGrid
            // 
            this.visitDataGrid.AllowUserToAddRows = false;
            this.visitDataGrid.AllowUserToDeleteRows = false;
            this.visitDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.visitDataGrid.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.visitDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.visitDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.visitDataGrid.Location = new System.Drawing.Point(947, 115);
            this.visitDataGrid.Margin = new System.Windows.Forms.Padding(7);
            this.visitDataGrid.MultiSelect = false;
            this.visitDataGrid.Name = "visitDataGrid";
            this.visitDataGrid.ReadOnly = true;
            this.visitDataGrid.RowHeadersVisible = false;
            this.visitDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.visitDataGrid.Size = new System.Drawing.Size(608, 719);
            this.visitDataGrid.TabIndex = 8;
            this.visitDataGrid.SelectionChanged += new System.EventHandler(this.visitDataGrid_SelectionChanged);
            // 
            // prisonerDataGrid
            // 
            this.prisonerDataGrid.AllowUserToAddRows = false;
            this.prisonerDataGrid.AllowUserToDeleteRows = false;
            this.prisonerDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prisonerDataGrid.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.prisonerDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.prisonerDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.prisonerDataGrid.Location = new System.Drawing.Point(248, 115);
            this.prisonerDataGrid.Margin = new System.Windows.Forms.Padding(7);
            this.prisonerDataGrid.MultiSelect = false;
            this.prisonerDataGrid.Name = "prisonerDataGrid";
            this.prisonerDataGrid.ReadOnly = true;
            this.prisonerDataGrid.RowHeadersVisible = false;
            this.prisonerDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.prisonerDataGrid.Size = new System.Drawing.Size(685, 719);
            this.prisonerDataGrid.TabIndex = 7;
            this.prisonerDataGrid.SelectionChanged += new System.EventHandler(this.prisonerDataGrid_SelectionChanged);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.ColumnCount = 4;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this.editVisitButton, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.addVisitButton, 0, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(943, 71);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(616, 34);
            this.tableLayoutPanel4.TabIndex = 9;
            // 
            // editVisitButton
            // 
            this.editVisitButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editVisitButton.Enabled = false;
            this.editVisitButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.editVisitButton.Location = new System.Drawing.Point(117, 3);
            this.editVisitButton.Name = "editVisitButton";
            this.editVisitButton.Size = new System.Drawing.Size(108, 28);
            this.editVisitButton.TabIndex = 2;
            this.editVisitButton.Text = "Upravit návštěvu";
            this.editVisitButton.UseVisualStyleBackColor = true;
            this.editVisitButton.Click += new System.EventHandler(this.editVisitButton_Click);
            // 
            // addVisitButton
            // 
            this.addVisitButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addVisitButton.Enabled = false;
            this.addVisitButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.addVisitButton.Location = new System.Drawing.Point(3, 3);
            this.addVisitButton.Name = "addVisitButton";
            this.addVisitButton.Size = new System.Drawing.Size(108, 28);
            this.addVisitButton.TabIndex = 1;
            this.addVisitButton.Text = "Přidat návštěvu";
            this.addVisitButton.UseVisualStyleBackColor = true;
            this.addVisitButton.Click += new System.EventHandler(this.addVisitButton_Click);
            // 
            // visitorTabPage
            // 
            this.visitorTabPage.Controls.Add(this.tableLayoutPanel5);
            this.visitorTabPage.Location = new System.Drawing.Point(4, 22);
            this.visitorTabPage.Name = "visitorTabPage";
            this.visitorTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.visitorTabPage.Size = new System.Drawing.Size(1576, 855);
            this.visitorTabPage.TabIndex = 1;
            this.visitorTabPage.Text = "Správa návštěvníků";
            this.visitorTabPage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.AutoSize = true;
            this.tableLayoutPanel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel6, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.visitorDataGrid, 0, 2);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.Padding = new System.Windows.Forms.Padding(8);
            this.tableLayoutPanel5.RowCount = 3;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1570, 849);
            this.tableLayoutPanel5.TabIndex = 2;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel6.AutoSize = true;
            this.tableLayoutPanel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel6.ColumnCount = 4;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.Controls.Add(this.editVisitorButton, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.forbidVisitorButton, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.addVisitorButton, 0, 0);
            this.tableLayoutPanel6.Location = new System.Drawing.Point(11, 71);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(1548, 34);
            this.tableLayoutPanel6.TabIndex = 1;
            // 
            // editVisitorButton
            // 
            this.editVisitorButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editVisitorButton.Enabled = false;
            this.editVisitorButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.editVisitorButton.Location = new System.Drawing.Point(129, 3);
            this.editVisitorButton.Name = "editVisitorButton";
            this.editVisitorButton.Size = new System.Drawing.Size(120, 28);
            this.editVisitorButton.TabIndex = 3;
            this.editVisitorButton.Text = "Upravit návštěvníka";
            this.editVisitorButton.UseVisualStyleBackColor = true;
            this.editVisitorButton.Click += new System.EventHandler(this.editVisitorButton_Click);
            // 
            // forbidVisitorButton
            // 
            this.forbidVisitorButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.forbidVisitorButton.Enabled = false;
            this.forbidVisitorButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.forbidVisitorButton.Location = new System.Drawing.Point(255, 3);
            this.forbidVisitorButton.Name = "forbidVisitorButton";
            this.forbidVisitorButton.Size = new System.Drawing.Size(120, 28);
            this.forbidVisitorButton.TabIndex = 1;
            this.forbidVisitorButton.Text = "Zakázat návštěvníka";
            this.forbidVisitorButton.UseVisualStyleBackColor = true;
            this.forbidVisitorButton.Click += new System.EventHandler(this.forbidVisitorButton_Click);
            // 
            // addVisitorButton
            // 
            this.addVisitorButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addVisitorButton.Enabled = false;
            this.addVisitorButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.addVisitorButton.Location = new System.Drawing.Point(3, 3);
            this.addVisitorButton.Name = "addVisitorButton";
            this.addVisitorButton.Size = new System.Drawing.Size(120, 28);
            this.addVisitorButton.TabIndex = 0;
            this.addVisitorButton.Text = "Přidat návštěvníka";
            this.addVisitorButton.UseVisualStyleBackColor = true;
            this.addVisitorButton.Click += new System.EventHandler(this.addVisitorButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.label2.Location = new System.Drawing.Point(15, 15);
            this.label2.Margin = new System.Windows.Forms.Padding(7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1540, 46);
            this.label2.TabIndex = 5;
            this.label2.Text = "Správa návštěvníků";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // visitorDataGrid
            // 
            this.visitorDataGrid.AllowUserToAddRows = false;
            this.visitorDataGrid.AllowUserToDeleteRows = false;
            this.visitorDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.visitorDataGrid.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.visitorDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.visitorDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.visitorDataGrid.Location = new System.Drawing.Point(15, 115);
            this.visitorDataGrid.Margin = new System.Windows.Forms.Padding(7);
            this.visitorDataGrid.MultiSelect = false;
            this.visitorDataGrid.Name = "visitorDataGrid";
            this.visitorDataGrid.ReadOnly = true;
            this.visitorDataGrid.RowHeadersVisible = false;
            this.visitorDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.visitorDataGrid.Size = new System.Drawing.Size(1540, 719);
            this.visitorDataGrid.TabIndex = 6;
            this.visitorDataGrid.SelectionChanged += new System.EventHandler(this.visitorDataGrid_SelectionChanged);
            // 
            // employeeTabPage
            // 
            this.employeeTabPage.Controls.Add(this.tableLayoutPanel7);
            this.employeeTabPage.Location = new System.Drawing.Point(4, 22);
            this.employeeTabPage.Name = "employeeTabPage";
            this.employeeTabPage.Size = new System.Drawing.Size(1576, 855);
            this.employeeTabPage.TabIndex = 2;
            this.employeeTabPage.Text = "Správa zaměstnanců";
            this.employeeTabPage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.AutoSize = true;
            this.tableLayoutPanel7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel8, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.employeeDataGrid, 0, 2);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.Padding = new System.Windows.Forms.Padding(8);
            this.tableLayoutPanel7.RowCount = 3;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(1576, 855);
            this.tableLayoutPanel7.TabIndex = 3;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel8.AutoSize = true;
            this.tableLayoutPanel8.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel8.ColumnCount = 4;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel8.Controls.Add(this.editEmployeeButton, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.fireEmployeeButton, 1, 0);
            this.tableLayoutPanel8.Controls.Add(this.addEmployeeButton, 0, 0);
            this.tableLayoutPanel8.Location = new System.Drawing.Point(11, 71);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(1554, 34);
            this.tableLayoutPanel8.TabIndex = 1;
            // 
            // editEmployeeButton
            // 
            this.editEmployeeButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editEmployeeButton.Enabled = false;
            this.editEmployeeButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.editEmployeeButton.Location = new System.Drawing.Point(149, 3);
            this.editEmployeeButton.Name = "editEmployeeButton";
            this.editEmployeeButton.Size = new System.Drawing.Size(140, 28);
            this.editEmployeeButton.TabIndex = 3;
            this.editEmployeeButton.Text = "Upravit zaměstnance";
            this.editEmployeeButton.UseVisualStyleBackColor = true;
            this.editEmployeeButton.Click += new System.EventHandler(this.editEmployeeButton_Click);
            // 
            // fireEmployeeButton
            // 
            this.fireEmployeeButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fireEmployeeButton.Enabled = false;
            this.fireEmployeeButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.fireEmployeeButton.Location = new System.Drawing.Point(295, 3);
            this.fireEmployeeButton.Name = "fireEmployeeButton";
            this.fireEmployeeButton.Size = new System.Drawing.Size(140, 28);
            this.fireEmployeeButton.TabIndex = 1;
            this.fireEmployeeButton.Text = "Propustit zaměstnance";
            this.fireEmployeeButton.UseVisualStyleBackColor = true;
            this.fireEmployeeButton.Click += new System.EventHandler(this.fireEmployeeButton_Click);
            // 
            // addEmployeeButton
            // 
            this.addEmployeeButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addEmployeeButton.Enabled = false;
            this.addEmployeeButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.addEmployeeButton.Location = new System.Drawing.Point(3, 3);
            this.addEmployeeButton.Name = "addEmployeeButton";
            this.addEmployeeButton.Size = new System.Drawing.Size(140, 28);
            this.addEmployeeButton.TabIndex = 0;
            this.addEmployeeButton.Text = "Přidat zaměstnance";
            this.addEmployeeButton.UseVisualStyleBackColor = true;
            this.addEmployeeButton.Click += new System.EventHandler(this.addEmployeeButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.label3.Location = new System.Drawing.Point(15, 15);
            this.label3.Margin = new System.Windows.Forms.Padding(7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1546, 46);
            this.label3.TabIndex = 5;
            this.label3.Text = "Správa zaměstnanců";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // employeeDataGrid
            // 
            this.employeeDataGrid.AllowUserToAddRows = false;
            this.employeeDataGrid.AllowUserToDeleteRows = false;
            this.employeeDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.employeeDataGrid.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.employeeDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.employeeDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.employeeDataGrid.Location = new System.Drawing.Point(15, 115);
            this.employeeDataGrid.Margin = new System.Windows.Forms.Padding(7);
            this.employeeDataGrid.MultiSelect = false;
            this.employeeDataGrid.Name = "employeeDataGrid";
            this.employeeDataGrid.ReadOnly = true;
            this.employeeDataGrid.RowHeadersVisible = false;
            this.employeeDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.employeeDataGrid.Size = new System.Drawing.Size(1546, 725);
            this.employeeDataGrid.TabIndex = 6;
            this.employeeDataGrid.SelectionChanged += new System.EventHandler(this.employeeDataGrid_SelectionChanged);
            // 
            // MenuWinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1584, 881);
            this.Controls.Add(this.tabControl1);
            this.MinimumSize = new System.Drawing.Size(1600, 39);
            this.Name = "MenuWinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vězeňský informační systém";
            this.tabControl1.ResumeLayout(false);
            this.cellPrisonerVisitTabPage.ResumeLayout(false);
            this.cellPrisonerVisitTabPage.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cellDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.visitDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.prisonerDataGrid)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.visitorTabPage.ResumeLayout(false);
            this.visitorTabPage.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.visitorDataGrid)).EndInit();
            this.employeeTabPage.ResumeLayout(false);
            this.employeeTabPage.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.employeeDataGrid)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage cellPrisonerVisitTabPage;
        private System.Windows.Forms.TabPage visitorTabPage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button editCellButton;
        private System.Windows.Forms.Button addCellButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button releasePrisonerButton;
        private System.Windows.Forms.Button editPrisonerButton;
        private System.Windows.Forms.Button addPrisonerButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView cellDataGrid;
        private System.Windows.Forms.DataGridView visitDataGrid;
        private System.Windows.Forms.DataGridView prisonerDataGrid;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TabPage employeeTabPage;
        private System.Windows.Forms.Button editVisitButton;
        private System.Windows.Forms.Button addVisitButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Button forbidVisitorButton;
        private System.Windows.Forms.Button addVisitorButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView visitorDataGrid;
        private System.Windows.Forms.Button editVisitorButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.Button editEmployeeButton;
        private System.Windows.Forms.Button fireEmployeeButton;
        private System.Windows.Forms.Button addEmployeeButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView employeeDataGrid;
    }
}