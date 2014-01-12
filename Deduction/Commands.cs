using System;
using System.IO;
using Deduction.GentzenPrime.Abstraction;
using Deduction.GentzenPrime.Processors;
using Deduction.Proposition.Parsing;
using Deduction.Proposition.Processors;

namespace Deduction
{
    public class Commands
    {
        protected Registry registry;
        protected TextWriter textWriter;

        public Commands(Registry registry, TextWriter textWriter)
        {
            this.registry = registry;
            this.textWriter = textWriter;
        }

        public bool Interprete(TextReader textReader)
        {
            this.textWriter.Write(": ");

            string line = textReader.ReadLine();
            string[] words = line.Split(new char[] { ' ', '\t' }, 2, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length > 0)
            {
                switch (words[0])
                {
                    case "q":
                        return false;

                    case "h":
                        this.Help();
                        break;

                    case "l":
                        this.LoadFromFile(words[1]);
                        break;

                    case "s":
                        this.LoadFromInput(words[1]);
                        break;

                    case "c":
                        if (this.textWriter == Console.Out)
                        {
                            Console.Clear();
                        }
                        break;

                    default:
                        this.textWriter.WriteLine("Invalid input, see help.");
                        this.textWriter.WriteLine();
                        break;
                }
            }

            return true;
        }

        public void Help()
        {
            this.textWriter.WriteLine("Help");
            this.textWriter.WriteLine("==================");
            this.textWriter.WriteLine();
            this.textWriter.WriteLine("l [path]        to load a sequent from file.");
            this.textWriter.WriteLine("s [sequent]     to load a sequent from input.");
            this.textWriter.WriteLine("c               to clear console.");
            this.textWriter.WriteLine("h               to help.");
            this.textWriter.WriteLine("q               to quit.");
            this.textWriter.WriteLine();
        }

        public void LoadFromFile(string path)
        {
            if (!File.Exists(path))
            {
                this.textWriter.WriteLine("File not found - " + path);
                this.textWriter.WriteLine();
                return;
            }

            string[] lines = File.ReadAllLines(path);
            foreach (string line in lines)
            {
                this.LoadFromInput(line);
            }
        }

        public void LoadFromInput(string sequentLine)
        {
            this.textWriter.WriteLine("Sequent: " + sequentLine);
            this.textWriter.WriteLine();

            Sequent sequent = SequentReader.Read(this.registry, sequentLine);
            if (sequent == null)
            {
                this.textWriter.WriteLine("Not a valid sequent. Ex: A & B -> B | C, D");
                this.textWriter.WriteLine();
                return;
            }

            Tree<Sequent> root = new Tree<Sequent>(sequent);

            Searcher searcher = new Searcher(registry);
            searcher.Search(root);

            Dumper dumper = new Dumper(registry);

            int depth = 0;
            Action<Tree<Sequent>> dumpAction = null;
            dumpAction = delegate(Tree<Sequent> seq)
            {
                string indentation = new string('\t', depth);

                this.textWriter.WriteLine("{0}sequent                = {1} -> {2}", indentation, dumper.Dump(seq.Content.Left), dumper.Dump(seq.Content.Right));
                this.textWriter.WriteLine("{0}sequent.isAxiom()      = {1}", indentation, seq.Content.IsAxiom());
                this.textWriter.WriteLine("{0}sequent.isAtomic()     = {1}", indentation, seq.Content.IsAtomic());
                this.textWriter.WriteLine();

                if (seq.Children.Count >= 2)
                {
                    depth++;
                }

                foreach (Tree<Sequent> child in seq.Children)
                {
                    dumpAction(child);
                }

                if (seq.Children.Count >= 2)
                {
                    depth--;
                }
            };

            dumpAction(root);

            this.textWriter.WriteLine("done.");
            this.textWriter.WriteLine();
        }
    }
}
