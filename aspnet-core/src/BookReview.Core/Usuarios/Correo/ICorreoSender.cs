using System.Threading.Tasks;

namespace BookReview.Usuarios.Correo
{
    public interface ICorreoSender
    {
        Task SendEmailNotificationAsync(Usuario user);
    }
}