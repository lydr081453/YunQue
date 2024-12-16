using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelTemplate
{
   public class TransferFactory
    {
       public static ITransfer CreateTransfer(string transfername)
       {
           object h = null;

           try
           {
               h = Activator.CreateInstance(Type.GetType(transfername));
           }
           catch (Exception e)
           {
               throw new Exception("It could not create instance named " + transfername, e);

           }
           return (ITransfer)h;

       }

    }
}
