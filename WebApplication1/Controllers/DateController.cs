using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class DateController : Controller
    {
        private readonly IShowDateTime _date;

        public DateController(IShowDateTime date)
        {
            _date = date;
        }

        public IActionResult Index([FromServices] IShowDateTime _date2)
        {
            var time1 = _date.GetDateTime.TimeOfDay;
            var time2 = _date2.GetDateTime.TimeOfDay;

            return Content($"Time 1: {time1}\nTime 2: {time2}");
        }
    }
}
