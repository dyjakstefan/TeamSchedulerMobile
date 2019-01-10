using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TSM.Services
{
    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
    }
}
