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
using System;
using System.Collections.Generic;

namespace FirstScreen
{
    /// <summary>
    /// TV Configuration
    /// </summary>
    public class TVConfiguration
    {
        /// <summary>
        /// Define the menu
        /// </summary>
        public class MenuDefinition
        {
            /// <summary>
            /// Description.
            /// </summary>
            public String description;
            /// <summary>
            /// Define the color of the text of the poster title.
            /// </summary>
            public bool whiteText;
            /// <summary>
            /// The size of the poster item
            /// </summary>
            public Size2D posterDimensions;
            /// <summary>
            /// Define the title of the each poster
            /// </summary>
            public List<String> posterTitles;
        }

        /// <summary>
        /// The collection of all the menu
        /// </summary>
        public class Configuration
        {
            public List<MenuDefinition> menuDefinitions;
        }

        private int _menuCount;

        /// <summary>
        /// Constructor.
        /// </summary>
        public TVConfiguration()
        {
            _menuCount = Constants.TotalPosterMenus;
        }

        /// <summary>
        /// Create the collection of the menu's configuration.
        /// </summary>
        /// <returns>the collection of the menu's configuration</returns>
        public Configuration GetConfiguration()
        {
            Configuration configuration = new Configuration();

            configuration.menuDefinitions = new List<MenuDefinition>();

            for (int i = 0; i < _menuCount; ++i)
            {
                MenuDefinition menuDefinition = new MenuDefinition();
                menuDefinition.posterTitles = new List<String>();
                menuDefinition.whiteText = true;
                menuDefinition.description = "Menu description";
                menuDefinition.posterDimensions = new Size2D(100, 100);
                configuration.menuDefinitions.Add(menuDefinition);
            }

            int menuIndex = 0;

            List<MenuDefinition> menuDefinitions = configuration.menuDefinitions;

            // Defining menus:      
            // Define and add "Live TV" to the menu
            menuDefinitions[menuIndex].description = "Live TV";
            menuDefinitions[menuIndex].whiteText = true;
            menuDefinitions[menuIndex].posterDimensions = new Size2D(162, 162);
            // Define the title of the posters
            menuDefinitions[menuIndex].posterTitles.Add("");
            menuDefinitions[menuIndex].posterTitles.Add("");
            menuDefinitions[menuIndex].posterTitles.Add("");
            menuDefinitions[menuIndex].posterTitles.Add("");
            menuDefinitions[menuIndex].posterTitles.Add("");
            menuDefinitions[menuIndex].posterTitles.Add("");
            menuDefinitions[menuIndex].posterTitles.Add("");
            menuDefinitions[menuIndex].posterTitles.Add("");
            menuDefinitions[menuIndex].posterTitles.Add("");
            menuDefinitions[menuIndex].posterTitles.Add("");
            menuDefinitions[menuIndex].posterTitles.Add("");
            menuDefinitions[menuIndex].posterTitles.Add("");
            menuDefinitions[menuIndex].posterTitles.Add("");
            ++menuIndex;

            //Define and add "Netflix" to the menu
            menuDefinitions[menuIndex].description = "Popular Shows";
            menuDefinitions[menuIndex].whiteText = true;
            menuDefinitions[menuIndex].posterDimensions = new Size2D(502, 278);
            // Define the title of the posters
            menuDefinitions[menuIndex].posterTitles.Add("My Beautiful Broken Brain");
            menuDefinitions[menuIndex].posterTitles.Add("Better Call Saul");
            menuDefinitions[menuIndex].posterTitles.Add("Pee-Wee's Big Holiday");
            menuDefinitions[menuIndex].posterTitles.Add("Good Morning Call");
            menuDefinitions[menuIndex].posterTitles.Add("Cuckoo");
            menuDefinitions[menuIndex].posterTitles.Add("Jimmy Carr");
            menuDefinitions[menuIndex].posterTitles.Add("Shadow Hunters");
            menuDefinitions[menuIndex].posterTitles.Add("Trailer Park Boys");
            ++menuIndex;

            // Define and add "Amazon Prime Video" to the menu
            menuDefinitions[menuIndex].description = "Amazon Originals and Exclusives";
            menuDefinitions[menuIndex].whiteText = true;
            menuDefinitions[menuIndex].posterDimensions = new Size2D(440, 189);
            // Define the title of the posters
            menuDefinitions[menuIndex].posterTitles.Add("");
            menuDefinitions[menuIndex].posterTitles.Add("The Sopranos");
            menuDefinitions[menuIndex].posterTitles.Add("The Wire");
            menuDefinitions[menuIndex].posterTitles.Add("Noah");
            menuDefinitions[menuIndex].posterTitles.Add("Bosch");
            menuDefinitions[menuIndex].posterTitles.Add("Sneaky Pete");
            menuDefinitions[menuIndex].posterTitles.Add("Dead Man Down");
            ++menuIndex;

            // Define and add "Internet" to the menu
            menuDefinitions[menuIndex].description = "Recommended For You";
            menuDefinitions[menuIndex].whiteText = true;
            menuDefinitions[menuIndex].posterDimensions = new Size2D(380, 232);
            // Define the title of the posters
            menuDefinitions[menuIndex].posterTitles.Add("Vector Boom");
            menuDefinitions[menuIndex].posterTitles.Add("Oxfam");
            menuDefinitions[menuIndex].posterTitles.Add("Flow");
            menuDefinitions[menuIndex].posterTitles.Add("Health");
            menuDefinitions[menuIndex].posterTitles.Add("Business");
            menuDefinitions[menuIndex].posterTitles.Add("EuroStar");
            ++menuIndex;

            //// Weather Network
            //menuDefinitions[menuIndex].description = "This Weeks Weather";
            //menuDefinitions[menuIndex].whiteText = true;
            //menuDefinitions[menuIndex].posterDimensions = new Size2D(195, 270);
            //menuDefinitions[menuIndex].posterTitles.Add("");
            //menuDefinitions[menuIndex].posterTitles.Add("");
            //menuDefinitions[menuIndex].posterTitles.Add("");
            //menuDefinitions[menuIndex].posterTitles.Add("");
            //menuDefinitions[menuIndex].posterTitles.Add("");
            //++menuIndex;

            // Define and add "Samsung SmartThings" to the menu
            menuDefinitions[menuIndex].description = "Connected Appliances";
            menuDefinitions[menuIndex].whiteText = false;
            menuDefinitions[menuIndex].posterDimensions = new Size2D(216, 213);
            // Define the title of the posters
            menuDefinitions[menuIndex].posterTitles.Add("");
            menuDefinitions[menuIndex].posterTitles.Add("Washing");
            menuDefinitions[menuIndex].posterTitles.Add("Mixer");
            menuDefinitions[menuIndex].posterTitles.Add("Fan");
            menuDefinitions[menuIndex].posterTitles.Add("Oven");
            menuDefinitions[menuIndex].posterTitles.Add("Power");
            menuDefinitions[menuIndex].posterTitles.Add("Shower");
            menuDefinitions[menuIndex].posterTitles.Add("Kettle");
            menuDefinitions[menuIndex].posterTitles.Add("Hob");
            menuDefinitions[menuIndex].posterTitles.Add("Iron");
            menuDefinitions[menuIndex].posterTitles.Add("Dryer");
            menuDefinitions[menuIndex].posterTitles.Add("Blender");
            menuDefinitions[menuIndex].posterTitles.Add("Toaster");
            ++menuIndex;

            // Define and add "YouTube" to the menu
            menuDefinitions[menuIndex].description = "Recommended Videos";
            menuDefinitions[menuIndex].whiteText = true;
            menuDefinitions[menuIndex].posterDimensions = new Size2D(534, 300);
            // Define the title of the posters
            menuDefinitions[menuIndex].posterTitles.Add("");
            menuDefinitions[menuIndex].posterTitles.Add("");
            menuDefinitions[menuIndex].posterTitles.Add("");
            menuDefinitions[menuIndex].posterTitles.Add("");
            menuDefinitions[menuIndex].posterTitles.Add("");
            menuDefinitions[menuIndex].posterTitles.Add("");
            ++menuIndex;

            // Define and add "Samsung Apps" to the menu
            menuDefinitions[menuIndex].description = "Popular Apps";
            menuDefinitions[menuIndex].whiteText = true;
            menuDefinitions[menuIndex].posterDimensions = new Size2D(171, 179);
            // Define the title of the posters
            menuDefinitions[menuIndex].posterTitles.Add("Plex");
            menuDefinitions[menuIndex].posterTitles.Add("Vimeo");
            menuDefinitions[menuIndex].posterTitles.Add("Dailymotion");
            menuDefinitions[menuIndex].posterTitles.Add("TechCrunch");
            menuDefinitions[menuIndex].posterTitles.Add("ToonsTV");
            menuDefinitions[menuIndex].posterTitles.Add("TED");
            menuDefinitions[menuIndex].posterTitles.Add("UFC.TV");
            menuDefinitions[menuIndex].posterTitles.Add("Deezer");
            menuDefinitions[menuIndex].posterTitles.Add("MUBI");
            menuDefinitions[menuIndex].posterTitles.Add("TG4");
            ++menuIndex;

            // Define and add "Samsung Games" to the menu
            menuDefinitions[menuIndex].description = "Popular Games";
            menuDefinitions[menuIndex].whiteText = true;
            menuDefinitions[menuIndex].posterDimensions = new Size2D(280, 280);
            // Define the title of the posters
            menuDefinitions[menuIndex].posterTitles.Add("Unchartered");
            menuDefinitions[menuIndex].posterTitles.Add("Tekken 4");
            menuDefinitions[menuIndex].posterTitles.Add("Halo");
            menuDefinitions[menuIndex].posterTitles.Add("Assassin's Creed");
            menuDefinitions[menuIndex].posterTitles.Add("Star Defender 4");
            menuDefinitions[menuIndex].posterTitles.Add("Soul Calibre");
            
            return configuration;
        }
    }

}
