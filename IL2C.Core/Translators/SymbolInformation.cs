﻿using System;
using Mono.Cecil;

namespace IL2C.Translators
{
    public struct SymbolInformation
    {
        public readonly string SymbolName;
        public readonly TypeReference TargetType;

        internal SymbolInformation(string symbolName, TypeReference targetType)
        {
            this.SymbolName = symbolName;
            this.TargetType = targetType;
        }

        public bool Equals(SymbolInformation rhs)
        {
            return this.SymbolName.Equals(rhs.SymbolName)
                   && this.TargetType.Equals(rhs.TargetType);
        }

        public override bool Equals(object rhs)
        {
            if (rhs is SymbolInformation)
            {
                return this.Equals((SymbolInformation)rhs);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return this.SymbolName.GetHashCode() ^ this.TargetType.GetHashCode();
        }
    }
}