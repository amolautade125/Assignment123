using DemoDI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDI.BL.Interfaces
{
    public interface IContactService
    {
        List<Contact> GetAllContacts();
        Contact GetContactById(int id);
        Contact AddOrUpdateContact(Contact objContact);
        bool DeleteContactById(int id);
    }
}
