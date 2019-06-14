using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using System.IO;
using System.Linq;
namespace Lab19.EventReceiver
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class EventReceiver : SPItemEventReceiver
    {

        /// <summary>
        /// An item is being added.
        /// </summary>
        /// 
        public override void ItemAdded(SPItemEventProperties properties)
        {
            

            using (SPSite siteCollection = new SPSite("http://mysites.innotech.cloud/sites/edmonton"))
            {

                
                //Collection of lists fom the Site
                SPWeb topSite = siteCollection.RootWeb;
                SPList list1 = topSite.Lists["NotificationEnabled"];
                SPListItemCollection items1 = list1.Items;
                int c = 0;
                int[] arrayitems = new int[list1.ItemCount];

                foreach (SPListItem item in items1) //Email recipients list                       
                {
                    arrayitems[c] = item.ID;   
                    c++;
                }
     
                int maxValue = arrayitems.Max();
                var itemID = maxValue;

                SPListItem notificationEnabled = list1.GetItemByIdSelectedFields(itemID, "EmailRecipient");                
                var list1EmailReceipt = notificationEnabled["EmailRecipient"];

                SPList list2 = topSite.Lists["Email Recipients"];
                SPListItemCollection items = list2.Items;

                //Iterate through each list item on both lists to find matching list items!
                using (StreamWriter w = File.AppendText(@"C:\\Testing" + "\\" + "Log_File_Name.txt"))
                {
                        
                        foreach (SPListItem item in items) //Email recipients list                       
                        {

                            SPListItem emailRecipient = list2.GetItemByIdSelectedFields(item.ID, "EmailAddress");
                            var list2EmailReceipt = emailRecipient["EmailAddress"];

                            if (list2EmailReceipt != null)
                            {
                                if (list1EmailReceipt.ToString().Trim().ToUpper() == (list2EmailReceipt.ToString().Trim().ToUpper()))
                                {
                           
                                    w.WriteLine("An email would have been sent to " + item["EmailAddress"].ToString() + " at {0} {1} ", DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString());
                                }
                                 
                            }
                            
                        }
                    
                }
            }
        }

        public override void ItemUpdated(SPItemEventProperties properties)
        {
            using (SPSite siteCollection = new SPSite("http://mysites.innotech.cloud/sites/edmonton"))
            {


                //Collection of lists fom the Site
                SPWeb topSite = siteCollection.RootWeb;
                SPList list1 = topSite.Lists["NotificationEnabled"];
                SPListItemCollection items1 = list1.Items;
                int c = 0;
                int[] arrayitems = new int[list1.ItemCount];

                foreach (SPListItem item in items1) //Email recipients list                       
                {
                    arrayitems[c] = item.ID;
                    c++;
                }

                int maxValue = arrayitems.Max();
                var itemID = maxValue;

                SPListItem notificationEnabled = list1.GetItemByIdSelectedFields(itemID, "EmailRecipient");
                var list1EmailReceipt = notificationEnabled["EmailRecipient"];

                SPList list2 = topSite.Lists["Email Recipients"];
                SPListItemCollection items = list2.Items;

                //Iterate through each list item on both lists to find matching list items!
                using (StreamWriter w = File.AppendText(@"C:\\Testing" + "\\" + "Log_File_Name.txt"))
                {

                    foreach (SPListItem item in items) //Email recipients list                       
                    {

                        SPListItem emailRecipient = list2.GetItemByIdSelectedFields(item.ID, "EmailAddress");
                        var list2EmailReceipt = emailRecipient["EmailAddress"];

                        if (list2EmailReceipt != null)
                        {
                            if (list1EmailReceipt.ToString().Trim().ToUpper() == (list2EmailReceipt.ToString().Trim().ToUpper()))
                            {

                                w.WriteLine("An email would have been sent to " + item["EmailAddress"].ToString() + " at {0} {1} ", DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString());
                            }

                        }

                    }

                }
            }
        }  
    }
}
    
