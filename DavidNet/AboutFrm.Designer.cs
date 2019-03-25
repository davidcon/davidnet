namespace DavidNet
{
    partial class AboutFrm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutFrm));
            this.LbVersiones = new System.Windows.Forms.Label();
            this.TbVersiones = new System.Windows.Forms.TextBox();
            this.BtCerrar = new System.Windows.Forms.Button();
            this.GbContacto = new System.Windows.Forms.GroupBox();
            this.BtLinkedIn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.BtGitHub = new System.Windows.Forms.Button();
            this.BtEmail = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.GbContacto.SuspendLayout();
            this.SuspendLayout();
            // 
            // LbVersiones
            // 
            this.LbVersiones.AutoSize = true;
            this.LbVersiones.Location = new System.Drawing.Point(6, 6);
            this.LbVersiones.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LbVersiones.Name = "LbVersiones";
            this.LbVersiones.Size = new System.Drawing.Size(164, 20);
            this.LbVersiones.TabIndex = 0;
            this.LbVersiones.Text = "Histórico de versiones";
            // 
            // TbVersiones
            // 
            this.TbVersiones.Location = new System.Drawing.Point(6, 31);
            this.TbVersiones.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TbVersiones.Multiline = true;
            this.TbVersiones.Name = "TbVersiones";
            this.TbVersiones.ReadOnly = true;
            this.TbVersiones.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TbVersiones.Size = new System.Drawing.Size(409, 133);
            this.TbVersiones.TabIndex = 1;
            this.TbVersiones.Text = "· v1";
            // 
            // BtCerrar
            // 
            this.BtCerrar.Location = new System.Drawing.Point(147, 284);
            this.BtCerrar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtCerrar.Name = "BtCerrar";
            this.BtCerrar.Size = new System.Drawing.Size(112, 35);
            this.BtCerrar.TabIndex = 2;
            this.BtCerrar.Text = "Cerrar";
            this.BtCerrar.UseVisualStyleBackColor = true;
            this.BtCerrar.Click += new System.EventHandler(this.BtCerrar_Click);
            // 
            // GbContacto
            // 
            this.GbContacto.Controls.Add(this.BtLinkedIn);
            this.GbContacto.Controls.Add(this.label2);
            this.GbContacto.Controls.Add(this.BtGitHub);
            this.GbContacto.Controls.Add(this.BtEmail);
            this.GbContacto.Location = new System.Drawing.Point(6, 172);
            this.GbContacto.Name = "GbContacto";
            this.GbContacto.Size = new System.Drawing.Size(409, 104);
            this.GbContacto.TabIndex = 4;
            this.GbContacto.TabStop = false;
            this.GbContacto.Text = "Contacto";
            // 
            // BtLinkedIn
            // 
            this.BtLinkedIn.Image = global::DavidNet.Properties.Resources.iconfinder_linkedin;
            this.BtLinkedIn.Location = new System.Drawing.Point(182, 25);
            this.BtLinkedIn.Name = "BtLinkedIn";
            this.BtLinkedIn.Size = new System.Drawing.Size(64, 64);
            this.BtLinkedIn.TabIndex = 6;
            this.ToolTip.SetToolTip(this.BtLinkedIn, "http://www.linkedin.com/in/condeacereda/");
            this.BtLinkedIn.UseVisualStyleBackColor = true;
            this.BtLinkedIn.Click += new System.EventHandler(this.BtLinkedIn_Click_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "David Conde";
            // 
            // BtGitHub
            // 
            this.BtGitHub.Image = global::DavidNet.Properties.Resources.iconfinder_github;
            this.BtGitHub.Location = new System.Drawing.Point(333, 25);
            this.BtGitHub.Name = "BtGitHub";
            this.BtGitHub.Size = new System.Drawing.Size(64, 64);
            this.BtGitHub.TabIndex = 1;
            this.ToolTip.SetToolTip(this.BtGitHub, "http://github.com/davidcon/davidnet");
            this.BtGitHub.UseVisualStyleBackColor = true;
            // 
            // BtEmail
            // 
            this.BtEmail.Image = global::DavidNet.Properties.Resources.iconfinder_mail;
            this.BtEmail.Location = new System.Drawing.Point(257, 25);
            this.BtEmail.Name = "BtEmail";
            this.BtEmail.Size = new System.Drawing.Size(64, 64);
            this.BtEmail.TabIndex = 0;
            this.BtEmail.UseVisualStyleBackColor = true;
            this.BtEmail.Click += new System.EventHandler(this.BtEmail_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "iconfinder_linkedin.png");
            this.imageList1.Images.SetKeyName(1, "iconfinder_mail.png");
            this.imageList1.Images.SetKeyName(2, "iconfinder_github.png");
            // 
            // AboutFrm
            // 
            this.AcceptButton = this.BtCerrar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 333);
            this.Controls.Add(this.GbContacto);
            this.Controls.Add(this.BtCerrar);
            this.Controls.Add(this.TbVersiones);
            this.Controls.Add(this.LbVersiones);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Acerca de DavidNet";
            this.Load += new System.EventHandler(this.AboutFrm_Load);
            this.GbContacto.ResumeLayout(false);
            this.GbContacto.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LbVersiones;
        private System.Windows.Forms.TextBox TbVersiones;
        private System.Windows.Forms.Button BtCerrar;
        private System.Windows.Forms.GroupBox GbContacto;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BtGitHub;
        private System.Windows.Forms.Button BtLinkedIn;
        private System.Windows.Forms.Button BtEmail;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolTip ToolTip;
    }
}