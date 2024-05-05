using System;
using System.Collections.Generic;
using System.IO;
using Abp.AspNetZeroCore.Net;
using Abp.Collections.Extensions;
using Abp.Dependency;
using Abp.Extensions;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using Suktas.Payroll.Dto;
using Suktas.Payroll.Storage;
using ICell = NPOI.SS.UserModel.ICell;

namespace Suktas.Payroll.DataExporting.Excel.NPOI;

public abstract class NpoiExcelExporterBase : PayrollAppServiceBase, ITransientDependency
{
    private readonly Dictionary<string, ICellStyle> _dateCellStyles = new();
    private readonly Dictionary<string, IDataFormat> _dateDateDataFormats = new();
    private readonly ITempFileCacheManager _tempFileCacheManager;

    private IWorkbook _workbook;

    protected NpoiExcelExporterBase(ITempFileCacheManager tempFileCacheManager)
    {
        _tempFileCacheManager = tempFileCacheManager;
    }

    private ICellStyle GetDateCellStyle(ICell cell, string dateFormat)
    {
        if (_workbook != cell.Sheet.Workbook)
        {
            _dateCellStyles.Clear();
            _dateDateDataFormats.Clear();
            _workbook = cell.Sheet.Workbook;
        }

        if (_dateCellStyles.ContainsKey(dateFormat)) return _dateCellStyles.GetValueOrDefault(dateFormat);

        var cellStyle = cell.Sheet.Workbook.CreateCellStyle();
        _dateCellStyles.Add(dateFormat, cellStyle);
        return cellStyle;
    }

    private IDataFormat GetDateDataFormat(ICell cell, string dateFormat)
    {
        if (_workbook != cell.Sheet.Workbook)
        {
            _dateDateDataFormats.Clear();
            _workbook = cell.Sheet.Workbook;
        }

        if (_dateDateDataFormats.ContainsKey(dateFormat)) return _dateDateDataFormats.GetValueOrDefault(dateFormat);

        var dataFormat = cell.Sheet.Workbook.CreateDataFormat();
        _dateDateDataFormats.Add(dateFormat, dataFormat);
        return dataFormat;
    }

    protected FileDto CreateExcelPackage(string fileName, Action<XSSFWorkbook> creator)
    {
        var file = new FileDto(fileName, MimeTypeNames.ApplicationVndOpenxmlformatsOfficedocumentSpreadsheetmlSheet);
        var workbook = new XSSFWorkbook();

        creator(workbook);

        Save(workbook, file);

        return file;
    }

    protected void AddMasterHeader(ISheet sheet, int row, string data)
    {
        if (string.IsNullOrWhiteSpace(data)) return;
        sheet.CreateRow(row);

        var cell = sheet.GetRow(row).CreateCell(0);
        cell.SetCellValue(data);
        var cellStyle = sheet.Workbook.CreateCellStyle();
        var font = sheet.Workbook.CreateFont();
        font.IsBold = true;
        font.FontHeightInPoints = 12;
        cellStyle.SetFont(font);
        cell.CellStyle = cellStyle;
    }

    protected void AddHeaderWithMainHeader(ISheet sheet, int rowNumber, params string[] headerTexts)
    {
        if (headerTexts.IsNullOrEmpty())
        {
            return;
        }

        sheet.CreateRow(rowNumber);

        for (var i = 0; i < headerTexts.Length; i++)
        {
            AddHeaderWithMainHeader(sheet, rowNumber, i, headerTexts[i]);
        }
    }

    protected void ColumnResize(ISheet sheet, int number, int start = 1)
    {
        for (var i = start; i <= number; i++)
            sheet.AutoSizeColumn(i);
    }

    

    protected void AddHeaderWithMainHeader(ISheet sheet, int rowNumber, int columnIndex, string headerText)
    {
        var cell = sheet.GetRow(rowNumber).CreateCell(columnIndex);
        cell.SetCellValue(headerText);
        var cellStyle = sheet.Workbook.CreateCellStyle();
        var font = sheet.Workbook.CreateFont();
        font.IsBold = true;
        font.FontHeightInPoints = 12;
        cellStyle.SetFont(font);
        cell.CellStyle = cellStyle;
    }

