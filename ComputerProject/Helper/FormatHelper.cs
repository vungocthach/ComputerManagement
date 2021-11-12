﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ComputerProject
{
    public static class FormatHelper
    {
        public static String GetErrorMessage(String description, String errorCode)
        {
            return string.Format("{0}.{1}Mã lỗi: {2}", description, Environment.NewLine, errorCode);
        }

        /// <summary>
        /// Convert from "Bằng pro" to "Bang pro"
        /// </summary>
        /// <param name="vn_str">Text in Vietnammese</param>
        /// <returns>Text in Dong Lao's language</returns>
        public static string ConvertTo_TiengDongLao(string vn_str)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = vn_str.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public static string ToMoney(int val, bool hasCurrency = false)
        {
            var rs = hasCurrency ? val.ToString("N0") + "VND" : val.ToString("N0");
            return rs;
        }

        public static int CheckMoney(string val)
        {
            int temp;
            if (Int32.TryParse(val, System.Globalization.NumberStyles.AllowThousands, System.Globalization.CultureInfo.CurrentCulture, out temp) && temp > 0)
            {
                return temp;
            }
            else return 0;
        }

        public static byte[] ImageToBytes(Bitmap image)
        {
            if (image == null) return null;
            byte[] rs;

            float width = 192, height = 108;
            int h = image.Height, w = image.Width;
            var scale = Math.Min(width / w, height / h);
            var newSize = new Size((int)(w * scale), (int)(h * scale));

            image = new Bitmap(image, newSize);

            //image = Scale(image);

            using (var ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Jpeg);

                rs = ms.ToArray();
            }

            return rs;
        }

        public static Bitmap Scale(Bitmap image)
        {
            float width = 192 * 2, height = 108 * 2;
            int h = image.Height, w = image.Width;
            var scale = Math.Min(width / h, height / w);
            var newSize = new Size((int)(w * scale), (int)(h * scale));

            var bmp = new Bitmap((int)width, (int)height);
            var graph = Graphics.FromImage(bmp);

            graph.InterpolationMode = InterpolationMode.High;
            graph.CompositingQuality = CompositingQuality.HighQuality;
            graph.SmoothingMode = SmoothingMode.AntiAlias;

            graph.FillRectangle(new SolidBrush(Color.White), new RectangleF(0, 0, width, height));
            graph.DrawImage(image, newSize.Width / 2, newSize.Height / 2, newSize.Width, newSize.Height);

            return bmp;
        }

        public static System.Windows.Media.Imaging.BitmapImage BytesToImage(byte[] bytes)
        {
            if (bytes == null) return null;
            System.Windows.Media.Imaging.BitmapImage rs = new System.Windows.Media.Imaging.BitmapImage();

            using (var ms = new System.IO.MemoryStream(bytes))
            {
                rs.BeginInit();
                rs.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad; // important here
                rs.StreamSource = ms;
                rs.EndInit();
            }

            return rs;
        }

        public static Bitmap TranferToBitmap(byte[] bytecode)
        {
            if (bytecode == null) return null;
            Bitmap bm;
            using(var ms = new MemoryStream(bytecode))
            {
                ms.Write(bytecode, 0, bytecode.Length);
                ms.Seek(0, SeekOrigin.Begin);

                bm = new Bitmap(ms);
            }

            return bm;
        }
    }
}
