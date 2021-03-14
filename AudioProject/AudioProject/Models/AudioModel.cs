using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioProject.Models
{
    public class AudioModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public string Description { get; set; }
        public string Artiste { get; set; }
    }
}
