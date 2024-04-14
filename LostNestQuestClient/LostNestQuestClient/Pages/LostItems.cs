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
    public partial class LostItems : Form
    {
        public LostItems()
        {
            InitializeComponent();
        }

        private void LostItems_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'lostNestQuestDBDataSet.LostItems' table. You can move, or remove it, as needed.
            this.lostItemsTableAdapter.Fill(this.lostNestQuestDBDataSet.LostItems);

        }
    }
}
