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
using System.Collections.Generic;

namespace FirstScreen
{
    // TODO: No need for overall duration if animators start and end times are percentage fractions.
    // TODO: Remove need for string lookups by encapsulating Views.
    // TODO: Create Serializable class and inherit from, allowing Effect to be data-driven.
    // TODO: It is better if individual animators are loopable. This saves us creating multiple animations AND doing a slow initial property set when the Effect is played.
    // TODO: Add "OnFinished" signal to the effect (technically this should not exist on individual elements as the effect should only make sense as a whole).
    // TODO: If animation supports start values (like animate between keyframes) we can remove the PreparePlay() method.
    // TODO: We can add built in common use actions - like "fade out" or "pulse"
    // TODO: We can add common effect wrappers - like ShowHideEffect which has 2 effects and the using View can rely on which is show and hide as it is standardized.
    public class Effect
    {
        /// <summary>
        /// Enumeration for the Element type of effect
        /// </summary>
        public enum ElementType
        {
            AnimateTo,
            AnimateBy,
            AnimateBetween,
            AnimateKeyframes,
            AnimatePath
        }

        public class EffectElementBase
        {
            public ElementType elementType;
            public String target;
            public String property;
            public object targetValue;
            public int startTime;
            public int endTime;
            public AlphaFunction alphaFunction;
            public bool looping = false;

            /// <summary>
            /// Creates and initializes a new instance of the EffectElementBase class.
            /// </summary>
            /// <param name="inAnimationMode">element type</param>
            /// <param name="inTarget">target</param>
            /// <param name="inProperty">property</param>
            /// <param name="inTargetValue">target value</param>
            /// <param name="inAlphaFunction">alpha function</param>
            /// <param name="inStartTime">start time</param>
            /// <param name="inEndTime">end time</param>
            public EffectElementBase(ElementType inAnimationMode, String inTarget, String inProperty, object inTargetValue, AlphaFunction inAlphaFunction, int inStartTime = 0, int inEndTime = -1)
            {
                elementType = inAnimationMode;
                target = inTarget;
                property = inProperty;
                targetValue = inTargetValue;
                startTime = inStartTime;
                endTime = inEndTime;
                alphaFunction = inAlphaFunction;
            }

            /// <summary>
            /// Set or get whether the effect is looping
            /// </summary>
            public bool Looping
            {
                get
                {
                    return looping;
                }

                set
                {
                    looping = value;
                }
            }
        }
        
        /// <summary>
        /// Set or get name string
        /// </summary>
        public String Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        /// <summary>
        /// Set or get use animation
        /// </summary>
        public Animation UseAnimation
        {
            get
            {
                return _useAnimation;
            }

            set
            {
                _useAnimation = value;
                EndAnimations();
                _animations.Clear();
                _animations.Add(_useAnimation);
            }
        }

        /// <summary>
        /// Set or get animation's end actions
        /// </summary>
        public Animation.EndActions EndAction
        {
            get
            {
                return _endAction;
            }

            set
            {
                _endAction = value;
            }
        }

        /// <summary>
        /// Set or get tag value
        /// </summary>
        public int Tag
        {
            get
            {
                return _tag;
            }

            set
            {
                _tag = value;
            }
        }

        public class AnimateTo : EffectElementBase
        {
            /// <summary>
            /// Creates and initializes a new instance of the AnimateTo class.
            /// </summary>
            /// <param name="inTarget">target string</param>
            /// <param name="inProperty">property string</param>
            /// <param name="inTargetValue">target value</param>
            /// <param name="inAlphaFunction">alpha function</param>
            /// <param name="inStartTime">start time</param>
            /// <param name="inEndTime">end time</param>
            public AnimateTo(String inTarget, String inProperty, object inTargetValue, AlphaFunction inAlphaFunction, int inStartTime = 0, int inEndTime = -1)
            : base(ElementType.AnimateTo, inTarget, inProperty, inTargetValue, inAlphaFunction, inStartTime, inEndTime)
            {
            }
        }

        public class AnimateBy : EffectElementBase
        {
            /// <summary>
            /// Creates and initializes a new instance of the AnimateBy class.
            /// </summary>
            /// <param name="inTarget">target string</param>
            /// <param name="inProperty">property string</param>
            /// <param name="inTargetValue">target value</param>
            /// <param name="inAlphaFunction">alpha function</param>
            /// <param name="inStartTime">start time, default value is 0</param>
            /// <param name="inEndTime">end time, default value is -1</param>
            public AnimateBy(String inTarget, String inProperty, object inTargetValue, AlphaFunction inAlphaFunction, int inStartTime = 0, int inEndTime = -1)
            : base(ElementType.AnimateBy, inTarget, inProperty, inTargetValue, inAlphaFunction, inStartTime, inEndTime)
            {
            }
        }

