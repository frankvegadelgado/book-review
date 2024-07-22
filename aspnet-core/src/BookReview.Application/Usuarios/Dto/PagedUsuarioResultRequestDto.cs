using Abp.Application.Services.Dto;
using System;

namespace BookReview.Usuarios.Dto
{
    //custom PagedResultRequestDto
    public class PagedUsuarioResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
    }
}
