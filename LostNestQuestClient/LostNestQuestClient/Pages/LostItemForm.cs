using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LostNestQuestClient;
using LostNestQuestClient.HTTP;

namespace LostNestQuestClient.Pages
{
    public partial class LostItemForm : Form
    {
        public LostItemForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string description = textBox2.Text;
            string location = textBox3.Text;
            string contactNumber = textBox4.Text;

            // Convert image to byte array
            byte[] imageBytes = null;
            if (pictureBox1.Image != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    pictureBox1.Image.Save(ms, ImageFormat.Jpeg); // Save the image to a memory stream
                    imageBytes = ms.ToArray(); // Convert the image to a byte array
                }
            }

            // Create a LostItem object
            HTTP.LostItem lostItem = new HTTP.LostItem
            {
                Image = imageBytes,
                Name = name,
                Description = description,
                Location = location,
                ContactNumber = contactNumber
            };

            // Call the ReportLostItem method from the service class to add the new item to the database
            HTTP.LostNestQuestServiceClient client1 = new HTTP.LostNestQuestServiceClient();

            string result = client1.ReportLostItem(lostItem);

            // Display a message box indicating the result
            MessageBox.Show(result, "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Set the selected image file path to the PictureBox control
                pictureBox1.ImageLocation = openFileDialog.FileName;
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Homepage hp = new Homepage();
            hp.Show();
            this.Hide();
        }
    }
}
