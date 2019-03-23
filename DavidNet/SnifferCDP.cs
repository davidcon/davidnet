using System;
using System.Collections.Generic;
using PcapDotNet.Core;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Ethernet;
using System.Text;
using System.Windows.Forms;
using System.Management;
using System.Threading;
using System.IO;

namespace DavidNet
{
    public delegate void GestorPaquetes(); 

    public class SnifferCDP
    {
        public event GestorPaquetes PaqueteRecibido;
        private string[] tarjetasRed;
        private IList<LivePacketDevice> allDevices;
        private System.Net.NetworkInformation.NetworkInterface miTarjeta;
        private string estado;
        private string deviceId;
        private string platform;
        private string switch_ip;
        private string port;
        private string paqueteRaw;
        private int paquetes_cdp;
        private int numero_paquetes;
        private PacketCommunicator comm;
        private System.ComponentModel.BackgroundWorker worker;

        /// <summary>
        /// Constructor
        /// </summary>
        public SnifferCDP()
        {
            //Obtenemos la lista de tarjetas de red
            this.allDevices = LivePacketDevice.AllLocalMachine;
            
            // Filtro las tarjetas de red
            // Creamos una copia de alldevices
            // En la lista añadimos solo las tarjetas que no tengan la palabra Microsoft
            //foreach (LivePacketDevice LaTarjeta in LivePacketDevice.AllLocalMachine)
            //{
                
            //}
            //for (int i = 0; i < this.allDevices.Count; i++)
            //{
            //    if (!this.allDevices[i].Description.Contains("Microsoft"))
            //    {
            //        newDevices.Add(this.allDevices[i]);
            //    }
            //}
            //this.allDevices = newDevices;
        }

        /// <summary>
        /// Se obtiene el estado de this.miTarjeta
        /// Si ya está iniciada la captura se muestran los paquetes recividos vs los cdp
        /// </summary>
        public string Estado
        {
            get
            {
                if (this.miTarjeta != null)
                {
                    switch (this.miTarjeta.OperationalStatus)
                    {
                        case System.Net.NetworkInformation.OperationalStatus.Dormant:
                            return "Estado físico DORMANT";
                        case System.Net.NetworkInformation.OperationalStatus.Down:
                            return "Estado físico DOWN";
                        case System.Net.NetworkInformation.OperationalStatus.LowerLayerDown:
                            return "Estado físico LOWER LAYER DOWN";
                        case System.Net.NetworkInformation.OperationalStatus.NotPresent:
                            return "Estado físico NO PRESENT";
                        case System.Net.NetworkInformation.OperationalStatus.Testing:
                            return "Estado físico TESTING";
                        case System.Net.NetworkInformation.OperationalStatus.Unknown:
                            return "Estado físico UNKNOW";
                        case System.Net.NetworkInformation.OperationalStatus.Up:
                        default:
                            break;
                    }
                }
                return this.paquetes_cdp + " / " + this.numero_paquetes + " Paquetes Recibidos";
            }
        }

        public string DeviceId
        {
            get
            {
                return this.deviceId;
            }
        }

        public string Platform
        {
            get
            {
                return this.platform;
            }
        }

        public string Switch_Ip
        {
            get
            {
                return this.switch_ip;
            }
        }

        public string Port
        {
            get
            {
                return this.port;
            }
        }

        public string PaqueteRaw
        {
            get
            {
                return this.paqueteRaw;
            }
        }

        /// <summary>
        /// Devuelve una lista con las descripiones de todas las tarjetas de red que existen en la maquina
        /// </summary>
        public string[] TarjetasRed
        {
            get
            {
                
                if (this.allDevices == null)
                {
                    //Si no hay ninguna tarjeta reportamos error!
                    this.tarjetasRed = new string[1] { "No interfaces found! Make sure WinPcap is installed." };
                }
                else
                {
                    this.tarjetasRed = new string[this.allDevices.Count];
                    string[] aux;
                    //Si tenemos tarjetas de red, introducimos en el array de salida los textos de las descripciones
                    for (int i = 0; i != allDevices.Count; ++i)
                    {
                        LivePacketDevice device = allDevices[i];
                        aux = device.Description.Split('\'');
                        this.tarjetasRed[i] = aux[1];
                        //Console.Write((i + 1) + ". " + device.Name);
                        //if (device.Description != null)
                        //    Console.WriteLine(" (" + device.Description + ")");
                        //else
                        //    Console.WriteLine(" (No description available)");
                    }
                }
                return this.tarjetasRed;
            }
        }

