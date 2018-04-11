using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScreenMirroringSample
{
    public interface IWiFiDirect
    {
        string SourceIp { get; set; }

        Task DiscoverySetup();
        Task DoConnection();
    }
}
