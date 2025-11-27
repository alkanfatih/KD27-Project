
namespace WebApplication1.Services
{
    public class ShowDateTime : IShowDateTime
    {
        public DateTime GetDateTime { get; } = DateTime.Now;
    }
}
