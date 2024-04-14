using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LostNestQuestHost
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ServiceHost sh = null;
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // Specify the base address for the service
                Uri tcpa = new Uri("net.tcp://localhost:8000/LostNestQuestService");

                // Create a new instance of the service host
                sh = new ServiceHost(typeof(LostNestQuestService.Services.LostNestQuestService), tcpa);

                // Create NetTcpBinding instance
                NetTcpBinding tcpb = new NetTcpBinding();

                // Add a service endpoint for HttpBinding (for metadata exchange)
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                sh.Description.Behaviors.Add(smb);


                // Define the base address for HTTP metadata exchange
                sh.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexTcpBinding(), "mex");

                // Add a service endpoint for the service contract
                sh.AddServiceEndpoint(typeof(LostNestQuestService.Contracts.ILostNestQuestService), tcpb, tcpa);
                

                // Open the service host
                sh.Open();

                // Update UI to indicate service is running
                label1.Text = "All Services are running at..." + tcpa ;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error starting service: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Close the service host when the form is closing
            if (sh != null && sh.State == CommunicationState.Opened)
            {
                sh.Close();
            }
        }
    }
}
