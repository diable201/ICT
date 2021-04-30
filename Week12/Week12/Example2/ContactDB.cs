using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Example2
{
    class ContactDB : IDataAccessLayer, IDisposable
    {

        SQLiteConnection connection = default(SQLiteConnection);
        readonly string cs = @"URI=file:test.db";
        public ContactDB()
        {
            connection = new SQLiteConnection(cs);
            connection.Open();
            //PrepareDB();
        }
        public void Dispose()
        {
            connection.Close();
        }
        public void ExecuteNonQuery(string commandText)
        {
            var cmd = new SQLiteCommand(connection);
            cmd.CommandText = commandText;
            cmd.ExecuteNonQuery();
        }
        private void PrepareDB()
        {
            //SQLiteConnection.CreateFile("test.db");
            ExecuteNonQuery("DROP TABLE IF EXISTS contacts");
            ExecuteNonQuery("CREATE TABLE contacts(id TEXT PRIMARY KEY, name TEXT, phone TEXT, address TEXT)");
        }
        public string CreateContact(ContactDTO contact)
        {
            var text = string.Format("INSERT INTO contacts(id, name, phone, address) " +
                "VALUES('{0}', '{1}', '{2}', '{3}');",
                contact.Id,
                contact.Name,
                contact.Phone,
                contact.Address);
            ExecuteNonQuery(text);
            MessageBox.Show("Contact Created!");
            return contact.Id;
        }

        public bool DeleteContactById(string id)
        {
            var command = string.Format("DELETE FROM contacts WHERE id = \"{0}\";", id);
            ExecuteNonQuery(command);
            MessageBox.Show("Contact Deleted!");
            return true;
        }

        public bool UpdateContact(ContactDTO contact)
        {
            var command = string.Format("UPDATE contacts SET id = " +
                "\"{0}\", name = \"{1}\", " +
                "phone = \"{2}\", " +
                "address = \"{3}\"  " +
                "WHERE id = \"{0}\"",
                contact.Id,
                contact.Name,
                contact.Phone,
                contact.Address);   
            ExecuteNonQuery(command);
            MessageBox.Show("Update of contact with id:" + contact.Id);
            return true;
        }

        public ContactDTO GetContactById(string name)
        {
            return null;
        }

        public List<ContactDTO> GetContactsByName(string name)
        {
            var selectSQL = string.Format("SELECT * FROM contacts WHERE name LIKE '%{0}%';", name);
            return GetContacts(selectSQL);
        }

        public List<ContactDTO> GetAllContacts()
        {
            var selectSQL = @"SELECT * FROM contacts;";
            return GetContacts(selectSQL);
        }

        public List<ContactDTO> GetAllContactsInPage(int offset, string property, string pattern, bool IsSorted)
        {
            string sortBy = "NULL";
            if (IsSorted) sortBy = property;
            string selectSQL = string.Format("SELECT * FROM contacts WHERE {0} " +
                "LIKE '%{2}%' ORDER BY (SELECT {3}) " +
                "LIMIT 5 OFFSET {1}", 
                property, offset, pattern, sortBy);
            return GetContacts(selectSQL);
        }
        public List<ContactDTO> GetContacts(string selectSQL)
        {
            List<ContactDTO> res = new List<ContactDTO>();
            using (SQLiteCommand command = new SQLiteCommand(selectSQL, connection))
            {
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var item = new ContactDTO
                    {
                        Id = reader.GetString(0),
                        Name = reader.GetString(1),
                        Phone = reader.GetString(2),
                        Address = reader.GetString(3)
                    };
                    res.Add(item);
                }
            }
            return res;
        }
    }
}
