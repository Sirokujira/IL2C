﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IL2C.Tests
{
    public class ClassTypeTestTarget
    {
        public static int Value1 = 123;
        public static ClassTypeTestTarget OR1;

        public int Value2;
        public ClassTypeTestTarget OR2;

        public ClassTypeTestTarget()
        {
        }

        public ClassTypeTestTarget(int value)
        {
            this.Value2 = value;
        }

        public int GetValue2(int a, int b)
        {
            return this.Value2 + a + b;
        }

        public int GetValue2(int a, int b, int c)
        {
            return this.Value2 + a + b + c;
        }
    }

    public class ClassTypeTest
    {
        public static int Test4()
        {
            var hoge3 = new ClassTypeTestTarget();
            hoge3.Value2 = 456;

            return hoge3.Value2;
        }

        public static int Test5()
        {
            var hoge3 = new ClassTypeTestTarget();
            hoge3.Value2 = 789;

            var result = hoge3.GetValue2(123, 456);
            return result;
        }

        public static int Test6()
        {
            var hoge3 = new ClassTypeTestTarget();
            hoge3.Value2 = 789;

            var result = hoge3.GetValue2(123, 456, 789);
            return result;
        }

        public static int Test7()
        {
            var hoge3 = new ClassTypeTestTarget(123);

            return hoge3.Value2;
        }
    }
}
