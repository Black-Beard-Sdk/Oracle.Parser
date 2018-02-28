using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bb.Oracle.Parser;
using static Bb.Oracle.Parser.PlSqlParser;
using System.IO;
using Bb.Antlr.Visualizer.Trees;
using Antlr4.Runtime;

namespace Black.Beard.Antlr.Visualizer
{
    public partial class Form1 : Form
    {

        private readonly ArgumentContext _context;

        public Form1(ArgumentContext context = null)
        {

            InitializeComponent();
            richTextBox1.HideSelection = false;
            this._context = context;

            if (this._context != null && !string.IsNullOrEmpty(this._context.folder))
                ShowFiles(this._context.folder);

        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            (e.Node as FolderTreeNode).PrepareExpand();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node is FileTreeNode f)
            {
                // A abstraire pour rendre le dev générique
                var script = ScriptParser.ParsePath(f.File.FullName);
                ParserRuleContext tree = script.Tree;
                richTextBox1.Text = script.Content.ToString();
                Parse(tree);

            }

        }

        private void Parse(ParserRuleContext tree)
        {
            treeView2.Nodes.Clear();
            AntlrNodeTree node = new AntlrNodeTree(tree);
            treeView2.Nodes.Add(node);
            node.Expand();
            treeView2.Focus();
        }

        private void treeView2_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node is AntlrNodeTree i)
            {

                if (i.ParseTree is ParserRuleContext p)
                {
                    int _start = p.Start.StartIndex;
                    int _end = p.Stop.StopIndex;

                    richTextBox1.Select(_start, _end - _start + 1);
                }
                else if (i.ParseTree is Antlr4.Runtime.Tree.TerminalNodeImpl t)
                {
                    int _start = t.Symbol.StartIndex;
                    int _end = t.Symbol.StopIndex;
                    richTextBox1.Select(_start, _end - _start + 1);
                }
                else
                {

                }

            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var script = Bb.Oracle.Parser.ScriptParser.ParseString(richTextBox1.Text);
            ParserRuleContext tree = script.Tree;
            Parse(tree);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

            //folderBrowserDialog1.RootFolder
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                ShowFiles(folderBrowserDialog1.SelectedPath);

        }

        private void ShowFiles(string path)
        {

            this.treeView1.Nodes.Clear();

            var dir = new DirectoryInfo(path);

            if (!dir.Exists)
                throw new System.IO.DirectoryNotFoundException(dir.FullName);

            FolderTreeNode node = new FolderTreeNode(dir);
            this.treeView1.Nodes.Add(node);

        }

    }
}
