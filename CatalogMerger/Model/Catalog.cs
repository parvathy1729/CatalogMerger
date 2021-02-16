using System.Collections.Generic;

namespace CatalogMerger.Model
{
    public class Catalog
    {
        public string SKU { get; set; }
        public string Description { get; set; }
    }

    public class CatalogCompare : IEqualityComparer<Catalog>
    {
        public bool Equals(Catalog x, Catalog y)
        {
            return y != null && x != null && x.SKU.Equals(y.SKU) && x.Description.Equals(y.Description);
        }

        public int GetHashCode(Catalog obj)
        {
            return obj.SKU.GetHashCode() ^ obj.Description.GetHashCode();
        }
    }


}
