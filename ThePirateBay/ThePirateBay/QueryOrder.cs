using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThePirateBay
{
    public enum QueryOrder
    {
        ByName = 1,
        ByNameDescending = 2,
        ByUploaded = 3,
        ByUploadedDescending = 4,
        BySize = 5,
        BySizeDescending = 6,
        BySeeds = 7,
        BySeedsDescending = 8,
        ByLeechers = 9,
        ByLeechersDescending = 10,
        ByUledBy = 11,
        ByUledByDescending = 12,
        ByType = 13,
        ByTypeDescending = 14,
        ByDefault = 99
    }
}
