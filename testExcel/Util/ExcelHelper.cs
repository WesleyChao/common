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

            int rowCount = dt.Rows.Count;
            int colCount = dt.Columns.Count;
            for (int i = 0; i < rowCount; i++)
            {
                DataRow row = dt.Rows[i];
                for (int j = 0; j < colCount; j++)
                {

                }
            }




            throw new NotImplementedException();
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
    }
}