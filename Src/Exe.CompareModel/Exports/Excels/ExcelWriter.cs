using ExcelLibrary.SpreadSheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompareModel.Exports.Excels
{

    public class ExcelWriter
    {

        private Workbook workbook;
        private Dictionary<Type, ExcelWriterWorksheet> _sheets = new Dictionary<Type, ExcelWriterWorksheet>();
        public ExcelWriter()
        {

            //create new xls file          
            this.workbook = new Workbook();

        }


        public void Append(SheetBase line)
        {

            Type type = line.GetType();

            ExcelWriterWorksheet worksheet;
            if (!this._sheets.TryGetValue(type, out worksheet))
            {
                worksheet = new ExcelWriterWorksheet(line.Title);
                this._sheets.Add(type, worksheet);
            }

            worksheet.Append(line);
            //worksheet.Cells[4, 0] = new Cell(32764.5, "#,##0.00");
            //worksheet.Cells[5, 1] = new Cell(DateTime.Now, @"YYYY-MM-DD");
            //worksheet.Cells.ColumnWidth[0, 1] = 3000;

        }

        private class ExcelWriterWorksheet
        {

            private Worksheet worksheet;
            private int rowIndex = 0;

            public ExcelWriterWorksheet(string title)
            {
                worksheet = new Worksheet(title);
            }

            internal void Append(SheetBase row)
            {
                int indexCol = 0;
                foreach (var item in row)
                {
                    worksheet.Cells[rowIndex, indexCol] = item.Cell();
                    worksheet.Cells.ColumnWidth[(ushort)indexCol, (ushort)indexCol] = 3000;
                    indexCol++;
                }
                rowIndex++;
            }


            public Worksheet Worksheet { get { return this.worksheet; } }

        }

        public void Save(string file)
        {

            foreach (var item in this._sheets)
            {
                workbook.Worksheets.Add(item.Value.Worksheet);
            }

            workbook.Save(file);

        }

    }



    /*
            // open xls file 
            Workbook book = Workbook.Load(file);
            Worksheet sheet = book.Worksheets[0];

            // traverse cells 
            foreach (Pair, Cell > cell in sheet.Cells) 
                dgvCells[cell.Left.Right, cell.Left.Left].Value = cell.Right.Value;

            // traverse rows by Index 
            for (int rowIndex = sheet.Cells.FirstRowIndex;
             rowIndex <= sheet.Cells.LastRowIndex;
             rowIndex++)
            {
                Row row = sheet.Cells.GetRow(rowIndex);
                for (int colIndex = row.FirstColIndex; colIndex <= row.LastColIndex; colIndex++)
                {
                    Cell cell = row.GetCell(colIndex);
                }
            }
    */


}