    protected void AddMasterHeader(ISheet sheet, int rowStart, int rowEnd, int columnStart, int columnEnd,
        string headerTexts, int fontSize = 12)
    {
        if (headerTexts.IsNullOrWhiteSpace())
        {
            return;
        }

        if (sheet.LastRowNum != rowStart || sheet.LastRowNum == 0)
            sheet.CreateRow(rowStart);
        var cell = sheet.GetRow(rowStart).CreateCell(columnStart);
        var cellRange = new CellRangeAddress(rowStart, rowEnd, columnStart, columnEnd);
        sheet.AddMergedRegion(cellRange);
        //     var style = sheet.Workbook.CreateCellStyle();
        var cellStyle = sheet.Workbook.CreateCellStyle();
        cellStyle.Alignment = HorizontalAlignment.Center;
        var font = sheet.Workbook.CreateFont();
        font.IsBold = true;
        //  font.Color = 14;          
        font.FontHeightInPoints = fontSize;
        cellStyle.SetFont(font);
        cell.CellStyle = cellStyle;
        cell.SetCellValue(headerTexts);
    }

    protected void AddMasterObject(ISheet sheet, int row, string data)
    {
        if (string.IsNullOrWhiteSpace(data)) return;
        var cell = sheet.GetRow(row).CreateCell(1);
        cell.SetCellValue(data);
    }

    protected void AddMasterDetailHeader(ISheet sheet, int row, params string[] headerTexts)
    {
        if (headerTexts.IsNullOrEmpty()) return;

        sheet.CreateRow(row);

        for (var i = 0; i < headerTexts.Length; i++)
        {
            var cell = sheet.GetRow(row).CreateCell(i);
            cell.SetCellValue(headerTexts[i]);
            var cellStyle = sheet.Workbook.CreateCellStyle();
            var font = sheet.Workbook.CreateFont();
            font.IsBold = true;
            font.FontHeightInPoints = 12;
            cellStyle.SetFont(font);
            cell.CellStyle = cellStyle;
        }
    }

    protected void AddMasterDetailObject<T>(ISheet sheet, int startRow, IList<T> items,
        params Func<T, object>[] propertySelectors)
    {
        if (items.IsNullOrEmpty() || propertySelectors.IsNullOrEmpty()) return;

        for (var i = startRow; i < items.Count + startRow; i++)
        {
            var row = sheet.CreateRow(i);

            for (var j = 0; j < propertySelectors.Length; j++)
            {
                var cell = row.CreateCell(j);
                var value = propertySelectors[j](items[i - startRow]);
                if (value != null)
                {
                    if (value.GetType() == typeof(decimal))
                        cell.SetCellValue(Convert.ToDouble(value));
                    else
                        cell.SetCellValue(value.ToString());
                }
            }
        }
    }

    protected void AddHeaderSecoundWithMerge(ISheet sheet, int rowNumber, int columnIndex,int mergeCell, string headerTexts)
    {
        if (headerTexts.IsNullOrWhiteSpace())
        {
            return;
        }
        sheet.CreateRow(rowNumber);
        var cell = sheet.GetRow(rowNumber).CreateCell(columnIndex);
        var cellRange = new CellRangeAddress(rowNumber, rowNumber, columnIndex, columnIndex + mergeCell);
        sheet.AddMergedRegion(cellRange);
        cell.SetCellValue(headerTexts);
        StyleCell(sheet, cell);
    }


    //protected void AddHeaderSecoundWithMerge(ExcelWorksheet sheet, int rowNumber, int columnIndex, string headerTexts)
    //{
    //    if (string.IsNullOrWhiteSpace(headerTexts))
    //    {
    //        return;
    //    }
    //    int startMergeIndex = 1;
    //    // Merge cells
    //    var mergedRange = sheet.Cells[rowNumber, startMergeIndex, rowNumber, columnIndex];
    //    mergedRange.Merge = true;
    //    mergedRange.Value = headerTexts;
    //    mergedRange.Style.Font.Bold = true;
    //    mergedRange.Style.Font.Size = 14;
    //}

    //protected void AddHeaderSecoundWithMerge(ExcelWorksheet sheet, int rowNumber, int startMergeIndex, int columnIndex, string headerTexts)
    //{
    //    if (string.IsNullOrWhiteSpace(headerTexts))
    //    {
    //        return;
    //    }

    //    var mergedRange = sheet.Cells[rowNumber, startMergeIndex, rowNumber, columnIndex];
    //    mergedRange.Merge = true;
    //    mergedRange.Value = headerTexts;
    //    mergedRange.Style.Font.Bold = true;
    //    mergedRange.Style.Font.Size = 14;
    //}


    protected void AddHeader(ISheet sheet, params string[] headerTexts)
    {
        if (headerTexts.IsNullOrEmpty()) return;

        sheet.CreateRow(0);

        for (var i = 0; i < headerTexts.Length; i++) AddHeader(sheet, i, headerTexts[i]);
    }

    

