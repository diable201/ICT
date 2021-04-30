using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example2
{
    class ContactDBMock : IDataAccessLayer
    {
        List<ContactDTO> contacts = new List<ContactDTO>();
        public string CreateContact(ContactDTO contact)
        {
            contacts.Add(contact);
            return contact.Id;
        }

        public bool DelecteContactById(string id)
        {
            ContactDTO contact = contacts.Find(x => x.Id == id);
            if (contact != null)
            {
                contacts.Remove(contact);
                return true;
            }
            return false;
        }

        public bool DeleteContactById(string id)
        {
            throw new NotImplementedException();
        }

        public List<ContactDTO> GetAllContacts()
        {
            return contacts;
        }

        public List<ContactDTO> GetAllContactsInPage(int offset, string name)
        {
            throw new NotImplementedException();
        }

        public List<ContactDTO> GetAllContactsInPage(int offset, string name, bool IsSorted)
        {
            throw new NotImplementedException();
        }

        public List<ContactDTO> GetAllContactsInPage(int offset, string property, string pattern, bool IsSorted)
        {
            throw new NotImplementedException();
        }

        public ContactDTO GetContactById(string id)
        {
            return contacts.Find(x => x.Id == id);
        }

        public List<ContactDTO> GetContactsByName(string name)
        {
            throw new NotImplementedException();
        }

        public bool UpdateContact(ContactDTO contact)
        {
            throw new NotImplementedException();
        }
    }
}
