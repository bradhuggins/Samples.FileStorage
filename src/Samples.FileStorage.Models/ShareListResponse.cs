
#region Using Statement
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace Samples.FileStorage.Models
{
    public class ShareListResponse
    {
        public List<string> shares { get; set; }

        public string share_url { get; set; } = "https://{base_url}/files/api/{source}{share}";

    }
}
