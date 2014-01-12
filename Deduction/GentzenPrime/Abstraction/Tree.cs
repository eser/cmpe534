using System;
using System.Collections.Generic;

namespace Deduction.GentzenPrime.Abstraction
{
    public class Tree<T>
    {
        protected T content;
        protected Tree<T> parent;
        protected readonly LinkedList<Tree<T>> children;

        public Tree(T content, Tree<T> parent = null)
        {
            this.content = content;
            this.parent = parent;
            this.children = new LinkedList<Tree<T>>();
        }

        public T Content
        {
            get
            {
                return this.content;
            }
            set
            {
                this.content = value;
            }
        }

        public Tree<T> Parent
        {
            get
            {
                return this.parent;
            }
            set
            {
                this.parent = value;
            }
        }

        public LinkedList<Tree<T>> Children
        {
            get
            {
                return this.children;
            }
        }

        public Tree<T> this[int index]
        {
            get
            {
                foreach (Tree<T> child in this.Children)
                {
                    if (--index == 0)
                    {
                        return child;
                    }
                }

                return null;
            }
        }

        public void AddChild(T content)
        {
            this.children.AddLast(new Tree<T>(content, this));
        }

        public void Traverse(Action<T, int> action, int depth = 0)
        {
            action(this.Content, depth);
            foreach (Tree<T> child in this.Children)
            {
                child.Traverse(action, depth + 1);
            }
        }

        public void BreadthFirstNodes(Queue<Tree<T>> queue)
        {
            foreach (Tree<T> child in this.Children)
            {
                queue.Enqueue(child);
            }

            foreach (Tree<T> child in this.Children)
            {
                child.BreadthFirstNodes(queue);
            }
        }
    }
}
