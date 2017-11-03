using System.Collections.Generic;
using System.IO;
using System.Linq;
using Gallery.TizenTV.DependencyService;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: Dependency(typeof(ImageSearchService))]
namespace Gallery.TizenTV.DependencyService
{
    public class ImageSearchService : IImageSearchService
    {
        public IList<string> GetImagePathsAsync()
        {
            var filters = new string[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp" };
            List<FileInfo> filesFound = new List<FileInfo>();
            var searchOption = SearchOption.AllDirectories;
            DirectoryInfo info = new DirectoryInfo(FormsApplication.Current.DirectoryInfo.Resource);

            foreach (var filter in filters)
            {
                filesFound.AddRange(info.GetFiles(string.Format("*.{0}", filter), searchOption));
            }

            var result = filesFound.Select((i, s) => i.Name).ToList();
            result.Sort();

            return result;
        }
    }
}