    private void StyleCell(ISheet sheet,ICell cell)
    {
        var cellStyle = sheet.Workbook.CreateCellStyle();
        var font = sheet.Workbook.CreateFont();
        font.IsBold = true;
        font.FontHeightInPoints = 12;
        cellStyle.SetFont(font);
        cell.CellStyle = cellStyle;
    }

    
    protected void AddSecondHeader(ISheet sheet,int row, int columnIndex, string headerText)
    {
        var cell = sheet.GetRow(row).CreateCell(columnIndex);
        cell.SetCellValue(headerText);
        StyleCell(sheet, cell);
    }

    
    
    protected void AddHeader(ISheet sheet, int columnIndex, string headerText)
    {
        var cell = sheet.GetRow(0).CreateCell(columnIndex);
        cell.SetCellValue(headerText);
        StyleCell(sheet, cell);
    }

    protected void AddObjects<T>(ISheet sheet, IList<T> items, params Func<T, object>[] propertySelectors)
    {
        if (items.IsNullOrEmpty() || propertySelectors.IsNullOrEmpty()) return;

        for (var i = 1; i <= items.Count; i++)
        {
            var row = sheet.CreateRow(i);

            for (var j = 0; j < propertySelectors.Length; j++)
            {
                var cell = row.CreateCell(j);
                var value = propertySelectors[j](items[i - 1]);
                switch (value)
                {
                    case null:
                        continue;
                    case decimal:
                        cell.SetCellValue(Convert.ToDouble(value));
                        break;
                    case bool:
                        cell.SetCellValue(Convert.ToBoolean(value));
                        break;
                    case int:
                        cell.SetCellValue(Convert.ToInt32(value));
                        break;
                    case long:
                        cell.SetCellValue(Convert.ToInt64(value));
                        break;
                    default:
                        cell.SetCellValue(value.ToString());
                        break;
                }
            }
        }
    }

    protected void AddTailer(ISheet sheet, int startCol, int rowIndex, params decimal[] tailerAmount)
    {
        var row = sheet.CreateRow(rowIndex + 1);
        for (var i = 0; i < tailerAmount.Length; i++)
        {
            var cell = sheet.GetRow(rowIndex + 1).CreateCell(startCol + i);
            cell.SetCellValue((double)tailerAmount[i]);

            var cellStyle = sheet.Workbook.CreateCellStyle();
            var font = sheet.Workbook.CreateFont();
            font.IsBold = true;
            font.FontHeightInPoints = 12;
            cellStyle.SetFont(font);
            cell.CellStyle = cellStyle;
        }
    }

    protected void AddObjectsSwastik<T>(ISheet sheet, int startRowIndex, IList<T> items,
        params Func<T, object>[] propertySelectors)
    {
        if (items.IsNullOrEmpty() || propertySelectors.IsNullOrEmpty()) return;

        var m = items.Count + startRowIndex - 1;
        for (var i = startRowIndex; i <= m; i++)
        {
            var row = sheet.CreateRow(i);

            for (var j = 0; j < propertySelectors.Length; j++)
            {
                var cell = row.CreateCell(j);
                var value = propertySelectors[j](items[i - startRowIndex]);
                if (value != null)
                    if (value.GetType() == typeof(string))
                    {
                        cell.SetCellValue(value.ToString());
                        cell.CellStyle.ShrinkToFit = true;
                        cell.CellStyle.Alignment = HorizontalAlignment.Fill;
                    }
                    else
                    {
                        cell.SetCellValue(Convert.ToDouble(value));
                        cell.CellStyle.ShrinkToFit = true;
                        cell.CellStyle.Alignment = HorizontalAlignment.Fill;
                    }
            }
        }
    }

    protected virtual void Save(XSSFWorkbook excelPackage, FileDto file)
    {
        using (var stream = new MemoryStream())
        {
            excelPackage.Write(stream);
            _tempFileCacheManager.SetFile(file.FileToken, stream.ToArray());
        }
    }

    protected void SetCellDataFormat(ICell cell, string dataFormat)
    {
        if (cell == null)
            return;

        var dateStyle = GetDateCellStyle(cell, dataFormat);
        var format = GetDateDataFormat(cell, dataFormat);

        dateStyle.DataFormat = format.GetFormat(dataFormat);
        cell.CellStyle = dateStyle;
        if (DateTime.TryParse(cell.StringCellValue, out var datetime))
            cell.SetCellValue(datetime);
    }
}