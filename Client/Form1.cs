using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        // Client socket
        readonly Socket clientSocket;
        // Server that I'm gonna connect to.
        readonly IPEndPoint serverAddress;
        public Form1()
        {           
            InitializeComponent();
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            serverAddress = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 4444);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == null)
            {
                MessageBox.Show("Empty Message!");
                textBox1.Focus();
                return;
            }
            
            // Get the text and encode it as bytes [cuz we gonna send it as streams.], add it to the buffer, then send it.
            byte[] buffer = Encoding.ASCII.GetBytes(textBox1.Text.Trim());
            clientSocket.SendTo(buffer, serverAddress);
            textBox1.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
