using System;
using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using BookReview.Authorization.Users;
using BookReview.Reviews;

namespace BookReview.Libros.Dto
{
    [AutoMapTo(typeof(Review))]
    public class CreateReviewDto
    {
        public string Opinion { get; set; }

        [Required]
        [Range(1, 5)]
        public int Calificacion { get; set; }
    }
}