        public void IniciarSniffer(int deviceIndex, System.ComponentModel.BackgroundWorker worker)
        {
            this.estado = "";
            this.deviceId = "";
            this.platform = "";
            this.switch_ip = "";
            this.port = "";
            this.numero_paquetes = 0;
            this.paquetes_cdp = 0;

            this.worker = worker;
            // Take the selected adapter
            PacketDevice selectedDevice = this.allDevices[deviceIndex];
            string[] identificador_tarjeta = selectedDevice.Name.Split(new string[1] { "NPF_" }, StringSplitOptions.None);

            System.Net.NetworkInformation.NetworkInterface[] misTarjetas = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces(); 
            foreach (System.Net.NetworkInformation.NetworkInterface laTarjeta in misTarjetas)
            {
                if (laTarjeta.Id == identificador_tarjeta[1])
                {
                    this.miTarjeta = laTarjeta;
                }
            }
            if (this.miTarjeta.OperationalStatus != System.Net.NetworkInformation.OperationalStatus.Up)
            {
                //Comprueba el estado del enlace
                //this.PaqueteRecibido();
                return;
            }

            
            //// Open the device
            //using (
            //    PacketCommunicator communicator =
            //    selectedDevice.Open(65536,                                  // portion of the packet to capture
            //    // 65536 guarantees that the whole packet will be captured on all the link layers
            //                        PacketDeviceOpenAttributes.Promiscuous, // promiscuous mode
            //                        1000))                                  // read timeout
            //{
            //    this.comm = communicator;
            //    // Mandar trama multicast para forzar la identificación del equipo
            //    // Creamos una trama broadcast
            //    Packet TramaBroadCast = this.BuildEthernetPacket(selectedDevice);
            //    //Mandamos la trama
            //    communicator.SendPacket(TramaBroadCast);
            //    // start the capture
            //    communicator.ReceivePackets(0, PacketHandler);

            //}


            PacketCommunicator communicator =
            selectedDevice.Open(65536,                                  // portion of the packet to capture
                // 65536 guarantees that the whole packet will be captured on all the link layers
                                PacketDeviceOpenAttributes.Promiscuous, // promiscuous mode
                                1000);                                  // read timeout

            this.comm = communicator;
            // Mandar trama multicast para forzar la identificación del equipo
            // Creamos una trama broadcast
            Packet TramaBroadCast = this.BuildEthernetPacket(selectedDevice);
            //Mandamos la trama
            communicator.SendPacket(TramaBroadCast);
            // start the capture
            communicator.ReceivePackets(0, PacketHandler);

            //Aqui llegas despues de hacer el dispose!
            this.worker.ReportProgress(100);
        }

        public void CancelarSniffer(int deviceIndex)
        { 
            //Aqui hay que restaurar la tarjeta ethernet
            if (this.comm != null)
            {
                this.comm.Break();
                try
                {
                    if (this.comm.DataLink.Kind == DataLink.Ethernet.Kind)
                    {
                        this.comm.Dispose();
                    }
                }
                catch (Exception)
                {

                }
               
            }

            if (this.worker != null)
            {
                //Se cierra el proceso
                this.worker.CancelAsync();
                //Configurar DHCP - CODIGO EN CambioIP
                //SnifferCDP.SetDHCP(this.miTarjeta.Description);
            }
                
                
            
        }

        public void CargarPaquete(string path)
        {
            // Specify a file to read from and to create.
            string pathSource = path;
            byte[] bytes;
            //string pathNew = path + "_new";
            try
            {

                using (FileStream fsSource = new FileStream(pathSource,
                    FileMode.Open, FileAccess.Read))
                {

                    // Read the source file into a byte array.
                    bytes = new byte[fsSource.Length];
                    int numBytesToRead = (int)fsSource.Length;
                    int numBytesRead = 0;
                    while (numBytesToRead > 0)
                    {
                        // Read may return anything from 0 to numBytesToRead.
                        int n = fsSource.Read(bytes, numBytesRead, numBytesToRead);

                        // Break when the end of the file is reached.
                        if (n == 0)
                            break;

                        numBytesRead += n;
                        numBytesToRead -= n;
                    }
                    
                    PaqueteCDP ElPaquete = new PaqueteCDP(bytes);

                    this.deviceId = ElPaquete.IdDevice;
                    this.platform = ElPaquete.Platform;
                    this.switch_ip = ElPaquete.SwitchAddress;
                    this.port = ElPaquete.Port;
                    this.paqueteRaw = ElPaquete.BufferRaw;

                    //numBytesToRead = bytes.Length;
                    //// Write the byte array to the other FileStream.
                    //using (FileStream fsNew = new FileStream(pathNew,
                    //    FileMode.Create, FileAccess.Write))
                    //{
                    //    fsNew.Write(bytes, 0, numBytesToRead);
                    //}
                }
            }
            catch (FileNotFoundException ioEx)
            {
                Console.WriteLine(ioEx.Message);
            }
        }

  

