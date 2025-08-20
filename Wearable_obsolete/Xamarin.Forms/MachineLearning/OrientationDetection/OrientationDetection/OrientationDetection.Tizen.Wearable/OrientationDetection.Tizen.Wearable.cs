/*
 * Copyright (c) 2020 Samsung Electronics Co., Ltd. All rights reserved.
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
 */

using Xamarin.Forms;

namespace OrientationDetection
{
  /**
   * @brief Represents xamarin forms of tizen platform app
   */
  class Program : Xamarin.Forms.Platform.Tizen.FormsApplication
  {
    /**
     * @brief On create method
     */
    protected override void OnCreate()
    {
      base.OnCreate();
      LoadApplication(new App());
    }

    /**
     * @breif Main method of Orientation Detection for tizen wearable
     */
    static void Main(string[] args)
    {
      var app = new Program();
      Forms.Init(app);
      app.Run(args);
    }
  }
}
