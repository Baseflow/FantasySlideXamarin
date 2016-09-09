using System;
using Android.Content;
using Android.Graphics;
using Android.Util;

namespace Sample
{
    public class CircleImageView : DE.Hdodenhof.Circleimageview.CircleImageView
    {
        public CircleImageView(Context context) : base(context)
        {
        }

        public CircleImageView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public CircleImageView(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
        }

        protected override void DispatchSetPressed(bool pressed)
        {
            base.DispatchSetPressed(pressed);
            if (pressed)
            {
                BorderColor = Resources.GetColor(Resource.Color.colorAccent);
            }
            else {
                BorderColor = Color.White;
            }
        }
    }
}

