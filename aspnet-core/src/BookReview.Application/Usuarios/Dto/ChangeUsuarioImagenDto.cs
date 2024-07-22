using System.ComponentModel.DataAnnotations;

namespace BookReview.Usuarios.Dto
{
    public class ChangeUsuarioImagenDto
    {
        [Required]
        [Url]
        public string ImagenEnlace { get; set; }
    }
}