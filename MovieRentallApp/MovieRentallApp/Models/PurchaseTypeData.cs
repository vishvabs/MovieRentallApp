using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRentallApp.Models
{
    public static class PurchaseTypeData
    {
        public static IList<PurchaseType> purchaseTypes { get; private set; }
        static PurchaseTypeData()
        {

            purchaseTypes = new List<PurchaseType>();
            purchaseTypes.Add(new PurchaseType
            {
                TypeName = "Physical Copy",
                Val = true
            }
            );

            purchaseTypes.Add(new PurchaseType
            {
                TypeName = "Streaming",
                Val = false
            }
          );
        }
    }
}
