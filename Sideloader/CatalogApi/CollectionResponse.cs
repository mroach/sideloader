using System.Collections.Generic;

namespace Sideloader.CatalogApi
{
    public class CollectionResponse<T>
    {
        public List<T> Collection { get; set; }
    }
}
