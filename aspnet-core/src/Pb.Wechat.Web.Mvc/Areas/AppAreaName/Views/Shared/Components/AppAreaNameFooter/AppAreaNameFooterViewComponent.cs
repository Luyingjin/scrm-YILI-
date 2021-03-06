﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pb.Wechat.Web.Areas.AppAreaName.Models.Layout;
using Pb.Wechat.Web.Session;
using Pb.Wechat.Web.Views;

namespace Pb.Wechat.Web.Areas.AppAreaName.Views.Shared.Components.AppAreaNameFooter
{
    public class AppAreaNameFooterViewComponent : AbpZeroTemplateViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;

        public AppAreaNameFooterViewComponent(IPerRequestSessionCache sessionCache)
        {
            _sessionCache = sessionCache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var footerModel = new FooterViewModel
            {
                LoginInformations = await _sessionCache.GetCurrentLoginInformationsAsync()
            };

            return View(footerModel);
        }
    }
}
