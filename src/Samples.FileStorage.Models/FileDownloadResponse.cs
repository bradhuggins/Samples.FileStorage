#region Using Statement
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace Samples.FileStorage.Models
{
    public class FileDownloadResponse
    {
        public string ErrorMessage { get; set; }

        public string Base64FileData { get; set; }

    }
}
