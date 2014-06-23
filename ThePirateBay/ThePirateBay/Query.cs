using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThePirateBay
{
    public class Query
    {
        public QueryOrder Order { get; set; }
        public int Category { get; set; }
        public int Page { get; set; }
        public string Term { get; set; }

        public Query(string term, int page = 0)
            : this(term, page, TorrentCategory.All, QueryOrder.ByDefault) { }
        public Query(string term, int page, int category)
            : this(term, page, category, QueryOrder.ByDefault) { }
        public Query(string term, int page, QueryOrder order)
            : this(term, page, TorrentCategory.All, order) { }
        public Query(string term, int page, int category, QueryOrder order)
        {
            Category = category;
            Order = order;
            Page = page;
            Term = term;
        }

        public string TranslateToUrl()
        {
            return string.Format(Constants.UrlSearch, Constants.UrlTpb[0], Term, Page.ToString(), ((int)Order).ToString(), Category.ToString());
        }
    }
}
