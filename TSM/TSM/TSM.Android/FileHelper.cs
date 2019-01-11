using TSM.Droid;
using TSM.Services;
using Environment = System.Environment;

[assembly: Xamarin.Forms.Dependency(typeof(FileHelper))]

namespace TSM.Droid
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return System.IO.Path.Combine(path, filename);
        }
    }
}