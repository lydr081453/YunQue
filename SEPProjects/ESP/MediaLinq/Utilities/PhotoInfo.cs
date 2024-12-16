using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Goheer.EXIF;

namespace ESP.MediaLinq.Utilities
{
    public class PhotoInfo
    {
        private EXIFextractor _exif;
        public EXIFextractor exif
        {
            get { return _exif; }
            set { _exif = value; }
        }
        private PhotoSize _size;
        public PhotoSize size
        {
            get { return _size; }
            set { _size = value; }
        }
        private string _filename;
        public string filename
        {
            get { return _filename; }
            set { _filename = value; }
        }

        public PhotoInfo()
        {
            size = new PhotoSize(0, 0);
            filename = "";
        }
    }

    public class PhotoSize
    {
        private int _width;
        private int _height;
        public int width
        {
            get { return _width; }
            set { _width = value; }
        }
        public int height
        {
            get { return _height; }
            set { _height = value; }
        }

        public PhotoSize(int w, int h)
        {
            width = w;
            height = h;
        }
    }
}

