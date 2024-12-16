using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace ESP.Web
{
    /// <summary>
    /// CAPTCHA 图像生成类
    /// </summary>
    public class CaptchaImage
    {
        #region Fields
        private static string[] _fontList = {
                                     "arial","arial black","comic sans ms","courier new",
                                     "estrangelo edessa","franklin gothic medium","georgia",
                                     "lucida console","lucida sans unicode","mangal",
                                     "microsoft sans serif","palatino linotype",
                                     "sylfaen","tahoma","times new roman","trebuchet ms","verdana"
                                 };

        private BackgroundNoiseLevel _backgroundNoise = BackgroundNoiseLevel.Low;
        private FontWarpFactor _fontWarp = FontWarpFactor.Low;
        private LineNoiseLevel _lineNoise = LineNoiseLevel.Low;
        private int _width = 80;
        private int _height = 30;
        private Random _rand;

        private const string RANDOM_TEXT_CHARS = "ACDEFGHJKLNPQRTUVXYZ2346789";
        private const int RANDOM_TEXT_LENGTH = 4;
        #endregion

        #region Private Methods
        /// <summary>
        /// 生成一个随机的CAPTCHA文本数组。
        /// </summary>
        /// <returns>随机的CAPTCHA文本数组。</returns>
        private char[] CreateRandomText()
        {
            char[] text = new char[RANDOM_TEXT_LENGTH];
            for (int i = RANDOM_TEXT_LENGTH - 1; i >= 0; i--)
            {
                text[i] = RANDOM_TEXT_CHARS[_rand.Next(RANDOM_TEXT_CHARS.Length)];
            }

            return text;
        }

        private void AddLine(Graphics graphics1, Rectangle rect)
        {
            int length;
            int linecount;
            float width;
            switch (this._lineNoise)
            {
                case LineNoiseLevel.Low:
                    length = 4;
                    width = Convert.ToSingle((double)(((double)this._height) / 31.25));
                    linecount = 1;
                    break;

                case LineNoiseLevel.Medium:
                    length = 5;
                    width = Convert.ToSingle((double)(((double)this._height) / 27.7777));
                    linecount = 1;
                    break;

                case LineNoiseLevel.High:
                    length = 3;
                    width = Convert.ToSingle((double)(((double)this._height) / 25.0));
                    linecount = 2;
                    break;

                case LineNoiseLevel.Extreme:
                    length = 3;
                    width = Convert.ToSingle((double)(((double)this._height) / 22.7272));
                    linecount = 3;
                    break;
                case LineNoiseLevel.None:
                default:
                    return;

            }
            PointF[] pf = new PointF[length + 1];
            Pen p = new Pen(Color.Black, width);
            for (int l = 1; l <= linecount; l++)
            {
                for (int i = 0; i <= length; i++)
                {
                    pf[i] = this.RandomPoint(rect);
                }
                graphics1.DrawCurve(p, pf, 1.75f);
            }
            p.Dispose();
        }

        private void AddNoise(Graphics graphics, Rectangle rect)
        {
            int density;
            int size;
            switch (this._backgroundNoise)
            {
                case BackgroundNoiseLevel.Low:
                    density = 30;
                    size = 40;
                    break;

                case BackgroundNoiseLevel.Medium:
                    density = 0x12;
                    size = 40;
                    break;

                case BackgroundNoiseLevel.High:
                    density = 0x10;
                    size = 0x27;
                    break;

                case BackgroundNoiseLevel.Extreme:
                    density = 12;
                    size = 0x26;
                    break;
                case BackgroundNoiseLevel.None:
                default:
                    return;
            }
            SolidBrush br = new SolidBrush(Color.Black);
            int max = Convert.ToInt32((double)(((double)Math.Max(rect.Width, rect.Height)) / ((double)size)));
            int n = Convert.ToInt32((double)(((double)(rect.Width * rect.Height)) / ((double)density)));
            for (int i = 0; i <= n; i++)
            {
                graphics.FillEllipse(br, this._rand.Next(rect.Width), this._rand.Next(rect.Height), this._rand.Next(max), this._rand.Next(max));
            }
            br.Dispose();
        }

        private Bitmap GenerateImagePrivate(out char[] text)
        {
            this._rand = new Random();

            text = CreateRandomText();

            Font fnt = null;
            Bitmap bmp = new Bitmap(this._width, this._height, PixelFormat.Format32bppArgb);
            Graphics gr = Graphics.FromImage(bmp);
            gr.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, this._width, this._height);
            Brush br = new SolidBrush(Color.White);
            gr.FillRectangle(br, rect);
            int charOffset = 0;
            double charWidth = ((double)this._width) / ((double)text.Length);
            int index = 0;
            int randomTextLenght = text.Length;
            while (index < randomTextLenght)
            {
                char c = text[index];
                fnt = this.GetFont();
                Rectangle rectChar = new Rectangle(Convert.ToInt32((double)(charOffset * charWidth)), 0, Convert.ToInt32(charWidth), this._height);
                GraphicsPath gp = this.TextPath(c.ToString(), fnt, rectChar);
                this.WarpText(gp, rectChar);
                br = new SolidBrush(Color.Black);
                gr.FillPath(br, gp);
                charOffset++;
                index++;
            }
            this.AddNoise(gr, rect);
            this.AddLine(gr, rect);
            fnt.Dispose();
            br.Dispose();
            gr.Dispose();
            return bmp;
        }

        private Font GetFont()
        {
            float fsize;
            switch (this.FontWarp)
            {
                case FontWarpFactor.Low:
                    fsize = Convert.ToInt32((double)(this._height * 0.8));
                    break;

                case FontWarpFactor.Medium:
                    fsize = Convert.ToInt32((double)(this._height * 0.85));
                    break;

                case FontWarpFactor.High:
                    fsize = Convert.ToInt32((double)(this._height * 0.9));
                    break;

                case FontWarpFactor.Extreme:
                    fsize = Convert.ToInt32((double)(this._height * 0.95));
                    break;

                case FontWarpFactor.None:
                default:
                    fsize = Convert.ToInt32((double)(this._height * 0.7));
                    break;
            }

            string fname = this.RandomFontFamily();

            if (fname == null)
                fname = FontFamily.GenericSerif.Name;

            try
            {
                return new Font(fname, fsize, FontStyle.Bold);
            }
            catch
            {
                return new Font(FontFamily.GenericSerif.Name, fsize, FontStyle.Bold);
            }
        }

        private string RandomFontFamily()
        {
            if (_fontList == null || _fontList.Length == 0)
                return null;

            return _fontList[this._rand.Next(0, _fontList.Length)];
        }

        private PointF RandomPoint(Rectangle rect)
        {
            return this.RandomPoint(rect.Left, rect.Width, rect.Top, rect.Bottom);
        }

        private PointF RandomPoint(int xmin, int xmax, int ymin, int ymax)
        {
            return new PointF((float)this._rand.Next(xmin, xmax), (float)this._rand.Next(ymin, ymax));
        }


        private GraphicsPath TextPath(string s, Font f, Rectangle r)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Near;
            GraphicsPath gp = new GraphicsPath();
            gp.AddString(s, f.FontFamily, (int)f.Style, f.Size, r, sf);
            return gp;
        }

        private void WarpText(GraphicsPath textPath, Rectangle rect)
        {
            float RangeModifier;
            float WarpDivisor;
            switch (this._fontWarp)
            {
                case FontWarpFactor.Low:
                    WarpDivisor = 6f;
                    RangeModifier = 1f;
                    break;

                case FontWarpFactor.Medium:
                    WarpDivisor = 5f;
                    RangeModifier = 1.3f;
                    break;

                case FontWarpFactor.High:
                    WarpDivisor = 4.5f;
                    RangeModifier = 1.4f;
                    break;

                case FontWarpFactor.Extreme:
                    WarpDivisor = 4f;
                    RangeModifier = 1.5f;
                    break;

                case FontWarpFactor.None:
                default:
                    return;
            }
            RectangleF rectF = new RectangleF(Convert.ToSingle(rect.Left), 0f, Convert.ToSingle(rect.Width), (float)rect.Height);
            int hrange = Convert.ToInt32((float)(((float)rect.Height) / WarpDivisor));
            int wrange = Convert.ToInt32((float)(((float)rect.Width) / WarpDivisor));
            int left = rect.Left - Convert.ToInt32((float)(wrange * RangeModifier));
            int top = rect.Top - Convert.ToInt32((float)(hrange * RangeModifier));
            int width = (rect.Left + rect.Width) + Convert.ToInt32((float)(wrange * RangeModifier));
            int height = (rect.Top + rect.Height) + Convert.ToInt32((float)(hrange * RangeModifier));
            if (left < 0)
            {
                left = 0;
            }
            if (top < 0)
            {
                top = 0;
            }
            if (width > this.Width)
            {
                width = this.Width;
            }
            if (height > this.Height)
            {
                height = this.Height;
            }

            PointF leftTop = RandomPoint(left, left + wrange, top, top + hrange);
            PointF rightTop = RandomPoint(width - wrange, width, top, top + hrange);
            PointF leftBottom = RandomPoint(left, left + wrange, height - hrange, height);
            PointF rightBottom = RandomPoint(width - wrange, width, height - hrange, height);

            PointF[] points = new PointF[] { leftTop, rightTop, leftBottom, rightBottom };
            Matrix m = new Matrix();
            m.Translate(0f, 0f);
            textPath.Warp(points, rectF, m, WarpMode.Perspective, 0f);
        }
        #endregion

        /// <summary>
        /// 生成图像
        /// </summary>
        /// <param name="text">文本字符数组。</param>
        /// <returns>生成的图像。</returns>
        public Bitmap RenderImage(out char[] text)
        {
            return this.GenerateImagePrivate(out text);
        }

        /// <summary>
        /// 背景噪音幅度
        /// </summary>
        public BackgroundNoiseLevel BackgroundNoise
        {
            get
            {
                return this._backgroundNoise;
            }
            set
            {
                this._backgroundNoise = value;
            }
        }

        /// <summary>
        /// 扭曲幅度
        /// </summary>
        public FontWarpFactor FontWarp
        {
            get
            {
                return this._fontWarp;
            }
            set
            {
                this._fontWarp = value;
            }
        }

        /// <summary>
        /// 可供选取的字体列表
        /// </summary>
        public static string[] Fontlist
        {
            get
            {
                string[] copy = new string[_fontList.Length];
                _fontList.CopyTo(copy, 0);
                return copy;
            }
            set
            {
                if (value == null || value.Length == 0)
                {
                    _fontList = null;
                }
                else
                {
                    string[] copy = new string[value.Length];
                    value.CopyTo(copy, 0);
                    _fontList = copy;
                }
            }
        }

        /// <summary>
        /// 图像高度（大于等于30，小于等于500）
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// 值超出范围，必须大于等于30且小于等于500
        /// </exception>
        public int Height
        {
            get
            {
                return this._height;
            }
            set
            {
                if (value <= 30 || value >=500)
                {
                    throw new ArgumentOutOfRangeException("height", value, "Height must be greater than 30 and less than 500.");
                }
                this._height = value;
            }
        }

        /// <summary>
        /// 图像宽度（大于等于60，小于等于1000）
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// 值超出范围，必须大于等于30且小于等于1000
        /// </exception>
        public int Width
        {
            get
            {
                return this._width;
            }
            set
            {
                if (value <= 60 || value >= 1000)
                {
                    throw new ArgumentOutOfRangeException("width", value, "Width must be greater than 60 and less than 1000.");
                }
                this._width = value;
            }
        }

        /// <summary>
        /// 随机线条幅度
        /// </summary>
        public LineNoiseLevel LineNoise
        {
            get
            {
                return this._lineNoise;
            }
            set
            {
                this._lineNoise = value;
            }
        }

        /// <summary>
        /// 背景噪音幅度
        /// </summary>
        public enum BackgroundNoiseLevel
        {
            /// <summary>
            /// 无
            /// </summary>
            None,

            /// <summary>
            /// 低
            /// </summary>
            Low,

            /// <summary>
            /// 中
            /// </summary>
            Medium,

            /// <summary>
            /// 高
            /// </summary>
            High,

            /// <summary>
            /// 非常高
            /// </summary>
            Extreme
        }

        /// <summary>
        /// 扭曲幅度
        /// </summary>
        public enum FontWarpFactor
        {
            /// <summary>
            /// 无
            /// </summary>
            None,

            /// <summary>
            /// 低
            /// </summary>
            Low,

            /// <summary>
            /// 中
            /// </summary>
            Medium,

            /// <summary>
            /// 高
            /// </summary>
            High,

            /// <summary>
            /// 非常高
            /// </summary>
            Extreme
        }

        /// <summary>
        /// 随机线条幅度
        /// </summary>
        public enum LineNoiseLevel
        {
            /// <summary>
            /// 无
            /// </summary>
            None,

            /// <summary>
            /// 低
            /// </summary>
            Low,

            /// <summary>
            /// 中
            /// </summary>
            Medium,

            /// <summary>
            /// 高
            /// </summary>
            High,

            /// <summary>
            /// 非常高
            /// </summary>
            Extreme
        }
    }
}
