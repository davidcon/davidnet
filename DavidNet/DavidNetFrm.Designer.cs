namespace DavidNet
{
    partial class DavidNetFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DavidNetFrm));
            this.LbTarjetaRed = new System.Windows.Forms.Label();
            this.CmbTarjetaRed = new System.Windows.Forms.ComboBox();
            this.BtIniciar = new System.Windows.Forms.Button();
            this.BtParar = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TxEstado = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxPort = new System.Windows.Forms.TextBox();
            this.TxIP = new System.Windows.Forms.TextBox();
            this.TxPlatform = new System.Windows.Forms.TextBox();
            this.TxDeviceID = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.resultLabel = new System.Windows.Forms.Label();
            this.resultLinkLabel = new System.Windows.Forms.LinkLabel();
            this.BtLoad = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LbTarjetaRed
            // 
            this.LbTarjetaRed.AutoSize = true;
            this.LbTarjetaRed.Location = new System.Drawing.Point(3, 4);
            this.LbTarjetaRed.Name = "LbTarjetaRed";
            this.LbTarjetaRed.Size = new System.Drawing.Size(78, 13);
            this.LbTarjetaRed.TabIndex = 0;
            this.LbTarjetaRed.Text = "Tarjeta de Red";
            // 
            // CmbTarjetaRed
            // 
            this.CmbTarjetaRed.FormattingEnabled = true;
            this.CmbTarjetaRed.Location = new System.Drawing.Point(6, 20);
            this.CmbTarjetaRed.Name = "CmbTarjetaRed";
            this.CmbTarjetaRed.Size = new System.Drawing.Size(290, 21);
            this.CmbTarjetaRed.TabIndex = 1;
            // 
            // BtIniciar
            // 
            this.BtIniciar.Location = new System.Drawing.Point(32, 222);
            this.BtIniciar.Name = "BtIniciar";
            this.BtIniciar.Size = new System.Drawing.Size(75, 22);
            this.BtIniciar.TabIndex = 2;
            this.BtIniciar.Text = "Iniciar";
            this.BtIniciar.UseVisualStyleBackColor = true;
            this.BtIniciar.Click += new System.EventHandler(this.BtIniciar_Click);
            // 
            // BtParar
            // 
            this.BtParar.Location = new System.Drawing.Point(113, 222);
            this.BtParar.Name = "BtParar";
            this.BtParar.Size = new System.Drawing.Size(75, 22);
            this.BtParar.TabIndex = 3;
            this.BtParar.Text = "Parar";
            this.BtParar.UseVisualStyleBackColor = true;
            this.BtParar.Click += new System.EventHandler(this.BtParar_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(6, 193);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(290, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TxEstado);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.TxPort);
            this.groupBox1.Controls.Add(this.TxIP);
            this.groupBox1.Controls.Add(this.TxPlatform);
            this.groupBox1.Controls.Add(this.TxDeviceID);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 47);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(290, 141);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de Red";
            // 
            // TxEstado
            // 
            this.TxEstado.Location = new System.Drawing.Point(70, 17);
            this.TxEstado.Name = "TxEstado";
            this.TxEstado.ReadOnly = true;
            this.TxEstado.Size = new System.Drawing.Size(214, 20);
            this.TxEstado.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Estado:";
            // 
            // TxPort
            // 
            this.TxPort.Location = new System.Drawing.Point(70, 114);
            this.TxPort.Name = "TxPort";
            this.TxPort.ReadOnly = true;
            this.TxPort.Size = new System.Drawing.Size(214, 20);
            this.TxPort.TabIndex = 10;
            // 
            // TxIP
            // 
            this.TxIP.Location = new System.Drawing.Point(70, 90);
            this.TxIP.Name = "TxIP";
            this.TxIP.ReadOnly = true;
            this.TxIP.Size = new System.Drawing.Size(214, 20);
            this.TxIP.TabIndex = 9;
            // 
            // TxPlatform
            // 
            this.TxPlatform.Location = new System.Drawing.Point(70, 66);
            this.TxPlatform.Name = "TxPlatform";
            this.TxPlatform.ReadOnly = true;
            this.TxPlatform.Size = new System.Drawing.Size(214, 20);
            this.TxPlatform.TabIndex = 8;
            // 
            // TxDeviceID
            // 
            this.TxDeviceID.Location = new System.Drawing.Point(70, 42);
            this.TxDeviceID.Name = "TxDeviceID";
            this.TxDeviceID.ReadOnly = true;
            this.TxDeviceID.Size = new System.Drawing.Size(214, 20);
            this.TxDeviceID.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 117);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Port:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "IP Switch:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Platform:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Device ID:";
            // 
            // resultLabel
            // 
            this.resultLabel.AutoSize = true;
            this.resultLabel.Location = new System.Drawing.Point(124, 4);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(0, 13);
            this.resultLabel.TabIndex = 6;
            // 
            // resultLinkLabel
            // 
            this.resultLinkLabel.AutoSize = true;
            this.resultLinkLabel.Location = new System.Drawing.Point(230, 4);
            this.resultLinkLabel.Name = "resultLinkLabel";
            this.resultLinkLabel.Size = new System.Drawing.Size(0, 13);
            this.resultLinkLabel.TabIndex = 7;
            this.resultLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.resultLinkLabel_LinkClicked);
            // 
            // BtLoad
            // 
            this.BtLoad.Location = new System.Drawing.Point(194, 221);
            this.BtLoad.Name = "BtLoad";
            this.BtLoad.Size = new System.Drawing.Size(75, 23);
            this.BtLoad.TabIndex = 8;
            this.BtLoad.Text = "Load File";
            this.BtLoad.UseVisualStyleBackColor = true;
            this.BtLoad.Click += new System.EventHandler(this.BtLoad_Click);
            // 
            // DavidNetFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 250);
            this.Controls.Add(this.BtLoad);
            this.Controls.Add(this.resultLinkLabel);
            this.Controls.Add(this.resultLabel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.BtParar);
            this.Controls.Add(this.BtIniciar);
            this.Controls.Add(this.CmbTarjetaRed);
            this.Controls.Add(this.LbTarjetaRed);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DavidNetFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "David - NetTool";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DavidNetFrm_FormClosed);
            this.Load += new System.EventHandler(this.DavidNetFrm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LbTarjetaRed;
        private System.Windows.Forms.ComboBox CmbTarjetaRed;
        private System.Windows.Forms.Button BtIniciar;
        private System.Windows.Forms.Button BtParar;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxEstado;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxPort;
        private System.Windows.Forms.TextBox TxIP;
        private System.Windows.Forms.TextBox TxPlatform;
        private System.Windows.Forms.TextBox TxDeviceID;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label resultLabel;
        private System.Windows.Forms.LinkLabel resultLinkLabel;
        private System.Windows.Forms.Button BtLoad;
    }
}