using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace NUI_CustomView
{
    public class ContactView : CustomView
    {
        private string name;
        private string resDir;

        private const int BASE_INDEX = 10000;
        private const int BACKGROUND_VISUAL_INDEX = BASE_INDEX + 1;
        private const int LABEL_VISUAL_INDEX = BASE_INDEX + 2;
        private const int CONTACT_BG_ICON_INDEX = BASE_INDEX + 3;
        private const int CONTACT_ICON_INDEX = BASE_INDEX + 4;
        private const int CONTACT_EDIT_INDEX = BASE_INDEX + 5;
        private const int CONTACT_FAVORITE_INDEX = BASE_INDEX + 6;
        private const int CONTACT_DELETE_INDEX = BASE_INDEX + 7;

        static ContactView()
        {
            CustomViewRegistry.Instance.Register(CreateInstance, typeof(ContactView));
        }

        static CustomView CreateInstance()
        {
            return new ContactView(null, null);
        }

        private void CreateBackground(Vector4 color)
        {
            PropertyMap map = new PropertyMap();
            map.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Color))
               .Add(ColorVisualProperty.MixColor, new PropertyValue(color));

            VisualBase background = VisualFactory.Instance.CreateVisual(map);
            RegisterVisual(BACKGROUND_VISUAL_INDEX, background);
            background.DepthIndex = BACKGROUND_VISUAL_INDEX;
        }

        private void CreateLabel(string text)
        {
            PropertyMap textVisual = new PropertyMap();
            textVisual.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Text))
                      .Add(TextVisualProperty.Text, new PropertyValue(text))
                      .Add(TextVisualProperty.TextColor, new PropertyValue(Color.Black))
                      .Add(TextVisualProperty.PointSize, new PropertyValue(12))
                      .Add(TextVisualProperty.HorizontalAlignment, new PropertyValue("CENTER"))
                      .Add(TextVisualProperty.VerticalAlignment, new PropertyValue("CENTER"));

            VisualBase label = VisualFactory.Instance.CreateVisual(textVisual);
            RegisterVisual(LABEL_VISUAL_INDEX, label);
            label.DepthIndex = LABEL_VISUAL_INDEX;

            PropertyMap imageVisualTransform = new PropertyMap();
            imageVisualTransform.Add((int)VisualTransformPropertyType.Offset, new PropertyValue(new Vector2(30, 5)))
                                .Add((int)VisualTransformPropertyType.OffsetPolicy, new PropertyValue(new Vector2((int)VisualTransformPolicyType.Absolute, (int)VisualTransformPolicyType.Absolute)))
                                .Add((int)VisualTransformPropertyType.SizePolicy, new PropertyValue(new Vector2((int)VisualTransformPolicyType.Absolute, (int)VisualTransformPolicyType.Absolute)))
                                .Add((int)VisualTransformPropertyType.Size, new PropertyValue(new Vector2(350, 100)));

            label.SetTransformAndSize(imageVisualTransform, new Vector2(this.SizeWidth, this.SizeHeight));
        }

        private void CreateIcon(string url, float x, float y, float w, float h, int index)
        {
            PropertyMap map = new PropertyMap();
            PropertyMap transformMap = new PropertyMap();

            map.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image))
               .Add(ImageVisualProperty.URL, new PropertyValue(url));

            VisualBase icon = VisualFactory.Instance.CreateVisual(map);

            PropertyMap imageVisualTransform = new PropertyMap();
            imageVisualTransform.Add((int)VisualTransformPropertyType.Offset, new PropertyValue(new Vector2(x, y)))
                                .Add((int)VisualTransformPropertyType.OffsetPolicy, new PropertyValue(new Vector2((int)VisualTransformPolicyType.Absolute, (int)VisualTransformPolicyType.Absolute)))
                                .Add((int)VisualTransformPropertyType.SizePolicy, new PropertyValue(new Vector2((int)VisualTransformPolicyType.Absolute, (int)VisualTransformPolicyType.Absolute)))
                                .Add((int)VisualTransformPropertyType.Size, new PropertyValue(new Vector2(w, h)))
                                .Add((int)VisualTransformPropertyType.Origin, new PropertyValue((int)Visual.AlignType.CenterBegin))
                                .Add((int)VisualTransformPropertyType.AnchorPoint, new PropertyValue((int)Visual.AlignType.CenterBegin));

            icon.SetTransformAndSize(imageVisualTransform, new Vector2(this.SizeWidth, this.SizeHeight));

            RegisterVisual(index, icon);
            icon.DepthIndex = index;
        }

        public ContactView(string name, string resDir) : base(typeof(ContactView).Name, CustomViewBehaviour.ViewBehaviourDefault)
        {
            this.name = name;
            this.resDir = resDir;

            CreateBackground(new Vector4(1.0f, 1.0f, 1.0f, 1.0f));
            CreateIcon(resDir + "/images/cbg.png", 10.0f, 5.0f, 100.0f, 100.0f, CONTACT_BG_ICON_INDEX);
            CreateIcon(resDir + "/images/contact.png", 10.0f, 5.0f, 100.0f, 100.0f, CONTACT_ICON_INDEX);
            CreateIcon(resDir + "/images/edit.png", 130.0f, 40.0f, 50.0f, 50.0f, CONTACT_EDIT_INDEX);
            CreateIcon(resDir + "/images/favorite.png", 180.0f, 40.0f, 50.0f, 50.0f, CONTACT_FAVORITE_INDEX);
            CreateIcon(resDir + "/images/delete.png", 640.0f, 40.0f, 50.0f, 50.0f, CONTACT_DELETE_INDEX);
            CreateLabel(name);
        }
    }
}