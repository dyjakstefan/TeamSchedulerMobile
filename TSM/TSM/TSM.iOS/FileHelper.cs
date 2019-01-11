using System;
using System.IO;
using TSM.iOS;
using TSM.Services;

[assembly: Xamarin.Forms.Dependency(typeof(FileHelper))]

namespace TSM.iOS
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            var fileName = "tsm.db3";
            var docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libFolder = Path.Combine(docFolder, "..", "Library");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, fileName);
        }
    }
}