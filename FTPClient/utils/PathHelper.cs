using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTPClient.utils
{
    public static class PathHelper
    {
        public static string GetLastPartOfPath(string fullPath)
        {
            // Kiểm tra nếu chuỗi không rỗng
            if (string.IsNullOrEmpty(fullPath))
            {
                return string.Empty;
            }

            // Nếu đường dẫn kết thúc bằng '/', loại bỏ '/' ở cuối để lấy phần cuối cùng
            if (fullPath.EndsWith("/"))
            {
                fullPath = fullPath.TrimEnd('/');
            }

            // Tìm vị trí của dấu '/' cuối cùng
            int lastIndex = fullPath.LastIndexOf('/');

            // Nếu không tìm thấy dấu '/', nghĩa là toàn bộ chuỗi là tên file/thư mục
            if (lastIndex == -1)
            {
                return fullPath;
            }

            // Lấy chuỗi ký tự sau dấu '/' cuối cùng
            return fullPath.Substring(lastIndex + 1);
        }

        public static string GetPathBeforeLastSlash(string input)
        {
            // Tìm vị trí của dấu '/' cuối cùng
            int lastSlashIndex = input.LastIndexOf('/');

            // Nếu không có dấu '/' trong chuỗi, trả về chuỗi gốc
            if (lastSlashIndex == -1)
            {
                return input;
            }

            // Cắt chuỗi từ đầu đến vị trí của dấu '/'
            return input.Substring(0, lastSlashIndex);
        }

    }

}
