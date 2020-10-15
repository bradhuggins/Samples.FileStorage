
#region Using Statement
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace Samples.FileStorage.Models
{
    public class ContainerListResponse
    {
        public List<string> containers { get; set; } = new List<string>(){ "native","azure","sql" };

        public string container_url { get; set; } = "https://{base_url}/files/api/{source}";

    }
}
