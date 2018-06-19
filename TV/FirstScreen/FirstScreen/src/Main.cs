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

namespace FirstScreen
{
    class FirstScreenDemo
    {

        [STAThread]
        static int Main(string[] args)
        {
            //Tizen.Log.Debug("NUI", "Main() called");
            MenuScreen menuScreen = new MenuScreen();

            float version = 0.1f;
            Console.WriteLine("NUI (DALi) TV-demo benchmark. Version: " + version);

            // Process command line arguments.
            if (args.Length > 0)
            {
                if (args[0] == "-b")
                {
                    menuScreen.BenchmarkMode = true; // Enable benchmarking mode.
                }
                else
                {
                    String argumentList = "";
                    for (int i = 0; i < args.Length; ++i)
                    {
                        argumentList += args[i] + " ";
                    }

                    Console.WriteLine("Invalid argument list: " + argumentList + "\nValid arguments:\n -b\tBenchmarking mode\nExiting...");
                    //return 1;
                }
            }

            menuScreen.Run(args);
            return 0;
        }

    }
}
