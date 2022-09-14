using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DZ_
{
    public partial class Form1 : Form
    {
        public delegate void TextDelegate(string text);
        public Form1()
        {
            InitializeComponent();
            btnNewClient.Enabled = false;
            btnStartServer.Enabled = true;
            textBox1.ReadOnly = true;
        }

        string adress = "127.0.0.1";
        int port = 8005;
        DateTime startServer;

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            Task.Run(ServerFunc);
        }

        private async void ServerFunc()
        {
            this.BeginInvoke(new TextDelegate(UpdateFormCaption), "Server has been started.");
            IPAddress ipAdress = IPAddress.Parse(adress);
            IPEndPoint iPEndPoint = new IPEndPoint(ipAdress, port);
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listenSocket.Bind(iPEndPoint);
                listenSocket.Listen(20);
                textBox1.BeginInvoke(new TextDelegate(UpdateTextBox), "Wait connection...");

                while (true)
                {
                    Socket handler = await listenSocket.AcceptAsync();
                    Task task = Task.Run(() => AcceptClientFunc(handler));
                }
            }
            catch(SocketException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void AcceptClientFunc(object obj)
        {
            Socket? handler = obj as Socket;
            StringBuilder sb = new StringBuilder();
            startServer = DateTime.Now;
            int count = 0;

            try
            {
                textBox1.BeginInvoke(new TextDelegate(UpdateTextBox), $"Client {handler!.RemoteEndPoint} has been connected. " +
                    $"Date of conection: {DateTime.Now.ToShortTimeString()}");

                while (count != 5)
                {
                    Thread.Sleep(2000);
                    string message = GenerateQuotes();
                    byte[] messageBytes = Encoding.Unicode.GetBytes(message);
                    await handler.SendAsync(new ArraySegment<byte>(messageBytes), SocketFlags.None);
                    sb.AppendLine(message);
                    count++;
                }
            }
            catch
            {
                textBox1.BeginInvoke(new TextDelegate(UpdateTextBox), $"---------------------------------------------------");
                textBox1.BeginInvoke(new TextDelegate(UpdateTextBox), $"Connecting time : {startServer.ToShortTimeString()}");
                textBox1.BeginInvoke(new TextDelegate(UpdateTextBox), $"Quotes sent:");
                textBox1.BeginInvoke(new TextDelegate(UpdateTextBox), $"{sb}");
                textBox1.BeginInvoke(new TextDelegate(UpdateTextBox), $"Disconnecting time: {DateTime.Now.ToShortTimeString()}");
                textBox1.BeginInvoke(new TextDelegate(UpdateTextBox), $"---------------------------------------------------");
                handler!.Shutdown(SocketShutdown.Send);
                handler.Close();
            }
            finally
            {
                string message = "Connection interrupted. The maximum number of quuotes has been collected.";
                byte[] messageBytes = Encoding.Unicode.GetBytes(message);
                await handler!.SendAsync(new ArraySegment<byte>(messageBytes), SocketFlags.None);
                textBox1.BeginInvoke(new TextDelegate(UpdateTextBox), $"---------------------------------------------------");
                textBox1.BeginInvoke(new TextDelegate(UpdateTextBox), $"Connecting time : {startServer.ToShortTimeString()}");
                textBox1.BeginInvoke(new TextDelegate(UpdateTextBox), $"Quotes sent:");
                textBox1.BeginInvoke(new TextDelegate(UpdateTextBox), $"{sb}");
                textBox1.BeginInvoke(new TextDelegate(UpdateTextBox), $"Disconnecting time: {DateTime.Now.ToShortTimeString()}");
                textBox1.BeginInvoke(new TextDelegate(UpdateTextBox), $"---------------------------------------------------");
                handler!.Shutdown(SocketShutdown.Send);
                handler.Close();
            }
        }

        private void btnNewClient_Click(object sender, EventArgs e)
        {
            Form_Client clientForm = new Form_Client();
            clientForm.Show();
        }

        private void UpdateTextBox(string text)
        {
            StringBuilder sb = new StringBuilder(textBox1.Text);
            sb.AppendLine(text);
            textBox1.Text = sb.ToString();
        }

        private void UpdateFormCaption(string text)
        {
            this.Text = text;
        }

        private string GenerateQuotes()
        {
            List<string> quotes = new List<string>();
            Random random = new Random();
            try
            {
                StreamReader readerQuotes = new StreamReader("Quotes.txt");
                string line = "";
                while (!readerQuotes.EndOfStream)
                {
                    line = readerQuotes.ReadLine()!;
                    quotes.Add(line);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return quotes[random.Next(1, quotes.Count)];
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                btnNewClient.Enabled = false;
                btnStartServer.Enabled = true;
            }
            else
            {
                btnNewClient.Enabled = true;
                btnStartServer.Enabled = false;
            }
        }
    }
}