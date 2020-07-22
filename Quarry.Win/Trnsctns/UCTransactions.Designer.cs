namespace Quarry.Win.Trnsctns
{
    partial class UCTransactions
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCTransactions));
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.TransactionGridControl = new DevExpress.XtraGrid.GridControl();
            this.transactionsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.TransactionGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTransactionNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOfficialReceipt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPermittee = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtSearch = new DevExpress.XtraEditors.SearchControl();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtCompanyName = new DevExpress.XtraEditors.TextEdit();
            this.txttTotalAmount = new DevExpress.XtraEditors.TextEdit();
            this.txtTransactionType = new DevExpress.XtraEditors.TextEdit();
            this.txtPermitteeName = new DevExpress.XtraEditors.TextEdit();
            this.txtTransNo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtOR = new DevExpress.XtraEditors.TextEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtDate = new DevExpress.XtraEditors.DateEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnPreview = new DevExpress.XtraEditors.SimpleButton();
            this.btnPayment = new DevExpress.XtraEditors.SimpleButton();
            this.lblTransNo = new DevExpress.XtraEditors.LabelControl();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TransactionGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.transactionsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TransactionGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompanyName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txttTotalAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTransactionType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPermitteeName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTransNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOR.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkBlue;
            this.panel1.Controls.Add(this.labelControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1190, 46);
            this.panel1.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(3, 8);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(76, 32);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Billings";
            this.labelControl1.Click += new System.EventHandler(this.labelControl1_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.TransactionGridControl);
            this.panel2.Controls.Add(this.panelControl2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 46);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(489, 594);
            this.panel2.TabIndex = 1;
            // 
            // TransactionGridControl
            // 
            this.TransactionGridControl.DataSource = this.transactionsBindingSource;
            this.TransactionGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TransactionGridControl.Location = new System.Drawing.Point(0, 47);
            this.TransactionGridControl.MainView = this.TransactionGridView;
            this.TransactionGridControl.Name = "TransactionGridControl";
            this.TransactionGridControl.Size = new System.Drawing.Size(487, 545);
            this.TransactionGridControl.TabIndex = 1;
            this.TransactionGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.TransactionGridView});
            // 
            // transactionsBindingSource
            // 
            this.transactionsBindingSource.DataSource = typeof(Models.Transactions);
            // 
            // TransactionGridView
            // 
            this.TransactionGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTransactionNumber,
            this.colOfficialReceipt,
            this.colPermittee});
            this.TransactionGridView.GridControl = this.TransactionGridControl;
            this.TransactionGridView.Name = "TransactionGridView";
            this.TransactionGridView.OptionsBehavior.ReadOnly = true;
            this.TransactionGridView.OptionsView.EnableAppearanceEvenRow = true;
            this.TransactionGridView.OptionsView.ShowGroupPanel = false;
            this.TransactionGridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.TransactionGridView_FocusedRowChanged);
            // 
            // colTransactionNumber
            // 
            this.colTransactionNumber.FieldName = "TransactionNumber";
            this.colTransactionNumber.Name = "colTransactionNumber";
            this.colTransactionNumber.Visible = true;
            this.colTransactionNumber.VisibleIndex = 0;
            // 
            // colOfficialReceipt
            // 
            this.colOfficialReceipt.FieldName = "OfficialReceipt";
            this.colOfficialReceipt.Name = "colOfficialReceipt";
            this.colOfficialReceipt.Visible = true;
            this.colOfficialReceipt.VisibleIndex = 1;
            // 
            // colPermittee
            // 
            this.colPermittee.Caption = "Permittee";
            this.colPermittee.FieldName = "Permitees.CompanyName";
            this.colPermittee.Name = "colPermittee";
            this.colPermittee.Visible = true;
            this.colPermittee.VisibleIndex = 2;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.labelControl6);
            this.panelControl2.Controls.Add(this.txtSearch);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(487, 47);
            this.panelControl2.TabIndex = 9;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(11, 18);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(37, 13);
            this.labelControl6.TabIndex = 2;
            this.labelControl6.Text = "Search:";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(54, 9);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Properties.Appearance.Options.UseFont = true;
            this.txtSearch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.txtSearch.Size = new System.Drawing.Size(407, 28);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txtCompanyName);
            this.panel4.Controls.Add(this.txttTotalAmount);
            this.panel4.Controls.Add(this.txtTransactionType);
            this.panel4.Controls.Add(this.txtPermitteeName);
            this.panel4.Controls.Add(this.txtTransNo);
            this.panel4.Controls.Add(this.labelControl7);
            this.panel4.Controls.Add(this.txtOR);
            this.panel4.Controls.Add(this.labelControl10);
            this.panel4.Controls.Add(this.labelControl9);
            this.panel4.Controls.Add(this.labelControl5);
            this.panel4.Controls.Add(this.labelControl2);
            this.panel4.Controls.Add(this.labelControl4);
            this.panel4.Controls.Add(this.labelControl3);
            this.panel4.Controls.Add(this.txtDate);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(489, 93);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(701, 547);
            this.panel4.TabIndex = 2;
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Location = new System.Drawing.Point(136, 117);
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCompanyName.Properties.Appearance.Options.UseFont = true;
            this.txtCompanyName.Properties.ReadOnly = true;
            this.txtCompanyName.Properties.UseReadOnlyAppearance = false;
            this.txtCompanyName.Size = new System.Drawing.Size(498, 24);
            this.txtCompanyName.TabIndex = 7;
            // 
            // txttTotalAmount
            // 
            this.txttTotalAmount.Location = new System.Drawing.Point(136, 167);
            this.txttTotalAmount.Name = "txttTotalAmount";
            this.txttTotalAmount.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttTotalAmount.Properties.Appearance.Options.UseFont = true;
            this.txttTotalAmount.Properties.DisplayFormat.FormatString = "n2";
            this.txttTotalAmount.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txttTotalAmount.Properties.ReadOnly = true;
            this.txttTotalAmount.Properties.UseReadOnlyAppearance = false;
            this.txttTotalAmount.Size = new System.Drawing.Size(498, 24);
            this.txttTotalAmount.TabIndex = 7;
            // 
            // txtTransactionType
            // 
            this.txtTransactionType.Location = new System.Drawing.Point(136, 142);
            this.txtTransactionType.Name = "txtTransactionType";
            this.txtTransactionType.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTransactionType.Properties.Appearance.Options.UseFont = true;
            this.txtTransactionType.Properties.ReadOnly = true;
            this.txtTransactionType.Properties.UseReadOnlyAppearance = false;
            this.txtTransactionType.Size = new System.Drawing.Size(498, 24);
            this.txtTransactionType.TabIndex = 7;
            // 
            // txtPermitteeName
            // 
            this.txtPermitteeName.Location = new System.Drawing.Point(136, 92);
            this.txtPermitteeName.Name = "txtPermitteeName";
            this.txtPermitteeName.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPermitteeName.Properties.Appearance.Options.UseFont = true;
            this.txtPermitteeName.Properties.ReadOnly = true;
            this.txtPermitteeName.Properties.UseReadOnlyAppearance = false;
            this.txtPermitteeName.Size = new System.Drawing.Size(498, 24);
            this.txtPermitteeName.TabIndex = 7;
            // 
            // txtTransNo
            // 
            this.txtTransNo.Location = new System.Drawing.Point(136, 67);
            this.txtTransNo.Name = "txtTransNo";
            this.txtTransNo.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTransNo.Properties.Appearance.Options.UseFont = true;
            this.txtTransNo.Properties.ReadOnly = true;
            this.txtTransNo.Properties.UseReadOnlyAppearance = false;
            this.txtTransNo.Size = new System.Drawing.Size(498, 24);
            this.txtTransNo.TabIndex = 7;
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Location = new System.Drawing.Point(9, 170);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(78, 17);
            this.labelControl7.TabIndex = 5;
            this.labelControl7.Text = "Total Amount";
            // 
            // txtOR
            // 
            this.txtOR.Location = new System.Drawing.Point(136, 42);
            this.txtOR.Name = "txtOR";
            this.txtOR.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOR.Properties.Appearance.Options.UseFont = true;
            this.txtOR.Properties.ReadOnly = true;
            this.txtOR.Properties.UseReadOnlyAppearance = false;
            this.txtOR.Size = new System.Drawing.Size(498, 24);
            this.txtOR.TabIndex = 6;
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl10.Appearance.Options.UseFont = true;
            this.labelControl10.Location = new System.Drawing.Point(8, 145);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(99, 17);
            this.labelControl10.TabIndex = 5;
            this.labelControl10.Text = "Transaction Type";
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl9.Appearance.Options.UseFont = true;
            this.labelControl9.Location = new System.Drawing.Point(9, 120);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(94, 17);
            this.labelControl9.TabIndex = 5;
            this.labelControl9.Text = "Company Name";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(9, 95);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(94, 17);
            this.labelControl5.TabIndex = 5;
            this.labelControl5.Text = "Permittee Name";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(9, 20);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(61, 17);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "Filing Date";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(9, 70);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(119, 17);
            this.labelControl4.TabIndex = 5;
            this.labelControl4.Text = "Transaction Number";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(9, 45);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(87, 17);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "Official Receipt";
            // 
            // txtDate
            // 
            this.txtDate.EditValue = null;
            this.txtDate.Location = new System.Drawing.Point(136, 17);
            this.txtDate.Name = "txtDate";
            this.txtDate.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDate.Properties.Appearance.Options.UseFont = true;
            this.txtDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDate.Properties.DisplayFormat.FormatString = "";
            this.txtDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtDate.Properties.EditFormat.FormatString = "";
            this.txtDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtDate.Properties.Mask.EditMask = "";
            this.txtDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.txtDate.Properties.ReadOnly = true;
            this.txtDate.Properties.UseReadOnlyAppearance = false;
            this.txtDate.Size = new System.Drawing.Size(498, 24);
            this.txtDate.TabIndex = 6;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnPreview);
            this.panelControl1.Controls.Add(this.btnPayment);
            this.panelControl1.Controls.Add(this.lblTransNo);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(489, 46);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(701, 47);
            this.panelControl1.TabIndex = 8;
            // 
            // btnPreview
            // 
            this.btnPreview.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPreview.Appearance.Options.UseFont = true;
            this.btnPreview.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnPreview.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPreview.ImageOptions.Image")));
            this.btnPreview.Location = new System.Drawing.Point(113, 10);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(98, 31);
            this.btnPreview.TabIndex = 1;
            this.btnPreview.Text = "Preview";
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnPayment
            // 
            this.btnPayment.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPayment.Appearance.Options.UseFont = true;
            this.btnPayment.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnPayment.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPayment.ImageOptions.Image")));
            this.btnPayment.Location = new System.Drawing.Point(9, 10);
            this.btnPayment.Name = "btnPayment";
            this.btnPayment.Size = new System.Drawing.Size(98, 31);
            this.btnPayment.TabIndex = 1;
            this.btnPayment.Text = "Payment";
            this.btnPayment.Click += new System.EventHandler(this.btnPayment_Click);
            // 
            // lblTransNo
            // 
            this.lblTransNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTransNo.Appearance.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransNo.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lblTransNo.Appearance.Options.UseFont = true;
            this.lblTransNo.Appearance.Options.UseForeColor = true;
            this.lblTransNo.Appearance.Options.UseTextOptions = true;
            this.lblTransNo.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblTransNo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTransNo.Location = new System.Drawing.Point(217, 6);
            this.lblTransNo.Name = "lblTransNo";
            this.lblTransNo.Size = new System.Drawing.Size(482, 32);
            this.lblTransNo.TabIndex = 0;
            this.lblTransNo.Text = "2020-0000";
            // 
            // UCTransactions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "UCTransactions";
            this.Size = new System.Drawing.Size(1190, 640);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TransactionGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.transactionsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TransactionGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompanyName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txttTotalAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTransactionType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPermitteeName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTransNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOR.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraGrid.GridControl TransactionGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView TransactionGridView;
        private System.Windows.Forms.BindingSource transactionsBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colTransactionNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colOfficialReceipt;
        private DevExpress.XtraGrid.Columns.GridColumn colPermittee;
        private System.Windows.Forms.Panel panel4;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.SearchControl txtSearch;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtPermitteeName;
        private DevExpress.XtraEditors.TextEdit txtTransNo;
        private DevExpress.XtraEditors.TextEdit txtOR;
        private DevExpress.XtraEditors.TextEdit txtCompanyName;
        private DevExpress.XtraEditors.TextEdit txttTotalAmount;
        private DevExpress.XtraEditors.TextEdit txtTransactionType;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit txtDate;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl lblTransNo;
        private DevExpress.XtraEditors.SimpleButton btnPreview;
        private DevExpress.XtraEditors.SimpleButton btnPayment;
    }
}
