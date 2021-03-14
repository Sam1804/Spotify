using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioProject.Models
{
    public class PlaylistModel
    {
        public int ID { get; set; }
        public string NamePlaylist { get; set; }
        public string UserID { get; set; }
        public string Description { get; set; }
    }
}
