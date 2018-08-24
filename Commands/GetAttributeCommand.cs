using System;
using Xamarin.Forms;

namespace Tizen.Appium
{
    internal class GetAttributeCommand : ICommand
    {
        public string Command => Commands.GetAttribute;

        public Result Run(Request req)
        {
            Log.Debug("Run: GetAttribute");

            var elementId = req.Params.ElementId;
            var propertyName = req.Params.Attribute;

            var result = new Result();

            var element = ElementUtils.GetElementWrapper(elementId)?.Element;
            if (element == null)
            {
                Log.Debug("Not Found Element");
                return result;
            }

            var value = element.GetType().GetProperty(propertyName)?.GetValue(element);
            if (value != null)
            {
                if (value is Element)
                {
                    var id = ElementUtils.GetIdByElement((Element)value);
                    if (!String.IsNullOrEmpty(id))
                    {
                        value = id;
                    }
                }
                result.Value = value.ToString();
                return result;
            }

            Log.Debug(elementId + " element does not have " + propertyName + " property.");
            return result;
        }
    }
}