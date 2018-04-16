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
using Tizen;
using ListSample;

/// <summary>
/// Namespace for channel list sample
/// </summary>
namespace ChannelList
{
    /// <summary>
    /// Main class for channel list sample
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main function.
        /// </summary>
        /// <param name="args">args</param>
        static void Main(string[] args)
        {
            ChannelListSample sample = new ChannelListSample();
            sample.Run(args);
            Log.Info("NUI.ChannelList", "========== Hello, world ==========");
        }
    }
}
