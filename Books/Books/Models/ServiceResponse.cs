namespace Books.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class ServiceResponse
    {
        [JsonProperty("error")]
        public long Error { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("books")]
        public List<Book> Books { get; set; }
    }
}