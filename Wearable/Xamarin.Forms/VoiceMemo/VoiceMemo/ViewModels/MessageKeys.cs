/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace VoiceMemo.ViewModels
{
    public class MessageKeys
    {
        // Notify that player's state has changed
        public const string PlayerStateChanged = "PlayerStateChanged";

        // Notify that Audio play has done
        public const string AudioPlayDone = "AudioPlayDone";

        // Notify that Error occurs
        public const string ErrorOccur = "ErrorOccur";

        // Notify that a voice memo is saved
        public const string SaveVoiceMemo = "SaveVoiceMemo";

        // Notify that a voice memo is saved in Database
        public const string SaveVoiceMemoInDB = "SaveVoiceMemoInDB";

        // Notify that a voice memo is deleted
        public const string DeleteVoiceMemo = "DeleteVoiceMemo";

        // Notify that language has been changed
        public const string LanguageChanged = "LanguageChanged";

        // Notify that STT configuration has been changed
        public const string SttSupportedChanged = "SttSupportedChanged";

        // TODO: It's not used yet.
        // Notify that Stt text is ready
        public const string SttText = "SttText";

        // Notify that permission from an user is got or not
        public const string UserPermission = "UserPermission";

        // Notify that long click event occurs in RecordListPage
        //public const string RecordListLongPressed = "RecordListLongPressed";

        // TODO : It's a missing feature
        // Notify that volume level is too high
        //public const string WarnHearingDamange = "WarnHearingDamange";

        // TODO : Need to consider this case
        public const string CanStopSTT = "CanStopSTT";

        // Notify that the state of audio recording service get Recording.
        public const string ReadyToRecord = "ReadyToRecord";

        // TODO : Need to consider this case
        // forcibly go back to main page when stt state is not ready for recording.
        // Even though stt is not ready, recording can be started. We can change it.
        public const string ForcePopRecordingPage = "ForcePopRecordingPage";

        // Notify that the some texts are needed to update for localization when the language has been changed.
        public const string UpdateByLanguageChange = "UpdateByLanguageChange";
    }
}
