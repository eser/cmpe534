// Commands.cs

using System;
using System.Collections.Generic;
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

                    case ".":
                        if (words.Length < 2)
                        {
                            this.textWriter.WriteLine("Required parameter is missing, type 'h' for help.");
                            this.textWriter.WriteLine();
                            break;
                        }

                        this.LoadFromFileProof(words[1]);
                        break;

                    case "?":
                        if (words.Length < 2)
                        {
                            this.textWriter.WriteLine("Required parameter is missing, type 'h' for help.");
                            this.textWriter.WriteLine();
                            break;
                        }

                        this.LoadFromInputProof(words[1]);
                        break;

                    case "f":
                        if (words.Length < 2)
                        {
                            this.textWriter.WriteLine("Required parameter is missing, type 'h' for help.");
                            this.textWriter.WriteLine();
                            break;
                        }

                        this.LoadFromInputFalsify(words[1]);
                        break;

                    case "c":
                        if (this.textWriter == Console.Out)
                        {
                            Console.Clear();
                        }
                        else
                        {
                            for (int i = 25; i >= 0; i--)
                            {
                                this.textWriter.WriteLine();
                            }
                        }
                        this.Help();
                        break;

                    default:
                        this.textWriter.WriteLine("Invalid input, type 'h' for help.");
                        this.textWriter.WriteLine("Hint: to prove a propositional formula, use '? {0}' command.", line);
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
            this.textWriter.WriteLine("? [sequent]     to load a sequent from input and show proof tree.");
            this.textWriter.WriteLine("f [sequent]     to load a sequent from input and list all falsifying valuations.");
            this.textWriter.WriteLine(". [path]        to load a sequent from file.");
            this.textWriter.WriteLine("c               to clear console.");
            this.textWriter.WriteLine("h               to help.");
            this.textWriter.WriteLine("q               to quit.");
            this.textWriter.WriteLine();
        }

        public void LoadFromFileProof(string path)
        {
            if (!File.Exists(path))
            {
                this.textWriter.WriteLine("File not found - {0}", path);
                this.textWriter.WriteLine();
                return;
            }

            this.textWriter.WriteLine("Reading file: {0}", path);
            this.textWriter.WriteLine();

            string[] lines = File.ReadAllLines(path);
            foreach (string line in lines)
            {
                string trimmedLine = line.Trim();
                if (trimmedLine.Length == 0 || trimmedLine.StartsWith("//") || trimmedLine.StartsWith("#"))
                {
                    continue;
                }

                this.textWriter.WriteLine("-------------------------------------------------");
                this.LoadFromInputProof(line);
            }
        }

        public void LoadFromInputProof(string sequentLine)
        {
            this.textWriter.WriteLine("Deduction tree of: {0}", sequentLine);
            this.textWriter.WriteLine();

            Sequent sequent = SequentReader.Read(this.registry, sequentLine);
            if (sequent == null)
            {
                this.textWriter.WriteLine("Not a valid sequent. Ex: A & B -> B | C, D");
                this.textWriter.WriteLine();
                return;
            }

            Tree<Sequent> root = new Tree<Sequent>(sequent);

            Prover prover = new Prover(registry);
            prover.Search(root);

            Falsifier falsifier = new Falsifier(this.registry);
            List<Dictionary<string, string>> falsifyingValuations = new List<Dictionary<string, string>>();

            Dumper dumper = new Dumper(registry);

            int depth = 0;
            List<string> counterExamples = new List<string>();
            Action<Tree<Sequent>> dumpAction = null;
            dumpAction = delegate(Tree<Sequent> seq)
            {
                string indentation = new string('\t', depth);

                string output = (dumper.Dump(seq.Content.Left) + " -> " + dumper.Dump(seq.Content.Right)).Trim();
                this.textWriter.WriteLine("{0}sequent = {1}", indentation, output);
                //this.textWriter.WriteLine("{0}sequent.isAxiom()      = {1}", indentation, seq.Content.IsAxiom());
                //this.textWriter.WriteLine("{0}sequent.isAtomic()     = {1}", indentation, seq.Content.IsAtomic());
                if (seq.Content.IsAxiom())
                {
                    this.textWriter.WriteLine("{0}          ** axiom node **", indentation);
                    this.textWriter.WriteLine();
                }
                else if (seq.Content.IsAtomic())
                {
                    counterExamples.Add(output);

                    this.textWriter.WriteLine("{0}          ** counter-example node **", indentation);
                    this.textWriter.WriteLine();

                    List<Dictionary<string, string>> valuations = falsifier.Falsify(seq.Content);
                    foreach (Dictionary<string, string> valuation in valuations)
                    {
                        falsifyingValuations.Add(valuation);
                    }
                }

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

            if (counterExamples.Count > 0)
            {
                this.textWriter.WriteLine("Formula is not valid, details below.");

                this.textWriter.WriteLine("+ Counter-examples:");
                foreach (string counterExample in counterExamples)
                {
                    this.textWriter.Write("  .. ");
                    this.textWriter.WriteLine(counterExample);
                }
                this.textWriter.WriteLine();

                this.textWriter.WriteLine("+ Falsifying valuations:");
                for (int i = 0; i < falsifyingValuations.Count; i++)
                {
                    this.textWriter.WriteLine("  .. Valuation #{0}:", i + 1);
                    foreach (KeyValuePair<string, string> valuation in falsifyingValuations[i])
                    {
                        this.textWriter.WriteLine("  ..                 {0} -> {1}", valuation.Key, valuation.Value);
                    }
                }
                this.textWriter.WriteLine();
            }
            else
            {
                this.textWriter.WriteLine("Formula is valid.");
            }

            this.textWriter.WriteLine();
        }

        public void LoadFromInputFalsify(string sequentLine)
        {
            Sequent sequent = SequentReader.Read(this.registry, sequentLine);
            if (sequent == null)
            {
                this.textWriter.WriteLine("Not a valid sequent. Ex: A & B -> B | C, D");
                this.textWriter.WriteLine();
                return;
            }

            Falsifier falsifier = new Falsifier(this.registry);
            List<Dictionary<string, string>> valuations = falsifier.Falsify(sequent);

            if (valuations.Count > 0)
            {
                this.textWriter.WriteLine("Falsifying valuations of: {0}", sequentLine);

                for (int i = 0; i < valuations.Count; i++)
                {
                    this.textWriter.WriteLine(".. Valuation #{0}:", i + 1);
                    foreach (KeyValuePair<string, string> valuation in valuations[i])
                    {
                        this.textWriter.WriteLine("..                 {0} -> {1}", valuation.Key, valuation.Value);
                    }
                }
            }
            else
            {
                this.textWriter.WriteLine("Falsifying needs atomic input. To prove a sequent use '? {0}' command instead.", sequentLine);
            }

            this.textWriter.WriteLine();
        }
    }
}
