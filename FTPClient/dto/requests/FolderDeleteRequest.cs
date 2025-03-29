using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dto.requests
{
    public class FolderDeleteRequest
    {
        private string _folderPath;

        public string FolderPath { get => _folderPath; set => _folderPath = value; }
    }
}
