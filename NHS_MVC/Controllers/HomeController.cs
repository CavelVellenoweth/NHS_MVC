using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NHS_MVC.Models;

namespace NHS_MVC.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        string conditionBaseURI = "https://api.nhs.uk/";
        string API_PKEY = "e7f10c4f6cf648e59c3fe08fd5465d39";
        string API_SKEY = "0afc9b215daf4ee1806a74fad77062e4";
        private readonly ILogger<HomeController> _logger;
        Dictionary<string,Tuple<string,string>> parts = new Dictionary<string, Tuple<string, string>>();

        //https://api.nhs.uk/conditions/?category=A&synonyms=false&childArticles=false
        //SignificantLink > name + url

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Route("{letter?}")]
        public IActionResult Search(char? letter = 'A')
        {
            StringBuilder medicalPageHTML = new StringBuilder();
            Dictionary<string, string> conditions = new Dictionary<string, string>();
           // List<Reservation> reservationList = new List<Reservation>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("subscription-key", API_PKEY);
                using (var response = httpClient.GetAsync(conditionBaseURI + "conditions/?category="+ letter + "&synonyms=false&childArticles=false"))
                {
                    var result = response.Result;
                    var content = result.Content.ReadAsStringAsync();
                    var r = content.Result;
                    Models.SearchResultsPage x  = JsonConvert.DeserializeObject<Models.SearchResultsPage>(r);

                    foreach (var condition in x.SignificantLink)
                    {
                        conditions.Add(condition.Name, AlterHyperlinks(condition.URL, false));
                    }
                }         
            }
            ViewBag.conditionTable = conditions;
            ViewBag.letter = letter;
            return View();
        }
        [Route("{type}/{condition}/{extra?}")]
        public IActionResult Conditions()
        {
            var type = RouteData.Values["type"];
            var search = RouteData.Values["condition"];            
            var extra = RouteData.Values["extra"];
            string URI = type.ToString();
            URI += "/" + search;
            if (extra != null)
            {
                URI += "/" + extra;
            }
            StringBuilder medicalPageHTML = new StringBuilder();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("subscription-key", API_PKEY);
                
                using (var response = httpClient.GetAsync(conditionBaseURI + URI + "?"))
                {
                    var result = response.Result;
                    var content = result.Content.ReadAsStringAsync();
                    var r = content.Result;

                    Models.CollectionPage page = JsonConvert.DeserializeObject<Models.CollectionPage>(r);
                    medicalPageHTML.Append("<h1>" + page.Name + "</h1>");
                    if (page.HasPart != null)
                    {
                        foreach (var part in page.HasPart)
                        {
                            if (part.Title != null && part.Title != "")
                            {
                                parts.Add(part.Title, new Tuple<string, string>(part.Description, "<div class = '" + part.Name + "'>" + part.Text + "</div>"));
                            }
                        }
                    }

                    foreach (var mainEntity in page.MainEntityOfPage)
                    {
                        //if (mainEntity.Headline != null && mainEntity.Headline != "")
                        //{
                        //    medicalPageHTML.Append("<h3>" + mainEntity.Headline + "</h3>");
                        //}
                        //if (mainEntity.Headline != null && parts.ContainsKey(mainEntity.Headline))
                        //{
                        //    medicalPageHTML.Append(AlterHyperlinks(parts[mainEntity.Headline].Item2));
                        //}

                         if (mainEntity.mainEntityOfPage != null)
                        {
                            if (mainEntity.Headline != null && mainEntity.Headline != "")
                            {
                                medicalPageHTML.Append("<h3>" + mainEntity.Headline + "</h3>");
                            }
                            foreach (var entity in mainEntity.mainEntityOfPage)
                            {
                                if (entity.Headline != null && entity.Headline != "")
                                {
                                    medicalPageHTML.Append("<h3>" + entity.Headline + "</h3>");
                                }
                                if (entity.MainEntity != null)  
                                {
                                    medicalPageHTML.Append("<div class = '" + Regex.Replace(entity.Name, @"[\W\s]+", String.Empty) + " top'>" + "<h3>"  + entity.subjectOf + "</h3>");
                                    foreach (var subEntity in entity.MainEntity)
                                    {
                                        if (subEntity.Text != null && subEntity.Text != "")
                                        {
                                            medicalPageHTML.Append("<div class = '"+ Regex.Replace(subEntity.Name, @"[\W\s]+", String.Empty) + "'>"  + AlterHyperlinks(subEntity.Text) + "</div>");
                                        }
                                    }
                                    medicalPageHTML.Append("</div>");

                                }
                                if(entity.acceptedAnswer != null)
                                {
                                    medicalPageHTML.Append("<div class = '" + Regex.Replace(entity.Name, @"[\W\s]+", String.Empty) + " top'>" + AlterHyperlinks(entity.Text));
                                    foreach (var answer in entity.acceptedAnswer.mainEntity)
                                    {
                                        if (answer.Text != null && answer.Text != "")
                                        {
                                            medicalPageHTML.Append("<div class = 'answer'>" + AlterHyperlinks(answer.Text) + "</div>");
                                        }
                                    }
                                    medicalPageHTML.Append("</div>");
                                }
                                else if (entity.Text != null && entity.Text != "")
                                {
                                    medicalPageHTML.Append("<div class = '" + Regex.Replace(entity.Name, @"[\W\s]+", String.Empty) + " top'>" + AlterHyperlinks(entity.Text) + "</div>");
                                }
                                
                            }
                        }
                         else if(mainEntity.Text != null && mainEntity.Text != "")
                        {
                            medicalPageHTML.Append("<div class = '" + Regex.Replace(mainEntity.Name, @"[\W\s]+", String.Empty) + " top'>" + AlterHyperlinks(mainEntity.Text) + "</div>");
                        }
                    }
                }
            }
            return View(new HtmlString(medicalPageHTML.ToString()));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("error")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Route("updateViewBag")]
        public IActionResult updateViewBag(char _letter)
        {
            return RedirectToAction("Search", new { letter = _letter });
        }
        private string AlterHyperlinks(string html, bool isHTML = true)
        {
            string altered = html;
            if(!isHTML)
            {
                altered = altered.Replace("https://api.nhs.uk", "");
                altered = altered.Replace("https://www.nhs.uk", "");
                altered = altered.Replace("https://beta.nhs.uk", "");
                return altered;
            }
            if(html.Contains(" href"))
            {
                altered = altered.Replace("https://api.nhs.uk", "");
                altered = altered.Replace("https://www.nhs.uk", "");
                altered = altered.Replace("https://beta.nhs.uk", "");
            }
            return altered;
        }   
    }
}
