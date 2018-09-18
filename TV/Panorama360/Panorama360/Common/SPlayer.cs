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
using Tizen.Multimedia;

namespace Panorama360
{
    /// <summary>
    /// Player manage Control
    /// </summary>
    public class SPlayer
    {
        /// <summary>
        /// Player instance
        /// </summary>
        private readonly Player _player = new Player();
        /// <summary>
        /// Player manage's construct
        /// </summary>
        public SPlayer()
        {
        }

        /// <summary>
        /// Set the Audio file address
        /// </summary>
        /// <param name="uri">Audio file absolute path</param>
        public void SetSource(string uri)
        {
            _player.SetSource(new MediaUriSource(uri));
        }

        /// <summary>
        /// To play audio
        /// </summary>
        /// <param name="isloop">Whether to loop</param>
        public async void ToPlay(bool isloop)
        {
            //prestrain
            await _player.PrepareAsync();
            var state = _player.State;
            _player.IsLooping = isloop;
            //if the player's state is prestrain ready or paused,the player will play audio. 
            if (state == Tizen.Multimedia.PlayerState.Ready || state == Tizen.Multimedia.PlayerState.Paused)
            {
                _player.Start();
            }
        }
        /// <summary>
        /// Stop playing
        /// </summary>
        public void Stop()
        {
            var state = _player.State;
            //if the player's state is Playing or paused,the player will stop playing.
            if (state == PlayerState.Playing || state == PlayerState.Paused)
            {
                _player.Stop();
            }
            //Unprepares the player.
            Unprepare();
        }

        /// <summary>
        /// Unprepares the player.
        /// </summary>
        public void Unprepare()
        {
            var state = _player.State;
            //When the player's state is Ready Playing or paused, it will Unprepares the player.
            if (state == Tizen.Multimedia.PlayerState.Ready || state == Tizen.Multimedia.PlayerState.Playing || state == Tizen.Multimedia.PlayerState.Paused)
            {
                _player.Unprepare();
            }
        }
    }

}
