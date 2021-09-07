using System;
using System.IO;

namespace Chatterin.Services
{
    public class IosFileService : FileService
    {
        public IosFileService()
        {
        }

        public override string DbFilePath => Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                "db.sqlite");
    }
}
