using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTPClient.dto.requests
{
    public class FileCopyRequest
    {
        // Cần copy file nào?
        private string _filePath;

        // Copy vào thư mục nào?
        private string _folderPath;

        public string FilePath { get => _filePath; set => _filePath = value; }

        public string FolderPath { get => _folderPath; set => _folderPath = value; }
    }
}
