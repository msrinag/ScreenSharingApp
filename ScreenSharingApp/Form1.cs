using System;
using System.Windows.Forms;

namespace ScreenSharingApp
{
    public partial class Form1 : Form
    {
        private ScreenServer _server;
        /// <summary>
        /// private const string Url = "http://localhost:8080/";
        /// </summary>
        private const string Url = "http://192.168.1.166:6868/";
        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                _server = new ScreenServer(Url);
                _server.Start();
                lblStatus.Text = $"Server started at {Url}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting server: {ex.Message}");
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                _server?.Stop();
                lblStatus.Text = "Server stopped.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error stopping server: {ex.Message}");
            }
        }

    }
}
