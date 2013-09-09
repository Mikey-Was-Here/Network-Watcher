namespace NetworkWatcher
{
    partial class frmMain
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
            this.lvMain = new System.Windows.Forms.ListView();
            this.colRemote = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLocal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colProcess = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCountry = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lvMain
            // 
            this.lvMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colRemote,
            this.colLocal,
            this.colPid,
            this.colProcess,
            this.colCountry});
            this.lvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvMain.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lvMain.Location = new System.Drawing.Point(0, 0);
            this.lvMain.MultiSelect = false;
            this.lvMain.Name = "lvMain";
            this.lvMain.Size = new System.Drawing.Size(426, 361);
            this.lvMain.TabIndex = 0;
            this.lvMain.UseCompatibleStateImageBehavior = false;
            this.lvMain.View = System.Windows.Forms.View.Details;
            this.lvMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvMain_KeyDown);
            // 
            // colRemote
            // 
            this.colRemote.Text = "Remote";
            // 
            // colLocal
            // 
            this.colLocal.Text = "Local";
            // 
            // colPid
            // 
            this.colPid.Text = "PID";
            // 
            // colProcess
            // 
            this.colProcess.Text = "Process";
            // 
            // colCountry
            // 
            this.colCountry.Text = "Country";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 361);
            this.Controls.Add(this.lvMain);
            this.Name = "frmMain";
            this.Text = "Network Watcher";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmMain_KeyPress);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvMain;
        private System.Windows.Forms.ColumnHeader colRemote;
        private System.Windows.Forms.ColumnHeader colLocal;
        private System.Windows.Forms.ColumnHeader colProcess;
        private System.Windows.Forms.ColumnHeader colPid;
        private System.Windows.Forms.ColumnHeader colCountry;
    }
}

