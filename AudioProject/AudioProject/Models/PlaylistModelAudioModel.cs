using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioProject.Models
{
    public class PlaylistModelAudioModel
    {
        public int PlaylistId { get; set; }
        public PlaylistModel Playlist { get; set; }

        public int AudioId { get; set; }
        public AudioModel Audio { get; set; }
    }
}
