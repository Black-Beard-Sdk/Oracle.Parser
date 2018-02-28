using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Antlr4.Runtime.Tree;

namespace Bb.Antlr.Visualizer.Trees
{

    public class AntlrNodeTree : TreeNode
    {

        public AntlrNodeTree(IParseTree item)
        {

            this.ParseTree = item;

            this.Text = item.GetType().Name;
            int c = item.ChildCount;
            for (int i = 0; i < c; i++)
            {
                IParseTree child = item.GetChild(i);
                AntlrNodeTree node = new AntlrNodeTree(child);
                this.Nodes.Add(node);
            }
        }

        public IParseTree ParseTree { get; }

    }

}
