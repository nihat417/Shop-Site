using Shop_Site.Models;

namespace Shop_Site.Repository
{
    public interface IEmailService
    {
        void SendEmail(Message message);
    }
}
