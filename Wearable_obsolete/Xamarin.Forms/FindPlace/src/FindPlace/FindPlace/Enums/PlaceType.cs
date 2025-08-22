/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd. All rights reserved.
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
namespace FindPlace.Enums
{
    /// <summary>
    /// Enum containing available place types.
    /// </summary>
    public enum PlaceType
    {
        [PlaceTypeDescription("Cinema", "cinema.png", "cinema")]
        Cinema,
        [PlaceTypeDescription("Restaurant", "Restaurant.png", "restaurant")]
        Restaurant,
        [PlaceTypeDescription("Hotel", "hotel.png", "hotel")]
        Hotel,
        [PlaceTypeDescription("Pharmacy", "Pharmacy.png", "pharmacy")]
        Pharmacy,
        [PlaceTypeDescription("Shopping Center", "ShoppingCenter.png", "mall")]
        ShoppingCenter,
        [PlaceTypeDescription("Parking", "Parking.png", "parking-facility")]
        Parking,
        [PlaceTypeDescription("ATM", "ATM.png", "atm-bank-exchange")]
        ATM
    }
}
