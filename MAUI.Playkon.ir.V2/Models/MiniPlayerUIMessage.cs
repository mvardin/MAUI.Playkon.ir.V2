using MediaManager.Player;
using System.Collections.ObjectModel;

namespace MAUI.Playkon.ir.V2.Models
{
    public class MiniPlayerUIMessage
    {
        public MediaPlayerState MediaPlayerState { get; set; }
        public bool IsFavourited { get; set; }
    }
}