        /// <summary>
        /// This function build an Ethernet with payload packet.
        /// </summary>
        private Packet BuildEthernetPacket(PacketDevice laTarjeta)
        {

            System.Net.NetworkInformation.PhysicalAddress direccionMAC = this.miTarjeta.GetPhysicalAddress();
            string direccionMACStr = direccionMAC.ToString();
            
            EthernetLayer ethernetLayer =
                new EthernetLayer
                {
                    //Falta rellenar aqui la dirección mac de la tarjeta seleccionada
                    Source = new MacAddress(direccionMACStr.Substring(0, 2) + ":" +
                                            direccionMACStr.Substring(2, 2) + ":" +
                                            direccionMACStr.Substring(4, 2) + ":" +
                                            direccionMACStr.Substring(6, 2) + ":" +
                                            direccionMACStr.Substring(8, 2) + ":" +
                                            direccionMACStr.Substring(10, 2)),
                    Destination = new MacAddress("01:00:0c:cc:cc:cc"),
                    EtherType = EthernetType.IpV4,
                };

            PayloadLayer payloadLayer =
                new PayloadLayer
                {
                    Data = new Datagram(Encoding.ASCII.GetBytes("Trama BroadCast by David Conde")),
                };

            PacketBuilder builder = new PacketBuilder(ethernetLayer, payloadLayer);

            return builder.Build(DateTime.Now);
        }

        // Callback function invoked by Pcap.Net for every incoming packet
        // Esta función es la que analiza el paquete recibido
        private void PacketHandler(Packet packet)
        {
            this.worker.ReportProgress(50);
            
            this.numero_paquetes++;
            //Miramos si el paquete recibido tiene la forma que necesitamos
                
            byte[] aux = packet.Buffer.ReadBytes(20, 2);

            //PID = 0x2000 OR PID = 0x0207
            if ((aux[0] == 32 && aux[1] == 0)
                || (aux[0] == 2 && aux[1] == 7))
            {

                PaqueteCDP ElPaquete = new PaqueteCDP(packet.Buffer);

                this.deviceId = ElPaquete.IdDevice;
                this.platform = ElPaquete.Platform;
                this.switch_ip = ElPaquete.SwitchAddress;
                this.port = ElPaquete.Port;
                this.paqueteRaw = ElPaquete.BufferRaw;


                this.paquetes_cdp++;
                
                //Lanzar evento paquete recibido!!!
                //this.PaqueteRecibido();

                //Cerramos el proceso
                this.worker.ReportProgress(100);
                this.comm.Break();
                this.comm.Dispose();
                //this.worker.CancelAsync();
                ////Byte long para nombre Duplex - 379
                //aux = packet.Buffer.ReadBytes(381, Convert.ToInt32(packet.Buffer[380]));
                //string duplex = System.Text.Encoding.ASCII.GetString(aux);
                //Console.WriteLine("Device Id: {0} Platform: {1} Switch IP: {2} Port: {3}", deviceId, platform, Ip, port);
            }
            else
            {
                //Cambio de nombre de etiqueta!!!
                
            }

        }
        /// <summary>
        /// Enable DHCP on the NIC
        /// </summary>
        /// <param name="nicName">Name of the NIC</param>
        public static void SetDHCP(string nicName)
        {
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {
                //En lugar de equals, contains
                if (mo["Caption"].Equals(nicName))
                {
                    ManagementBaseObject newDNS = mo.GetMethodParameters("SetDNSServerSearchOrder");
                    newDNS["DNSServerSearchOrder"] = null;
                    ManagementBaseObject enableDHCP = mo.InvokeMethod("EnableDHCP", null, null);
                    ManagementBaseObject setDNS = mo.InvokeMethod("SetDNSServerSearchOrder", newDNS, null);
                }

            }
        }
    }

}
