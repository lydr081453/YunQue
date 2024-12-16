using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ESP.MediaLinq.Utilities
{
    public class SaveFIleHelper
    {
        public static string SaveFile(string path, string name, byte[] data, bool checkdic)
        {
            string fname = string.Empty;//name;
            fname = name.Substring(name.LastIndexOf('\\') + 1);
            string serverpath = name.Substring(0, name.LastIndexOf('\\'));
            if (checkdic)
            {
                if (!Directory.Exists(serverpath))
                    Directory.CreateDirectory(serverpath);
            }
            if (File.Exists(name))
            {
                fname = Guid.NewGuid().ToString() + fname;
            }
            FileStream fs = new FileStream(serverpath + "\\" + fname, FileMode.CreateNew);
            fs.Write(data, 0, data.Length);
            fs.Close();

            return path + fname;
        }
    }
}
