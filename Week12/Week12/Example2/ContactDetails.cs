using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Example2
{
    public partial class ContactDetails : Form
    {
        public ContactDetails()
        {
            InitializeComponent();
        }
        public ContactDetails(CreateContactCommand createContactCommand)
        {
            InitializeComponent();
            this.nameLabel.Text = createContactCommand.Name;
            this.phoneLabel.Text = createContactCommand.Phone;
            this.addressLabel.Text = createContactCommand.Address;
        }

        private void ShowContactForm_Load(object sender, EventArgs e)
        {

        }

        private void nameLabel_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
