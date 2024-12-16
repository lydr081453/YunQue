using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.MediaLinq.SqlDataAccess;
namespace ESP.MediaLinq.BusinessLogic
{
    public class BaseEntityInfo
    {
            protected static MediaLinqDataContext dc = new MediaLinqDataContext("Data Source=172.16.11.208;Initial Catalog=Media2;User ID=sa;Password=sa");

           // protected static ESP.MediaLinq.Entity.Media2Entities mdc = new ESP.MediaLinq.Entity.Media2Entities("Data Source=172.16.11.208;Initial Catalog=Media2;User ID=sa;Password=sa"); 

           // protected static ESP.MediaLinq.Entity.Media2Entities mdc = new ESP.MediaLinq.Entity.Media2Entities();

            protected  const string ConnectionStringSettings = "Media2Entities";
    }
}
