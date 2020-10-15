
#region Using Statement
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace Samples.FileStorage.Models
{
    public class FileListResponse
    {
        public List<string> files { get; set; }

        public string file_url { get; set; } = "https://{base_url}/files/api/{source}{share}/{folder}/{filename}";

    }
}
