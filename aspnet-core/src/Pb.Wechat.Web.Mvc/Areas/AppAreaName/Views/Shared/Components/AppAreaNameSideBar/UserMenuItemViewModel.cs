﻿using Abp.Application.Navigation;

namespace Pb.Wechat.Web.Areas.AppAreaName.Views.Shared.Components.AppAreaNameSideBar
{
    public class UserMenuItemViewModel
    {
        public UserMenuItem MenuItem { get; set; }

        public string CurrentPageName { get; set; }

        public int MenuItemIndex { get; set; }

        public bool RootLevel { get; set; }
    }
}
