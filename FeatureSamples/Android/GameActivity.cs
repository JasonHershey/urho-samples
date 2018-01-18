using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using Org.Libsdl.App;
using Urho.Droid;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace Urho.Samples.Droid
{
	[Activity(Label = "UrhoSharp",
		Icon = "@drawable/icon", Theme = "@android:style/Theme.NoTitleBar.Fullscreen",
		ConfigurationChanges = ConfigChanges.KeyboardHidden | ConfigChanges.Orientation,
		ScreenOrientation = ScreenOrientation.Landscape)]
	public class GameActivity : Activity
	{
		UrhoSurfacePlaceholder surface;
		Application app;

		protected override async void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			var mLayout = new AbsoluteLayout(this);
			surface = UrhoSurface.CreateSurface(this);// (this, , true);
			mLayout.AddView(surface);
			SetContentView(mLayout);
			app = await surface.Show(Type.GetType(Intent.GetStringExtra("Type")), new ApplicationOptions("Data"));
            AppCenter.Start("1ee54659-f01e-4605-a401-9b276c3708d7",
                   typeof(Analytics), typeof(Crashes));
        }

		protected override void OnResume()
		{
			UrhoSurface.OnResume();
			base.OnResume();
		}

		protected override void OnPause()
		{
			UrhoSurface.OnPause();
			base.OnPause();
		}

		public override void OnLowMemory()
		{
			UrhoSurface.OnLowMemory();
			base.OnLowMemory();
		}

		protected override void OnDestroy()
		{
			UrhoSurface.OnDestroy();
			base.OnDestroy();
		}

		public override bool DispatchKeyEvent(KeyEvent e)
		{
			if (e.KeyCode == Android.Views.Keycode.Back)
			{
				this.Finish();
				return false;
			}

			if (!UrhoSurface.DispatchKeyEvent(e))
				return false;
			return base.DispatchKeyEvent(e);
		}

		public override void OnWindowFocusChanged(bool hasFocus)
		{
			UrhoSurface.OnWindowFocusChanged(hasFocus);
			base.OnWindowFocusChanged(hasFocus);
		}
	}
}