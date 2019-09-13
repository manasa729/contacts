using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Contacts
{
    class Authentication
    {      
        public ClientContext Credentials(string password)
        {                       
            SecureString securePassword = convertToSecurePassword(password);            
            using (var clientContext = new ClientContext("https://technoverg.sharepoint.com/sites/SpPractice"))
            {
                clientContext.Credentials = new SharePointOnlineCredentials(Constants.siteUrl, securePassword);
                Web web = clientContext.Web;
                clientContext.Load(web);
                clientContext.ExecuteQuery();
                
                return clientContext;
            }
        } 
        private static SecureString convertToSecurePassword(string password)
        {           
            var securePassword = new SecureString();
            //Convert string to secure string  
            foreach (char c in password)
            securePassword.AppendChar(c);
            securePassword.MakeReadOnly();
            return securePassword;
        }
    }                      
}
