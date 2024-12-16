using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.IO.Compression;
/// <summary>
///ZipFile 的摘要说明
/// </summary>
/// 
namespace ESP.Salary.Utility
{
    public class ZipFileHelper
    {
        public ZipFileHelper()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public static string ZipFIle(byte[] filebuffer, string serverPath, string fileType, string extention)
        {
            byte[] bufferWrite = filebuffer;
            GZipStream gzCompressed;
            string filename = fileType + "_" + Guid.NewGuid().ToString() + extention;
            string filepath = serverPath + filename;
            FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write);
            fs.Write(bufferWrite, 0, bufferWrite.Length);
            fs.Close();
            fs = new FileStream(filepath + ".zip", FileMode.OpenOrCreate, FileAccess.Write);
            gzCompressed = new GZipStream(fs, CompressionMode.Compress);
            gzCompressed.Write(bufferWrite, 0, bufferWrite.Length);
            gzCompressed.Close();
            fs.Close();
            return filename + ".zip";
        }
    }
}