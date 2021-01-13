using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleTrackingUi.Entities.Storage
{
    public class Rack
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string IdDescription => $"{Id} - {Description}";
    }
}
