using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DavidNet
{
    public partial class AboutFrm : Form
    {
        private string ElPaqueteString;
        public AboutFrm(string elPaqueteRaw)
        {
            InitializeComponent();
            this.ElPaqueteString = elPaqueteRaw;
        }

        private void BtCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AboutFrm_Load(object sender, EventArgs e)
        {
            this.BtLinkedIn.Image = this.imageList1.Images[0];
            this.BtEmail.Image = this.imageList1.Images[1];
            this.BtGitHub.Image = this.imageList1.Images[2];
        }

        private void BtLinkedIn_Click(object sender, EventArgs e)
        {
            string command = @"http://www.linkedin.com/in/condeacereda/";
            Process.Start(command);
        }

        private void BtGitHub_Click(object sender, EventArgs e)
        {
            string command = @"http://github.com/davidcon/davidnet";
            Process.Start(command);
        }

        private void BtEmail_Click(object sender, EventArgs e)
        {
            string command = @"mailto:conde.acereda@gmail.com?subject=[DavidNet] - Sugerencias";
            Process.Start(command);
        }

        private void BtLinkedIn_Click_1(object sender, EventArgs e)
        {

        }
    }
}
