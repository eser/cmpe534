using System;
using System.Collections.Generic;

namespace Deduction.Proposition.Parsing
{
    public class Registry
    {
        protected readonly List<RegistryMember> members;

        public List<RegistryMember> Members
        {
            get
            {
                return this.members;
            }
        }

        public Registry()
        {
            this.members = new List<RegistryMember>();
        }

        public void AddMembers(params RegistryMember[] members)
        {
            this.Members.AddRange(members);
        }

        public RegistryMember GetMemberByType(Type type)
        {
            foreach (RegistryMember domainMember in this.Members)
            {
                if (domainMember.Type == type)
                {
                    return domainMember;
                }
            }

            return null;
        }

        public RegistryMember GetMemberBySymbolChar(string symbolChar)
        {
            foreach (RegistryMember domainMember in this.Members)
            {
                if (domainMember.SymbolChar == symbolChar)
                {
                    return domainMember;
                }

                if (domainMember.Aliases != null)
                {
                    foreach (string alias in domainMember.Aliases)
                    {
                        if (alias == symbolChar)
                        {
                            return domainMember;
                        }
                    }
                }
            }

            return null;
        }

        public RegistryMember[] GetMembersSorted()
        {
            RegistryMember[] domainMembers = this.Members.ToArray();

            Array.Sort(
                domainMembers,
                (RegistryMember x, RegistryMember y) =>
                {
                    if (x.Precedence > y.Precedence)
                    {
                        return 1;
                    }

                    if (y.Precedence > x.Precedence)
                    {
                        return -1;
                    }

                    return 0;
                }
            );

            return domainMembers;
        }
    }
}
