// GentzenPrime/Processors/Falsifier.cs

using System.Collections.Generic;
using Deduction.GentzenPrime.Abstraction;
using Deduction.Proposition.Abstraction;
using Deduction.Proposition.Parsing;
using Deduction.Proposition.Processors;

namespace Deduction.GentzenPrime.Processors
{
    public class Falsifier
    {
        protected readonly Registry registry;

        public Falsifier(Registry registry)
        {
            this.registry = registry;
        }

        public List<Dictionary<string, string>> Falsify(Sequent sequent)
        {
            List<Dictionary<string, string>> result = new List<Dictionary<string, string>>();

            Substitutor substitutor = new Substitutor(this.registry);
            List<string> table = new List<string>();

            foreach (IMember member in sequent.Left)
            {
                substitutor.GetSymbols(member, ref table);
            }

            foreach (IMember member in sequent.Right)
            {
                substitutor.GetSymbols(member, ref table);
            }

            List<Dictionary<string, string>> allPossibleValuations = new List<Dictionary<string, string>>();
            for (int i = 0; i < (1 << table.Count); i++)
            {
                Dictionary<string, string> row = new Dictionary<string, string>();
                for (int j = 0; j < table.Count; j++)
                {
                    row.Add(table[j], (i & (1 << j)) > 0 ? "t" : "f");
                }

                allPossibleValuations.Add(row);
            }

            foreach (Dictionary<string, string> valuation in allPossibleValuations)
            {
                bool leftSide = true;
                foreach (IMember member in sequent.Left)
                {
                    IMember resultMember = substitutor.Substitute(member, valuation);
                    if (resultMember is Constant)
                    {
                        leftSide = leftSide && (resultMember as Constant).Value;
                    }
                }

                bool rightSide = false;
                foreach (IMember member in sequent.Right)
                {
                    IMember resultMember = substitutor.Substitute(member, valuation);
                    if (resultMember is Constant)
                    {
                        rightSide = rightSide || (resultMember as Constant).Value;
                    }
                }

                if (leftSide && !rightSide)
                {
                    result.Add(valuation);
                }
            }

            return result;
        }
    }
}
