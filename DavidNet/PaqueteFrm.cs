using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DavidNet
{
    public partial class PaqueteFrm : Form
    {
        private string ElPaqueteString;
        public PaqueteFrm(string elPaqueteRaw)
        {
            InitializeComponent();
            this.ElPaqueteString = elPaqueteRaw;
        }

        private void BtCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PaqueteFrm_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = this.ElPaqueteString.Replace("\\0", "_");
            
        }
    }
}
