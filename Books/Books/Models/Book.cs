namespace Books.Models
{
    using Newtonsoft.Json;
    using System;

    public class Book
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("subtitle")]
        public string Subtitle { get; set; }

        [JsonProperty("isbn13")]
        public string Isbn13 { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("image")]
        public Uri Image { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }
}