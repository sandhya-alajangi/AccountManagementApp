using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AccountManagementApp.Model.Models
{
    public class ReadingRequest
    {
        public IFormFile File { get; set; }

        [JsonIgnore]
        public string Path { get; set; }
    }
}