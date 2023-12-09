using System.Collections.ObjectModel;

namespace MAUI.Playkon.ir.V2.Models
{
    public class MiniPlayerMessage
    {
        public MediaItemModel CurrentMusic { get; set; }
        public ObservableCollection<MediaItemModel> QueueLList { get; set; }
    }
}
