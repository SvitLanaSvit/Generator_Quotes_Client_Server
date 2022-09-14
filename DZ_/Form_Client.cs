using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using Timer = System.Windows.Forms.Timer;

namespace DZ_
{
    public partial class Form_Client : Form
    {
        delegate void TextDelegate(string text);
        public Form_Client()
        {
            InitializeComponent();
            txtClientInfo.ReadOnly = true;
            btnDisconnect.Enabled = false;
            btnConnectToServer.Enabled = true;
        }

        string adress = "127.0.0.1";
        int port = 8005;
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Timer timer = null!;

        private void Form_Client_Load(object sender, EventArgs e)
        {
            timer = new Timer();
            timer.Interval = 2000;
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            GetQuotes(socket);
        }

        private void btnConnectToServer_Click(object sender, EventArgs e)
        {
            Task.Run(() => ConnectToServer(adress, port));
            txtClientInfo.BeginInvoke(new TextDelegate(UpdateTexBox), "Client was connected with server!");
            timer.Start();
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }

        private async void ConnectToServer(string ipAdress, int port)
        {
            IPAddress ipAddress = IPAddress.Parse(ipAdress);
            IPEndPoint iPEndPoint = new IPEndPoint(ipAddress, port);
            try
            {
                await socket.ConnectAsync(iPEndPoint);
            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void GetQuotes(Socket socket)
        {
            StringBuilder sb;
            try
            {
                if (socket.Connected)
                {
                    int bytes = 0;
                    byte[] dataBuffer = new byte[256];
                    sb = new StringBuilder();

                    do
                    {
                        bytes = await socket.ReceiveAsync(new ArraySegment<byte>(dataBuffer), SocketFlags.None);
                        sb.Append(Encoding.Unicode.GetString(new ArraySegment<byte>(dataBuffer).Array!, 0, bytes));

                    } while (socket.Available > 0);
                    txtClientInfo.BeginInvoke(new TextDelegate(UpdateTexBox), sb.ToString());

                    if (sb.ToString().Contains("Connection interrupted."))
                        btnDisconnect.Enabled = false;
                }
            }
            catch { }
        }

        private void UpdateTexBox(string text)
        {
            StringBuilder sb = new StringBuilder(txtClientInfo.Text);
            sb.AppendLine(text);
            txtClientInfo.Text = sb.ToString();
        }

        private void txtClientInfo_TextChanged(object sender, EventArgs e)
        {
            if (txtClientInfo.Text != "")
            {
                btnConnectToServer.Enabled = false;
            }
            else
            {
                btnConnectToServer.Enabled = true;
            }
        }

        private void btnConnectToServer_MouseClick(object sender, MouseEventArgs e)
        {
            btnDisconnect.Enabled = true;
        }
    }
}
