﻿namespace MAUI.Playkon.ir.V2.Helper
{
    public class GlobalSetting
    {
        private static readonly GlobalSetting _instance = new GlobalSetting();
        public static GlobalSetting Instance
        {
            get { return _instance; }
        }

        public string token { get; set; }
    }
}
