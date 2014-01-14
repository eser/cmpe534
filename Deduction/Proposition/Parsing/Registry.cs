// Proposition/Parsing/Registry.cs

using System;
using System.Collections.Generic;

namespace Deduction.Proposition.Parsing
{
    public class Registry
    {
        protected readonly List<RegistryMember> members;

        public Registry()
        {
            this.members = new List<RegistryMember>();
        }

        public List<RegistryMember> Members
        {
            get
            {
                return this.members;
            }
        }

        public void AddMembers(params RegistryMember[] members)
        {
            this.Members.AddRange(members);
        }

        public RegistryMember GetMemberByType(Type type)
        {
            foreach (RegistryMember registryMember in this.Members)
            {
                if (registryMember.Type == type)
                {
                    return registryMember;
                }
            }

            return null;
        }

        public RegistryMember GetMemberBySymbolChar(string symbolChar)
        {
            foreach (RegistryMember registryMember in this.Members)
            {
                if (registryMember.SymbolChar == symbolChar)
                {
                    return registryMember;
                }

                if (registryMember.Aliases != null)
                {
                    foreach (string alias in registryMember.Aliases)
                    {
                        if (alias == symbolChar)
                        {
                            return registryMember;
                        }
                    }
                }
            }

            return null;
        }
    }
}
