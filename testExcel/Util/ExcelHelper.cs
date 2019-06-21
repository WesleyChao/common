using System;
using System.Data;
using System.IO;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
namespace Qx.Util.Office
{
    // excel 导入导出
    public class ExcelHelper
    {

        // 从DataTable 生成 Excel
        public static MemoryStream Export(DataTable dt)
        {
            XSSFWorkbook wb;
            wb = new XSSFWorkbook();

            ISheet sheet1;
            sheet1 = wb.CreateSheet("sheet1");

            int rowCount = dt.Rows.Count + 1;
            int colCount = dt.Columns.Count;

            // 创建表头
            IRow titles = sheet1.CreateRow(0);
            for (int i = 0; i < colCount; i++)
            {
                // 获取dt的表头
                string title = dt.Columns[i].ColumnName;
                ICell cell = titles.CreateCell(i);
                cell.SetCellValue(title);
                sheet1.AutoSizeColumn(i);
            }


            for (int i = 1; i < rowCount; i++)
            {
                DataRow row = dt.Rows[i - 1];
                IRow excelRow = sheet1.CreateRow(i);
                for (int j = 0; j < colCount; j++)
                {
                    string content = row[j].ToString();
                    ICell cell = excelRow.CreateCell(j);
                    cell.SetCellValue(content);
                }
            }
            NpoiMemoryStream result = new NpoiMemoryStream();
            result.AllowClose = false;
            wb.Write(result);
            result.Flush();
            result.Seek(0, SeekOrigin.Begin);
            result.AllowClose = true;
            return result;
        }


        // 从 Excel 生成 DataTable
        public static DataTable Import(MemoryStream dt)
        {
            XSSFWorkbook wb = new XSSFWorkbook(dt);
            ISheet sheet = wb.GetSheet("sheet1");
            int rowsCount = sheet.PhysicalNumberOfRows; //取行Excel的最大行数
            int colsCount = sheet.GetRow(0).PhysicalNumberOfCells;//取得Excel的列数

            DataTable result = new DataTable();
            for (int i = 0; i < colsCount; i++)
            {
                result.Columns.Add(new DataColumn(sheet.GetRow(0).GetCell(i).StringCellValue, Type.GetType("System.String")));
            }
            for (int rowIndex = 1; rowIndex < rowsCount; rowIndex++)
            {
                DataRow dr = result.NewRow();
                for (int colIndex = 0; colIndex < colsCount; colIndex++)
                {
                    string t = sheet.GetRow(rowIndex).GetCell(colIndex).ToString();
                    dr[colIndex] = t;
                }
                result.Rows.Add(dr);
            }
            return result;
        }

        public class NpoiMemoryStream : MemoryStream
        {
            public NpoiMemoryStream()
            {
                AllowClose = true;
            }

            public bool AllowClose { get; set; }

            public override void Close()
            {
                if (AllowClose)
                    base.Close();
            }
        }
    }
}