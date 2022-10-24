using ooapi.v5.Enums.Params;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ooapi.v5.Models.Params
{
    public class PagingParams
    {

        /// <summary>
        /// The number of items per page <br/>
        /// Default: 10
        /// </summary>
        [DefaultValue(PageSizeEnum.Ten)]
        public PageSizeEnum pageSize { get; set; }

        /// <summary>
        /// The page number to get. Page numbers start at 1. <br/>
        /// Example: pageNumber=1
        /// </summary>
        /// <param name="pageNumber" example="IETS"></param>
        [MinLength(1)]
        public int pageNumber { get; set; } = 1;





    }
}
