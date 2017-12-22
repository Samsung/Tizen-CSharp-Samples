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
using Tizen.NUI.UIComponents;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Constants;
using System;

namespace FirstScreen
{
    /// <summary>
    /// The class of focus date.
    /// </summary>
    public class FocusData
    {
        // Name used for FocusData object (mainly to differentiate key frame animation )
        private string _name;
        // Image File Name (to be loaded from disk) used for
        // ImageView used in key frame animation
        private string _imageName;
        // ParentOrigin applied to ImageView
        private Position _parentOrigin;
        // InitSize used for key frame animation
        private Vector2 _initSize;
        // TargetSize used for key frame animation
        private Vector2 _targetSize;
        // KeyFrameStart used for key frame animation
        private float _keyFrameStart;
        // KeyFrameEnd used for key frame animation
        private float _keyFrameEnd;
        // Direction used for key frame animation
        private Direction _direction;
        // ImageView used in key frame animation
        private ImageView _imageFocus;

        /// <summary>
        /// Initialize FocusData used for key frame animation
        /// </summary>
        /// <param name= "name">name</param>
        /// <param name= "imageName">image url</param>
        /// <param name= "direction">direction</param>
        /// <param name= "parentOrigin">parentOrigin</param>
        /// <param name= "initSize">inital size</param>
        /// <param name= "targetSize">target size</param>
        /// <param name= "keyFrameStart">key frame start</param>
        /// <param name= "keyFrameEnd">key frame end</param>
        public FocusData(string name, string imageName, Direction direction, Position parentOrigin, Size2D initSize,
                         Size2D targetSize, float keyFrameStart, float keyFrameEnd)
        {
            _name = name;
            _imageName = imageName;
            _parentOrigin = parentOrigin;
            _initSize = initSize;
            _targetSize = targetSize;
            _keyFrameStart = keyFrameStart;
            _keyFrameEnd = keyFrameEnd;
            _direction = direction;

             // Target
            _imageFocus = new ImageView(Constants.ImageResourcePath + "/focuseffect/" + _imageName);

            // Set _imageFocus's position.
            _imageFocus.ParentOrigin = _parentOrigin;
            _imageFocus.PivotPoint = PivotPoint.Center;
            _imageFocus.PositionUsesPivotPoint = true;
            _imageFocus.Name = _name;
        }

        /// <summary>
        /// The enum of direction.
        /// </summary>
        public enum Direction
        {
            Horizontal,
            Vertical
        };

        /// <summary>
        /// Get/Set FocusDirection
        /// </summary>
        public Direction FocusDirection
        {
            get { return _direction; }
            set { _direction = value; }
        }

        /// <summary>
        /// Get/Set Name
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Get/Set _imageName
        /// </summary>
        public string ImageName
        {
            get { return _imageName; }
            set { _imageName = value; }
        }

        /// <summary>
        /// Get/Set _parentOrigin
        /// </summary>
        public Position ParentOrigin
        {
            get
            {
                return _parentOrigin;
            }

            set
            {
                _parentOrigin = value;
                _imageFocus.ParentOrigin = _parentOrigin;
            }
        }

        /// <summary>
        /// Get/Set _initSize
        /// </summary>
        public Size2D InitSize
        {
            get { return _initSize; }
            set { _initSize = value; }
        }

        /// <summary>
        /// Get/Set _targetSize
        /// </summary>
        public Size2D TargetSize
        {
            get { return _targetSize; }
            set { _targetSize = value; }
        }

        /// <summary>
        /// Get/Set _keyFrameStart
        /// </summary>
        public float KeyFrameStart
        {
            get { return _keyFrameStart; }
            set { _keyFrameStart = value; }
        }

        /// <summary>
        /// Get/Set _keyFrameEnd
        /// </summary>
        public float KeyFrameEnd
        {
            get { return _keyFrameEnd; }
            set { _keyFrameEnd = value; }
        }

        /// <summary>
        /// Get _imageFocus
        /// </summary>
        public ImageView ImageItem
        {
            get { return _imageFocus; }
        }

    }
}
