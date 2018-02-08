using System;
using Xamarin.Forms.Platform.Tizen;
using Xamarin.Forms;
using Tizen.Appium.Renderer;

[assembly: ExportRenderer(typeof(ListView), typeof(TestableListViewRenderer))]

namespace Tizen.Appium.Renderer
{
    public class TestableListViewRenderer : ListViewRenderer
    {
        public TestableListViewRenderer() : base()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            if (!String.IsNullOrEmpty(e.NewElement.AutomationId))
            {
                ElementUtils.AddTestableElement(Element.AutomationId, Element);
            }

            Control.ItemRealized += (sender, arg) =>
            {
                var text = arg.Item.GetPartText("elm.text");
                if (!String.IsNullOrEmpty(text))
                {
                    text = text.Substring(text.IndexOf('>') + 1);
                    string key = text.Substring(0, text.IndexOf('<'));
                    ElementUtils.AddTestableItem(key, arg.Item);
                }
            };

            Control.ItemPressed += (sender, arg) =>
            {
                // TODO : to be removed. It is a temporaty solution for item pressed event. because of bug in TrackObject.
                arg.Item.InvokeCallback(arg.Item.GetHashCode().ToString());
            };
        }
    }
}