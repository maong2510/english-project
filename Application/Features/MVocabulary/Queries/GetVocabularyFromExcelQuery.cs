using Application.Common.Models.MVocabulary;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OfficeOpenXml;
using RestSharp;
using Share.EnpointConfig;
using Share.Exceptions;
using Share.Extentions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Features.MVocabulary.Queries
{
    public class GetVocabularyFromExcelQuery : IRequest<ApiResult>
    {
        public IFormFile File { get; set; }

    }
    public class GetVocabularyFromExcelQueryValidator : AbstractValidator<GetVocabularyFromExcelQuery>
    {
        public GetVocabularyFromExcelQueryValidator()
        {
        }
    }

    public class GetVocabularyFromExcelQueryHandler : IRequestHandler<GetVocabularyFromExcelQuery, ApiResult>
    {
        public GetVocabularyFromExcelQueryHandler()
        {
        }
        public async Task<ApiResult> Handle(GetVocabularyFromExcelQuery request, CancellationToken cancellationToken)
        {
            try
            {
                string url = "https://api.dictionaryapi.dev/api/v2/entries/en";
                RestClient restClient = new RestClient(url);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                VacabularyResponse vacabularyResponse = new VacabularyResponse();
                List<VacabularyConvert> vacabularyConverts = new List<VacabularyConvert>();
                List<string> words = new List<string>();
                MemoryStream Stream = new MemoryStream();
                request.File.OpenReadStream().CopyTo(Stream);
                List<string> Errors = new List<string>();
                #region refile Excel
                if (Stream != null)
                {
                    using (var package = new ExcelPackage(Stream))
                    {
                        var workBook = package.Workbook;
                        foreach (var workSheet in workBook.Worksheets)
                        {
                            int startColumn = 1;
                            int startRow = 2;
                            List<string> columns = new List<string>();
                            for (int column = startColumn; column <= workSheet.Dimension.End.Column; column++)
                            {
                                string columnName = workSheet.Cells[startRow, column].ToString() ?? "";
                                columns.Add(columnName);
                            }
                            for (int row = startRow + 1; row <= workSheet.Dimension.End.Row; row++)
                            {
                                try
                                {
                                    var STT = workSheet.Cells[row, 1].Value?.ToString().RemoveWhiteSpace();
                                    if (STT.ToLower() == "end")
                                    {
                                        break;
                                    }
                                    var word = workSheet.Cells[row, 2].Value?.ToString();
                                    if (word.IsNullOrEmpty() == false && !words.Any(x => word == x))
                                    {
                                        words.Add(word);
                                    }
                                }
                                catch
                                {

                                }
                            }

                        }
                    }
                }
                if (words != null && words.Count > 0)
                {
                    Parallel.ForEach(words, word =>
                    {
                        RestRequest restRequest = new RestRequest($"/{word}");
                        var response = restClient.Execute<VacabularyConvert>(restRequest);
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var data = response.Content;

                            List<VacabularyConvert> vacabularyConvert = JsonConvert.DeserializeObject<List<VacabularyConvert>>(data);
                            lock (vacabularyConvert)

                                if (vacabularyConvert.Count > 0)
                                {
                                    lock (vacabularyConvert)
                                    {
                                        vacabularyConverts = vacabularyConverts?.Where(x => x != null).ToList();
                                        vacabularyConverts.AddRange(vacabularyConvert);
                                    }
                                }
                        }
                    });
                }
                vacabularyConverts =  vacabularyConverts.OrderBy(x=>x.word).ToList();
                #endregion
                return new ApiResult(vacabularyConverts);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


}
