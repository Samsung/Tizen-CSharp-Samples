//Copyright 2018 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using System.Collections.Generic;
using Xamarin.Forms.Maps;

namespace MapsView
{
    public class Pins
    {
        /// <summary>
        /// This is a predefined list of locations available within the app.
        /// Add yours to get acustomed with how Xamarin.Forms.Maps.Pin work
        /// </summary>
        public static IReadOnlyCollection<Pin> Predefined = new List<Pin> {
            new Pin() {
                Label = "Shark Bay, Indian Ocean, Australia",
                Position = new Position(-25.9670857, 113.0757838)
            },
            new Pin() {
                Label = "Ayers Rock (Uluru), Australia",
                Position = new Position(-25.345657, 131.0283911)
            },
            new Pin() {
               Label = "The Boneyard, Tucson, Arizona, USA",
               Position = new Position(32.1536835, -110.8318124)
            },
            new Pin() {
               Label = "A Saharan Sea Of Dunes, The Grand Erg of Bilma, Niger",
               Position = new Position(17.1699123, 7.2034294)
            },
            new Pin() {
                Label = "Fresno, California, USA",
                Position = new Position(36.8219538, -119.8330368)
            },
            new Pin() {
                Label = "Irrawaddy Delta, Myanmar",
                Position = new Position(16.0181631, 94.7724951)
            },
            new Pin() {
                Label = "Crop circles, Cansas, USA",
                Position = new Position(37.6405309, -100.6957721)
            },
            new Pin() {
                Label = "Canyonlands National Park, Utah, USA",
                Position = new Position(38.31684779, -109.9087715)
            },
        };
    }
}
