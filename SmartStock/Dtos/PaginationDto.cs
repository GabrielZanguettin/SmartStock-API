namespace SmartStock.Dtos
{
    /// <summary>
    /// Base pagination parameters used for listing resources.
    /// </summary>
    public class PaginationDto
    {
        /// <summary>
        /// Page number (1-based). Defaults to 1.
        /// </summary>
        public int? Page { get; set; } = 1;

        /// <summary>
        /// Page size (items per page). Defaults to 20.
        /// </summary>
        public int? PageSize { get; set; } = 20;
    }
}
