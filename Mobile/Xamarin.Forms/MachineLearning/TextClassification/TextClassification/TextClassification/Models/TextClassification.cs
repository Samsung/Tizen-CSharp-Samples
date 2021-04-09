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
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using Tizen.MachineLearning.Inference;
using Log = Tizen.Log;

namespace TextClassification
{
    /// <summary>
    /// Handle the input text
    /// </summary>
    public class HandleText
    {
        const string TAG = "TextClassification.Models";

        private static string ResourcePath { get => Tizen.Applications.Application.Current.DirectoryInfo.Resource; }
        public string[] tokenized_label;
        public string[] tokenized_vocab;
        private Hashtable ht = new Hashtable();

        /// <summary>
        /// HandleText Constructor
        /// </summary>
        public HandleText()
        {
        }

        /// <summary>
        /// Split the input text and convert to float array.
        /// </summary>
        /// <param name="str"> user input string </param>
        /// <returns> Converted float array from user input </returns>
        public float[] SplitStr(string str)
        {
            float[] float_array = new float[256];
            int cnt, len, start, unknown, pad, value;

            start = Int32.Parse((string)ht["<START>"]);
            pad = Int32.Parse((string)ht["<PAD>"]);
            unknown = Int32.Parse((string)ht["<UNKNOWN>"]);

            float_array[0] = (float)start;

            /// Split words and convert to lowercase
            string[] words = str.ToLower().Split(' ');
            len = words.Length;

            for (cnt = 1; cnt <= len; cnt++)
            {
                /// Check words and fill the float array with proper values
                if (ht.ContainsKey(words[cnt - 1]))
                {
                    value = Int32.Parse((string)ht[words[cnt - 1]]);
                    float_array[cnt] = (float)value;
                }
                else
                {
                    Log.Debug(TAG, "Given word is not contained.");
                    float_array[cnt] = (float)unknown;
                }
            }

            /// Remainers are filled with 0
            while (cnt < 256)
            {
                float_array[cnt++] = (float)pad;
            }

            return float_array;
        }

        /// <summary>
        /// Load files and make hash table
        /// </summary>
        public void Init_model()
        {
            string label = Path.Combine(ResourcePath, "models/labels.txt");
            string vocab = Path.Combine(ResourcePath, "models/vocab.txt");

            if (File.Exists(label))
            {
                /// Load label file and tokenize
                string rawLabel = File.ReadAllText(label);
                tokenized_label = rawLabel.Split('\n');
            }
            else
            {
                Log.Error(TAG, "Label file is not exist");
            }

            if (File.Exists(vocab))
            {
                /// Load vocab file and tokenize
                string rawVocab = File.ReadAllText(vocab);
                tokenized_vocab = rawVocab.Split('\n');

                int len = tokenized_vocab.Length;
                /// Make vocab hash table to find word fast
                for (int i = 0; i < len - 1; i++)
                {
                    string[] words = tokenized_vocab[i].Replace("\r", "").Split(' ');
                    if (!ht.ContainsKey(words[0]))
                    {
                        ht.Add(words[0], words[1]);
                    }
                }
            }
            else
            {
                Log.Error(TAG, "Vocab file is not exist");
            }
        }

        /// <summary>
        /// Invoke text classification model and return result
        /// </summary>
        /// <param name="input"> float array which is converted from user input </param>
        /// <returns> Probability of the paragraph being positive or negative </returns>
        public float[] Invoke_model(float[] input)
        {
            byte[] in_buffer = new byte[4 * 256];
            byte[] out_buffer;
            float[] output = new float[2];
            TensorsInfo in_info;
            TensorsInfo out_info;
            TensorsData in_data;
            TensorsData out_data;
            string model_path = Path.Combine(ResourcePath, "models/text_classification.tflite");

            if (!File.Exists(model_path))
            {
                Log.Error(TAG, "Model file is not exist");
                return output;
            }

            Buffer.BlockCopy(input, 0, in_buffer, 0, in_buffer.Length);

            /// Set input & output TensorsInfo
            in_info = new TensorsInfo();
            in_info.AddTensorInfo(TensorType.Float32, new int[4] { 256, 1, 1, 1 });

            out_info = new TensorsInfo();
            out_info.AddTensorInfo(TensorType.Float32, new int[4] { 2, 1, 1, 1 });

            /// Create single inference engine
            SingleShot single = new SingleShot(model_path, in_info, out_info);

            /// Set input data
            in_data = in_info.GetTensorsData();
            in_data.SetTensorData(0, in_buffer);

            /// Single shot invoke
            out_data = single.Invoke(in_data);

            /// Get output data from TensorsData
            out_buffer = out_data.GetTensorData(0);
            Buffer.BlockCopy(out_buffer, 0, output, 0, out_buffer.Length);

            return output;
        }
    }
}
