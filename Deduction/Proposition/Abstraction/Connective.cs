// Proposition/Abstraction/Connective.cs

using System;
using System.Collections.Generic;
using Deduction.Proposition.Parsing;

namespace Deduction.Proposition.Abstraction
{
    public abstract class Connective : IMember
    {
        protected readonly List<IMember> parameters;

        public Connective(params IMember[] parameters)
        {
            this.parameters = new List<IMember>(parameters);
        }

        public Connective(IEnumerable<IMember> parameters)
        {
            this.parameters = new List<IMember>(parameters);
        }

        public List<IMember> Parameters
        {
            get
            {
                return this.parameters;
            }
        }

        public bool IsAtomic
        {
            get
            {
                return false;
            }
        }

        public abstract int ParameterCount
        {
            get;
        }

        public abstract bool Operation(bool[] values);

        public virtual IMember Simplify(Registry registry)
        {
            return null;
        }

        public override bool Equals(object obj)
        {
            Connective connective = obj as Connective;
            if (connective == null || !this.GetType().IsInstanceOfType(connective))
            {
                return false;
            }

            int length = this.Parameters.Count;
            if (length != connective.Parameters.Count)
            {
                return false;
            }

            for (int i = length - 1; i >= 0; i--)
            {
                if (!this.Parameters[i].Equals(connective.Parameters[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return this.GetType().GetHashCode() + this.Parameters.GetHashCode();
            }
        }

        public object Clone()
        {
            Connective clone = Activator.CreateInstance(this.GetType(), new IMember[0]) as Connective;

            for (int i = 0; i < clone.ParameterCount; i++)
            {
                clone.Parameters.Add(this.Parameters[i].Clone() as IMember);
            }

            return clone;
        }
    }
}
