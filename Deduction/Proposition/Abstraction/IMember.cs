// Proposition/Abstraction/IMember.cs

using System;

namespace Deduction.Proposition.Abstraction
{
    public interface IMember : ICloneable
    {
        bool IsAtomic { get; }
    }
}
