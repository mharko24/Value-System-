﻿using Microsoft.AspNetCore.Mvc;

namespace ContractManagementValue.Common
{
    public class PaginatedRequest
    {
        [FromQuery(Name = "p")]
        public int PageNumber { get; set; } = 1;
        public const int ITEMS_PER_PAGE = 10;
        [FromQuery(Name = "s")]
        public string? SearhKeyword { get; set; } 
    }
}
