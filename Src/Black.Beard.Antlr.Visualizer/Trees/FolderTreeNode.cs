using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bb.Antlr.Visualizer.Trees
{

    public class FolderTreeNode : TreeNode
    {

        public FolderTreeNode(DirectoryInfo dir)
        {
            this.Directory = dir;
            this.Text = dir.Name;
            this._calculated = false;
            this.Nodes.Add(new TreeNode("_test"));
        }

        public DirectoryInfo Directory { get; }

        private bool _calculated;

        internal void PrepareExpand()
        {
            if (!_calculated)
            {

                this.Nodes.Clear();
                _calculated = true;

                foreach (var item in this.Directory.GetDirectories())
                    this.Nodes.Add(new FolderTreeNode(item));

                foreach (var item in this.Directory.GetFiles())
                    this.Nodes.Add(new FileTreeNode(item));

            }
        }


    }

}
