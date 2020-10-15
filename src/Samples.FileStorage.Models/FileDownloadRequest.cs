#region Using Statement
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace Samples.FileStorage.Models
{
    public class FileDownloadRequest
    {
        public string Container { get; set; }

        public string Share { get; set; }

        public string Folder { get; set; }

        public string Filename { get; set; }

    }
}
