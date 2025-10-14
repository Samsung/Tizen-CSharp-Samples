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

using Tizen.NUI.BaseComponents;

namespace FirstScreen
{
    /// <summary>
    /// Example Effect wrapper for the common use case of a show+hide effect standardization.
    /// This class contains 2 effects, 1 for show and 1 for hide.
    /// It has the option of automatically staging and unstaging the effect when the animation completes.
    /// </summary>
    class ShowHideEffect
    {
        private Effect[] _showHideEffect;
        private bool _autoStage = false;
        private View _parent;

        /// <summary>
        /// Set or get show effect
        /// </summary>
        public Effect ShowEffect
        {
            get
            {
                return _showHideEffect[0];
            }

            set
            {
                _showHideEffect[0] = value;
            }
        }

        /// <summary>
        /// Set or get hide effect
        /// </summary>
        public Effect HideEffect
        {
            get
            {
                return _showHideEffect[1];
            }

            set
            {
                _showHideEffect[1] = value;
                _showHideEffect[1].Finished += OnHideEffectFinished;
            }
        }

        /// <summary>
        /// Set or get auto stage
        /// </summary>
        public bool AutoStage
        {
            get
            {
                return _autoStage;
            }

            set
            {
                _autoStage = value;
            }
        }

        /// <summary>
        /// Creates and initializes a new instance of the ShowHideEffect class.
        /// </summary>
        public ShowHideEffect()
        {
            _showHideEffect = new Effect[2];
        }

        /// <summary>
        /// Show or play show effect
        /// </summary>
        /// <param name="parent">parent view</param>
        public void Show(View parent)
        {
            //Stop & Unstage the hide effect.
            _showHideEffect[1].Finish();

            if (_autoStage)
            {
                _parent = parent;
                //Stage & Play the show effect.
                _showHideEffect[0].Show(_parent);
            }
            else
            {
                //Play the show effect.
                _showHideEffect[0].Play(); 
            }
        }

        /// <summary>
        /// Show or play hide effect
        /// </summary>
        public void Hide()
        {
            //Stop the show effect.
            _showHideEffect[0].Finish();

            if (_autoStage)
            {
                // Stage & Play the hide effect. This will be unstaged automatically when finished.
                _showHideEffect[1].Show(_parent);
            }
            else
            {
                //Play the hide effect.
                _showHideEffect[0].Play();
            }
        }

        private bool OnHideEffectFinished(object sender, Effect.EffectEventArgs e)
        {
            // If we are set to AutoStage, automatically un-stage the effect.
            if (_autoStage)
            {
                _showHideEffect[1].UnStage();
            }

            return true;
        }
    }

}
