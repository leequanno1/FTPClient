using dto.dbdto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dto.responses
{
    public class ListResponse
    {
        private List<CompositeItemDTO> folders; 
        private List<CompositeItemDTO> files;

        internal List<CompositeItemDTO> Folders { get => folders; set => folders = value; }
        internal List<CompositeItemDTO> Files { get => files; set => files = value; }
    }
}
