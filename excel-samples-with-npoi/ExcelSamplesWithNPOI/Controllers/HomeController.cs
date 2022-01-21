using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ExcelSamplesWithNPOI.Models;
using NPOI.HSSF.UserModel;
using MFramework.Services.FakeData;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using NPOI.SS.UserModel;

namespace ExcelSamplesWithNPOI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHostingEnvironment _environment;

        public HomeController(ILogger<HomeController> logger, IHostingEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ExcelExport()
        {
            // https://steemit.com/utopian-io/@haig/how-to-create-excel-spreadsheets-using-npoi

            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet("Sheet 1");

            IFont fontBold = workbook.CreateFont();
            fontBold.Boldweight = (short)FontBoldWeight.Bold;

            var headerCellStyle = workbook.CreateCellStyle();
            headerCellStyle.SetFont(fontBold);

            var header = sheet.CreateRow(0);
            ICell col1 = header.CreateCell(0); col1.CellStyle = headerCellStyle; col1.SetCellValue("Fullname");
            ICell col2 = header.CreateCell(1); col2.CellStyle = headerCellStyle; col2.SetCellValue("Country");
            ICell col3 = header.CreateCell(2); col3.CellStyle = headerCellStyle; col3.SetCellValue("Email");
            ICell col4 = header.CreateCell(3); col4.CellStyle = headerCellStyle; col4.SetCellValue("Numbers");
            ICell col5 = header.CreateCell(4); col5.CellStyle = headerCellStyle; col5.SetCellValue("Datetime");
            ICell col6 = header.CreateCell(5); col6.CellStyle = headerCellStyle; col6.SetCellValue("Sentence");

            var currencyCellStyle = workbook.CreateCellStyle();
            currencyCellStyle.Alignment = HorizontalAlignment.Right;
            currencyCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("₺#.##0,00"); ;

            int length = 50;

            for (int i = 0; i < length; i++)
            {
                var row = sheet.CreateRow(i + 1);
                row.CreateCell(0).SetCellValue(NameData.GetFullName());
                row.CreateCell(1).SetCellValue(PlaceData.GetCountry());
                row.CreateCell(2).SetCellValue(NetworkData.GetEmail());

                ICell numberCell = row.CreateCell(3);
                numberCell.CellStyle = currencyCellStyle;
                numberCell.SetCellValue(NumberData.GetDouble() * 1000);

                row.CreateCell(4).SetCellValue(DateTimeData.GetDatetime().ToShortDateString());
                row.CreateCell(5).SetCellValue(TextData.GetSentence());
            }

            var totalRow = sheet.CreateRow(length + 1);
            ICell totalCell = totalRow.CreateCell(3);

            ICellStyle totalCellStyle = workbook.CreateCellStyle();
            totalCellStyle.CloneStyleFrom(sheet.GetRow(length).GetCell(3).CellStyle);

            totalCell.CellStyle = totalCellStyle;
            totalCell.CellStyle.SetFont(fontBold);
            totalCell.SetCellType(CellType.Formula);
            totalCell.CellFormula = $"SUM(D2:D{length + 1})";   // Headercell den sonrası.

            string root = _environment.WebRootPath;
            string folder = Path.Combine(root, "Excels");
            string filename = "f_" + Guid.NewGuid().ToString() + ".xls";
            string file = Path.Combine(folder, filename);

            if (Directory.Exists(folder) == false)
                Directory.CreateDirectory(folder);

            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                workbook.Write(fileStream);
            }

            return File("~/Excels/" + filename, "application/vnd.ms-excel");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
