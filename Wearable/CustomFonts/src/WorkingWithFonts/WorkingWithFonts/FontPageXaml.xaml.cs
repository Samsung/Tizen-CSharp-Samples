using Tizen.Wearable.CircularUI.Forms;

namespace WorkingWithFonts
{	
	/// <summary>
	/// Using fonts in Xamarin.Forms 1.3 is slightly different to earlier versions.
	/// The 'Font' property has been replaced with three separate properties:
	/// - FontFamily
	/// - FontSize
	/// - FontAttributes
	/// Use these three properties directly and avoid using the Font class.
	/// </summary>
	public partial class FontPageXaml : CirclePage
    {	
		public FontPageXaml ()
		{
			InitializeComponent ();
		}
	}
}

