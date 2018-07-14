using Api.Common.Base;
using Microsoft.AspNetCore.Http;

namespace Api.Common {

    public class FileUploadVM {
        public IFormFile File { get; set; }
        public string Source { get; set; }
        public long Size { get; set; }
        public string Extension { get; set; }
    }

}