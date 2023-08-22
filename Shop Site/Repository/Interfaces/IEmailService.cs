using Shop_Site.Models;

namespace Shop_Site.Repository.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(Message message);
    }
}
