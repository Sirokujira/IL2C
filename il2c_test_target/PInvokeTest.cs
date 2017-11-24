﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace il2c_test_target
{
    public class Wio
    {
        [DllImport("WioLTE.h", EntryPoint = "Wio.Init")]
        public static extern void Init();

        [DllImport("WioLTE.h", EntryPoint = "Wio.LedSetRGB")]
        public static extern void LedSetRGB(int r, int g, int b);

        [DllImport("WioLTE.h")]
        public static extern void delay(int milliseconds);
    }

#if false
    public class WioTest
    {
        private static readonly int INTERVAL = 50;
        private static int Hue = 0;

        public static void loop()
        {
            int r;
            int g;
            int b;

            if (Hue < 60)
            {
                r = 255;
                g = Hue * 255 / 60;
                b = 0;
            }
            else if (Hue < 120)
            {
                r = (120 - Hue) * 255 / 60;
                g = 255;
                b = 0;
            }
            else if (Hue < 180)
            {
                r = 0;
                g = 255;
                b = (Hue - 120) * 255 / 60;
            }
            else if (Hue < 240)
            {
                r = 0;
                g = (240 - Hue) * 255 / 60;
                b = 255;
            }
            else if (Hue < 300)
            {
                r = (Hue - 240) * 255 / 60;
                g = 0;
                b = 255;
            }
            else
            {
                r = 255;
                g = 0;
                b = (360 - Hue) * 255 / 60;
            }

            Wio.LedSetRGB(r, g, b);

            Hue += 10;
            if (Hue >= 360) Hue = 0;

            Wio.delay(INTERVAL);
        }
    }
#endif
}