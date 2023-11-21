using System.Xml.Schema;

namespace ContractManagementValue.Common
{
    public class PaginatedResult<T>
    {
        public IEnumerable<T>? result { get; set; }
        public int Page { get; set; }
        public int TotalCount { get; set; }
        public string? Keyword { get; set; }
    }
}
