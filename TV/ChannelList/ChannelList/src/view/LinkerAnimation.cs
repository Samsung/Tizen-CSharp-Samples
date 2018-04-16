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

using System;
using System.Collections.Generic;
using System.Linq;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

/// <summary>
/// namespace for Tizen.NUI package
/// </summary>
namespace ChannelList
{
    /// <summary>
    /// Workaround Solution to support list scroll, 
    /// need to change to MultiObjectAnimation which support Via.
    /// </summary>
    /// <code>
    /// 	scrollAni = new LinkerAnimation(mItemGroup, scrollAniDuration, 100, 0.1f);
    /// </code> 
    internal class LinkerAnimation
    {
        private List<Animation> scrollAniList; //scroll animation list
        private int startDuration; // start duration
        private int endDuration; //end duration
        private View targetView; //target view
        private float accRatio; //acc ratio
        
        private event EventHandler ViaEventHandler; //via event handler
        
        /// <summary>
        /// Via event handler, user can add/remove
        /// </summary>
        public event EventHandler ViaEvent
        {
            add
            {
                ViaEventHandler += value;
            }

            remove
            {
                ViaEventHandler -= value;
            }
        }
    
        /// <summary>
        /// Property to get animation state.
        /// </summary>
        public Animation.States States
        {
            get
            {
                Animation.States ret = Animation.States.Stopped;
                foreach (Animation ani in scrollAniList)
                {
                    if (ani.State == Animation.States.Playing)
                    {
                        ret = Animation.States.Playing;
                        break;
                    }
                }

                return ret;
            }
        }
    
        /// <summary>
        /// Constructor to create LinkerAnimation.
        /// </summary>
        /// <param name="obj">Animation view.</param>
        /// <param name="startDuration">Start duration.</param>
        /// <param name="endDuration">End duration.</param>
        /// <param name="accRatio">Ratio.</param>
        public LinkerAnimation(View obj, int startDuration, int endDuration, float accRatio)
        {
            this.startDuration = startDuration;
            this.endDuration = endDuration;
            this.accRatio = accRatio;
            targetView = obj;
            
            scrollAniList = new List<Animation>();
        }
        
        /// <summary>
        /// Set next destination for the aniamtion.
        /// </summary>
        /// <param name="property">Property name which need animate.</param>
        /// <param name="destValue">Destination value.</param>
        public void SetDestination(string property, float destValue)
        {
            int duration = 0;
            int count = scrollAniList.Count;
            if (count == 0)
            {
                duration = startDuration;
            }
            else
            {
                Animation lastAni = scrollAniList.ElementAt(count - 1);
                duration = lastAni.Duration - 300 > endDuration ? lastAni.Duration - 100 : endDuration;
            }

            Animation ani = new Animation(startDuration);
            ani.AnimateTo(targetView, property, destValue);
            ani.Finished += OnAnimationFinished;
            scrollAniList.Add(ani);
        }
    
        /// <summary>
        /// Play animation.
        /// </summary>
        public void Play()
        {
            if (IsPlaying())
            {
                return;
            }
            
            if (scrollAniList.Count != 0)
            {
                scrollAniList.ElementAt(0).Play();
            }
        }
        
        /// <summary>
        /// Stop animation
        /// </summary>
        /// <param name="action">EndAction for stop.</param>
        public void Stop(Animation.EndActions action = Animation.EndActions.Cancel)
        {
            foreach (Animation ani in scrollAniList)
            {
                if (ani.State == Animation.States.Playing)
                {
                    ani.Stop(action);
                }
            }
        }

        /// <summary>
        /// Check whehter animation is playing.
        /// </summary>
        /// <returns>Playing or not.</returns>
        public bool IsPlaying()
        {
            bool ret = false;
            foreach (Animation ani in scrollAniList)
            {
                if (ani.State == Animation.States.Playing)
                {
                    ret = true;
                    break;
                }

            }

            return ret;
        }
    
        /// <summary>
        /// Dispose of LinkerAnimation.
        /// </summary>
        public void Dispose()
        {
            foreach (Animation ani in scrollAniList)
            {
                ani?.Stop();
                ani?.Dispose();
            }

            scrollAniList.Clear();
        }
        
        private void OnAnimationFinished(object obj, EventArgs args)
        {
            if (obj == null)
            {
                return;
            }

            Animation ani = obj as Animation;
            scrollAniList.Remove(ani);
            
            ViaEventHandler(this, null);
            if (scrollAniList.Count != 0)
            {
                scrollAniList[0].Play();
            }
        }
    }
}
