using System;
using System.Collections.Generic;
using System.Text;
using ESP.Media.Access;
using ESP.Media.Entity;
using ESP.Media.DataAccess;

using ESP.Media.Access.Utilities;
namespace ESP.Media.BusinessLogic
{
    public class MediaattachmentsManager
    {
        public static MediaattachmentsInfo GetModel(int attachmentid)
        {
            if (attachmentid <= 0) return new MediaattachmentsInfo();
            return MediaattachmentsDataProvider.Load(attachmentid);
        }
    }
}
