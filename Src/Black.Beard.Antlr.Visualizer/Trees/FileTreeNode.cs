using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bb.Antlr.Visualizer.Trees
{

    public class FileTreeNode : TreeNode
    {

        public FileTreeNode(FileInfo file)
        {
            this.File = file;
            this.Text = file.Name;
        }

        public FileInfo File { get; }

        internal void PrepareExpand()
        {

        }


    }

}
