using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LostNestQuestClient.Pages
{
    public partial class FoundItems : Form
    {
        public FoundItems()
        {
            InitializeComponent();
        }

        private void FoundItems_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'lostNestQuestDBDataSet1.FoundItems' table. You can move, or remove it, as needed.
            this.foundItemsTableAdapter.Fill(this.lostNestQuestDBDataSet1.FoundItems);

        }
    }
}
