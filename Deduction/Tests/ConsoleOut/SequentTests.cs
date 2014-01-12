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

            Tree<Sequent> root = new Tree<Sequent>(sequent);

            Searcher searcher = new Searcher(registry);
            searcher.Search(root);

            Dumper dumper = new Dumper(registry);
            //output.WriteLine("sequent                   = {0} -> {1}", dumper.Dump(sequent.Left), dumper.Dump(sequent.Right));
            //output.WriteLine("sequent.isAxiom()         = {0}", sequent.IsAxiom());
            //output.WriteLine("sequent.isAtomic()        = {0}", sequent.IsAtomic());
            //output.WriteLine();

            int i = 0;
            root.Traverse(
                delegate(Sequent seq, int depth)
                {
                    string indentation = new string('\t', depth);

                    output.WriteLine("{0}sequent #{1}                = {2} -> {3}", indentation, i + 1, dumper.Dump(seq.Left), dumper.Dump(seq.Right));
                    output.WriteLine("{0}sequent #{1}.isAxiom()      = {2}", indentation, i + 1, seq.IsAxiom());
                    output.WriteLine("{0}sequent #{1}.isAtomic()     = {2}", indentation, i + 1, seq.IsAtomic());
                    output.WriteLine();

                    i++;
                }
            );
        }
    }
}
