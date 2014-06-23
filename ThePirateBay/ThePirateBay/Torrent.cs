using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThePirateBay
{
    public class Torrent
    {
        public string Name { get; set; }
        public string Magnet { get; set; }
        public string File { get; set; }
        public string Uploaded { get; set; }
        public string Size { get; set; }
        public decimal SizeBytes { get; set; }
        public string Uled { get; set; }
        public int Seeds { get; set; }
        public int Leechers { get; set; }
        public int CategoryParent { get; set; }
        public int Category { get; set; }
        public int Comments { get; set; }
        public bool HasCoverImage { get; set; }
        public bool IsTrusted { get; set; }
        public bool IsVip { get; set; }
    }
}
