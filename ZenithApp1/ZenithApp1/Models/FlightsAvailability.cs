﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZenithApp1.Models
{
    public class FlightsAvailability
    {
        [JsonProperty(PropertyName = "flightCompanyId")]
        public string FlightCompanyId { get; set; }

        [JsonProperty(PropertyName = "flights")]
        public IEnumerable<FlightAvailability> FlightAvailability { get; set; }
    }
}