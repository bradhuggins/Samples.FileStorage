#region Using Statement
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace Samples.FileStorage.Models
{
    public class FileUploadRequest
    {
        //public string Source { get; set; }

        //public string Share { get; set; }

        //public string Folder { get; set; }

        //public string Filename { get; set; }

        public string InputFilename { get; set; }

        public string Base64FileData { get; set; }

    }
}
