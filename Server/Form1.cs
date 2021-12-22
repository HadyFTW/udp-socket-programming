using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace Server
{
    public partial class Form1 : Form
    {
        Socket serverSocket;
        IPEndPoint serverAddress;
        Thread thread;

        void StartServer()
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            serverAddress = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 4444);
            serverSocket.Bind(serverAddress);
            thread = new Thread(new ThreadStart(Listening));
            thread.Start();
        }

        bool isRunning = true;

        void Listening()
        {
            while (isRunning)
            {
                byte[] buffer = new byte[4096];
                EndPoint clientAddress = new IPEndPoint(IPAddress.Any, 0);
                int bytesCount = serverSocket.ReceiveFrom(buffer, ref clientAddress);
                string msg = Encoding.ASCII.GetString(buffer, 0, bytesCount);
                textBox1.Invoke(new Action(delegate
                {
                    textBox1.AppendText($"{clientAddress}: {msg}{Environment.NewLine}");
                }));
            }
        }

        void StopServer()
        {
            isRunning = false;
            thread.Abort();
            serverSocket.Close();
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = true;
            label1.Text = "Connected";
            label1.ForeColor = Color.Green;
            StartServer();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = false;
            label1.Text = "Disconnected";
            label1.ForeColor = Color.Red;
            StopServer();
        }
    }
}
