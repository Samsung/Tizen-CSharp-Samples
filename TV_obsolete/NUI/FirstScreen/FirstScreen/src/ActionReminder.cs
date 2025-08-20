/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */

using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using System;

namespace FirstScreen
{
    /// <summary>
    /// Manager action reminder animation
    /// </summary>
    public class ActionReminder
    {
        private Vector2 _remoteTouchPointOrigin;
        private ImageView _remoteTouch;
        private Animation _actionReminderAnimation;
        private int _actionReminderTouchAnimationDuration;
        private int _actionReminderAnimationDuration;
        private ImageView _actionReminderRemote;
        private ImageView _actionReminderRemoteCloseUp;
        private int _actionReminderProgressCount;
        private int _actionReminderProgressMax;
        private bool _actionReminderTouchOriginToggle;
        private View _parent;
        private Effect[] _showHideEffects;
        private Effect _touchEffect;

        /// <summary>
        /// Class construction
        /// </summary>
        /// <param name="parent">parent view</param>
        public ActionReminder(View parent)
        {
            _parent = parent;
            _actionReminderProgressCount = 0;
            _actionReminderTouchOriginToggle = false;
            _actionReminderProgressMax = 4;

            _actionReminderAnimationDuration = Constants.ActionReminderAnimationDuration;
            _actionReminderTouchAnimationDuration = Constants.ActionReminderTouchAnimationDuration;

            // We want to zoom into the circular control on the remote. This is slightly higher up than the center of the image.
            float imageZoomCenter = 0.354f;

            _actionReminderRemote = new ImageView(Constants.ResourcePath + "/images/ActionReminder/remote.png");
            _actionReminderRemote.Name = "remote";
            _actionReminderRemote.WidthResizePolicy = ResizePolicyType.UseNaturalSize;
            _actionReminderRemote.HeightResizePolicy = ResizePolicyType.UseNaturalSize;
            _actionReminderRemote.PivotPoint = new Position(0.5f, imageZoomCenter, 0.5f);
            _actionReminderRemote.Scale = new Size(0.1f, 0.1f, 0.1f);
            _actionReminderRemote.ParentOrigin = Tizen.NUI.ParentOrigin.Center;
            _actionReminderRemote.PositionUsesPivotPoint = true;
            _actionReminderRemote.SetProperty(_actionReminderRemote.GetPropertyIndex("ColorAlpha"), new PropertyValue(0.0f));

            _actionReminderRemoteCloseUp = new ImageView(Constants.ResourcePath + "/images/ActionReminder/remote-close-up.png");
            _actionReminderRemoteCloseUp.Name = "remoteCloseUp";
            _actionReminderRemoteCloseUp.WidthResizePolicy = ResizePolicyType.UseNaturalSize;
            _actionReminderRemoteCloseUp.HeightResizePolicy = ResizePolicyType.UseNaturalSize;
            _actionReminderRemoteCloseUp.PivotPoint = Tizen.NUI.PivotPoint.Center;
            _actionReminderRemoteCloseUp.ParentOrigin = new Position(0.5f, imageZoomCenter, 0.5f);
            _actionReminderRemoteCloseUp.PositionUsesPivotPoint = true;
            _actionReminderRemoteCloseUp.Position = new Position(-0.6f, 0.0f, 0.0f);
            _actionReminderRemoteCloseUp.Scale = new Size(0.1f, 0.1f, 0.1f);
            _actionReminderRemoteCloseUp.InheritScale = false;
            //_actionReminderRemoteCloseUp.InheritOpacity = false;
            _actionReminderRemoteCloseUp.SetProperty(_actionReminderRemoteCloseUp.GetPropertyIndex("ColorAlpha"), new PropertyValue(0.0f));

            _remoteTouch = new ImageView(Constants.ResourcePath + "/images/ActionReminder/touch-point-highlight.png");
            _remoteTouch.Name = "remoteTouch";
            _remoteTouch.WidthResizePolicy = ResizePolicyType.UseNaturalSize;
            _remoteTouch.HeightResizePolicy = ResizePolicyType.UseNaturalSize;
            _remoteTouch.PivotPoint = Tizen.NUI.PivotPoint.Center;
            _remoteTouch.ParentOrigin = new Position(0.315f, 0.41f, 0.5f);
            _remoteTouch.PositionUsesPivotPoint = true;
            _remoteTouch.InheritScale = false;
            //_remoteTouch.InheritOpacity = false;
            _remoteTouch.SetProperty(_actionReminderRemoteCloseUp.GetPropertyIndex("ColorAlpha"), new PropertyValue(0.0f));
            _remoteTouchPointOrigin = new Vector2(0.1855f, 0.408f);

            _actionReminderRemoteCloseUp.Add(_remoteTouch);
            _actionReminderRemote.Add(_actionReminderRemoteCloseUp);

            _actionReminderAnimation = new Animation(_actionReminderAnimationDuration);
            _actionReminderAnimation.Finished += AnimationFinished;
            _actionReminderProgressCount = 0;

            // Define the animation effects used by the ActionReminder.
            CreateEffects();
        }

        /// <summary>
        /// Get action reminder remote view
        /// </summary>
        /// <returns>action reminder remote view</returns>
        public View GetView()
        {
            return _actionReminderRemote;
        }

        /// <summary>
        /// play the effect (forward)
        /// </summary>
        public void Show()
        {
            PlayAnimation(false);
        }

