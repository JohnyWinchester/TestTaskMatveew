using Microsoft.AspNetCore.Mvc;
using TestTaskMatveew.Services.Interfaces;

namespace TestTaskMatveew.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _Configuration;
        private readonly string _XmlURL;
        private readonly IXmlOffer _XmlOffer;

        public HomeController(IXmlOffer XmlOffer,ILogger<HomeController> logger, IConfiguration Configuration)
        {
            _logger = logger;
            _Configuration = Configuration;
            _XmlURL = Configuration["XmlOffersDocument"] ?? throw new ArgumentNullException(nameof(Configuration));
            _XmlOffer = XmlOffer;
        }

        public async Task<IActionResult> Index()
        {
            var offer = await _XmlOffer.GetOffer(_XmlURL, "12344");
            return View(offer);
        }
    }
}