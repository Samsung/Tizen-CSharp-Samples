/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd. All rights reserved.
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

using System.Collections.ObjectModel;

namespace Maps.Models
{
    /// <summary>
    /// Provides data for application view.
    /// </summary>
    class DefinedPoints
    {
        #region methods

        /// <summary>
        /// Returns static collection of defined points.
        /// </summary>
        /// <returns>Collection of Pins.</returns>
        public static ObservableCollection<Pin> GetCollection()
        {
            ObservableCollection<Pin> PinPoints = new ObservableCollection<Pin>();

            PinPoints.Add(new Pin("Shark Bay, Indian Ocean, Australia", new Position(-25.9670857, 113.0757838)));
            PinPoints.Add(new Pin("Ayers Rock (Uluru), Australia", new Position(-25.345657, 131.0283911)));
            PinPoints.Add(new Pin("The Boneyard, Tucson, Arizona, USA", new Position(32.1536835, -110.8318124)));
            PinPoints.Add(new Pin("A Saharan Sea Of Dunes, The Grand Erg of Bilma, Niger", new Position(17.1699123, 7.2034294)));
            PinPoints.Add(new Pin("Fresno, California, USA", new Position(36.8219538, -119.8330368)));
            PinPoints.Add(new Pin("Irrawaddy Delta, Myanmar", new Position(16.0181631, 94.7724951)));
            PinPoints.Add(new Pin("Crop circles, Cansas, USA", new Position(37.6405309, -100.6957721)));
            PinPoints.Add(new Pin("Canyonlands National Park, Utah, USA", new Position(38.31684779, -109.9087715)));

            return PinPoints;
        }

        #endregion
    }
}
