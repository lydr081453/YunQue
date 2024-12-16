using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Configuration;

namespace ModelTemplate.BLL
{
   public class ModelProcessFactory
    {
          private static readonly string path = System.Configuration.ConfigurationSettings.AppSettings["Template"];
         
        private ModelProcessFactory() { }

        public static ModelTemplate.IDAL.IModelProcess CreateModelProcess()
        {
            string className = path + ".ModelProcess";
            return (ModelTemplate.IDAL.IModelProcess)Assembly.Load(path).CreateInstance(className);
        }
    }
}
