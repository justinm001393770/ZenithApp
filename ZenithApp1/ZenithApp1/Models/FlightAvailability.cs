using Newtonsoft.Json;
using System;

namespace ZenithApp1.Models
{
    public class FlightAvailability
    {
        [JsonProperty(PropertyName = "chatTextId")]
        public long ChatTextId { get; set; }

        [JsonProperty(PropertyName = "chatId")]
        public long ChatId { get; set; }

        [JsonProperty(PropertyName = "posterId")]
        public long PosterId { get; set; }

        [JsonProperty(PropertyName = "chatText")]
        public string ChatText { get; set; }

        [JsonProperty(PropertyName = "createdDate")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty(PropertyName = "userName")]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "imagePath")]
        public string ImagePath { get; set; }
    }
}