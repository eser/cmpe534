using System;
using System.Collections.Generic;
using Deduction.Abstraction.DomainMembers;

namespace Deduction.Abstraction
{
    public class Domain
    {
        protected static Domain instance = null;
        protected readonly List<DomainMember> members;

        public List<DomainMember> Members
        {
            get
            {
                return this.members;
            }
        }

        public static Domain Instance
        {
            get
            {
                if (Domain.instance == null)
                {
                    Domain.instance = new Domain();
                }

                return Domain.instance;
            }
        }

        protected Domain()
        {
            this.members = new List<DomainMember>()
            {
                new DomainMember("&", typeof(And), 2, null),
                new DomainMember("|", typeof(Or), 3, null),
                new DomainMember("!", typeof(Not), 1, null),
                new DomainMember(">", typeof(Implication), 4, null),
                new DomainMember("=", typeof(Equivalence), 4, null),

                new DomainMember("f", typeof(PropositionSymbol), 0, false),
                new DomainMember("t", typeof(PropositionSymbol), 0, true)
            };
        }

        public static DomainMember GetMemberByType(Type type)
        {
            foreach (DomainMember domainMember in Domain.Instance.Members)
            {
                if (domainMember.Type == type)
                {
                    return domainMember;
                }
            }

            return null;
        }

        public static DomainMember GetMemberBySymbolChar(string symbolChar)
        {
            foreach (DomainMember domainMember in Domain.Instance.Members)
            {
                if (domainMember.SymbolChar == symbolChar)
                {
                    return domainMember;
                }
            }

            return null;
        }

        public static DomainMember[] GetMembersSorted()
        {
            DomainMember[] domainMembers = Domain.Instance.Members.ToArray();

            Array.Sort(
                domainMembers,
                (DomainMember x, DomainMember y) =>
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
