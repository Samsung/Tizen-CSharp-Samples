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

/* global CITY_DATA: true, WEEKDAY: true, MONTH: true*/

(function() {
    // Dummy data for test
    var CITIES = ['Seoul', 'Cape Town', 'London', 'New York', 'Vancouver'];
    var currentIndex = 0;

    /**
     * Sets the data in clock by name of city
     * @private
     * @param {String} cityName - The name of city
     */
    function setClock(cityName) {
        var d = new Date(),
            timeDiff, len, gmp, bg, sunrise, sunset, tz, i;

        len = CITY_DATA.length;
        for (i = 0; i < len; i++) {
            if (CITY_DATA[i].cityName === cityName) {
                gmp = CITY_DATA[i].gmp;
                bg = CITY_DATA[i].background;
                sunrise = CITY_DATA[i].sunrise;
                sunset = CITY_DATA[i].sunset;
                break;
            }
        }

        document.getElementById('city-name').textContent = cityName;
        document.getElementById('page-clock').style.backgroundImage = 'url(' + bg + ')';
        document.getElementById('sunrise-time').textContent = sunrise;
        document.getElementById('sunset-time').textContent = sunset;

        if (gmp < 0) {
            timeDiff = (gmp * -1) + ' hrs behind';
        } else if (gmp > 0) {
            timeDiff = gmp + ' hrs ahead';
        } else {
            timeDiff = 'Same as local time';
        }
        document.getElementById('city-diff').textContent = timeDiff;

        tz = d.getTime() + (d.getTimezoneOffset() * 60000) + (gmp * 3600000);
        d.setTime(tz);
        document.getElementById('clock-time').textContent = convertTimeFormat(d);
        document.getElementById('clock-ampm').textContent = d.getHours() > 12 ? 'PM' : 'AM';

        document.getElementById('clock-date').textContent = convertDateFormat(d);
    }

    /**
     * Converts time format to string
     * @private
     * @param {Date} d - The date object
     * @returns {String} The converted time
     */
    function convertTimeFormat(d) {
        var hours = d.getHours() % 12,
            minutes = d.getMinutes();

        if (minutes < 10) {
            minutes = '0' + minutes;
        }
        return hours + ':' + minutes;
    }

    /**
     * Converts date format to string
     * @private
     * @param {Date} d - The date object
     * @returns {String} The converted date
     */
    function convertDateFormat(d) {
        var month = d.getMonth(),
            day = d.getDay();

        return WEEKDAY[day] + ', ' + MONTH[month] + ' ' + day;
    }

    /**
     * Gets the cities from the world clock application
     * Now it returns dummy data for test
     * @private
     * @returns {Array} - The stored cities
     */
    function getCities() {
        return CITIES;
    }

    /**
     * Changes widget to next city
     * @private
     */
    function toggle(event) {
        var cities = getCities();

        currentIndex = (currentIndex + 1) % cities.length;
        setClock(cities[currentIndex]);
	event.stopPropagation();
    }

    /**
     * Launches the world clock application
     * @private
     */
    function launchApp(){
        var app = window.tizen.application.getCurrentApplication();
        var appId = app.appInfo.id.substring(0, (app.appInfo.id.lastIndexOf('.')) );
        window.tizen.application.launch(appId);
    }

    /**
     * Handles the back key event
     * @private
     */
    function keyEventHandler(event) {
        if (event.keyName === "back") {
            try {
                tizen.application.getCurrentApplication().exit();
            } catch (ignore) {}
        }
    }

    /**
     * Initializes the application
     * @private
     */
    function init() {
        var cities = getCities(),
            pageNoList = document.getElementById('page-no-list'),
            pageClock = document.getElementById('page-clock'),
            len;

        len = cities.length;
        if (len > 0) {
            pageNoList.style.display = 'none';
            pageClock.style.display = 'block';
            setClock(cities[currentIndex]);
        } else {
            pageClock.style.display = 'none';
            pageNoList.style.display = 'block';
        }
        pageNoList.addEventListener('click', launchApp);
        pageClock.addEventListener('click', launchApp);
        document.getElementById('city-toggle').addEventListener('click', toggle);
        window.addEventListener('tizenhwkey', keyEventHandler);
    }

    window.onload = init();
}());
