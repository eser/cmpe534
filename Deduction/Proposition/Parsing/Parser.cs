// Proposition/Parsing/Parser.cs

using System;
using System.Collections.Generic;
using Deduction.Proposition.Abstraction;

namespace Deduction.Proposition.Parsing
{
    public class Parser
    {
        protected readonly Registry registry;

        protected List<Token> tokens;

        protected int currentPosition;
        protected Stack<Token> connectives;
        protected Stack<IMember> members;

        public Parser(Registry registry)
        {
            this.registry = registry;
        }

        public IMember Parse(List<Token> tokens)
        {
            Stack<RegistryMember> parentheses = new Stack<RegistryMember>();

            this.tokens = tokens;
            this.Reset();

            while (true)
            {
                // read a token
                Token curr = this.GetNext();
                if (curr == null)
                {
                    break;
                }

                // if the token is a connective
                if (curr.RegistryMember != null && typeof(Connective).IsAssignableFrom(curr.RegistryMember.Type))
                {
                    // if the token is an opening parenthesis also,
                    if (typeof(Parenthesis).IsAssignableFrom(curr.RegistryMember.Type))
                    {
                        // push it onto parantheses stack.
                        parentheses.Push(curr.RegistryMember);
                    }
                    // otherwise,
                    else
                    {
                        // loop while there is an connective token at the top of the stack,
                        while (this.connectives.Count > 0)
                        {
                            Token topConnective = this.connectives.Peek();

                            // if it's left-associative,
                            if (!topConnective.RegistryMember.IsRightAssociative)
                            {
                                // check precedence is less than or equal to current token's precedence.
                                if (curr.RegistryMember.Precedence < topConnective.RegistryMember.Precedence)
                                {
                                    break;
                                }
                            }
                            // if it's right-associative,
                            else
                            {
                                // check precedence is less than current token's precedence.
                                if (curr.RegistryMember.Precedence <= topConnective.RegistryMember.Precedence)
                                {
                                    break;
                                }
                            }

                            // pop the connective at the top of the stack,
                            // make a tree node with it.
                            this.MakeNodeTree();
                        }
                    }

                    // push the token onto the connectives stack.
                    this.connectives.Push(curr);
                }
                // if the token is closing parenthesis,
                else if (parentheses.Count > 0 && parentheses.Peek().ClosesWith == curr.Content)
                {
                    // pop the last paranthesis off the parantheses stack,
                    RegistryMember lastParenthesis = parentheses.Pop();

                    // loop until the token at the top of the stack is the opening parenthesis,
                    while (this.connectives.Count > 0 && this.connectives.Peek().Content != lastParenthesis.SymbolChar)
                    {
                        // pop connectives off the stack,
                        // to make tree nodes with it.
                        this.MakeNodeTree();
                    }

                    this.connectives.Pop();
                }
                else if (curr.RegistryMember != null && typeof(Constant).IsAssignableFrom(curr.RegistryMember.Type))
                {
                    Constant constant = new Constant(curr.Content, curr.RegistryMember.Value.Value);
                    this.members.Push(constant);
                }
                else
                {
                    Symbol symbol = new Symbol(curr.Content);
                    this.members.Push(symbol);
                }
            }

            while (this.connectives.Count > 0)
            {
                this.MakeNodeTree();
            }

            return this.members.Pop();
        }

        protected void MakeNodeTree()
        {
            Token poppedConnective = this.connectives.Pop();
            Connective connective = (Connective)Activator.CreateInstance(poppedConnective.RegistryMember.Type, new IMember[0]);

            IMember[] connectiveParameters = new IMember[connective.ParameterCount];
            for (int i = connective.ParameterCount - 1; i >= 0; i--)
            {
                IMember poppedMember = this.members.Pop();
                connectiveParameters[i] = poppedMember;
            }
            connective.Parameters.AddRange(connectiveParameters);

            this.members.Push(connective);
        }

        protected Token GetNext()
        {
            if (this.currentPosition >= this.tokens.Count)
            {
                return null;
            }

            return this.tokens[this.currentPosition++];
        }

        protected void Reset()
        {
            this.currentPosition = 0;

            this.connectives = new Stack<Token>();
            this.members = new Stack<IMember>();
        }
    }
}
