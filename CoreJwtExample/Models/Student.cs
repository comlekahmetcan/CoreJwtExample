using Microsoft.AspNetCore.Http;

namespace CoreJwtExample.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Roll { get; set; }
        public IFormFile Files { get; set; } = null;
        public byte[] ImgByte { get; set; }
        public string Message { get; set; }
    }
}
