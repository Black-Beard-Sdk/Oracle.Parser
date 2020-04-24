using Bb.Oracle.Models;
using Bb.Oracle.Structures.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CompareModel.Exports.Excels
{

    public class SheetBase : IEnumerable<ExcelColumn>
    {

        public static bool WithFile = true;
        private List<ExcelColumn> columns;

        public SheetBase(string title, params object[] args)
        {
            this.Title = title;
            this.columns = new List<ExcelColumn>();

            foreach (var item in args)
            {
                this.columns.Add(new ExcelColumn(item as string));
            }
           
        }

        protected SheetBase(ItemBase source, ItemBase target, string title, params object[] args)
        {

            this.Title = title;
            this.columns = new List<ExcelColumn>();


            foreach (var item in args)
                this.columns.Add(new ExcelColumn(item as string));

            if (WithFile)
            {

                if (source != null && source.Files.Any())
                    this.columns.Add(new ExcelColumn(source.Files.ToString()));
                else
                    this.columns.Add(new ExcelColumn("no file"));

                string commiter = source != null && source.Tag != null
                    ? string.Empty //(source.Tag as ChangesetRef)?.Author?.DisplayName
                    : string.Empty;
                this.columns.Add(new ExcelColumn(commiter));


                if (target != null && target.Files.Any())
                    this.columns.Add(new ExcelColumn(target.Files.ToString()));
                else
                    this.columns.Add(new ExcelColumn("no file"));
                commiter = target != null && target.Tag != null
                    ? string.Empty //(target.Tag as ChangesetRef)?.Author?.DisplayName
                    : string.Empty;
                this.columns.Add(new ExcelColumn(commiter));
            }


        }

        public string Title { get; internal set; }

        private void AppendColumn(ExcelColumn c)
        {
            this.columns.Add(c);
        }

        public IEnumerator<ExcelColumn> GetEnumerator()
        {
            return columns.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return columns.GetEnumerator();
        }

    }

}