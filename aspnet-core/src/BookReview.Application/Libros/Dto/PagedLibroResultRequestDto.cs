using Abp.Application.Services.Dto;
using System;

namespace BookReview.Libros.Dto
{
    //custom PagedResultRequestDto
    public class PagedLibroResultRequestDto : PagedResultRequestDto
    {
        public int? AutorId { get; set; }

        public string Editorial { get; set; }

        public DateTime? BeforeDate { get; set; }

        public DateTime? AfterDate { get; set; }

        public bool? SortRanking { get; set; }


    }
}
