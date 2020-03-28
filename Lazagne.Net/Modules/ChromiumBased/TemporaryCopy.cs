using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lazagne.Net.Modules.ChromiumBased
{
    class TemporaryCopy : IDisposable
    {
        public string TempPath { get; private set; }

        public TemporaryCopy(string path)
        {
            TempPath = Path.GetTempFileName();
            File.Copy(path, TempPath, true);
        }

        public void Dispose()
        {
            if (File.Exists(TempPath))
                File.Delete(TempPath);
        }
    }
}
