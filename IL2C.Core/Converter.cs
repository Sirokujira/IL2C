﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

using IL2C.ILConveters;

namespace IL2C
{
    public static class Converter
    {
        private static readonly Dictionary<ushort, ILConverter> ilConverters;

        static Converter()
        {
            ilConverters = typeof(ILConverter)
                .Assembly
                .GetTypes()
                .Where(type => type.IsSealed && typeof(ILConverter).IsAssignableFrom(type))
                .Select(type => (ILConverter) Activator.CreateInstance(type))
                .ToDictionary(ilc => (ushort) ilc.OpCode.Value);
        }

        internal sealed class ILData
        {
            public readonly ILConverter ILConverter;
            public readonly object Operand;

            public ILData(ILConverter ilc)
            {
                this.ILConverter = ilc;
                this.Operand = null;
            }

            public ILData(ILConverter ilc, object operand)
            {
                this.ILConverter = ilc;
                this.Operand = operand;
            }

            public string Apply(Stack<string> stack)
            {
                return this.ILConverter.Apply(this.Operand, stack);
            }
        }

        internal static IEnumerable<ILData> DecodeAndEnumerateOpCodes(byte[] ilBytes)
        {
            var index = 0;
            while (index < ilBytes.Length)
            {
                var byte0 = ilBytes[index++];
                if (ilConverters.TryGetValue(byte0, out var ilc) == false)
                {
                    var byte1 = ilBytes[index++];
                    var word = (ushort)(byte0 << 8 | byte1);
                    ilc = ilConverters[word];
                }

                switch (ilc.OpCode.OperandType)
                {
                    case OperandType.ShortInlineBrTarget:
                        object operand = ilBytes[index++];
                        yield return new ILData(ilc, operand);
                        break;
                    default:
                        yield return new ILData(ilc);
                        break;
                }
            }
        }

        private static string GetCLanguageTypeName(Type type)
        {
            return (type == typeof(System.Int32)) ? "int" : type.FullName;
        }

        public static void Convert(TextWriter tw, MethodInfo method)
        {
            Convert(tw, method.ReturnType, method.Name, method.GetMethodBody());
        }

        private static void Convert(
            TextWriter tw,
            Type returnType,
            string methodName,
            MethodBody body)
        {
            var locals = body.LocalVariables;

            var returnTypeName =
                GetCLanguageTypeName(returnType);

            tw.WriteLine("{0} {1}(void)", returnTypeName, methodName);
            tw.WriteLine("{");

            foreach (var local in locals)
            {
                tw.WriteLine(
                    "{0} local{1};",
                    GetCLanguageTypeName(local.LocalType),
                    local.LocalIndex);                
            }

            tw.WriteLine();

            var ilBytes = body.GetILAsByteArray();

            var stack = new Stack<string>();
            foreach (var ilData in DecodeAndEnumerateOpCodes(ilBytes))
            {
                var sourceCode = ilData.Apply(stack);
                if (sourceCode != null)
                {
                    tw.WriteLine(sourceCode);
                }
            }

            tw.WriteLine("}");
        }
    }
}