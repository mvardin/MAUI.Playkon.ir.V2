using Android.Provider;
using Com.Google.Android.Exoplayer2;
using MAUI.Playkon.ir.V2.Models;
using MediaManager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI.Playkon.ir.V2.Helper
{
    public static class QueueHelper
    {
        public static void AddToQueue(List<MediaItemModel> list)
        {
            CrossMediaManager.Current.Queue.Clear();

            foreach (var item in list)
            {
                if (!CrossMediaManager.Current.Queue.Any(a => a.MediaUri == item.MediaUri))
                    CrossMediaManager.Current.Queue.Add(item);
            }
        }
    }
}
