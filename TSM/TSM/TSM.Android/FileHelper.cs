using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using TSM.Services;
using Environment = System.Environment;

namespace TSM.Droid
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            var path = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return System.IO.Path.Combine(path, filename);
        }
    }
}