using System;
using System.Linq;
using System.Windows.Forms;

namespace Example2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadContacts();
        }

        private BLL bll = default(BLL);
        private int currentPage = 0;
        string searchFor = "name";
        private bool sort = false;
        private void LoadContacts()
        {
            ContactDB contacts2 = new ContactDB();
            bll = new BLL(contacts2);
            UpdateView();
            bindingNavigator1.BindingSource = bindingSource1;
            dataGridView1.DataSource = bindingSource1;
        }

        private void UpdateView()
        {
            string text = toolStripTextBox1.Text;
            toolStripStatusLabel1.Text = bll.GetContactsInPage(currentPage, searchFor, text, sort).Count().ToString();
            bindingSource1.DataSource = bll.GetContactsInPage(currentPage, searchFor, text, sort);
        }

        private void SortByName()
        {
            string text = toolStripTextBox1.Text;
            string searchFor = "address";
            toolStripStatusLabel1.Text = bll.GetContactsInPage(currentPage, searchFor, text, sort).Count().ToString();
            bindingSource1.DataSource = bll.GetContactsInPage(currentPage, searchFor, text, sort);
        }

        private void AddContact(object sender, EventArgs e)
        {
            CreateContactForm createContactForm = new CreateContactForm();
            if (createContactForm.ShowDialog() == DialogResult.OK)
            {
                CreateContactCommand command = new CreateContactCommand();
                command.Name = createContactForm.nameTxtBx.Text;
                command.Phone = createContactForm.phoneTxtBx.Text;
                command.Address = createContactForm.addressTxtBx.Text;
                bll.CreateContact(command);
                UpdateView();
            }
        }

        private void DeleteContact(object sender, EventArgs e)
        {
            int row = Convert.ToInt32(bindingNavigatorPositionItem.Text) - 1;
            var text = dataGridView1.Rows[row].Cells[0].Value;
            bll.RemoveContact(text.ToString());
            bindingSource1.RemoveCurrent();
            UpdateView();
        }

        private void SearchContacts(object sender, EventArgs e)
        {
            currentPage = 0;
            UpdateView();
        }

        private void UpdateContact(object sender, EventArgs e)
        {
            int row = Convert.ToInt32(bindingNavigatorPositionItem.Text) - 1;
            var text = dataGridView1.Rows[row];
            ContactDTO contact = new ContactDTO();
            contact.Id = text.Cells[0].Value.ToString();
            contact.Name = text.Cells[1].Value.ToString();
            contact.Phone = text.Cells[2].Value.ToString();
            contact.Address = text.Cells[3].Value.ToString();
            bll.UpdateContact(contact);
        }

        private void PreviousPage(object sender, EventArgs e)
        {
            currentPage = Math.Max(0, currentPage - 5);
            UpdateView();
        }

        private void NextPage(object sender, EventArgs e)
        {
            currentPage = Math.Max(0, Math.Min(bll.GetContacts().Count, currentPage + 5));
            UpdateView();
        }

        private void DataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int row = Convert.ToInt32(bindingNavigatorPositionItem.Text) - 1;
            var text = dataGridView1.Rows[row];
            CreateContactCommand contact = new CreateContactCommand();
            contact.Name = text.Cells[1].Value.ToString();
            contact.Phone = text.Cells[2].Value.ToString();
            contact.Address = text.Cells[3].Value.ToString();
            ContactDetails contactDetails = new ContactDetails(contact);
            contactDetails.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Button7_Click(object sender, EventArgs e)
        {
            sort = !sort;
            UpdateView();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sort = !sort;
            SortByName();
        }
    }
}
