using Common.AspNetCore.Autorizetion.DynamicAuthorizationService.Enum;
using Hangfire;
using Hangfire.Common;
using JobsModule.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Administrator.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBackgroundJobClient _backgroundJobClient;

        public HomeController(ILogger<HomeController> logger, IBackgroundJobClient backgroundJobClient)
        {
            _logger = logger;
            _backgroundJobClient = backgroundJobClient;
        }

        [Authorize(Policy = ConstantPolicies.RequireAdministratorRole)]
        public IActionResult Index()
        {

            ActiveMenu("Dashboards");
            return View();
        }

        public IActionResult Privacy()
        {
            //_backgroundJobClient.Enqueue<IJobService>(x => x.SendTestJob(""));
            //_backgroundJobClient.Schedule<IJobService>(x => x.SendTestJob("Schedule"), TimeSpan.FromSeconds(30));
            //var manager = new RecurringJobManager();
            //manager.AddOrUpdate<IJobService>("test", x => Console.WriteLine(""), Cron.Minutely);
            return View();
        }


    }
}