
#region Using Statement
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace Samples.FileStorage.Models
{
    public class FolderListResponse
    {
        public List<string> folders { get; set; }

        public string folder_url { get; set; } = "https://{base_url}/files/api/{source}{share}/{folder}";

    }
}
