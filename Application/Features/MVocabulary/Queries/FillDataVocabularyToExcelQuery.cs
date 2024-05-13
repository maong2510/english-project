using Application.Common.Models.MVocabulary;
using FluentValidation;
using MediatR;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Share.Extentions;
using System.Drawing;

namespace Application.Features.MVocabulary.Queries
{
    public class FillDataVocabularyToExcelQuery : IRequest<MemoryStream>
    {
        public MemoryStream MemoryStream { get; set; } = new MemoryStream();
        public List<VacabularyConvert> VacabularyConvert { get; set; }
    }

    public class FillDataVocabularyToExcelQueryValidator : AbstractValidator<FillDataVocabularyToExcelQuery>
    {
        public FillDataVocabularyToExcelQueryValidator()
        {
        }
    }

    public class FillDataVocabularyToExcelQueryHandler : IRequestHandler<FillDataVocabularyToExcelQuery, MemoryStream>
    {
        public FillDataVocabularyToExcelQueryHandler()
        {
        }

        public async Task<MemoryStream> Handle(FillDataVocabularyToExcelQuery request, CancellationToken cancellationToken)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage excel = new ExcelPackage(request.MemoryStream))
            {
                ExcelWorksheet worksheet = excel.Workbook.Worksheets.Add("Vocabulary");
                worksheet.Cells["A1"].Value = "Any Content";
                worksheet.Cells["A1"].Style.Font.Bold = true;
                worksheet.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["A1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells["A1"].Style.Font.Size = 14f;
                worksheet.Cells["A1:J1"].Merge = true;

                worksheet.Cells["A2"].Value = "No.";
                worksheet.Cells["B2"].Value = "Word";
                worksheet.Cells["C2"].Value = "Phonetic";
                worksheet.Cells["D2"].Value = "WordType";
                worksheet.Cells["E2"].Value = "Definition";
                worksheet.Column(5).Width = 100;
               
                worksheet.Cells["F2"].Value = "Example";
                worksheet.Column(6).Width = 600;
                worksheet.Cells["G2"].Value = "Synonyms";
                worksheet.Cells["H2"].Value = "Antonyms";
                worksheet.Cells["I2"].Value = "Audio";
                worksheet.Cells["J2"].Value = "Source";
                worksheet.Cells["A2:J2"].Style.Font.Bold = true;
                worksheet.Cells["A2:J2"].AutoFitColumns();
                int lastRow = 3;
                int STT = 1;
                foreach (var data in request.VacabularyConvert)
                {
                    var itemLastRow = lastRow;
                    var isOdd = false;
                    var check = STT % 2 != 0 ? isOdd == true : isOdd == false;

                    worksheet.Cells[lastRow, 1].Value = STT;
                    worksheet.Cells[lastRow, 2].Value = data.word;
                    if (check)
                    {
                        worksheet.Cells[lastRow, 1, lastRow, 10].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[lastRow, 1, lastRow, 10].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#ffbb99"));
                    }
                    else
                    {
                        worksheet.Cells[lastRow, 1, lastRow, 10].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[lastRow, 1, lastRow, 10].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#e6f2ff"));
                    }
                    var phonetic = data.phonetic;
                    var firstPhonetic = data.phonetics.Where(x => !string.IsNullOrWhiteSpace(x.audio) && !string.IsNullOrWhiteSpace(x.text)).FirstOrDefault();
                    if (firstPhonetic != null)
                    {
                        if (firstPhonetic == null)
                        {
                            firstPhonetic = data.phonetics.FirstOrDefault();
                        }
                        phonetic = phonetic.IsNullOrEmpty() ? firstPhonetic.text : phonetic;
                        worksheet.Cells[lastRow, 9].Value = firstPhonetic.audio;
                        worksheet.Cells[lastRow, 10].Value = firstPhonetic.sourceUrl;
                    }
                    worksheet.Cells[lastRow, 3].Value = phonetic;

                    var meanings = data.meanings.DistinctBy(x => x.partOfSpeech).ToList();
                    if (meanings != null && meanings.Count > 0)
                    {
                        var meaningRow = lastRow;
                        foreach (var meaning in meanings)
                        {
                            worksheet.Cells[meaningRow, 4].Value = meaning.partOfSpeech;
                            worksheet.Cells[meaningRow, 7].Value = string.Join(", ", meaning.synonyms);
                            worksheet.Cells[meaningRow, 8].Value = string.Join(", ", meaning.antonyms);
                            var definition = meaning.definitions.Where(x => x.definition.IsNullOrEmpty() == false).FirstOrDefault();
                            if (definition != null)
                            {
                                worksheet.Cells[meaningRow, 5].Value = definition.definition;
                                worksheet.Cells[meaningRow, 6].Value = definition.example;
                            }
                            if (check)
                            {
                                worksheet.Cells[meaningRow, 1, meaningRow, 10].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells[meaningRow, 1, meaningRow, 10].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#ffbb99"));
                            }
                            else
                            {
                                worksheet.Cells[meaningRow, 1, meaningRow, 10].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells[meaningRow, 1, meaningRow, 10].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#e6f2ff"));
                            }
                            meaningRow++;
                        }
                        lastRow = meaningRow - 1;
                    }
                    //worksheet.Cells[1, 1, lastRow - 1, 10].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);
                    lastRow++;
                    STT++;
                }

                worksheet.Cells[1, 1, lastRow - 1, 10].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[1, 1, lastRow - 1, 10].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[1, 1, lastRow - 1, 10].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[1, 1, lastRow - 1, 10].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                excel.Save();
            }

            return request.MemoryStream;
        }
    }
}