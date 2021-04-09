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

using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using Tizen.MachineLearning.Inference;
using Log = Tizen.Log;

namespace OrientationDetection
{
  /**
   * @brief Orientation Detection App
   */
  public class App : Application
  {
    private const string TAG = "OrientationDetection";
    private Label label;
    private static string ResourcePath = Tizen.Applications.Application.Current.DirectoryInfo.Resource;
    private int orientation = 12;
    private Pipeline pipeline_handle;
    private Pipeline.SinkNode sink_handle;

    /**
     * @brief Initialize the nnstreamer pipeline and manage the handles
     */
    private void Init_pipeline()
    {
      string model_path = ResourcePath + "orientation_detection.tflite";
      string pipeline_description =
          "tensor_src_tizensensor type=accelerometer framerate=10/1 ! " +
          "tensor_filter framework=tensorflow-lite model=" + model_path + " ! " +
          "tensor_sink name=sinkx sync=true";

      pipeline_handle = new Pipeline(pipeline_description);
      if (pipeline_handle == null)
      {
        Log.Error(TAG, "Cannot create pipeline");
        return;
      }

      sink_handle = pipeline_handle.GetSink("sinkx");
      if (sink_handle == null)
      {
        Log.Error(TAG, "Cannot get sink handle");
        return;
      }

      sink_handle.DataReceived += SinkxDataReceived;
    }

    /**
     * @brief Destroy the pipeline
     */
    private void Destroy_pipeline()
    {
      sink_handle.DataReceived -= SinkxDataReceived;
      pipeline_handle.Dispose();
    }

    /**
     * @brief get output from the tensor_sink and update the orientation
     */
    private void SinkxDataReceived(object sender, DataReceivedEventArgs args)
    {
      TensorsData data_from_sink = args.Data;
      if (data_from_sink == null)
      {
        Log.Error(TAG, "Cannot get TensorsData");
        return;
      }

      byte[] out_buffer;
      out_buffer = data_from_sink.GetTensorData(0);
      float[] output = new float[4];
      Buffer.BlockCopy(out_buffer, 0, output, 0, out_buffer.Length);

      orientation = 0;

      for (int i = 1; i < 4; ++i)
      {
        if (output[orientation] < output[i])
        {
            orientation = i;
        }
      }

      orientation *= 3;
      if (orientation == 0)
      {
        orientation = 12;
      }
    }

    /**
     * @brief Update the label every 50ms
     */
    private async void LabelUpdateLoop()
    {
      while (true)
      {
        await Task.Delay(50);
        label.Text = string.Format("{0}", orientation);
      }
    }

    /**
     * @brief Orientation Detection App Class
     */
    public App()
    {
      label = new Label
      {
        Text = "12",
        HorizontalOptions = LayoutOptions.Center,
        FontSize = 24,
      };

      MainPage = new ContentPage
      {
        Content = new StackLayout
        {
          VerticalOptions = LayoutOptions.Center,
          Children =
          {
            label,
            new Label
            {
              HorizontalTextAlignment = TextAlignment.Center,
              Text = "UP",
              FontSize = 24,
            },
          }
        }
      };
    }

    /**
     * @brief Handle when app starts
     */
    protected override void OnStart()
    {
      LabelUpdateLoop();
    }

    /**
     * @brief Handle when app sleeps
     */
    protected override void OnSleep()
    {
      pipeline_handle.Stop();
      Destroy_pipeline();
    }

    /**
     * @brief Handle when app resumes
     */
    protected override void OnResume()
    {
      Init_pipeline();
      pipeline_handle.Start();
    }
  }
}
