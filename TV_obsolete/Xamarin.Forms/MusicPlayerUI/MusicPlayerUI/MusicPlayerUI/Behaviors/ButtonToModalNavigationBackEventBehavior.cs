using System;
using Xamarin.Forms;

namespace MusicPlayerUI.Behaviors
{
	public class ImageButtonToModalNavigationBackEventBehavior : Behavior<ImageButton>
	{
		public static readonly BindableProperty IsBackEventProperty = BindableProperty.Create("IsBackEvent", typeof(bool), typeof(ImageButtonToModalNavigationBackEventBehavior), false);

		public bool IsBackEvent
		{
			get { return (bool)GetValue(IsBackEventProperty); }
			set { SetValue(IsBackEventProperty, value); }
		}

		protected override void OnAttachedTo(ImageButton bindable)
		{
			bindable.Clicked += Bindable_Clicked;
		}

		private async void Bindable_Clicked(object sender, EventArgs e)
		{
			if (IsBackEvent)
			{
				await Application.Current.MainPage.Navigation.PopModalAsync();
			}
		}

		protected override void OnDetachingFrom(ImageButton bindable)
		{
			bindable.Clicked -= Bindable_Clicked;
		}
	}
}
