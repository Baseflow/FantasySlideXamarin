using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Graphics.Drawable;
using Android.Graphics;
using Android.Support.V4.Widget;
using Android.Animation;
using Android.Support.V4.View;
using Android.Views;
using System;
using Com.Github.Mzule.Fantasyslide;

namespace Sample
{
    [Activity(Label = "FantasySlide", MainLauncher = true, Icon = "@mipmap/icon", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity, DrawerLayout.IDrawerListener
    {
        private DrawerLayout drawerLayout;
        private DrawerArrowDrawable indicator;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_main);

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            indicator = new DrawerArrowDrawable(this);
            indicator.Color = Color.White;
            SupportActionBar.SetHomeAsUpIndicator(indicator);

            SetTransformer();
            // setListener();
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawerLayout);
            drawerLayout.SetScrimColor(Color.Transparent);
            drawerLayout.AddDrawerListener(this);
        }

        private void SetTransformer()
        {
            float spacing = Resources.GetDimensionPixelSize(Resource.Dimension.spacing);
            SideBar rightSideBar = FindViewById<SideBar>(Resource.Id.rightSideBar);
            rightSideBar.SetTransformer(new Transformer(spacing));
        }

        public override bool OnOptionsItemSelected(Android.Views.IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                if (drawerLayout.IsDrawerOpen(GravityCompat.Start))
                {
                    drawerLayout.CloseDrawer(GravityCompat.Start);
                }
                else {
                    drawerLayout.OpenDrawer(GravityCompat.Start);
                }
            }
            return true;
        }

        public void OnDrawerClosed(View drawerView)
        {
        }

        public void OnDrawerOpened(View drawerView)
        {
        }

        public void OnDrawerSlide(View drawerView, float slideOffset)
        {
            if (((ViewGroup)drawerView).GetChildAt(1).Id == Resource.Id.leftSideBar)
            {
                indicator.Progress = slideOffset;
            }
        }

        public void OnDrawerStateChanged(int newState)
        {
        }


        public void OnClick(View view)
        {
           if (view is TextView) {
               String title = ((TextView)view).Text.ToString();
               if (title.StartsWith("星期"))
               {
                   Toast.MakeText(this, title, ToastLength.Short).Show();
               }
               else {
                   //StartActivity(UniversalActivity.newIntent(this, title));
               }
           } else if (view.Id == Resource.Id.userInfo)
           {
               //StartActivity(UniversalActivity.newIntent(this, "个人中心"));
           }
        }
    }

    public class Transformer : Java.Lang.Object, ITransformer
    {
        private View lastHoverView;
        private float spacing;

        public Transformer(float spacing)
        {
            this.spacing = spacing;
        }

        public void Apply(ViewGroup sideBar, View itemView, float touchY, float slideOffset, bool isLeft)
        {
            bool hovered = itemView.Pressed;
            if (hovered && lastHoverView != itemView)
            {
                animateIn(itemView);
                animateOut(lastHoverView);
                lastHoverView = itemView;
            }
        }

        private void animateOut(View view)
        {
            if (view == null)
            {
                return;
            }
            ObjectAnimator translationX = ObjectAnimator.OfFloat(view, "translationX", -spacing, 0);
            translationX.SetDuration(200);
            translationX.Start();
        }

        private void animateIn(View view)
        {
            ObjectAnimator translationX = ObjectAnimator.OfFloat(view, "translationX", 0, -spacing);
            translationX.SetDuration(200);
            translationX.Start();
        }
    }
}