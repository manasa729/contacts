using Microsoft.SharePoint.Client;
using System;
using System.Security;
using ConsoleTables;

namespace Contacts
{
    class Program
    {
        static void Main(string[] args)
        {                           
            Authentication authentication = new Authentication();
            Console.WriteLine(Constants.passWord);
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        password = password.Substring(0, password.Length - 1);
                        int pos = Console.CursorLeft;
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }            
            authentication.Credentials(password);            
            PrintContent("Authentication Process is done");
            ContactService contactService = new ContactService();            
            while (true)
                {
                    var listItems = contactService.GetItems(password);
                    Console.WriteLine(Constants.availableContacts);
                    foreach (ListItem list in listItems)
                    {                        
                        Console.WriteLine("{0,0} {1,-10} {2,-10} {3,-10} {4,-8} {5,-10}",
                            list[Constants.id].ToString(), 
                            list[Constants.contactName].ToString() ,
                            list[Constants.department].ToString(),
                            list[Constants.phoneNumber].ToString() ,
                            list[Constants.email].ToString() ,
                            list[Constants.location].ToString()
                        );
                    }
                    PrintContent(Constants.menuToPerformOperations);
                    int.TryParse(Console.ReadLine(), out int selectedOption);
                    if (selectedOption == 1)
                    {
                        PrintContent(Constants.requiredInformation);
                        PrintContent(Constants.contactName);
                        string contactName = Console.ReadLine();
                        PrintContent(Constants.department);
                        string department = Console.ReadLine();
                        PrintContent(Constants.phoneNumber);
                        string phone = Console.ReadLine();
                        PrintContent(Constants.email);
                        string email = Console.ReadLine();
                        PrintContent(Constants.location);
                        string location = Console.ReadLine();
                        contactService.AddingItem(password,contactName, department,phone,email,location);
                        
                    }
                    else if (selectedOption == 2)
                    {
                        var clientContext = authentication.Credentials(password);
                        PrintContent(Constants.enterIdToDelete);
                        int.TryParse(Console.ReadLine(), out int id);
                        var contactListItem = contactService.UpdateItem(id, password);
                        while (true)
                        {
                            PrintContent(Constants.field);
                            string field = Console.ReadLine();
                            if (field != "-1")
                            {
                                PrintContent(Constants.changedValue);
                                string updatedValue = Console.ReadLine();
                                contactListItem[field] = updatedValue;
                                contactListItem.Update();
                                clientContext.ExecuteQuery();
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    else if(selectedOption==3){
                        PrintContent(Constants.enterId);
                        int.TryParse(Console.ReadLine(), out int id);
                        contactService.DeleteItem(id, password);                                        
                    }

                    else
                    {
                        break;
                    }                
                }
        }

        public static void PrintContent(string text)
        {
            Console.WriteLine(text);
        }
    }
}
