using System.IO;
using Deduction.GentzenPrime;
using Deduction.Proposition.Parsing;
using Deduction.Proposition.Processors;

namespace Deduction.Tests.ConsoleOut
{
    public class SequentTests
    {
        public static void Test(TextWriter output, Registry registry, string prop)
        {
            Sequent sequent = Sequent.Read(registry, prop);

            if (sequent == null)
            {
                output.WriteLine("not a valid sequent.");
                return;
            }

            Dumper dumper = new Dumper(registry);
            output.WriteLine("sequent                   = {0} -> {1}", dumper.Dump(sequent.Left), dumper.Dump(sequent.Right));
            output.WriteLine("isAxiom()                 = {0}", sequent.IsAxiom());
        }
    }
}
