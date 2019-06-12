using ElmSharp;
using System;
using Tizen.Applications;

namespace ElmSharpWidget
{
    public class MyWidget : WidgetBase
    {
        public override void OnCreate(Bundle content, int w, int h)
        {
            base.OnCreate(content, w, h);
            Console.WriteLine("[OnCreate]   ###");
            makeUI();
        }

        public override void OnPause()
        {
            base.OnPause();
            Console.WriteLine("[OnPause]   ###");
        }

        public override void OnResume()
        {
            base.OnResume();
            Console.WriteLine("[OnResume]   ###");
        }

        public override void OnResize(int w, int h)
        {
            base.OnResize(w, h);
            Console.WriteLine("[OnResize]   ###");
        }

        public override void OnUpdate(Bundle content, bool isForce)
        {
            base.OnUpdate(content, isForce);
            Console.WriteLine("[OnUpdate]   ###");
        }

        public override void OnDestroy(WidgetBase.WidgetDestroyType reason, Bundle content)
        {
            base.OnDestroy(reason, content);
            Console.WriteLine("[OnDestroy]   ###");
        }

        void makeUI()
        {
            try
            {
                Conformant conformant = new Conformant(Window);
                conformant.Show();

                Box box = new Box(Window)
                {
                    AlignmentX = -1,
                    AlignmentY = -1,
                    WeightX = 1,
                    WeightY = 1,
                    BackgroundColor = Color.Orange,
                };
                conformant.SetContent(box);
                box.Show();

                Button btn = new Button(Window)
                {
                    Text = "ElmSharpWidget",
                    AlignmentX = -1,
                    AlignmentY = -1,
                    WeightX = 1,
                    WeightY = 1,
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
            Console.WriteLine("Button is clicked..");
        }
    }
}
