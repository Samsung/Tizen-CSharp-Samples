using System.Collections.Generic;

namespace Gallery
{
    public interface IImageSearchService
    {
        IList<string> GetImagePathsAsync();
    }
}
