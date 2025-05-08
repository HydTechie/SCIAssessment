namespace ExcelAddIn2
{
    partial class CustomRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public CustomRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tab1 = this.Factory.CreateRibbonTab();
            this.Actions = this.Factory.CreateRibbonGroup();
            this.btnConvertAlpha = this.Factory.CreateRibbonButton();
            this.btnUndo = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.Actions.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.Actions);
            this.tab1.Label = "TabAddIns";
            this.tab1.Name = "tab1";
            // 
            // Actions
            // 
            this.Actions.Items.Add(this.btnConvertAlpha);
            this.Actions.Items.Add(this.btnUndo);
            this.Actions.Label = "Actions";
            this.Actions.Name = "Actions";
            // 
            // btnConvertAlpha
            // 
            this.btnConvertAlpha.Label = "Convert2Alpha";
            this.btnConvertAlpha.Name = "btnConvertAlpha";
            this.btnConvertAlpha.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnConvertAlpha_Click);
            // 
            // btnUndo
            // 
            this.btnUndo.Label = "Undo";
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnUndo_Click);
            // 
            // CustomRibbon
            // 
            this.Name = "CustomRibbon";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.CustomRibbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.Actions.ResumeLayout(false);
            this.Actions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup Actions;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnConvertAlpha;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnUndo;
    }

    partial class ThisRibbonCollection
    {
        internal CustomRibbon Ribbon1
        {
            get { return this.GetRibbon<CustomRibbon>(); }
        }
    }
}
