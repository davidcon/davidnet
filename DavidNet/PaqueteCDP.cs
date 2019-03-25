using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcapDotNet.Core;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Ethernet;
using System.Collections;

namespace DavidNet
{
    enum TypeData
    {
        IdDevice = 1,
        SwitchAdress = 2,
        Port = 3,
        Capabilites = 4,
        SoftwareVersion = 5,
        Platform = 6,
        IpPrefix = 7,
        Domain = 9,
        VLAN = 10,
        FullDuplex = 11,
        VoIp = 14,
        TrustBitMap = 18,
        UntrustedPortCos = 19,
        ManagementoAddress = 22,
        PowerAvailable = 26,
        SpearPairPoE = 31
    }

    public class PaqueteCDP
    {
        private string idDevice = "";
        private string softwareVersion = "";
        private string platform = "";
        private string switchAddress = "";
        private string port = "";
        private string capabilities = "";
        private string ipPrefix = "";
        private string domain = "";
        private string vLan = "";
        private string bufferStr = "";
        private bool fullDuplex = false;
        private string voIp = "";
        private bool powerAvailable = false;
        //private struct campoDatos
        //{
        //    public byte[] type;
        //    public int length;
        //    public byte[] data;
        //}
        
        /// <summary>
        /// Esta clase recibe un paquete de tipo CDP
        /// PID = CDP => 0x2000 == byte[0] = 32 byte[1] = 0 en decimal
        /// Offset = en wireshark valor hexadecimal, cada pareja un byte
        /// El offset para el PID es 16 x 1 + 4
        /// La longitud es fija = 2
        /// </summary>
        public PaqueteCDP(byte[] pBuffer)
        {
            byte[] aux;
            int type;
            int length;
            //Creamos un array auxiliar para transformar la longitud de hex a int
            //los dos primeros valores son siempre 0, ya que en el paquete la longitud 
            //se expresa solo con dos bytes
            byte[] lengthHex = new byte[4];
            byte[] datos;

            //Salvamos la cadena entera en formato string por si hay algún error en el troceado de la misma.
            this.bufferStr = System.Text.Encoding.ASCII.GetString(
                                pBuffer.ReadBytes(26, pBuffer.Length - 26));
            //Quitamos las cadenas de escape
            //Lo convertimos a char y el que tiene valor cero se elimina
            char[] bufferChar = this.bufferStr.ToCharArray();
            string ListaAux = "";
            foreach (char charaux in bufferChar)
            {
                if (charaux != 0)
                {
                    ListaAux += Char.ToString(charaux);
                }
            }
            this.bufferStr = ListaAux;
           //Troceamos el paquete en distintos struct
           //Empezamos en el byte 26, que será el correspondiente a IdDevice
            for (int i = 26; i < pBuffer.Length; i++)
            {
                aux = pBuffer.ReadBytes(i,2);
                type = Convert.ToInt32(aux[1]);
                int extraType = Convert.ToInt32(aux[0]);
                //transfomamos los bytes i+2 e i+3 en decimal

                //length = (Convert.ToInt32(p.Buffer[i + 2]) * 16) + Convert.ToInt32(p.Buffer[i + 3]);
                lengthHex[0] = 0X00;
                lengthHex[1] = 0X00;
                lengthHex[2] = pBuffer[i + 2];
                lengthHex[3] = pBuffer[i + 3];
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(lengthHex); //need the bytes in the reverse order
                length = BitConverter.ToInt32(lengthHex, 0);

                //Hay un error en la conversión hexadecimal a decimal.
                //para solucionarlo miramos que la longitud sea inferior al la del plaquete
                if (i + length <= pBuffer.Length)
                {
                    //obtenemos la cadena desde i+4 hasta length pero restamos los dos bytes de type y los dos bytes de length
                    datos = pBuffer.ReadBytes(i + 4, length - 4);
                    if (extraType == 0)
                    {
                        switch (type)
                        {
                            //ID IdDevice
                            case 1:
                                this.idDevice = System.Text.Encoding.ASCII.GetString(datos);
                                break;
                            //IP Address
                            //Cogemos la ultima dirección del bloque
                            case 2:
                                this.switchAddress = "";
                                for (int idatos = datos.Length - 4; idatos < datos.Length; idatos++)
                                {
                                    this.switchAddress += datos[idatos].ToString();
                                    if (idatos != datos.Length - 1)
                                    {
                                        this.switchAddress += ":";
                                    }
                                }
                                break;
                            case 3:
                                this.port = System.Text.Encoding.ASCII.GetString(datos);
                                break;
                            case 4:
                                this.capabilities = System.Text.Encoding.ASCII.GetString(datos);
                                break;
                            case 5:
                                this.softwareVersion = System.Text.Encoding.ASCII.GetString(datos);
                                break;
                            case 6:
                                this.platform = System.Text.Encoding.ASCII.GetString(datos);
                                break;
                            case 7:
                                //Habría que modificarlo para sacar mejor informacion
                                //Se puede obtener la ip de la puerta de enlace
                                this.ipPrefix = System.Text.Encoding.ASCII.GetString(datos);
                                break;
                            case 9:
                                this.domain = System.Text.Encoding.ASCII.GetString(datos);
                                break;
                            case 10:
                                //Hay que convertirlo a entero
                                this.vLan = System.Text.Encoding.ASCII.GetString(datos);
                                break;
                            case 11:
                                //No hay que hacer una comparación con string hay que hacerla con exadecimal 0x01
                                if (System.Text.Encoding.ASCII.GetString(datos) == "1")
                                    this.fullDuplex = true;
                                else
                                    this.fullDuplex = false;
                                break;
                            case 14:
                                this.voIp = System.Text.Encoding.ASCII.GetString(datos);
                                break;
                            case 18:
                                //this.trustBitmap = System.Text.Encoding.ASCII.GetString(datos);
                                break;
                            case 19:
                                //untrustedPortCos
                                break;
                            case 22:
                                //managementAddress
                                break;
                            case 26:
                                //this.powerAvailable = System.Text.Encoding.ASCII.GetString(datos);
                                //Tiene una estructura en tres partes
                                break;
                            case 31:
                                //Spear Pair PoE
                                //Tiene una estructura en binario
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        switch (type)
                        {
                            case 4:
                                //Unknown data
                                break;
                            case 3:
                                //Radio 1 channel
                                break;
                            default:
                                break;
                        }
                    }
                }
                //la longitud de los datos incluye 2bytes para type y 2bytes para length
                i += length - 1;
            }
        }

        /// <summary>
        /// Type = 0x0001
        /// </summary>
        public string IdDevice
        {
            get { return idDevice; }
        }

        /// <summary>
        /// Type = 0x0005
        /// </summary>
        public string SoftwareVerion
        {
            get { return softwareVersion; }
        }

        /// <summary>
        /// Type = 0x0006
        /// </summary>
        public string Platform
        {
            get { return platform; }
        }

        /// <summary>
        /// Type = 0x0002
        /// </summary>
        public string SwitchAddress
        {
            get { return switchAddress; }
        }

        /// <summary>
        /// Type = 0x0003
        /// </summary>
        public string Port
        {
            get { return port; }
        }

        /// <summary>
        /// Type = 0x0004
        /// </summary>
        public string Capabilities
        {
            get { return capabilities; }
        }

        /// <summary>
        /// Type = 0x0007
        /// </summary>
        public string IpPrefix
        {
            get { return ipPrefix; }
        }

        /// <summary>
        /// Type = 0x0009
        /// </summary>
        public string Domain
        {
            get { return domain; }
        }

        /// <summary>
        /// Type = 0x000a
        /// </summary>
        public string VLan
        {
            get { return vLan; }
        }

        /// <summary>
        /// Type = 0x000b
        /// </summary>
        public bool FullDuplex
        {
            get { return fullDuplex; }
        }

        /// <summary>
        /// Type = 0x000e
        /// </summary>
        public string VoIp
        {
            get { return voIp; }
        }

        /// <summary>
        /// Type = 0x001a
        /// </summary>
        public bool PowerAvailable
        {
            get { return powerAvailable; }
        }

        /// <summary>
        /// Devuelve el paquete completo
        /// </summary>
        public string BufferRaw
        {
            get { return this.bufferStr; }
        }
    }
}
