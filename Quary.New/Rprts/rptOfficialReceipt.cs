using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Helpers;

/// <summary>
/// Summary description for rptOfficialReceipt
/// </summary>
public class rptOfficialReceipt : DevExpress.XtraReports.UI.XtraReport
{
    private TopMarginBand TopMargin;
    private BottomMarginBand BottomMargin;
    private DetailBand Detail;
    private XRLabel xrLabel2;
    private XRLabel xrLabel1;
    private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell3;
    private XRLabel xrLabel3;
    private XRLabel xrLabel4;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell1;
    private XRTable xrTable3;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell2;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public rptOfficialReceipt()
    {
        InitializeComponent();

        //
        // TODO: Add constructor logic here
        //
        xrLabel4.BeforePrint += xrLabel4_BeforePrint;
    }



    private void xrLabel4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        XRLabel label = (XRLabel)sender;
        if (string.IsNullOrEmpty(label.Text))
            return;
        label.Text = Convert.ToDecimal(label.Text).AmountInWords();
    }

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

    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.objectDataSource1 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // TopMargin
        // 
        this.TopMargin.HeightF = 10F;
        this.TopMargin.Name = "TopMargin";
        // 
        // BottomMargin
        // 
        this.BottomMargin.HeightF = 30F;
        this.BottomMargin.Name = "BottomMargin";
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3,
            this.xrTable2,
            this.xrLabel4,
            this.xrLabel3,
            this.xrTable1,
            this.xrLabel2,
            this.xrLabel1});
        this.Detail.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
        this.Detail.HeightF = 753F;
        this.Detail.Name = "Detail";
        this.Detail.StylePriority.UseFont = false;
        this.Detail.StylePriority.UseTextAlignment = false;
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
        // 
        // xrLabel3
        // 
        this.xrLabel3.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[LastModifiedBy]")});
        this.xrLabel3.Font = new System.Drawing.Font("Arial", 8.5F);
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(237.0834F, 692.9998F);
        this.xrLabel3.Multiline = true;
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(127.2917F, 23F);
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.StylePriority.UseTextAlignment = false;
        this.xrLabel3.Text = "xrLabel3";
        this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrTable1
        // 
        this.xrTable1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(275F, 372F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.SizeF = new System.Drawing.SizeF(100F, 25F);
        this.xrTable1.StylePriority.UseFont = false;
        this.xrTable1.StylePriority.UseTextAlignment = false;
        this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell3});
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 1D;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Amount]")});
        this.xrTableCell3.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell3.Multiline = true;
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.StylePriority.UseFont = false;
        this.xrTableCell3.Text = "xrTableCell3";
        this.xrTableCell3.TextFormatString = "{0:n2}";
        this.xrTableCell3.Weight = 1.00000048828125D;
        // 
        // xrLabel2
        // 
        this.xrLabel2.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Transactions].[Permitees].[FullName]")});
        this.xrLabel2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 249F);
        this.xrLabel2.Multiline = true;
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(375F, 23F);
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "xrLabel2";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel1
        // 
        this.xrLabel1.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[DateCreated]")});
        this.xrLabel1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(33F, 189F);
        this.xrLabel1.Multiline = true;
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(100F, 23F);
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "Date";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrLabel1.TextFormatString = "{0:MM-dd-yy}";
        // 
        // xrLabel4
        // 
        this.xrLabel4.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Amount]")});
        this.xrLabel4.Font = new System.Drawing.Font("Times New Roman", 7F, System.Drawing.FontStyle.Italic);
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 557F);
        this.xrLabel4.Multiline = true;
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel4.Scripts.OnBeforePrint = "xrLabel4_BeforePrint";
        this.xrLabel4.SizeF = new System.Drawing.SizeF(375F, 23F);
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.StylePriority.UseTextAlignment = false;
        this.xrLabel4.Text = "xrLabel4";
        this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // objectDataSource1
        // 
        this.objectDataSource1.DataSource = typeof(Models.Billings);
        this.objectDataSource1.Name = "objectDataSource1";
        // 
        // xrTable2
        // 
        this.xrTable2.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 372F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.xrTable2.SizeF = new System.Drawing.SizeF(242.9948F, 25.00003F);
        this.xrTable2.StylePriority.UseFont = false;
        this.xrTable2.StylePriority.UseTextAlignment = false;
        this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1});
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 1D;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Transactions].[TransactionTypes].[TransactionType]")});
        this.xrTableCell1.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell1.Multiline = true;
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.StylePriority.UseFont = false;
        this.xrTableCell1.Text = "SAND AND GRAVEL";
        this.xrTableCell1.TextFormatString = "{0:n2}";
        this.xrTableCell1.Weight = 1.00000048828125D;
        // 
        // xrTable3
        // 
        this.xrTable3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
        this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(275F, 506.1146F);
        this.xrTable3.Name = "xrTable3";
        this.xrTable3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
        this.xrTable3.SizeF = new System.Drawing.SizeF(100F, 25F);
        this.xrTable3.StylePriority.UseFont = false;
        this.xrTable3.StylePriority.UseTextAlignment = false;
        this.xrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell2});
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.Weight = 1D;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Amount]")});
        this.xrTableCell2.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell2.Multiline = true;
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.StylePriority.UseFont = false;
        this.xrTableCell2.Text = "xrTableCell3";
        this.xrTableCell2.TextFormatString = "{0:n2}";
        this.xrTableCell2.Weight = 1.00000048828125D;
        // 
        // rptOfficialReceipt
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail});
        this.BookmarkDuplicateSuppress = false;
        this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.objectDataSource1});
        this.DataSource = this.objectDataSource1;
        this.Font = new System.Drawing.Font("Arial", 9.75F);
        this.Margins = new System.Drawing.Printing.Margins(15, 20, 10, 30);
        this.PageWidth = 410;
        this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
        this.ScriptsSource = "\r\n";
        this.Version = "18.2";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
