using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.IO.Compression;
/// <summary>
///ZipFile 的摘要说明
/// </summary>
/// 
namespace ESP.Finance.Utility
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
            // string strfilename = fileType + Guid.NewGuid().ToString() + ".zip";
            // byte[] bufferWrite;
            // // Will open the file to be compressed
            // //FileStream fsSource;
            // // Will write the new zip file
            // FileStream fsDest;
            // GZipStream gzCompressed;
            //// fsSource = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            // // Set the buffer size to the size of the file
            // bufferWrite = filebuffer;
            // // Read the data from the stream into the buffer
            // //fsSource.Read(bufferWrite, 0, bufferWrite.Length);
            // // Open the FileStream to write to
            // fsDest = new FileStream(serverPath+strfilename, FileMode.OpenOrCreate, FileAccess.Write);
            // // Will hold the compressed stream created from the destination stream
            // gzCompressed = new GZipStream(fsDest, CompressionMode.Compress, true);
            // // Write the compressed stream from the bytes array to a file
            // gzCompressed.Write(bufferWrite, 0, bufferWrite.Length);
            // // Close the streams
            // //fsSource.Close();
            // gzCompressed.Close();
            // fsDest.Close();
            // return strfilename;

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