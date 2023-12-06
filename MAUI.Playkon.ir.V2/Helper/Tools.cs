using Com.Google.Android.Exoplayer2;
using MAUI.Playkon.ir.V2.Models;
using MediaManager;
using MediaManager.Queue;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI.Playkon.ir.V2.Helper
{
    public static class Tools
    {
        public static bool CompareList(IMediaQueue queue, ObservableCollection<MediaItemModel> list)
        {
            if (queue.Count != list.Count) return false;

            foreach (var item in queue)
            {
                if (!list.Any(a => a.MediaUri == item.MediaUri))
                    return false;
            }
            return true;
        }
    }
}
