using System.Collections.Generic;

namespace BusinessLogic.ObjectModels
{
    public class PlaylistObj
    {
        public string playlistUuid { get; set; }
        public string playlistETag { get; set; }
        public IEnumerable<int> trackIds { get; set; }
        public int toIndex { get; set; }
    }
}
