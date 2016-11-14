/* Copyright (c) 2016 Qianzhan Information Lim. Co. All rights reserved.
 * Contributor: Sha Jianjian
 * 2016
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Reflection;

namespace QZ.Foundation.Utility
{
    public class FileHelper
    {
        private static IDictionary<string, ImageFormat> _imageFormats = GetImageFormats();
        public static IDictionary<string, ImageFormat> ImageFormats { get { return _imageFormats; } }
        public static string Ext_Get(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image img = Image.FromStream(ms);
            foreach (var pair in ImageFormats)
            {
                if (pair.Value.Guid == img.RawFormat.Guid)
                    return pair.Key;
            }
            return null;
        }


        private static IDictionary<string, ImageFormat> GetImageFormats()
        {
            var dict = new Dictionary<string, ImageFormat>();
            var properties = typeof(ImageFormat).GetProperties(BindingFlags.Static | BindingFlags.Public);
            foreach (var p in properties)
            {
                var fmt = p.GetValue(null, null) as ImageFormat;
                if (fmt != null)
                    dict.Add(("." + p.Name).ToLower(), fmt);
            }
            return dict;
        }
    }
}
