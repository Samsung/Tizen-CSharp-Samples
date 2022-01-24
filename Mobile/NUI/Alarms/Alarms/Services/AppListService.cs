using System;
using System.Collections.Generic;
using System.Text;
using Tizen.Applications;
using Alarms.Models;

namespace Alarms.Services
{
    public static class AppListService
    {
        public static List<AppInfo> GetAppList()
        {
            List<AppInfo> appList = new List<AppInfo>();
            IEnumerable<Package> packageList = PackageManager.GetPackages();
            foreach (Package pkg in packageList)
            {
                var list = pkg.GetApplications();
                foreach (var app in list)
                {
                    if (!app.IsNoDisplay)
                    {
                        appList.Add(new AppInfo(app.Label, app.ApplicationId));
                    }
                }
            }
            return appList;
        }
    }
}