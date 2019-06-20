using System;
using System.Data;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
namespace Qx.Util.Office
{
    // excel 导入导出
    public class ExcelHelper
    {

        // 从DataTable 生成 Excel
        public MemoryStream Export(DataTable dt)
        {
            HSSFWorkbook wb;
            wb = new HSSFWorkbook();

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
        public DataTable Import(MemoryStream dt)
        {
            throw new NotImplementedException();
        }
    }
}