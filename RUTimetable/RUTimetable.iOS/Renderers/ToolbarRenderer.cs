
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using UIKit;
using System.Linq;
using CoreGraphics;
using System;
using System.Collections.Generic;
using System.Windows.Input;

[assembly: ExportRenderer (typeof(Page), typeof(RUTimetableIOS.iOS.Renderers.ToolbarRenderer))]

namespace RUTimetableIOS.iOS.Renderers
{
    public class ToolbarRenderer : PageRenderer
    {
        UIToolbar toolbar;
        List<ToolbarItem> secondaryItems;
        readonly Dictionary<UIBarButtonItem, ToolbarItem> buttonCommands = new Dictionary<UIBarButtonItem, ToolbarItem>();

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            var page = e.NewElement as Page;
            if (page != null)
            {
                secondaryItems = page.ToolbarItems.Where(i => i.Order == ToolbarItemOrder.Secondary).ToList();
                secondaryItems.ForEach(t => page.ToolbarItems.Remove(t));
            }
            base.OnElementChanged(e);
        }

        public override void ViewWillAppear(bool animated)
        {
            if (secondaryItems != null && secondaryItems.Count > 0)
            {
                var tools = new List<UIBarButtonItem>();
                buttonCommands.Clear();
                foreach (var tool in secondaryItems)
                {
#pragma warning disable 618
                    var systemItemName = tool.Name;
#pragma warning restore 618
                    UIBarButtonItem button;
                    UIBarButtonSystemItem systemItem;
                    button = Enum.TryParse<UIBarButtonSystemItem>(systemItemName, out systemItem)
                        ? new UIBarButtonItem(systemItem, ToolClicked)
                        : new UIBarButtonItem(tool.Text, UIBarButtonItemStyle.Plain, ToolClicked);
                    buttonCommands.Add(button, tool);
                    tools.Add(button);
                }

                NavigationController.SetToolbarHidden(false, animated);
                toolbar = new UIToolbar(CGRect.Empty) { Items = tools.ToArray() };
                NavigationController.View.Add(toolbar);
            }

            base.ViewWillAppear(animated);
        }

        void ToolClicked(object sender, EventArgs args)
        {
            var tool = sender as UIBarButtonItem;
            var command = buttonCommands[tool].Command;
            command.Execute(buttonCommands[tool].CommandParameter);
        }

        public override void ViewWillDisappear(bool animated)
        {
            if (toolbar != null)
            {
                NavigationController.SetToolbarHidden(true, animated);
                toolbar.RemoveFromSuperview();
                toolbar = null;
                buttonCommands.Clear();
            }
            base.ViewWillDisappear(animated);
        }
    }
}