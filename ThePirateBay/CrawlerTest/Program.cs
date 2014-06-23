using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePirateBay;

namespace CrawlerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<Torrent> torrents = Tpb.Search(new Query("frozen", 0, QueryOrder.BySeeds));
        }
    }
}
