using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Dtos
{

    public class DocumentListDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string FilePath { get; set; }
        public string StudentNo { get; set; }
        public string DownloadLink { get; set; }
    }




}
