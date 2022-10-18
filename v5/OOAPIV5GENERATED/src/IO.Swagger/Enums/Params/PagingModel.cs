using System.ComponentModel.DataAnnotations;

namespace IO.Swagger.Enums.Params
{
    public class PagingModel
    {

        /// <summary>
        /// The number of items per page <br/>
        /// Default: 10
        /// </summary>
        public PageSizeEnum pageSize { get; set; }

        /// <summary>
        /// The page number to get. Page numbers start at 1.
        /// </summary>
        [MinLength(1)]
        public int pageNumber { get; set; } = 1;





    }
}
