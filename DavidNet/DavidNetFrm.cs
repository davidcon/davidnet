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
    public partial class DavidNetFrm : Form
    {
        private SnifferCDP MiSniffer;
        private int indiceSelecionado;
        private string paqueteRaw;

        public DavidNetFrm()
        {
            this.InitializeComponent();
            this.InitializeBackgroundWorker();
        }

        private void DavidNetFrm_Load(object sender, EventArgs e)
        {
            //Se cargan las tarjetas de red en el combo
            try
            {
                this.MiSniffer = new SnifferCDP();
            }
            catch (System.IO.FileNotFoundException exc)
            {
                throw new Exception("WinPCap no está instalado.",exc.InnerException);
            }
            
            this.CmbTarjetaRed.Items.AddRange(this.MiSniffer.TarjetasRed);
            //Seleccionamos la INTEL
            int aux = 0;
            for (int i = 0; i < this.CmbTarjetaRed.Items.Count; i++)
            {
                if (this.CmbTarjetaRed.Items[i].ToString().Contains("Intel") == true) 
                {
                    aux = i;
                    break;
                }
            }
            this.CmbTarjetaRed.SelectedIndex = aux;
            //this.MiSniffer.PaqueteRecibido += MiSniffer_PaqueteRecibido;
        }

        private void MiSniffer_PaqueteRecibido()
        {
            
            //this.LiberarTarjetaRed();
        }

        private void BtIniciar_Click(object sender, EventArgs e)
        {
            this.indiceSelecionado = this.CmbTarjetaRed.SelectedIndex;
            if (this.backgroundWorker1.IsBusy != true)
            {
                // Start the asynchronous operation.
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void BtParar_Click(object sender, EventArgs e)
        {
            this.LiberarTarjetaRed();
        }

        private void DavidNetFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                this.LiberarTarjetaRed();
            }
            catch (Exception)
            {
                
            }
            
        }

        private void LiberarTarjetaRed()
        {
            if (backgroundWorker1.WorkerSupportsCancellation == true)
            {
                // Cancel the asynchronous operation.
                backgroundWorker1.CancelAsync();
            }
            //Cerrar comunicación
            this.MiSniffer.CancelarSniffer(this.CmbTarjetaRed.SelectedIndex);
        }


        private void InitializeBackgroundWorker()
        {
            backgroundWorker1.DoWork +=
                new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.ProgressChanged +=
                new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
        }

        // This event handler is where the time-consuming work is done.
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            //Lanzamos el proceso que bloquea la interfaz
            //Lanzamos el modo sniffer
            this.MiSniffer.IniciarSniffer(this.indiceSelecionado, worker);

            
        }

        // This event handler updates the progress.
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            resultLabel.Text = ("Buscado...");
            if (this.progressBar1.Value == 100)
            {
                this.progressBar1.Value = 0;
            }
            else
            {
                this.progressBar1.PerformStep();
            }
            //Se usa esto porque el subproceso no puede escribir directamente en la capa de datos.
            this.Refrescar();
        }

        private void Refrescar()
        {
            this.TxEstado.Text = this.MiSniffer.Estado;
            this.TxDeviceID.Text = this.MiSniffer.DeviceId;
            this.TxPlatform.Text = this.MiSniffer.Platform;
            this.TxIP.Text = this.MiSniffer.Switch_Ip;
            this.TxPort.Text = this.MiSniffer.Port;
            this.paqueteRaw = this.MiSniffer.PaqueteRaw;
            Application.DoEvents();
        }

        // This event handler deals with the results of the background operation.
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                resultLabel.Text = "Cancelado!";
                resultLinkLabel.Text = "";
            }
            else if (e.Error != null)
            {
                resultLabel.Text = "Error: " + e.Error.Message;
                resultLinkLabel.Text = "";
            }
            else
            {
                resultLabel.Text = "";
                resultLinkLabel.Text = "Conseguido!";
            }

            this.progressBar1.Value = 100;
            //Escribimos el resultado
            this.Refrescar();
            

        }

        private void resultLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.resultLinkLabel.Text == "Conseguido!")
            {
                PaqueteFrm ElPaquete = new PaqueteFrm(this.paqueteRaw);
                ElPaquete.ShowDialog();
            }
        }

        private void BtLoad_Click(object sender, EventArgs e)
        {
            this.MiSniffer.CargarPaquete(System.IO.Path.Combine(Application.StartupPath,"PaqueteEjemplo.raw"));
            resultLinkLabel.Text = "Conseguido!";
            this.Refrescar();
        }

    }
}
