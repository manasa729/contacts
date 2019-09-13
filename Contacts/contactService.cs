using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts
{
    class ContactService
    {
        Authentication authentication = new Authentication();       
        public ListItemCollection GetItems(string password)
        {
            var clientContext = authentication.Credentials(password);
            List contactsList = clientContext.Web.Lists.GetByTitle(Constants.contacts);
            CamlQuery query = new CamlQuery();
            query.ViewXml = "<View/>";
            ListItemCollection listItems = contactsList.GetItems(query);
            clientContext.Load(listItems);
            clientContext.ExecuteQuery();         
            return listItems;
        }

        public void AddingItem(string password,string contactName, string email, string department, string phone, string location)
        {
            var clientContext = authentication.Credentials(password);
            List contactList = clientContext.Web.Lists.GetByTitle(Constants.contacts);
            ListItemCreationInformation listCreationInformation = new ListItemCreationInformation();
            ListItem contactListItem = contactList.AddItem(listCreationInformation);
            contactListItem[Constants.contactName] = contactName;
            contactListItem[Constants.title] = contactName;
            contactListItem[Constants.email] = email;
            contactListItem[Constants.department] = department;
            contactListItem[Constants.phoneNumber] = phone;
            contactListItem[Constants.location] = location;
            contactListItem.Update();
            clientContext.ExecuteQuery();
        }

        public ListItem UpdateItem(int id,string password)
        {
            var clientContext= authentication.Credentials(password);
            List contactList = clientContext.Web.Lists.GetByTitle("Contacts");
            ListItem contactListItem = contactList.GetItemById(id);

            return contactListItem;
        }

        public void DeleteItem(int id, string password)
        {
            var clientContext = authentication.Credentials(password);
            List contactList = clientContext.Web.Lists.GetByTitle("Contacts");
            ListItem contactListItem = contactList.GetItemById(id);
            contactListItem.DeleteObject();
            clientContext.ExecuteQuery();
        }        
    }
}
