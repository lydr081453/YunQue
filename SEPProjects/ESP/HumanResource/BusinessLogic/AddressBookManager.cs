using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.HumanResource.Entity;

namespace ESP.HumanResource.BusinessLogic
{
    public class AddressBookManager
    {
        private static ESP.HumanResource.DataAccess.AddressBookDataProvider dataProvider = new ESP.HumanResource.DataAccess.AddressBookDataProvider();
        public static IList<AddressItemInfo> GetData(int id, int parentId)
        {
            return dataProvider.GetData(id, parentId);
        }
        public static int Add(AddressBookInfo addressBook)
        {
            return dataProvider.Add(addressBook);
        }
        public static IList<AddressBookInfo> GetList(string where) 
        {
            return dataProvider.GetList(where);
        }
        public static bool AddItem(int id) 
        {
            return dataProvider.AddItem(id);
        }
        public static bool DeleteAll(int id) 
        {
            return dataProvider.DeleteAll(id);
        }
    }
}
