using System;
using System.Collections.Generic;
using System.Linq;
using ScreenMirroringSample.Tizen.Mobile;
using Xamarin.Forms;
using System.Threading.Tasks;
using Tizen.Network.WiFiDirect;
using Tizen;

[assembly: Dependency(typeof(WiFiDirectController))]
namespace ScreenMirroringSample.Tizen.Mobile
{
    class WiFiDirectController : IWiFiDirect
    {
        private static string _sourceIp = null;
        private static WiFiDirectPeer s_peer = null;
        private const string LogTag = "Tizen.Multimedia.ScreenMirroring";
        private static bool s_flagPeerFound = false;
        private static bool s_flagConnected = false;
        private static string s_address = null;
        public WiFiDirectController(){
            try
            {
                Log.Error(LogTag, "WiFi_Direct_controller is made");
                IEnumerable<WiFiDirectPeer> peerList = WiFiDirectManager.GetConnectedPeers();
                foreach (WiFiDirectPeer peer in peerList)
                {
                    _sourceIp = peer.IpAddress;
                  Log.Info(LogTag, "Wi-Fi Direct peer ip : " + _sourceIp);
                }
            }
            catch (Exception err)
            {
                Log.Error(LogTag, "Error is occurred in Create Window: " + err.ToString());
            }

        }
        public async Task DiscoverySetup()
        {
            WiFiDirectManager.PeerFound += EventHandlerPeerFound;
            WiFiDirectManager.StartDiscovery(false, WifidirectUtils.DiscoveryTime);
            await WaitForPeerFoundflag();
            WiFiDirectManager.CancelDiscovery();
            await Task.Delay(WifidirectUtils.CancelDiscoveryDelayTime);
            WiFiDirectManager.PeerFound -= EventHandlerPeerFound;
        }

        public async Task DoConnection()
        {
            //s_peer.ConnectionStateChanged += EventHandlerConnectionState;
            //s_peer.Connect();
            //await WaitConnectedFlag();
            //await Task.Delay(WifidirectUtils.ConnectDelayTime);
            IEnumerable<WiFiDirectPeer> peerList = WiFiDirectManager.GetConnectedPeers();
            if (!peerList.Any())
            {
                //Assert.Fail("No connected peers are found.");
            }
            else
            {
                foreach (WiFiDirectPeer peer in peerList)
                {
                    //LogUtils.Write(LogUtils.DEBUG, LogUtils.TAG, "Connected peer IpAddress: " + peer.IpAddress);
                }
            }
        }

        public static async Task WaitConnectedFlag()
        {
            int count = 0;
            while (true)
            {
                await Task.Delay(WifidirectUtils.DelayTime);
                count++;
                if (s_flagConnected)
                    break;
                if (count == WifidirectUtils.ConnectionWaitCounter)
                    break;
            }
        }
        public static void EventHandlerConnectionState(object sender, ConnectionStateChangedEventArgs e)
        {
            if (e.Error == WiFiDirectError.ConnectionCancelled && e.State == WiFiDirectConnectionState.ConnectionRsp)
            {
                //LogUtils.Write(LogUtils.DEBUG, LogUtils.TAG, "Connection cancelled");
                 
            }
            if (e.Error == WiFiDirectError.None && e.State == WiFiDirectConnectionState.ConnectionRsp)
            {
                //LogUtils.Write(LogUtils.DEBUG, LogUtils.TAG, "ConnectionRsp State");
                s_address = e.MacAddress;
                s_flagConnected = true;

            }
            if (e.State == WiFiDirectConnectionState.ConnectionInProgress)
            {
                //LogUtils.Write(LogUtils.DEBUG, LogUtils.TAG, "ConnectionInProgress State");
                s_address = e.MacAddress;
            }
            if (e.Error == WiFiDirectError.None && e.State == WiFiDirectConnectionState.DisconnectRsp)
            {
                //LogUtils.Write(LogUtils.DEBUG, LogUtils.TAG, "disconnectrsp State");
                s_address = e.MacAddress;

            }
        }
        public static void EventHandlerPeerFound(object sender, PeerFoundEventArgs e)
        {
            if (e.DiscoveryState == WiFiDirectDiscoveryState.Found)
            {
                if (e.Peer.Name.Contains(WifidirectUtils.PeerDeviceName))
                {
                    //LogUtils.Write(LogUtils.DEBUG, LogUtils.TAG, "Peer found" + e.Peer.Name);
                    s_peer = e.Peer;
                    s_flagPeerFound = true;
                }
            }
        }
        public static async Task WaitForPeerFoundflag()
        {
            int count = 0;
            while (true)
            {
                await Task.Delay(WifidirectUtils.DelayTime);
                count++;
                if (s_flagPeerFound)
                    break;
                if (count == WifidirectUtils.PeerWaitCounter)
                    break;
            }
        }
        public String SourceIp
        {
            get => _sourceIp;

            set
            {
                if (_sourceIp != value)
                {
                    _sourceIp = value;
                }
            }
        }
    }
    internal static class WifidirectUtils
    {
        internal const string PeerDeviceName = "WifidirectCsapiTest";
        internal const int DiscoveryTime = 30;
        internal const int DelayTime = 2000;
        internal const int DiscoverDelayTime = 1000;
        internal const int StartDiscoveryDelayTime = 10000;
        internal const int CancelDiscoveryDelayTime = 5000;
        internal const int DisconnectDelayTime = 7000;
        internal const int ConnectDelayTime = 6000;
        internal const int PeerWaitCounter = 15;
        internal const int ConnectionWaitCounter = 8;
        internal const int DiscoveryWaitCounter = 5;
        internal const int LostWaitCounter = 50;
    }

}
