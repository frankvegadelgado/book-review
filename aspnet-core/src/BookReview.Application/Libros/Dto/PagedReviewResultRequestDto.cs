using Abp.Application.Services.Dto;
using System;
using System.ComponentModel.DataAnnotations;

namespace BookReview.Libros.Dto
{
    //custom PagedResultRequestDto
    public class PagedReviewResultRequestDto : PagedResultRequestDto
    {
        [Range(1, 5)]
        public int? ReviewType { get; set; }

        public bool? SortByDateCreation { get; set; }


    }
}
