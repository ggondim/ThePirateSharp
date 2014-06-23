using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;
using System.IO;
using System.Globalization;

namespace ThePirateBay
{
	public class Tpb
	{
		public static IEnumerable<Torrent> Search(Query query)
		{
			List<Torrent> result = new List<Torrent>();
			WebRequest request = WebRequest.Create(query.TranslateToUrl());
            ((HttpWebRequest)request).UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/35.0.1916.153 Safari/537.36";
            ((HttpWebRequest)request).AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
			WebResponse response = request.GetResponse();
			HtmlDocument doc = new HtmlDocument();
            string html = string.Empty;
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                html = reader.ReadToEnd();
            }
			doc.LoadHtml(html);

			foreach (var tr in doc.DocumentNode.Descendants().Where(x => x.Name == "tr"))
			{
				if (!tr.Attributes.Any(x => x.Name == "class") || tr.Attributes.Single(x => x.Name == "class").Value != "header")
				{
					Torrent torrent = new Torrent();

					// First TD: parent category and category
					HtmlNode td1 = tr.Descendants().Where(x => x.Name == "td").First();
                    IEnumerable<HtmlNode> categories = td1.Descendants("a");
					torrent.CategoryParent = int.Parse(categories.First().Attributes["href"].Value.Split('/').Last());
					torrent.Category = int.Parse(categories.Last().Attributes["href"].Value.Split('/').Last());

					// Second TD: all another infos, but seeds and leechers
					HtmlNode td2 = tr.Descendants().Where(x => x.Name == "td").ElementAt(1);
					HtmlNode aTorrentName = td2.Descendants("div").Single().Descendants("a").Single();
					torrent.Name = aTorrentName.InnerText;
					torrent.Magnet = td2.Descendants("a").Where(a => a.Attributes.Any(attr => attr.Name == "href") && a.Attributes["href"].Value.StartsWith("magnet:")).Single().Attributes["href"].Value;
					IEnumerable<HtmlNode> possibleDownloadLink = td2.Descendants("a").Where(a => a.Attributes.Any(attr => attr.Name == "href") && a.Attributes["href"].Value.Contains(".torrent") && a.Attributes.Any(attr => attr.Name == "title") && a.Attributes["title"].Value == "Download this torrent");
					if (possibleDownloadLink.Count() == 1)
					{
						torrent.File = possibleDownloadLink.Single().Attributes["href"].Value;
						if (torrent.File.StartsWith("//"))
						{
							torrent.File = "http:" + torrent.File;
						}
					}
                    IEnumerable<HtmlNode> icons = td2.Descendants("img");
                    foreach (var icon in icons)
                    {
                        if (icon.Attributes.Any(x => x.Name == "alt"))
                        {
                            if (icon.Attributes["alt"].Value == "Trusted")
                            {
                                torrent.IsTrusted = true;
                            }
                            if (icon.Attributes["alt"].Value == "This torrent has a cover image")
                            {
                                torrent.HasCoverImage = true;
                            }
                            if (icon.Attributes["alt"].Value.Contains(" comments"))
                            {
                                torrent.Comments = int.Parse(icon.Attributes["alt"].Value.Split(' ')[3]);
                            }
                            if (icon.Attributes["alt"].Value == "VIP")
                            {
                                torrent.IsVip = true;
                            }
                        }
                    }
                    HtmlNode details = td2.Descendants("font").First();
                    string[] parameters = details.InnerText.Replace("&nbsp;", " ").Split(',');
                    foreach (var parameter in parameters)
                    {
                        if (parameter.Trim().StartsWith("Uploaded"))
                        {
                            string[] uploaded = parameter.Trim().Split(' ');
                            if (uploaded.Length > 2)
                            {
                                torrent.Uploaded = string.Join(" ", uploaded.Skip(1));
                            }
                            else
                            {
                                torrent.Uploaded = uploaded.Last();
                            }
                        }
                        else if (parameter.Trim().StartsWith("Size"))
                        {
                            string[] size = parameter.Trim().Split(' ');
                            torrent.Size = string.Join(" ", size.Skip(1));
                            if (size.Last().Contains("KiB"))
                            {
                                torrent.SizeBytes = decimal.Parse(size[1], System.Globalization.NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US")) * 1024M;
                            }
                            else if (size.Last().Contains("MiB"))
                            {
                                torrent.SizeBytes = decimal.Parse(size[1], System.Globalization.NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US")) * 1024M * 1024M;
                            }
                            else if (size.Last().Contains("GiB"))
                            {
                                torrent.SizeBytes = decimal.Parse(size[1], System.Globalization.NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US")) * 1024M * 1024M * 1024M;
                            }
                            else if (size.Last().Contains("TiB"))
                            {
                                torrent.SizeBytes = decimal.Parse(size[1], System.Globalization.NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US")) * 1024M * 1024M * 1024M * 1024M;
                            }
                        }
                        else if (parameter.Trim().StartsWith("ULed"))
                        {
                            string[] uled = parameter.Trim().Split(' ');
                            torrent.Uled = uled.Last();
                        }
                    }

					// Third TD: seeds
					HtmlNode td3 = tr.Descendants().Where(x => x.Name == "td").ElementAt(2);
					torrent.Seeds = int.Parse(td3.InnerText);

					// Fourth TD: leechers
					HtmlNode td4 = tr.Descendants().Where(x => x.Name == "td").ElementAt(3);
					torrent.Leechers = int.Parse(td4.InnerText);

                    result.Add(torrent);
				}
			}
            return result;
		}
	}
}
