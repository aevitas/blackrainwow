namespace BlackWeather
{
    partial class Main
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
            this.grp_Process = new System.Windows.Forms.GroupBox();
            this.lbl_AttachedTo = new System.Windows.Forms.Label();
            this.btn_SelectProcess = new System.Windows.Forms.Button();
            this.cmb_ProcessSelection = new System.Windows.Forms.ComboBox();
            this.grp_ObjectManager = new System.Windows.Forms.GroupBox();
            this.btn_Refresh = new System.Windows.Forms.Button();
            this.lst_Objects = new System.Windows.Forms.ListBox();
            this.lbl_PoweredBy = new System.Windows.Forms.Label();
            this.btn_Inject = new System.Windows.Forms.Button();
            this.grp_Process.SuspendLayout();
            this.grp_ObjectManager.SuspendLayout();
            this.SuspendLayout();
            // 
            // grp_Process
            // 
            this.grp_Process.Controls.Add(this.lbl_AttachedTo);
            this.grp_Process.Controls.Add(this.btn_SelectProcess);
            this.grp_Process.Controls.Add(this.cmb_ProcessSelection);
            this.grp_Process.Location = new System.Drawing.Point(12, 12);
            this.grp_Process.Name = "grp_Process";
            this.grp_Process.Size = new System.Drawing.Size(397, 69);
            this.grp_Process.TabIndex = 0;
            this.grp_Process.TabStop = false;
            this.grp_Process.Text = "Process";
            // 
            // lbl_AttachedTo
            // 
            this.lbl_AttachedTo.AutoSize = true;
            this.lbl_AttachedTo.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.lbl_AttachedTo.Location = new System.Drawing.Point(12, 45);
            this.lbl_AttachedTo.Name = "lbl_AttachedTo";
            this.lbl_AttachedTo.Size = new System.Drawing.Size(72, 13);
            this.lbl_AttachedTo.TabIndex = 2;
            this.lbl_AttachedTo.Text = "Not attached!";
            // 
            // btn_SelectProcess
            // 
            this.btn_SelectProcess.Location = new System.Drawing.Point(316, 16);
            this.btn_SelectProcess.Name = "btn_SelectProcess";
            this.btn_SelectProcess.Size = new System.Drawing.Size(75, 23);
            this.btn_SelectProcess.TabIndex = 1;
            this.btn_SelectProcess.Text = "Select";
            this.btn_SelectProcess.UseVisualStyleBackColor = true;
            this.btn_SelectProcess.Click += new System.EventHandler(this.btn_SelectProcess_Click);
            // 
            // cmb_ProcessSelection
            // 
            this.cmb_ProcessSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_ProcessSelection.FormattingEnabled = true;
            this.cmb_ProcessSelection.Location = new System.Drawing.Point(12, 18);
            this.cmb_ProcessSelection.Name = "cmb_ProcessSelection";
            this.cmb_ProcessSelection.Size = new System.Drawing.Size(299, 21);
            this.cmb_ProcessSelection.TabIndex = 0;
            // 
            // grp_ObjectManager
            // 
            this.grp_ObjectManager.Controls.Add(this.btn_Inject);
            this.grp_ObjectManager.Controls.Add(this.btn_Refresh);
            this.grp_ObjectManager.Controls.Add(this.lst_Objects);
            this.grp_ObjectManager.Location = new System.Drawing.Point(12, 87);
            this.grp_ObjectManager.Name = "grp_ObjectManager";
            this.grp_ObjectManager.Size = new System.Drawing.Size(397, 310);
            this.grp_ObjectManager.TabIndex = 1;
            this.grp_ObjectManager.TabStop = false;
            this.grp_ObjectManager.Text = "Object Manager";
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.Location = new System.Drawing.Point(15, 279);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(75, 23);
            this.btn_Refresh.TabIndex = 1;
            this.btn_Refresh.Text = "Refresh";
            this.btn_Refresh.UseVisualStyleBackColor = true;
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // lst_Objects
            // 
            this.lst_Objects.FormattingEnabled = true;
            this.lst_Objects.Location = new System.Drawing.Point(13, 22);
            this.lst_Objects.Name = "lst_Objects";
            this.lst_Objects.Size = new System.Drawing.Size(371, 251);
            this.lst_Objects.TabIndex = 0;
            // 
            // lbl_PoweredBy
            // 
            this.lbl_PoweredBy.AutoSize = true;
            this.lbl_PoweredBy.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.lbl_PoweredBy.Location = new System.Drawing.Point(294, 400);
            this.lbl_PoweredBy.Name = "lbl_PoweredBy";
            this.lbl_PoweredBy.Size = new System.Drawing.Size(115, 13);
            this.lbl_PoweredBy.TabIndex = 2;
            this.lbl_PoweredBy.Text = "Powered by BlackRain";
            // 
            // btn_Inject
            // 
            this.btn_Inject.Location = new System.Drawing.Point(96, 279);
            this.btn_Inject.Name = "btn_Inject";
            this.btn_Inject.Size = new System.Drawing.Size(75, 23);
            this.btn_Inject.TabIndex = 2;
            this.btn_Inject.Text = "Injection";
            this.btn_Inject.UseVisualStyleBackColor = true;
            this.btn_Inject.Click += new System.EventHandler(this.btn_Inject_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 419);
            this.Controls.Add(this.lbl_PoweredBy);
            this.Controls.Add(this.grp_ObjectManager);
            this.Controls.Add(this.grp_Process);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.ShowIcon = false;
            this.Text = "BlackWeather";
            this.Load += new System.EventHandler(this.Main_Load);
            this.grp_Process.ResumeLayout(false);
            this.grp_Process.PerformLayout();
            this.grp_ObjectManager.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grp_Process;
        private System.Windows.Forms.Label lbl_AttachedTo;
        private System.Windows.Forms.Button btn_SelectProcess;
        private System.Windows.Forms.ComboBox cmb_ProcessSelection;
        private System.Windows.Forms.GroupBox grp_ObjectManager;
        private System.Windows.Forms.Button btn_Refresh;
        private System.Windows.Forms.ListBox lst_Objects;
        private System.Windows.Forms.Label lbl_PoweredBy;
        private System.Windows.Forms.Button btn_Inject;
    }
}