        public class AnimateBetween : EffectElementBase
        {
            public object startValue;

            /// <summary>
            /// Creates and initializes a new instance of the AnimateBetween class.
            /// </summary>
            /// <param name="inTarget">target string</param>
            /// <param name="inProperty">property string</param>
            /// <param name="inStartValue">start value</param>
            /// <param name="inTargetValue">target value</param>
            /// <param name="inAlphaFunction">alpha function</param>
            /// <param name="inStartTime">start time, default value is 0</param>
            /// <param name="inEndTime">end time, default value is -1</param>
            public AnimateBetween(String inTarget, String inProperty, object inStartValue, object inTargetValue, AlphaFunction inAlphaFunction, int inStartTime = 0, int inEndTime = -1)
            : base(ElementType.AnimateBetween, inTarget, inProperty, inTargetValue, inAlphaFunction, inStartTime, inEndTime)
            {
                startValue = inStartValue;
            }
        }

        public class AnimateKeyframes : EffectElementBase
        {
            public KeyFrames keyframes;
            public Animation.Interpolation interpolation;

            /// <summary>
            /// Creates and initializes a new instance of the AnimateKeyframes class.
            /// </summary>
            /// <param name="inTarget">target string</param>
            /// <param name="inProperty">property string</param>
            /// <param name="inKeyframes">key frames</param>
            /// <param name="inInterpolation">Interpolation</param>
            /// <param name="inAlphaFunction">alpha function</param>
            /// <param name="inStartTime">start time, default value is 0</param>
            /// <param name="inEndTime">end time, default value is -1</param>
            public AnimateKeyframes(String inTarget, String inProperty, KeyFrames inKeyframes, Animation.Interpolation inInterpolation, AlphaFunction inAlphaFunction, int inStartTime = 0, int inEndTime = -1)
            : base(ElementType.AnimateBetween, inTarget, inProperty, null, inAlphaFunction, inStartTime, inEndTime)
            {
                keyframes = inKeyframes;
                interpolation = inInterpolation;
            }
        }

        public class AnimatePath : EffectElementBase
        {
            public Path path;

            /// <summary>
            /// Creates and initializes a new instance of the AnimatePath class.
            /// </summary>
            /// <param name="inTarget">target string</param>
            /// <param name="inPath">path</param>
            /// <param name="forward">forward</param>
            /// <param name="inAlphaFunction">alpha function</param>
            /// <param name="inStartTime">start time, default value is 0</param>
            /// <param name="inEndTime">end time, default value is -1</param>
            public AnimatePath(String inTarget, Path inPath, Vector3 forward, AlphaFunction inAlphaFunction, int inStartTime = 0, int inEndTime = -1)
            : base(ElementType.AnimatePath, inTarget, "Position", forward, inAlphaFunction, inStartTime, inEndTime)
            {
                path = inPath;
            }
        }

        public class EffectEventArgs : EventArgs
        {
            public String name;
            public int tag;
        }

        public EventHandlerWithReturnType<object, EffectEventArgs, bool> Finished;

        private List<EffectElementBase> _elements;
        private List<Animation> _animations;
        private String _name;
        private int _tag;
        private View _rootView;
        private int _duration;
        private Animation _useAnimation;
        private Animation.EndActions _endAction = Animation.EndActions.Discard;
        private int _animatorsRunning;

        /// <summary>
        /// Creates and initializes a new instance of the Effect class.
        /// </summary>
        /// <param name="parent">parent view</param>
        /// <param name="duration">effect duration, default value is 1000</param>
        public Effect(View parent, int duration = 1000)
        {
            _elements = new List<EffectElementBase>();
            _animations = new List<Animation>();
            _rootView = parent;
            _duration = duration;
        }

        /// <summary>
        /// Get certain index element
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>certain index element</returns>
        public EffectElementBase At(int index)
        {
            return _elements[index];
        }

        /// <summary>
        /// Add element to list
        /// </summary>
        /// <param name="element">effect element</param>
        public void AddAction(EffectElementBase element)
        {
            _elements.Add(element);
        }

