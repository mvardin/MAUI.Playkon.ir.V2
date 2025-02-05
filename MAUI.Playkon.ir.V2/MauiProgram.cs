﻿using MediaManager;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using FFImageLoading.Maui;
using MediaManager.Platforms.Android.MediaSession;
using MAUI.Playkon.ir.V2.Services;
using MAUI.Playkon.ir.V2.Helper;
using MAUI.Playkon.ir.V2.ViewModels;
using SQLitePCL;

namespace MAUI.Playkon.ir.V2
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            Batteries.Init();
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseFFImageLoading()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            CrossMediaManager.Current.Init();

            new MediaManagerEventHelper().Init();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
