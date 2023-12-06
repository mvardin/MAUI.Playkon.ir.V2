using System.Collections.ObjectModel;

namespace MAUI.Playkon.ir.V2.Models
{
    public class MiniPlayerMessage
    {
        public MediaItemModel Music { get; set; }
        public ObservableCollection<MediaItemModel> MusicList { get; set; }
        public bool PlayNewInstance { get; set; } = false;
    }
}
