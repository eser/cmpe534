using System.Collections.Generic;
using System.IO;
using Deduction.GentzenPrime.Abstraction;
using Deduction.GentzenPrime.Processors;
using Deduction.Proposition.Parsing;
using Deduction.Proposition.Processors;

namespace Deduction.Tests.ConsoleOut
{
    public class SequentTests
    {
        public static void Test(TextWriter output, Registry registry, string prop)
        {
            Sequent sequent = SequentReader.Read(registry, prop);

            if (sequent == null)
            {
                output.WriteLine("not a valid sequent.");
                return;
            }

            Searcher searcher = new Searcher(registry);
            List<Sequent> sequents = searcher.Expand(sequent);

            Dumper dumper = new Dumper(registry);
            output.WriteLine("sequent                   = {0} -> {1}", dumper.Dump(sequent.Left), dumper.Dump(sequent.Right));
            output.WriteLine("sequent.isAxiom()         = {0}", sequent.IsAxiom());
            output.WriteLine("sequent.isAtomic()        = {0}", sequent.IsAtomic());
            output.WriteLine();

            for (int i = 0; i < sequents.Count; i++)
            {
                output.WriteLine("sequent #{0}                = {1} -> {2}", i + 1, dumper.Dump(sequents[i].Left), dumper.Dump(sequents[i].Right));
                output.WriteLine("sequent #{0}.isAxiom()      = {1}", i + 1, sequents[i].IsAxiom());
                output.WriteLine("sequent #{0}.isAtomic()     = {1}", i + 1, sequents[i].IsAtomic());
                output.WriteLine();
            }
        }
    }
}
