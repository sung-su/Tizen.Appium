using System;
using Tizen.NUI;
using Tizen.Appium.NUI;

namespace Tizen.Appium
{
    public class NUIAdapter : IAppAdapter
    {
        NUIViewList _objectList = new NUIViewList();

        IObjectList IAppAdapter.ObjectList => _objectList;

        public static int ScreenWidth { get; private set; }
        public static int ScreenHeight { get; private set; }

        public NUIAdapter()
        {
            Window.Instance.ViewAdded += (s, e) =>
            {
                _objectList.Add(s);
            };
        }

        public static void Initialize()
        {
            ScreenWidth = Tizen.Appium.Utils.GetScreeenWidth();
            ScreenHeight = Tizen.Appium.Utils.GetScreenHeight();

            TizenAppium.Start(new NUIAdapter());
        }

        public static void Terminate()
        {
            TizenAppium.Stop();
        }
    }
}
