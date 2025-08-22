/*
 *      Copyright (c) 2016 Samsung Electronics Co., Ltd
 *
 *      Licensed under the Flora License, Version 1.1 (the "License");
 *      you may not use this file except in compliance with the License.
 *      You may obtain a copy of the License at
 *
 *              http://floralicense.org/license/
 *
 *      Unless required by applicable law or agreed to in writing, software
 *      distributed under the License is distributed on an "AS IS" BASIS,
 *      WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *      See the License for the specific language governing permissions and
 *      limitations under the License.
 */


/*
 * We must declare global variables to let the validator know the
 * global variables we use.
 * To declare global variables, write global variables along with
 * default values inside a comment on top of each source code.
 * The comment should start with the keyword, global.
 */

/*global CITY_DATA: true, WEEKDAY: true, MONTH: true*/

CITY_DATA = [{
    cityName: 'Seoul',
    gmp: 9,
    background: 'image/widget_gmt_plus_9.png',
    sunrise: '5:13 AM',
    sunset: '7:57 PM'
}, {
    cityName: 'Cape Town',
    gmp: 2,
    background: 'image/widget_gmt_plus_2.png',
    sunrise: '7:52 AM',
    sunset: '5:47 PM'
}, {
    cityName: 'London',
    gmp: 0,
    background: 'image/widget_gmt_0.png',
    sunrise: '4:46 AM',
    sunset: '9:22 PM'
}, {
    cityName: 'New York',
    gmp: -5,
    background: 'image/widget_gmt_minus_5.png',
    sunrise: '5:27 AM',
    sunset: '8:31 PM'
}, {
    cityName: 'Vancouver',
    gmp: -8,
    background: 'image/widget_gmt_minus_8.png',
    sunrise: '5:09 AM',
    sunset: '9:22 PM'
}];
WEEKDAY = ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'];
MONTH = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
