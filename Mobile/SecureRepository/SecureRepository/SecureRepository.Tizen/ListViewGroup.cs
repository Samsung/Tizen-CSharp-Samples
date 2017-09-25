using System.Collections.Generic;

namespace SecureRepository.Tizen
{
    public enum AliasType { Data,Key,Certificate }
    public class PageModel //PageModel will be stored inside PageTypeGroup, and then displayed in listView
    {
        public string Alias { get; }
        public AliasType Type { get; }
        public PageModel(string alias,AliasType type)
        {
            Alias = alias;
            Type = type;
        }
    }
    public class PageTypeGroup : List<PageModel>
    {
        public string Title { get; }
        public string ShortName { get; }
        public PageTypeGroup(AliasType aliasType)
        {
            switch(aliasType)
            {
                case AliasType.Certificate:
                    Title = "Certificate";
                    ShortName = "C";
                    break;
                case AliasType.Key:
                    Title = "Key";
                    ShortName = "K";
                    break;
                case AliasType.Data:
                    Title = "Data";
                    ShortName = "D";
                    break;
            }
        }        
    }
}
