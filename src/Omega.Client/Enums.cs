#region License

// Copyright 2011 Jason Walker
// ungood@onetrue.name
// 
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and 
// limitations under the License.

#endregion

namespace Omega.Client
{
    public enum Ascii : byte
    {
        NUL = 0x00,
        SOH = 0x01,
        STX = 0x02,
        ETX = 0x03,
        EOT = 0x04,

        ESC = 0x1B
    }

    public enum SignType : byte
    {
        /// <summary>
        /// All signs with Visual Verification.
        /// </summary>
        AllSignsVerify = 0x21,
        AllSigns = 0x5A,

        // TODO: Fill this in from the protocol documentation
    }

    #region TEXT Enums

    public enum DisplayPosition : byte
    {
        Middle = 0x20,
        Top = 0x22,
        Bottom = 0x26,
        Fill = 0x30,
        Left = 0x31,
        Right = 0x32,
    }

    public enum DisplayMode : byte
    {
        Rotate = 0x61,
        Hold = 0x62,
        Flash = 0x63,
        RollUp = 0x65,
        RollDown = 0x66,
        RollLeft = 0x67,
        RollRight = 0x68,
        WipeUp = 0x69,
        WipeDown = 0x6A,
        WipeLeft = 0x6B,
        WipeRight = 0x6C,
        Scroll = 0x6D,
        AutoMode = 0x6F,
        RollIn = 0x70,
        RollOut = 0x71,
        WipeIn = 0x72,
        WipeOut = 0x73,
        CompressedRotate = 0x74,
        Explode = 0x75,
        Clock = 0x76,
        Special = 0x6E,
    }

    public enum SpecialMode : byte
    {
        None = 0x00,
        Twinkle = 0x30,
        Sparkle = 0x31,
        Snow = 0x32,
        Interlock = 0x33,
        Switch = 0x34,
        Slide = 0x35,
        Spray = 0x36,
        Starburst = 0x37,
        Welcome = 0x38,
        SlotMachine = 0x39,
        NewsFlash = 0x3A,
        Trumpet = 0x3B,
        CycleColors = 0x43,
    }

    public enum SpecialGraphic : byte
    {
        None = 0x00,
        ThankYou = 0x53,
        NoSmoking = 0x55,
        DontDrinkAndDrive = 0x56,
        RunningAnimal = 0x57,
        Fireworks = 0x58,
        TurboCar = 0x59,
        CherryBomb = 0x60,
    }

    #endregion
}