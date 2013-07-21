using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
namespace GameServer
{
    public class GameServerMain: System.Windows.Forms.Form
    {
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
       // private System.ComponentModel.IContainer components;
        
        private System.Windows.Forms.RichTextBox richTextBoxReceivedMsg;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblTotalConnection;
        private delegate void UpdateRichEditCallback(string text);
        private Socket mainSocket;
        private bool isAutoStart = false;

        #region Contructor
        public GameServerMain()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameServerMain));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblIP = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.lblTotalConnection = new System.Windows.Forms.Label();
            this.richTextBoxReceivedMsg = new System.Windows.Forms.RichTextBox();


            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server IP";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.label2.Location = new System.Drawing.Point(192, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Port";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnStart
            // 
            this.btnStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnStart.Location = new System.Drawing.Point(286, 3);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(88, 24);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "Start";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStop.Enabled = false;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnStop.Location = new System.Drawing.Point(469, 4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 4;
            this.btnStop.Text = "Stop";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStatus.ForeColor = System.Drawing.Color.Red;
            this.lblStatus.Location = new System.Drawing.Point(288, 248);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(256, 40);
            this.lblStatus.TabIndex = 22;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(288, 232);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Status :";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(389, 192);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "Total connections :";

            this.lblIP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblIP.Location = new System.Drawing.Point(64, 5);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(120, 20);
            this.lblIP.TabIndex = 24;
            this.lblIP.Text = "255.255.255.255";
            this.lblIP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPort
            // 
            this.lblPort.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPort.Location = new System.Drawing.Point(224, 5);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(48, 20);
            this.lblPort.TabIndex = 25;
            this.lblPort.Text = "9876";
            this.lblPort.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalConnection
            // 
            this.lblTotalConnection.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotalConnection.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblTotalConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalConnection.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTotalConnection.Location = new System.Drawing.Point(392, 208);
            this.lblTotalConnection.Name = "lblTotalConnection";
            this.lblTotalConnection.Size = new System.Drawing.Size(152, 20);
            this.lblTotalConnection.TabIndex = 22;
            this.lblTotalConnection.Text = "0";
            this.lblTotalConnection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // richTextBoxReceivedMsg
            // 
            this.richTextBoxReceivedMsg.BackColor = System.Drawing.SystemColors.HighlightText;
            this.richTextBoxReceivedMsg.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxReceivedMsg.Location = new System.Drawing.Point(8, 32);
            this.richTextBoxReceivedMsg.Name = "richTextBoxReceivedMsg";
            this.richTextBoxReceivedMsg.Size = new System.Drawing.Size(536, 152);
            this.richTextBoxReceivedMsg.TabIndex = 20;
            this.richTextBoxReceivedMsg.Text = "";

            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(551, 403);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblTotalConnection);
            this.Controls.Add(this.richTextBoxReceivedMsg);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.lblIP);

            this.Name = "GameServer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Server";
            this.Load += new System.EventHandler(this.MGameServerMain_Load);
            
        }

        #endregion

        private void MGameServerMain_Load(object sender, System.EventArgs e)
        {
            //get Version
            string appName = Application.ProductName + ".exe";
            this.Text = Application.ProductName +
                " | Version: " + System.Windows.Forms.Application.ProductVersion.ToString();// +
            //" | Build: " + System.IO.File.GetCreationTime(appName);

            // Get IP
            ServerManager.serverIP = GetIP();

            // Load config
            LoadConfig();
           
           

            lblIP.Text = ServerManager.serverIP;
            int port = ServerManager.serverPort;
            lblPort.Text = port.ToString();


            if (isAutoStart)
            {
                btnStart_Click(sender, e);
            }
        }

        #region Load config file
        private void LoadConfig()
        {
            FileStream fleReader = new FileStream("config.ini", FileMode.Open, FileAccess.Read);
            StreamReader stmReader = new StreamReader(fleReader);
            isAutoStart = ((stmReader.ReadLine().Replace("[AutoStart]", string.Empty)).Trim() == "1") ? true : false;
            ServerManager.serverPort = int.Parse((stmReader.ReadLine().Replace("[Port]", string.Empty)).Trim());
            ServerManager.maxConnection = int.Parse((stmReader.ReadLine().Replace("[MaxConnection]", string.Empty)).Trim());
            this.Text = stmReader.ReadLine().Replace("[ServerName]", string.Empty).Trim();
            
            stmReader.Close();
            fleReader.Close();
        }

        private string getListDomain(StreamReader strReader, int port)
        {
            string res = "";
            try
            {
                while (!strReader.EndOfStream)
                {
                    string domain = strReader.ReadLine();
                    if (domain.Contains("[DomainName]"))
                    {
                        domain = domain.Replace("[DomainName]", string.Empty).Trim();
                        if (domain.Length > 0)
                            res += string.Format("<allow-access-from domain=\"{0}\" to-ports=\"{1}\" />", domain, port);
                    }
                }
            }
            catch (System.Exception e)
            {
                AppendToRichEditControl("Error Load List Domain : " + e.Message);
            }
            return res;
        }
        #endregion

        #region Get server IP
        private string GetIP()
        {
            string strHostName = Dns.GetHostName();
            // Find host by name
            IPHostEntry iphostentry = Dns.GetHostByName(strHostName);

            //Check manual set Ip Server
            int manualIp = int.Parse(Globals.getConfigKey("ManualIP"));
            if (manualIp == 0)
            {
                // Grab the first IP addresses			
                foreach (IPAddress ipaddress in iphostentry.AddressList)
                {
                    return ipaddress.ToString();
                }
            }
            else
            {
                return Globals.getConfigKey("IPSet");
            }
            return "";
        }
        #endregion

        private void btnStart_Click(object sender, System.EventArgs e)
        {
            try
            {
                Globals.totalConnection = 0;
                if (lblPort.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Please enter a Port Number");
                    return;
                }

                // Create the listening socket...
                mainSocket = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream,
                    ProtocolType.Tcp);
                //IPEndPoint ipLocal = new IPEndPoint (IPAddress.Any, ServerManager.serverPort);
                IPEndPoint ipLocal = new IPEndPoint(IPAddress.Any, ServerManager.serverPort);
                // Bind to local IP Address...
                mainSocket.Bind(ipLocal);
                // Start listening...g

                mainSocket.Listen(4);
                // Create the call back for any client connections...
                mainSocket.BeginAccept(new AsyncCallback(OnClientConnect), null);
                // Set buttons status
                btnStart.Enabled = false;
                btnStop.Enabled = true;
                lblStatus.Text = "Working ...";
                AppendToRichEditControl("Server started !");
                
            }
            catch (SocketException se)
            {
                ServerManager.WriteLogInfoServer(se, "Btn_start_Err");
                this.Close();
            }
        }

        private void btnStop_Click(object sender, System.EventArgs e)
        {
            mainSocket.Close();

            // Set buttons status
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            lblStatus.Text = "Stop receiving new connection.";
            AppendToRichEditControl("Server stopped !");
        }

        #region AppendToRichEditControl
        // This method could be called by either the main thread or any of the
        // worker threads
        public void AppendToRichEditControl(string msg)
        {
            try
            {
                msg = DateTime.Today.ToShortDateString() + " " +
                    DateTime.Now.ToLongTimeString() + " >>  " + msg + "\n";
                if (InvokeRequired)
                {
                    object[] pList = { msg };
                    richTextBoxReceivedMsg.BeginInvoke(new UpdateRichEditCallback(OnUpdateRichEdit), pList);
                }
                else
                {
                    OnUpdateRichEdit(msg);
                }
            }
            catch
            {

            }
        }
        #endregion

        #region UpdateRichEdit
        private void OnUpdateRichEdit(string msg)
        {
            richTextBoxReceivedMsg.AppendText(msg);
        }
        #endregion

        #region Dispose And Main
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //log4net.Config.XmlConfigurator.Configure();
            Application.EnableVisualStyles();
            Application.DoEvents();
            Application.Run(ServerManager.MainForm);
        }
        #endregion

        #region OnClientConnect
        // This is the call back function, which will be invoked when a client is connected
        public void OnClientConnect(IAsyncResult asyn)
        {
            try
            {
                // Add the workerSocket reference to our ArrayList
                
                Client newClt = new Client();
                newClt.ClientSocket = mainSocket.EndAccept(asyn);                
               
                if (newClt.ClientSocket.Connected)
                {
                    String data = newClt.ClientSocket.RemoteEndPoint.ToString();
                    String[] ip = data.Split(':');
                    if (!ServerManager.CheckExistClient(ip[0]))
                    {
                        return;
                    }
                    ServerManager.ClientList.Add(ip[0], newClt);
                    AppendToRichEditControl("OnClientConnect : " + newClt.ClientSocket.RemoteEndPoint);
                    newClt.WaitForData();
                    GameRequest.sendConnected(newClt, newClt.parentParticipant.ClientId);
                }
                else
                    AppendToRichEditControl("OnClientConnect : khong the ket noi");

                // Since the main Socket is now free, it can go back and wait for
                // other clients who are attempting to connect
                mainSocket.BeginAccept(new AsyncCallback(OnClientConnect), null);
            }
            catch (Exception ex)
            {
                ServerManager.WriteLogInfoServer(ex, "OnClientConnect-Err");
            }
        }
        #endregion
    }
}
