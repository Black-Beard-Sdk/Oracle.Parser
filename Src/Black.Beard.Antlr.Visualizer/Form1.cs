﻿using System;
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
            this._context = context;


            if (this._context != null)
            {
                var dir = new DirectoryInfo(this._context.folder);
                if (!dir.Exists)
                    throw new System.IO.DirectoryNotFoundException(dir.FullName);

                FolderTreeNode node = new FolderTreeNode(dir);
                this.treeView1.Nodes.Add(node);

            }

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
                var script = Bb.Oracle.Parser.ScriptParser.ParsePath(f.File.FullName);
                ParserRuleContext tree = script.Tree;

                Parse(tree);

            }

        }

        private void Parse(ParserRuleContext tree)
        {
            treeView2.Nodes.Clear();
            AntlrNodeTree node = new AntlrNodeTree(tree);
            treeView2.Nodes.Add(node);
        }

    }
}
