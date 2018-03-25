using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Antlr4.Runtime.Tree;
using Antlr4.Runtime;
using Bb.Oracle.Parser;

namespace Bb.Antlr.Visualizer.Trees
{

    public class AntlrNodeTree : TreeNode
    {

        public AntlrNodeTree(IParseTree item)
        {

            if (item != null)
            {

                this.ParseTree = item;

                if (item is ParserRuleContext r)
                    this.Text = PlSqlParser.ruleNames[r.RuleIndex];

                else if (item is ErrorNodeImpl e)
                    this.Text = "ERR - " + e.Symbol.Text;

                else if (item is TerminalNodeImpl t)
                {
                    //this.Text = PlSqlParser.DefaultVocabulary.GetLiteralName(t.Symbol.Type);

                    //if (string.IsNullOrEmpty(this.Text))
                    //{
                        var _text = PlSqlParser.DefaultVocabulary.GetSymbolicName(t.Symbol.Type);
                        this.Text = $"{_text} ({t.Symbol.Text})";
                    //}

                }

                int c = item.ChildCount;
                for (int i = 0; i < c; i++)
                {
                    IParseTree child = item.GetChild(i);
                    AntlrNodeTree node = new AntlrNodeTree(child);
                    this.Nodes.Add(node);
                }

            }
        }

        public IParseTree ParseTree { get; }

    }

}
