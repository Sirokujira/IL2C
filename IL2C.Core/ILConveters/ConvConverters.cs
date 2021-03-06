﻿using System;

using Mono.Cecil.Cil;

using IL2C.Translators;

namespace IL2C.ILConveters
{
    internal sealed class Conv_i8Converter : InlineNoneConverter
    {
        public override OpCode OpCode => OpCodes.Conv_I8;

        public override Func<IExtractContext, string[]> Apply(DecodeContext decodeContext)
        {
            var siFrom = decodeContext.PopStack();
            var targetType = decodeContext.Module.GetSafeInt64Type().GetStackableType();
            var symbolName = decodeContext.PushStack(targetType);

            return extractContext => new[] { string.Format(
                "{0} = {1}",
                symbolName,
                extractContext.GetRightExpression(targetType, siFrom)) };
        }
    }

    internal sealed class Conv_u8Converter : InlineNoneConverter
    {
        public override OpCode OpCode => OpCodes.Conv_U8;

        public override Func<IExtractContext, string[]> Apply(DecodeContext decodeContext)
        {
            var siFrom = decodeContext.PopStack();
            var targetType = decodeContext.Module.GetSafeUInt64Type().GetStackableType();
            var symbolName = decodeContext.PushStack(targetType);

            return extractContext => new[] { string.Format(
                "{0} = {1}",
                symbolName,
                extractContext.GetRightExpression(targetType, siFrom)) };
        }
    }

    internal sealed class Conv_u1Converter : InlineNoneConverter
    {
        public override OpCode OpCode => OpCodes.Conv_U1;

        public override Func<IExtractContext, string[]> Apply(DecodeContext decodeContext)
        {
            var siFrom = decodeContext.PopStack();
            if (siFrom.TargetType.IsNumericPrimitive() == false)
            {
                throw new InvalidProgramSequenceException(
                    "Cannot convert to numeric type: ILByteOffset={0}, FromType={1}",
                    decodeContext.Current.Offset,
                    siFrom.TargetType.FullName);
            }

            var resultName = decodeContext.PushStack(decodeContext.Module.GetSafeInt32Type());
            return _ => new[] { string.Format("{0} = (uint8_t){1}", resultName, siFrom.SymbolName) };
        }
    }

    internal sealed class Conv_i1Converter : InlineNoneConverter
    {
        public override OpCode OpCode => OpCodes.Conv_I1;

        public override Func<IExtractContext, string[]> Apply(DecodeContext decodeContext)
        {
            var siFrom = decodeContext.PopStack();
            if (siFrom.TargetType.IsNumericPrimitive() == false)
            {
                throw new InvalidProgramSequenceException(
                    "Cannot convert to numeric type: ILByteOffset={0}, FromType={1}",
                    decodeContext.Current.Offset,
                    siFrom.TargetType.FullName);
            }

            var resultName = decodeContext.PushStack(decodeContext.Module.GetSafeInt32Type());
            return _ => new[] { string.Format("{0} = (int8_t){1}", resultName, siFrom.SymbolName) };
        }
    }

    internal sealed class Conv_i2Converter : InlineNoneConverter
    {
        public override OpCode OpCode => OpCodes.Conv_I2;

        public override Func<IExtractContext, string[]> Apply(DecodeContext decodeContext)
        {
            var siFrom = decodeContext.PopStack();
            if (siFrom.TargetType.IsNumericPrimitive() == false)
            {
                throw new InvalidProgramSequenceException(
                    "Cannot convert to numeric type: ILByteOffset={0}, FromType={1}",
                    decodeContext.Current.Offset,
                    siFrom.TargetType.FullName);
            }

            var resultName = decodeContext.PushStack(decodeContext.Module.GetSafeInt32Type());
            return _ => new[] { string.Format("{0} = (int16_t){1}", resultName, siFrom.SymbolName) };
        }
    }

    internal sealed class Conv_u2Converter : InlineNoneConverter
    {
        public override OpCode OpCode => OpCodes.Conv_U2;

        public override Func<IExtractContext, string[]> Apply(DecodeContext decodeContext)
        {
            var siFrom = decodeContext.PopStack();
            if (siFrom.TargetType.IsNumericPrimitive() == false)
            {
                throw new InvalidProgramSequenceException(
                    "Cannot convert to numeric type: ILByteOffset={0}, FromType={1}",
                    decodeContext.Current.Offset,
                    siFrom.TargetType.FullName);
            }

            var resultName = decodeContext.PushStack(decodeContext.Module.GetSafeInt32Type());
            return _ => new[] { string.Format("{0} = (uint16_t){1}", resultName, siFrom.SymbolName) };
        }
    }

    internal sealed class Conv_uConverter : InlineNoneConverter
    {
        public override OpCode OpCode => OpCodes.Conv_U;

        public override Func<IExtractContext, string[]> Apply(DecodeContext decodeContext)
        {
            var siFrom = decodeContext.PopStack();
            if (siFrom.TargetType.IsNumericPrimitive() == false)
            {
                throw new InvalidProgramSequenceException(
                    "Cannot convert to numeric type: ILByteOffset={0}, FromType={1}",
                    decodeContext.Current.Offset,
                    siFrom.TargetType.FullName);
            }

            var resultName = decodeContext.PushStack(decodeContext.Module.GetSafeIntPtrType());
            return _ => new[] { string.Format("{0} = (intptr_t)(uintptr_t){1}", resultName, siFrom.SymbolName) };
        }
    }
}
