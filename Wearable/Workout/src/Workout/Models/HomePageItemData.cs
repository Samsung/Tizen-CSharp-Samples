using Xamarin.Forms;

namespace Workout.Models
{
    public class HomePageItemData
    {
        /// <summary>
        /// Label text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Animation file path.
        /// </summary>
        public string File { get; set; }

        /// <summary>
        /// Animation layout bounds.
        /// </summary>
        public Rectangle Bounds { get; set; }

        /// <summary>
        /// Item index.
        /// </summary>
        public int Index { get; set; }
    }
}
