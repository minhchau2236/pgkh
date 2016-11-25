using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Services;
using System.Net;
using System.IO;
using System.Xml;

namespace PSCPortal.Services
{
    /// <summary>
    /// Summary description for WeatherService
    /// </summary>
    [ScriptService]
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WeatherService : System.Web.Services.WebService
    {

        [ScriptMethod]
        [WebMethod]
        public object GetWeatherInfo(string url)
        {
            string weather = string.Empty;
            try
            {
                //string url = "http://weather.msn.com/RSS.aspx?wealocations=wc:VMXX0007&weadegreetype=C";
                WebRequest wr = WebRequest.Create(url);
                //http://weather.msn.com/RSS.aspx?wealocations=wc:VMXX0029&weadegreetype=C (Nha Trang)
                // http://weather.msn.com/RSS.aspx?wealocations=wc:VMXX0007&weadegreetype=C (HCM)
                //http://weather.msn.com/RSS.aspx?wealocations=wc:VMXX0006&weadegreetype=C (HaNoi)
                //http://weather.msn.com/RSS.aspx?wealocations=wc:8456&weadegreetype=C (Dalat)
                //http://www.edg3.co.uk/snippets/weather-location-codes/vietnam/ (tra cuu thoi tiet cac tinh vn)
                WebResponse wresponse = wr.GetResponse();
                StreamReader reader = new StreamReader(wresponse.GetResponseStream());
                string content = reader.ReadToEnd();


                XmlDocument doc = new XmlDocument();
                doc.LoadXml(content);

                XmlNodeList elements = doc.GetElementsByTagName("description");
                weather = elements.Item(1).InnerText;
                if (weather.IndexOf("km/hr") != -1)
                {
                    weather = weather.Substring(0, (weather.IndexOf("km/hr") != -1 ? weather.IndexOf("km/hr") + 5 : weather.IndexOf("NE"))) + "</p>";
                    //     weather = weather.Substring(0,weather.IndexOf("NE")) + "</p>";
                    weather = "<?xml version=\"1.0\" ?>" + weather;
                    doc.LoadXml(weather);

                    XmlNode img = doc.GetElementsByTagName("img")[0];
                    string weatherInfo = doc.LastChild.LastChild.InnerText;

                    //tim nhiet do
                    string info = img.Attributes["title"].FirstChild.Value;
                    int start = info.Length + 2;
                    int end = weatherInfo.IndexOf("(");
                    string temperature = weatherInfo.Substring(start, end - start - 1);

                    //tim do am
                    start = weatherInfo.LastIndexOf("Humidity: ") + "Humidity: ".Length;
                    end = weatherInfo.IndexOf("%") + 1;
                    string humidity = weatherInfo.Substring(start, end - start);

                    //tim toc do gio
                    start = weatherInfo.LastIndexOf("Winds: ") + "Winds: ".Length;
                    end = weatherInfo.LastIndexOf("km/hr") + "km/hr".Length;
                    string windSpeed = weatherInfo.Substring(start, end - start);
//                    weather = string.Format(@"<table>
//	                                            <tbody>
//		                                            <tr>
//			                                            <td style='padding-left:15px; width:45px;'>{0}</td>
//                                                        <td style='font-size:13px; font-weight:bold; padding-left:20px; width:75px;'>TP.HCM {1}</td>
//                                                        <td style='width:110px; padding-left:8px; font-size:11px; border-left:1px solid #171715;'>
//            	                                            <div style='float:left; clear:both'>
//                                                            {2}
//                                                            </div>
//                                                            <div style='float:left; clear:both;'>
//                                                            Gió :{3}
//                                                            </div>
//                                                            <div style='float:left; clear:both;'>
//                                                            Độ ẩm :{4}
//                                                            </div>
//                                                        </td>
//                                                    </tr>
//                                                </tbody>
//                                            </table>", img.OuterXml, temperature, info, windSpeed, humidity);
                    // weather = string.Format("['{0}','{1}','{2}','{3}']", temperature, info, windSpeed, humidity);
                    return new string[] { img.OuterXml, temperature, info, windSpeed, humidity };

                }
                else
                {
                    weather = elements.Item(1).InnerText;
                    weather = "<p>" + weather.Substring(weather.IndexOf("Today") + 21, weather.IndexOf("Chance of precipitation") - weather.IndexOf("Today") - 21) + "</p>";
                    weather = "<?xml version=\"1.0\" ?>" + weather;
                    doc.LoadXml(weather);
                    XmlNode img = doc.GetElementsByTagName("img")[0];
                    string weatherInfo = doc.LastChild.LastChild.InnerText;
                    //tim nhiet do
                    string info = img.Attributes["title"].FirstChild.Value;
                    string start = weatherInfo.Substring(weatherInfo.IndexOf("Lo: ") + 4, weatherInfo.IndexOf(".") - weatherInfo.IndexOf("Lo: ") - 4);
                    string end = weatherInfo.Substring(weatherInfo.IndexOf("Hi: ") + 4, weatherInfo.LastIndexOf(".") - weatherInfo.IndexOf("Hi: ") - 4);
                    string temperature = start + " - " + end;


                    weather = string.Format("<table ><tbody><tr><td>{0}</td><td align=\"left\"><strong><font face=\"Arial\" color=\"#005482\" size=\"5\">{1}</font></strong></td></tr></tbody></table>", img.OuterXml, temperature);
                }


            }
            catch (Exception ex)
            {
                string err = ex.ToString();
            }
            return weather;
        }
        [ScriptMethod]
        [WebMethod]
        public string GetWeatherEnglishInfo(string url)
        {
            string weather = string.Empty;
            try
            {
                WebRequest wr = WebRequest.Create(url);
                //http://weather.msn.com/RSS.aspx?wealocations=wc:VMXX0029&weadegreetype=C (Nha Trang)
                // http://weather.msn.com/RSS.aspx?wealocations=wc:VMXX0007&weadegreetype=C (HCM)
                //http://weather.msn.com/RSS.aspx?wealocations=wc:VMXX0006&weadegreetype=C (HaNoi)
                //http://weather.msn.com/RSS.aspx?wealocations=wc:8456&weadegreetype=C (Dalat)
                WebResponse wresponse = wr.GetResponse();
                StreamReader reader = new StreamReader(wresponse.GetResponseStream());
                string content = reader.ReadToEnd();


                XmlDocument doc = new XmlDocument();
                doc.LoadXml(content);

                XmlNodeList elements = doc.GetElementsByTagName("description");
                weather = elements.Item(1).InnerText;
                if (weather.IndexOf("km/hr") != -1)
                {
                    weather = weather.Substring(0, (weather.IndexOf("km/hr") != -1 ? weather.IndexOf("km/hr") + 5 : weather.IndexOf("NE"))) + "</p>";
                    //     weather = weather.Substring(0,weather.IndexOf("NE")) + "</p>";
                    weather = "<?xml version=\"1.0\" ?>" + weather;
                    doc.LoadXml(weather);

                    XmlNode img = doc.GetElementsByTagName("img")[0];
                    string weatherInfo = doc.LastChild.LastChild.InnerText;

                    //tim nhiet do
                    string info = img.Attributes["title"].FirstChild.Value;
                    int start = info.Length + 2;
                    int end = weatherInfo.IndexOf("(");
                    string temperature = weatherInfo.Substring(start, end - start - 1);

                    //tim do am
                    start = weatherInfo.LastIndexOf("Humidity: ") + "Humidity: ".Length;
                    end = weatherInfo.IndexOf("%") + 1;
                    string humidity = weatherInfo.Substring(start, end - start);

                    //tim toc do gio
                    start = weatherInfo.LastIndexOf("Winds: ") + "Winds: ".Length;
                    end = weatherInfo.LastIndexOf("km/hr") + "km/hr".Length;
                    string windSpeed = weatherInfo.Substring(start, end - start);
                    weather = string.Format("<table ><tbody><tr><td>{0}</td><td align=\"left\"><strong><font face=\"Arial\" color=\"#005482\" size=\"6\">{1}</font></strong></td></tr><tr><td colspan=\"2\" align=\"left\">{2}</td></tr></tbody></table>", img.OuterXml, temperature, "<font face=\"Arial\" size=\"2\">" + info + "<br/>" + "Humidity: " + humidity + "<br/>" + "Wind Speed: " + windSpeed + "</font>");
                }
                else
                {
                    weather = elements.Item(1).InnerText;
                    weather = "<p>" + weather.Substring(weather.IndexOf("Today") + 21, weather.IndexOf("Chance of precipitation") - weather.IndexOf("Today") - 21) + "</p>";
                    weather = "<?xml version=\"1.0\" ?>" + weather;
                    doc.LoadXml(weather);
                    XmlNode img = doc.GetElementsByTagName("img")[0];
                    string weatherInfo = doc.LastChild.LastChild.InnerText;
                    //tim nhiet do
                    string info = img.Attributes["title"].FirstChild.Value;
                    string start = weatherInfo.Substring(weatherInfo.IndexOf("Lo: ") + 4, weatherInfo.IndexOf(".") - weatherInfo.IndexOf("Lo: ") - 4);
                    string end = weatherInfo.Substring(weatherInfo.IndexOf("Hi: ") + 4, weatherInfo.LastIndexOf(".") - weatherInfo.IndexOf("Hi: ") - 4);
                    string temperature = start + " - " + end;


                    weather = string.Format("<table ><tbody><tr><td>{0}</td><td align=\"left\"><strong><font face=\"Arial\" color=\"#005482\" size=\"5\">{1}</font></strong></td></tr></tbody></table>", img.OuterXml, temperature);
                }


            }
            catch (Exception ex)
            {
                string err = ex.ToString();
            }
            return weather;
        }
    }
}
