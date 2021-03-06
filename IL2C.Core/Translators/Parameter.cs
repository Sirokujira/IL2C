﻿using Mono.Cecil;

namespace IL2C.Translators
{
    public struct Parameter
    {
        public readonly string Name;
        internal readonly TypeReference ParameterType;

        internal Parameter(string name, TypeReference parameterType)
        {
            this.Name = name;
            this.ParameterType = parameterType;
        }

        public string ParameterTypeName => this.ParameterType.GetFullMemberName();

        public override string ToString()
        {
            return string.Format("{0}: {1}", this.Name, this.ParameterTypeName);
        }
    }
}
