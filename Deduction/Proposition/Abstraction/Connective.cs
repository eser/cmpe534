using System.Collections.Generic;

namespace Deduction.Proposition.Abstraction
{
    public abstract class Connective : IMember
    {
        protected readonly List<IMember> parameters;

        public List<IMember> Parameters
        {
            get
            {
                return this.parameters;
            }
        }

        public Connective(params IMember[] parameters)
        {
            this.parameters = new List<IMember>(parameters);
        }

        public Connective(IEnumerable<IMember> parameters)
        {
            this.parameters = new List<IMember>(parameters);
        }

        public abstract int ParameterCount
        {
            get;
        }

        public abstract bool RightAssociative
        {
            get;
        }

        public abstract bool Operation(bool[] values);

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
    }
}
