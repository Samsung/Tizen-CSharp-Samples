using ElmSharp;
using System;
using System.IO;
using System.Net;
using Tizen;
using Tizen.Applications;
using Tizen.Network.Connection;

namespace WidgetSample
{
    public class MyFirstWidget : WidgetBase
    {
        public async override void OnCreate(Bundle content, int w, int h)
        {
            base.OnCreate(content, w, h);
            // Reqeust user's permission for internet privilege
            // Check detailed info : https://developer.tizen.org/development/guides/.net-application/security/privacy-related-permissions
            // https://developer.tizen.org/development/training/.net-application/security-and-api-privileges
            UserPermission userperm = new UserPermission();
            var result = await userperm.CheckAndRequestPermission(Utility.InternetPrivilege);
            if (!result)
            {
                Log.Error(Utility.LOG_TAG, "Failed to obtain user consent.");
                // Terminate this application.
                Application.Current.Exit();
            }
            makeUI();
        }

        public override void OnPause()
        {
            base.OnPause();
        }

        public override void OnResume()
        {
            base.OnResume();
        }

        public override void OnResize(int w, int h)
        {
            base.OnResize(w, h);
        }

        public override void OnUpdate(Bundle content, bool isForce)
        {
            base.OnUpdate(content, isForce);
        }

        public override void OnDestroy(WidgetBase.WidgetDestroyType reason, Bundle content)
        {
            base.OnDestroy(reason, content);
        }

        void makeUI()
        {
            try
            {
                Conformant conformant = new Conformant(Window);
                conformant.Show();
                Scroller scroller = new Scroller(Window)
                {
                    AlignmentX = -1,
                    AlignmentY = -1,
                    WeightX = 1,
                    WeightY = 1,
                    ScrollBlock = ScrollBlock.None,
                };
                scroller.Show();

                Box box = new Box(Window)
                {
                    AlignmentX = -1,
                    AlignmentY = -1,
                    WeightX = 1,
                    WeightY = 1,
                };
                box.Show();
                scroller.SetContent(box);
                conformant.SetContent(scroller);

                Button btn = new Button(Window)
                {
                    Text = "Get Data",
                    AlignmentX = -1,
                    AlignmentY = -1,
                    WeightX = 1,
                    WeightY = 1
                };
                btn.Clicked += Btn_Clicked;
                box.PackEnd(btn);
                btn.Show();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception : " + e.Message);
            }
        }

        private void Btn_Clicked(object sender, EventArgs e)
        {
            try
            {
                ConnectionItem currentConnection = ConnectionManager.CurrentConnection;
                Log.Info(Utility.LOG_TAG, "connection(" + currentConnection.Type + ", " + currentConnection.State + ")");
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://github.sec.samsung.net/pages/dotnet/Tizen.NET/");

                // When a watch is paired with a mobile device, we can use WebProxy.
                if (currentConnection.Type == ConnectionType.Ethernet)
                {
                    string proxyAddr = ConnectionManager.GetProxy(AddressFamily.IPv4);
                    WebProxy myproxy = new WebProxy(proxyAddr, true);
                    request.Proxy = myproxy;
                }

                request.Method = "GET";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Log.Info(Utility.LOG_TAG, "StatusDescription: " + ((HttpWebResponse)response).StatusDescription);

                // Get the stream containing content returned by the server.
                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.  
                string responseFromServer = reader.ReadToEnd();
                // Display the content.  
                Log.Info(Utility.LOG_TAG, "responseFromServer :" + responseFromServer);
                // Clean up the streams and the response.
                reader.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                Log.Info(Utility.LOG_TAG, "An error occurs : " + ex.Message);
            }
        }
    }
}
