using ooapi.v5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ooapi.v5.core.Models.Many2Many
{
    public class NewsItemsNewsFeeds
    {
        public Guid NewsItemId { get; set; }
        public NewsItem NewsItem{ get; set; }

        public Guid NewsFeedId { get; set; }
        public NewsFeed NewsFeed { get; set; }

    }
}
