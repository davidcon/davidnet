/*
 * Created by SharpDevelop.
 * User: JavCasta - http://javcasta.com/
 * Ref: sharpPcap http://sourceforge.net/projects/sharppcap/
 * Date: 04/11/2012
 * Time: 21:22
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

using PacketDotNet;
using SharpPcap;

namespace DavidNet
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		private bool BackgroundThreadStop;
		private object QueueLock = new object();
		private List<RawCapture> PacketQueue = new List<RawCapture>();
		private DateTime LastStatisticsOutput;
		private TimeSpan LastStatisticsInterval = new TimeSpan(0, 0, 2);
        private System.Threading.Thread backgroundThread;
        private ICaptureDevice device;
        private ComboBox comboBox1;
        private Label label1;
        int ndevice = 0;
		
		public MainForm()
		{
			InitializeComponent();
			Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
			CaptureDeviceList devices = CaptureDeviceList.Instance;
			//Si no encuentra NICs -> error
			comboBox1.Text = "Elige la NIC donde escuchar los paquetes CDP";
			if(devices.Count < 1)
			{
    			comboBox1.Text  = "No se han encontrado NICs en esta maquina";
    			//return;
    			button1.Enabled = false;
			}
			int i = 0;
			foreach(ICaptureDevice devs in devices) 
			{
				i++;
				try {
					comboBox1.Items.Add(i.ToString()+": "+ devs.Name + " - " + devs.Description);
				} catch (Exception ex) { richTextBox1.Text = ex.ToString();}
			}
		}
		void Application_ApplicationExit(object sender, EventArgs e)
        {
			//Shutdown();
        }
		
		
		void ComboBox1SelectedIndexChanged(object sender, EventArgs e)
		{
			//elegimos nic
			string [] eleccion = comboBox1.Text.Split(new Char[] {':'});
			ndevice = int.Parse(eleccion[0]) -1;
			button1.Enabled = true;
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			StartCapture(ndevice);
			statusStrip1.Visible = true;
			toolStripStatusLabel1.Text = "Capturando paquetes CDP";
			toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
		}
		
		public class PacketWrapper
        {
            public RawCapture p;

            public int Count { get; private set; }
            public PosixTimeval Timeval { get { return p.Timeval; } }
            public LinkLayers LinkLayerType { get { return p.LinkLayerType; } }
            public int Length { get { return p.Data.Length; } }
            //ref http://www.cisco.com/univercd/cc/td/doc/product/lan/trsrb/frames.htm#xtocid12
            public string Mac_Origen { get { return getValores(6,11,p); } }
            public string Plataforma { get { return getEntre2Valores("0006","0002",p); } }
            public string IP { get { return getEntre2ValoresIP("cc00","0003",p); } }
            public string Port_Que_Envia_CDP { get { return getEntre2Valores("0003","0004",p).Substring(2); } }
            public string Duplex { get { return getValorDuplex("000b",p); } }

            public string getEntre2Valores( string valor1, string valor2, RawCapture datos)
            {
            	string cadena = "";
            	string subcadena = "";
            	for (int i = 0; i < datos.Data.Length -1; i++) {
            		cadena += String.Format("{0:x2}",datos.Data.GetValue(i));
            	}
            	try {
            	int pos1 = cadena.IndexOf(valor1,64);
            	int pos2 = cadena.IndexOf(valor2);
            	int incremento = 64;
            	while (pos2<pos1 || incremento >= cadena.Length) 
            	{
            		pos2 = cadena.IndexOf(valor2,incremento);
            		incremento = incremento + 2;
            	}
            	
            	int lon = valor1.Length;
            	subcadena = cadena.Substring(pos1+lon,pos2-pos1-lon);
            	subcadena = HexString2Ascii(subcadena);
            	return subcadena;}
            	catch { return "????";}
            	
            }
            
            public string getValorDuplex( string valor1, RawCapture datos)
            {
            	string cadena = "";
            	string subcadena = "";
            	for (int i = 0; i < datos.Data.Length -1; i++) {
            		cadena += String.Format("{0:x2}",datos.Data.GetValue(i));
            	}
            	try {
            	int pos1 = cadena.IndexOf(valor1,64);
            	try {subcadena = cadena.Substring(pos1+8,2);} 
            	catch {subcadena = "Half (¿duplex mismatch?)";}
            	if (subcadena.Equals("00")) subcadena = "Half";
            	if (subcadena.Equals("01")) subcadena = "Full";
            	return subcadena;}
            	catch { return "Duplex ????";}
            	
            }
            
             public string getEntre2ValoresIP( string valor1, string valor2, RawCapture datos)
            {
            	//int=0;
            	string cadena = "";
            	string subcadena = "";
            	for (int i = 0; i < datos.Data.Length -1; i++) {
            		cadena += String.Format("{0:x2}",datos.Data.GetValue(i));
            	}
            	try {
            	int pos1 = cadena.IndexOf(valor1,64);
            	int pos2 = cadena.IndexOf(valor2);
            	int lon = valor1.Length;
            	subcadena = (cadena.Substring(pos1+lon,pos2-pos1-lon)).Substring(2);
            	string [] lasips = subcadena.Split(new String[] {"cc00"},StringSplitOptions.RemoveEmptyEntries);
            	string devolver = "";
            	//subcadena = subcadena.Substring(2);
            	foreach (string  elemento in lasips) {
            		char [] componentes = elemento.ToCharArray();
            		string ipa = (int.Parse((componentes[0].ToString()+componentes[1].ToString()), NumberStyles.HexNumber)).ToString();
            		string ipb = (int.Parse((componentes[2].ToString()+componentes[3].ToString()), NumberStyles.HexNumber)).ToString();
            		string ipc = (int.Parse((componentes[4].ToString()+componentes[5].ToString()), NumberStyles.HexNumber)).ToString();
            		string ipd = (int.Parse((componentes[6].ToString()+componentes[7].ToString()), NumberStyles.HexNumber)).ToString();
            		devolver += ipa+"."+ipb+"."+ipc+"."+ipd+" ";
            	}
            	return devolver;}
            	catch { return "IP ???"; }
            	
            }

            
			public string getValores(int indice1, int indice2, RawCapture datos)
        	{
				string cadena = "";
        		for (int i = indice1; i <= indice2; i++) {
					string hex=String.Format("{0:x2}",datos.Data.GetValue(i));
					cadena += hex + ":";
            	}
				return cadena.Substring(0,cadena.Length-1);
         	}
			
			public string getPlataforma( int indice2, RawCapture datos)
        	{
				string cadena = "";
				int i = indice2;
				string compara = "";
				
				while (!compara.Equals("0006000e"))
				{
					//00  06 00 0e
					string hex0=String.Format("{0:x2}",datos.Data.GetValue(i));
					string hex1=String.Format("{0:x2}",datos.Data.GetValue(i+1));
					string hex2=String.Format("{0:x2}",datos.Data.GetValue(i+1));
					string hex3=String.Format("{0:x2}",datos.Data.GetValue(i+1));
					compara = hex0 + hex1;
					if (compara.Equals("00020011")) {MessageBox.Show(cadena);break;}
					cadena += hex0;
					i++;
				}
        		
				return HexString2Ascii(cadena);
         	}
			
			private string HexString2Ascii(string hexString)
			{
				StringBuilder sb = new StringBuilder();
				for (int i = 0; i <= hexString.Length - 2; i += 2)
				{
					sb.Append(Convert.ToString(Convert.ToChar(Int32.Parse(hexString.Substring(i, 2), System.Globalization.NumberStyles.HexNumber))));
				}
				return sb.ToString();
			}
			
            public PacketWrapper(int count, RawCapture p)
            {
                this.Count = count;
                this.p = p;
            }
        }
		
		private PacketArrivalEventHandler arrivalEventHandler;
        private CaptureStoppedEventHandler captureStoppedEventHandler;
		
        
        private void Shutdown()
        {
            if (device != null)
            {
                device.StopCapture();
                device.Close();
                device.OnPacketArrival -= arrivalEventHandler;
                device.OnCaptureStopped -= captureStoppedEventHandler;
                device = null;
                // ask the background thread to shut down
                BackgroundThreadStop = true;
                // wait for the background thread to terminate
                backgroundThread.Join();
            }
        }
        
		private void StartCapture(int itemIndex)
        {
            packetCount = 0;
            device = CaptureDeviceList.Instance[itemIndex];
            packetStrings = new Queue<PacketWrapper>();
            bs = new BindingSource();
            //richTextBox1.Text += bs.ToString();
            dataGridView1.DataSource = bs;
            LastStatisticsOutput = DateTime.Now;
            // start the background thread
            BackgroundThreadStop = false;
            backgroundThread = new System.Threading.Thread(BackgroundThread);
            backgroundThread.Start();
            // setup background capture
            arrivalEventHandler = new PacketArrivalEventHandler(device_OnPacketArrival);
            device.OnPacketArrival += arrivalEventHandler;
            captureStoppedEventHandler = new CaptureStoppedEventHandler(device_OnCaptureStopped);
            device.OnCaptureStopped += captureStoppedEventHandler;
            device.Open();
            //device.Filter = "ether host 01:00:0c:cc:cc:cc";
			device.Filter = textBox1.Text;
            // force an initial statistics update
            captureStatistics = device.Statistics;
            //UpdateCaptureStatistics();
            // start the background capture
            //device.Capture();
            
            device.StartCapture();
        }
		
		void device_OnCaptureStopped(object sender, CaptureStoppedEventStatus status)
        {
            if (status != CaptureStoppedEventStatus.CompletedWithoutError)
            {
                MessageBox.Show("Error parando captura", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
		
		private Queue<PacketWrapper> packetStrings;

        private int packetCount;
        private BindingSource bs;
        private ICaptureStatistics captureStatistics;
        private bool statisticsUiNeedsUpdate = false;

        void device_OnPacketArrival(object sender, CaptureEventArgs e)
        {
            // print out periodic statistics about this device
            var Now = DateTime.Now; // cache 'DateTime.Now' for minor reduction in cpu overhead
            var interval = Now - LastStatisticsOutput;
            if (interval > LastStatisticsInterval)
            {
                Console.WriteLine("device_OnPacketArrival: " + e.Device.Statistics);
                captureStatistics = e.Device.Statistics;
                statisticsUiNeedsUpdate = true;
                LastStatisticsOutput = Now;
            }

            // lock QueueLock to prevent multiple threads accessing PacketQueue at
            // the same time
            lock (QueueLock)
            {
                PacketQueue.Add(e.Packet);
            }
        }
		
		
		void Button2Click(object sender, EventArgs e)
		{
			if (device == null)
            {
                comboBox1.Text = "Elige NIC donde escuchar los paquetes CDP";
                button1.Enabled = false;
                toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
                toolStripStatusLabel1.Text = "Captura detenida";
            }
            else
            {
                Shutdown();
                comboBox1.Text = "Elige NIC donde escuchar los paquetes CDP";
                button1.Enabled = false;
                toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
                toolStripStatusLabel1.Text = "Captura detenida";
            }
		}
		
		private void BackgroundThread()
        {
            while (!BackgroundThreadStop)
            {
                bool shouldSleep = true;

                lock (QueueLock)
                {
                    if (PacketQueue.Count != 0)
                    {
                        shouldSleep = false;
                    }
                }

                if (shouldSleep)
                {
                    System.Threading.Thread.Sleep(250);
                }
                else // should process the queue
                {
                    List<RawCapture> ourQueue;
                    lock (QueueLock)
                    {
                        // swap queues, giving the capture callback a new one
                        ourQueue = PacketQueue;
                        PacketQueue = new List<RawCapture>();
                    }

                    Console.WriteLine("BackgroundThread: ourQueue.Count is {0}", ourQueue.Count);

                    foreach (var packet in ourQueue)
                    {
                        // Here is where we can process our packets freely without
                        // holding off packet capture.
                        //
                        // NOTE: If the incoming packet rate is greater than
                        //       the packet processing rate these queues will grow
                        //       to enormous sizes. Packets should be dropped in these
                        //       cases

                        var packetWrapper = new PacketWrapper(packetCount, packet);
                        this.BeginInvoke(new MethodInvoker(delegate
                        {
                            packetStrings.Enqueue(packetWrapper);
                        }
                        ));

                        packetCount++;

                        var time = packet.Timeval.Date;
                        var len = packet.Data.Length;
                        Console.WriteLine("BackgroundThread: {0}:{1}:{2},{3} Len={4}",
                            time.Hour, time.Minute, time.Second, time.Millisecond, len);
                    }

                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        bs.DataSource = packetStrings.Reverse();
                    }
                    ));

                    if (statisticsUiNeedsUpdate)
                    {
                        //UpdateCaptureStatistics();
                        statisticsUiNeedsUpdate = false;
                    }
                }
            }
        }
		
		/* private void UpdateCaptureStatistics()
        {
            toolStripStatusLabel1.Text = string.Format("Received packets: {0}, Dropped packets: {1}, Interface dropped packets: {2}",
                                                       captureStatistics.ReceivedPackets,
                                                       captureStatistics.DroppedPackets,
                                                       captureStatistics.InterfaceDroppedPackets);
        }*/

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Shutdown();
        }
		
		
		void DataGridView1SelectionChanged(object sender, EventArgs e)
		{
			if (dataGridView1.SelectedCells.Count == 0)
                return;
            var packetWrapper = (PacketWrapper)dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].DataBoundItem;
            var packet = Packet.ParsePacket(packetWrapper.p.LinkLayerType, packetWrapper.p.Data);
            richTextBox1.Text = packet.ToString(StringOutputType.VerboseColored);
            var wol = PacketDotNet.EthernetPacket.GetEncapsulated(packet);
            if(wol.PayloadData != null)
            {
                string hexa =  wol.PrintHex();
                richTextBox1.Text += wol.PrintHex();
            }
		}
		
		
		void Button3Click(object sender, EventArgs e)
		{
			Process.Start("http://www.javcasta.com/2012/11/06/cisco-cdplistener-utilidad-en-c-con-sharppcap-para-escuchar-paquetes-cdp/");
		}

        private void InitializeComponent()
        {
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(3, 20);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(332, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tarjeta de Red";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(339, 262);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Name = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
		
	}
}
