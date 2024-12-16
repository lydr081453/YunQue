using System;
using System.Collections.Generic;
using System.Data;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;

namespace ESP.Purchase.BusinessLogic
{
    public static class FeedBackManager
    {
        private static FeedBackProvider dal = new FeedBackProvider();
        public static int Add(FeedBackInfo model)
        {
            return dal.Add(model);
        }
    }
}
