using Android.Content;
using Android.Graphics.Drawables;
using AndroidX.AppCompat.Widget;
using AndroidX.Core.Content;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Handlers;
using Northboundei.Mobile.Controls;
using Graphics = Android.Graphics;

namespace Northboundei.Mobile.Platforms.Android
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                // Remove the underline
                Control.Background = null; // This removes the underline
                Control.SetPadding(0, 0, 0, 0); // Optional: remove padding
            }

            if (e.NewElement is CustomEntry customEntry)
            {
                UpdateCornerRadius(customEntry.CornerRadius);
            }
        }

        private void UpdateCornerRadius(float cornerRadius)
        {
            if (Control != null)
            {
                var shape = new GradientDrawable();
                shape.SetColor(Graphics.Color.Transparent); // Background color
                shape.SetCornerRadius(cornerRadius); // Set the corner radius
                Control.Background = shape;
            }
        }
    }


}
