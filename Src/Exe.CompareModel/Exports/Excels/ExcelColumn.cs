using System;
using ExcelLibrary.SpreadSheet;

namespace CompareModel.Exports.Excels
{

    public class ExcelColumn
    {

        private readonly Cell c;

        public ExcelColumn(string value)
        {
            this.c = new Cell(value);
        }

        public ExcelColumn(short value)
        {
            this.c = new Cell(value);
        }

        internal Cell Cell()
        {
            return c;
        }

        public override string ToString()
        {
            return c.Value.ToString();
        }

    }

}