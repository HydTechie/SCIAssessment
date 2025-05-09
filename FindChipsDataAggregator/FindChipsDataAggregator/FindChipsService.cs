using HtmlAgilityPack;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FindChipsScraper
{
    public class DistributorOffer
    {
        public string Distributor { get; set; }
        public string Seller { get; set; }
        public string MOQ { get; set; }
        public string SPQ { get; set; }
        public string UnitPrice { get; set; }
        public string Currency { get; set; }
        public string OfferUrl { get; set; }
    }

    public class FindChipsService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FindChipsService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task RunAsync()
        {
            var client = _httpClientFactory.CreateClient();
            string url = "https://www.findchips.com/search/2N222";
            var html = await client.GetStringAsync(url);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var offers = new List<DistributorOffer>();
            var offerNodes = htmlDoc.DocumentNode.SelectNodes("//div[@data-distributor_name]")?.Take(5);
            var unitPrice = "N/A";
            var currency = "N/A";

            if (offerNodes != null)
            {
                foreach (var node in offerNodes)
                {
                    try
                    {
                        var distributor = node.GetAttributeValue("data-distributor_name", "") ?? "N/A";
                        
                        // Need 5, but head row extra, so take 6 rows
                        var tableRowNodes= node.SelectNodes(".//tr")?.Take(6);

                        if (tableRowNodes == null) 
                            continue; // skip Distributor node

                        foreach( var rowNode in tableRowNodes)
                        {
                           
                            if (rowNode.ParentNode.Name == "thead")
                                continue; // skip header row
                            
                            //data-title="Package Mult." data-title="Min Qty"  in description TD
                            var seller_mfr = rowNode.GetAttributeValue("data-mfr", "") ?? "N/A";
                            var moq = rowNode.SelectSingleNode(".//td[contains(@class, 'td-desc more')]//span[(@data-title ='Package Mult.')]")?.InnerText?.Trim() ?? "N/A";
                            var spq = rowNode.SelectSingleNode(".//td[contains(@class, 'td-desc more')]//span[(@data-title ='Min Qty')]")?.InnerText?.Trim() ?? "N/A";

                            var offerUrl = rowNode.SelectSingleNode(".//td[contains(@class, 'td-buy last')]//a")?.Attributes["href"].Value;

                            //Finding sibbling of unit value 1, followed by data-baseprice data-basecurrency
                            var unitPriceNode = rowNode.SelectSingleNode(".//td[contains(@class, 'td-price')]//li/span[normalize-space(text())='1']//following-sibling::span[1]");
                            if (unitPriceNode != null)
                            {
                                unitPrice = unitPriceNode.GetAttributeValue("data-baseprice", "");
                                currency = unitPriceNode.GetAttributeValue("data-basecurrency", "");
                            }
                            

                            offers.Add(new DistributorOffer
                            {
                                Distributor = distributor,
                                Seller = seller_mfr,
                                MOQ = moq,
                                SPQ = spq,
                               UnitPrice = unitPrice,
                               Currency = currency,
                                OfferUrl = string.IsNullOrEmpty(offerUrl) ? "N/A" : $"{offerUrl}"
                            });
                        }
                       
                    }
                    catch
                    {
                        // Handle or log individual parse failures if needed
                    }
                }
            }

            await ExportToExcelAsync(offers);
            Console.WriteLine("Export complete: FindChips_Offers.xlsx");
        }

        private async Task ExportToExcelAsync(List<DistributorOffer> offers)
        {
            //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("Offers");

            var headers = new[] { "Distributor", "Seller", "MOQ", "SPQ", "Unit Price", "Currency", "Offer URL" };
            for (int i = 0; i < headers.Length; i++)
                sheet.Cells[1, i + 1].Value = headers[i];

            for (int i = 0; i < offers.Count; i++)
            {
                var o = offers[i];
                sheet.Cells[i + 2, 1].Value = o.Distributor;
                sheet.Cells[i + 2, 2].Value = o.Seller;
                sheet.Cells[i + 2, 3].Value = o.MOQ;
                sheet.Cells[i + 2, 4].Value = o.SPQ;
                sheet.Cells[i + 2, 5].Value = o.UnitPrice;
                sheet.Cells[i + 2, 6].Value = o.Currency;
                sheet.Cells[i + 2, 7].Value = o.OfferUrl;
            }

            var filePath = "../../../FindChips_Offers.xlsx";
            package.SaveAs(new FileInfo(filePath));
        }
    }
}