        /// <summary>
        /// Play animations
        /// </summary>
        /// <param name="targetView">target view</param>
        /// <param name="speedFactor">speed factor,default value is 1.0f</param>
        public void Play(View targetView = null, float speedFactor = 1.0f)
        {
            EndAnimations();

            Animation animation = new Animation(); // Uninitialized as created as required during loop.
            if (_useAnimation == null)
            {
                _animations.Clear();
            }
            else
            {
                animation = _animations[0];
                animation.SpeedFactor = speedFactor;
            }

            PreparePlay();

            // We initialise this true to force an animation to be created on the first loop.
            bool lastElementLoopState = true;

            View target = targetView;

            // Add all the animations within the effect-elements to the final animation.
            for (int i = 0; i < _elements.Count; ++i)
            {
                EffectElementBase element = _elements[i];
                int useEndTime = element.endTime == -1 ? _duration : element.endTime;

                if (targetView == null)
                {
                    target = _rootView.FindChildByName(element.target);
                }

                if (target == null)
                {
                    continue;
                }
                
                // We want to know if we are going from looping to non-looping, or vice-versa. This is so we know when to create a new animation.
                // It is important that we also do this if we stay as looping, so the only valid state to NOT create an animation is neither old or new are looping.
                // We also check if the current animation has been populated, as if it is empty it can be used anyway.
                // If we are using a user provide animation, we skip this.
                if ((element.looping || lastElementLoopState) && (_useAnimation == null))
                {
                    _animations.Add(new Animation());
                    animation = _animations[_animations.Count - 1];
                    animation.Duration = _duration; //TODO: Remove the need for this ? Or keep it if start and end times become percentage fractions.
                    animation.Looping = element.looping;
                    animation.EndAction = _endAction;
                    animation.SpeedFactor = speedFactor;
                }

                lastElementLoopState = element.looping;
                
                // Add an animator to the current animation.
                switch (element.elementType)
                {
                    case Effect.ElementType.AnimateTo:
                    case Effect.ElementType.AnimateBetween:
                    {
                        animation.AnimateTo(target, element.property, element.targetValue, element.startTime, useEndTime, element.alphaFunction);
                        break;
                    }

                    case Effect.ElementType.AnimateBy:
                    {
                        animation.AnimateBy(target, element.property, element.targetValue, element.startTime, useEndTime, element.alphaFunction);
                        break;
                    }

                    case Effect.ElementType.AnimateKeyframes:
                    {
                        AnimateKeyframes animateKeyframes = element as AnimateKeyframes;
                        animation.AnimateBetween(target, element.property, animateKeyframes.keyframes, element.startTime, useEndTime, animateKeyframes.interpolation, element.alphaFunction);
                        break;
                    }

                    case Effect.ElementType.AnimatePath:
                    {
                        AnimatePath animatePath = element as AnimatePath;
                        animation.AnimatePath(target, animatePath.path, element.targetValue as Vector3, element.startTime, useEndTime, element.alphaFunction);
                        break;
                    }
                }
            }

            // Loop through the created animations and play them.
            _animatorsRunning = _animations.Count;
            for (int i = 0; i < _animatorsRunning; ++i)
            {
                // Play the current animation
                _animations[i].Finished += AnimatorFinishedHandler;
                _animations[i].Play();
            }
        }

        /// <summary>
        /// Add root view in parent and play root view
        /// </summary>
        /// <param name="parent">parent view</param>
        public void Show(View parent)
        {
            parent.Add(_rootView);
            Play();
        }

        /// <summary>
        /// If root view and it's parent not null, remove root view
        /// </summary>
        public void UnStage()
        {
            if (_rootView != null && _rootView.Parent != null)
            {
                _rootView.Parent.Remove(_rootView);
            }
        }

        /// <summary>
        /// Finish all animations
        /// </summary>
        public void Finish()
        {
            for (int i = 0; i < _animations.Count; ++i)
            {
                _animations[i].Stop();
            }
        }

        private void AnimatorFinishedHandler(object source, EventArgs e)
        {
            if (--_animatorsRunning <= 0)
            {
                // All animators have finished
                if (Finished != null)
                {
                    // Signal to listeners (if any).
                    EffectEventArgs eventArgs = new EffectEventArgs();
                    eventArgs.name = _name;
                    eventArgs.tag = _tag;
                    Finished.Invoke(this, eventArgs);
                }
            }
        }

        private void PreparePlay()
        {
            for (int i = 0; i < _elements.Count; ++i)
            {
                EffectElementBase element = _elements[i];

                // Check if we have a start value that needs setting.
                if (element.elementType == ElementType.AnimateBetween)
                {
                    View target = _rootView.FindChildByName(element.target);
                    if (target == null)
                    {
                        Console.WriteLine("Effect: Error: Could not find target: " + element.target);
                    }
                    else
                    {
                        int propertyIndex = target.GetPropertyIndex(element.property);
                        if (propertyIndex == -1)
                        {
                            Console.WriteLine("Effect: Error: Could not find property: " + element.property + " in target " + element.target);
                        }
                        else
                        {
                            PropertyValue propertyValue = PropertyValue.CreateFromObject((element as AnimateBetween).startValue);
                            target.SetProperty(propertyIndex, propertyValue);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// End all animations
        /// </summary>
        private void EndAnimations()
        {
            for (int i = 0; i < _animations.Count; ++i)
            {
                _animations[i].Clear();
            }
        }
    }

}
