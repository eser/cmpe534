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

        public List<KeyValuePair<string, bool>> Falsify(Sequent sequent)
        {
            List<KeyValuePair<string, bool>> result = new List<KeyValuePair<string, bool>>();
            List<string> table = new List<string>();

            Substitutor substitutor = new Substitutor(this.registry);
            foreach (IMember member in sequent.Left)
            {
                substitutor.GetSymbols(member, ref table);
            }

            foreach (IMember member in sequent.Right)
            {
                substitutor.GetSymbols(member, ref table);
            }

            foreach (string item in table)
            {
                result.Add(new KeyValuePair<string, bool>(item, false));
            }

            return result;
        }

        public void Merge(ref List<KeyValuePair<string, bool>> original, List<KeyValuePair<string, bool>> valuations2)
        {
            bool found = false;
            foreach (KeyValuePair<string, bool> valuation2 in valuations2)
            {
                foreach (KeyValuePair<string, bool> valuation1 in original)
                {
                    if (valuation1.Key == valuation2.Key && valuation1.Value == valuation2.Value)
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    original.Add(valuation2);
                }
            }
        }
    }
}
