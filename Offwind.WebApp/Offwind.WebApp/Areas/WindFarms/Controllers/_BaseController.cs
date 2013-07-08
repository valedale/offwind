﻿using System.Web.Configuration;
using System.Web.Mvc;
using Offwind.WebApp.Infrastructure;
using Offwind.WebApp.Infrastructure.Navigation;
using Offwind.WebApp.Models;
using Offwind.WebApp.Models.Account;

namespace Offwind.WebApp.Areas.WindFarms.Controllers
{
    [Authorize(Roles = SystemRole.User)]
    public class _BaseController : Controller
    {
        protected string _currentGroup;
        protected OffwindEntities _ctx = new OffwindEntities();
        protected bool _noNavigation = false;

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            ViewBag.Version = WebConfigurationManager.AppSettings["AppVersion"];
            ViewBag.IsAdmin = AccountsHelper.IsAdmin(filterContext.HttpContext.User.Identity.Name);

            if (!_noNavigation)
            {
                InitNavigation();
            }
        }

        private void InitNavigation()
        {
            var navigation = new NavItem<NavUrl>();

            foreach (var grp in navigation)
            {
                grp.IsActive = grp.Title == _currentGroup;
            }
            ViewBag.SideNav = navigation;
        }
    }
}