        /// <summary>
        /// Create effects
        /// </summary>
        private void CreateEffects()
        {
            // TODO: These 3 effects could be timelined in to 1 automated effect.
            _showHideEffects = new Effect[2];
            int duration = _actionReminderAnimationDuration;
            int durationSlice = duration / 10;
            float scale = 1.77f;
            float scaleClose = 0.6f;
            AlphaFunction overshootAlphaFunction = new AlphaFunction(new Vector2(0.32f, 0.08f), new Vector2(0.38f, 1.72f));
            _showHideEffects[0] = new Effect(_actionReminderRemote);
            _showHideEffects[0].UseAnimation = _actionReminderAnimation; // Tell the effect to use this animation instead of creating one. We do this to use our custom OnAnimationFinished handler.
            _showHideEffects[0].AddAction(new Effect.AnimateTo("remote", "Scale", new Size(scale, scale, scale), overshootAlphaFunction, 0, duration));
            _showHideEffects[0].AddAction(new Effect.AnimateTo("remote", "ColorAlpha", 1.0f, new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseIn), 0, durationSlice));
            _showHideEffects[0].AddAction(new Effect.AnimateTo("remote", "ColorAlpha", 0.0f, new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseIn), durationSlice * 3, durationSlice * 5));
            _showHideEffects[0].AddAction(new Effect.AnimateTo("remoteCloseUp", "Scale", new Size(scaleClose, scaleClose, scaleClose), overshootAlphaFunction, 0, duration));
            _showHideEffects[0].AddAction(new Effect.AnimateTo("remoteCloseUp", "ColorAlpha", 1.0f, new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseIn), durationSlice * 2, durationSlice * 4));

            scale = 0.3f;
            scaleClose = 0.1f;
            _showHideEffects[1] = new Effect(_actionReminderRemote);
            _showHideEffects[1].UseAnimation = _actionReminderAnimation;
            _showHideEffects[1].AddAction(new Effect.AnimateTo("remoteCloseUp", "Scale", new Size(scaleClose, scaleClose, scaleClose), new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOut), 0, duration));
            _showHideEffects[1].AddAction(new Effect.AnimateTo("remoteCloseUp", "ColorAlpha", 0.0f, new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseIn), durationSlice * 3, durationSlice * 5));
            _showHideEffects[1].AddAction(new Effect.AnimateTo("remote", "Scale", new Size(scale, scale, scale), new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOut), 0, duration));
            _showHideEffects[1].AddAction(new Effect.AnimateTo("remote", "ColorAlpha", 1.0f, new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseIn), durationSlice * 2, durationSlice * 4));
            _showHideEffects[1].AddAction(new Effect.AnimateTo("remote", "ColorAlpha", 0.0f, new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseIn), duration - durationSlice, duration));

            scale = 0.8f;
            _touchEffect = new Effect(_remoteTouch);
            _touchEffect.UseAnimation = _actionReminderAnimation;
            _touchEffect.AddAction(new Effect.AnimateTo("remoteTouch", "Scale", new Size(scale, scale, scale), overshootAlphaFunction, 0, _actionReminderTouchAnimationDuration));
            _touchEffect.AddAction(new Effect.AnimateTo("remoteTouch", "ColorAlpha", 1.0f, new AlphaFunction(AlphaFunction.BuiltinFunctions.Bounce), 0, _actionReminderTouchAnimationDuration));
        }

        /// <summary>
        /// Play effect(forward or reverse)
        /// </summary>
        /// <param name="reverse">Is reverse or not</param>
        private void PlayAnimation(bool reverse)
        {
            if (!reverse)
            {
                _parent.Add(_actionReminderRemote);
                _actionReminderProgressCount = 0;
            }

            _actionReminderAnimation.Duration = _actionReminderAnimationDuration;

            // Play the effect (forward or reverse).
            _showHideEffects[reverse ? 1 : 0].Play();
        }

        /// <summary>
        /// This function be invoked when animation finished
        /// </summary>
        /// <param name="source">animation object</param>
        /// <param name="e">event</param>
        private void AnimationFinished(object source, EventArgs e)
        {
            // Animation state machine:
            // Guides the animation though each phase.
            if (_actionReminderProgressCount++ < _actionReminderProgressMax)
            {
                // Position the touch effect horizontally, and toggle the position for the next play.
                float touchPointXOrigin = 0.5f + (_actionReminderTouchOriginToggle ? _remoteTouchPointOrigin.X : -_remoteTouchPointOrigin.X);
                _remoteTouch.ParentOrigin = new Position(touchPointXOrigin, _remoteTouchPointOrigin.Y, 0.5f);
                _remoteTouch.Scale = new Size(0.1f, 0.1f, 0.1f);
                _actionReminderTouchOriginToggle = !_actionReminderTouchOriginToggle;
                _actionReminderAnimation.Duration = _actionReminderTouchAnimationDuration;

                // Play the touch effect.
                _touchEffect.Play();
            }
            else if (_actionReminderProgressCount == (_actionReminderProgressMax + 1))
            {
                PlayAnimation(true);
            }
            else if (_actionReminderProgressCount == (_actionReminderProgressMax + 2))
            {
                // Finished the animation state machine. Remove it from stage.
                _parent.Remove(_actionReminderRemote);
            }
        }
    }

}
