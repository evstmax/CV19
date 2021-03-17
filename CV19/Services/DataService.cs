using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CV19.Models;

namespace CV19.Services
{
    internal class DataService
    {

        private const string _DataSourceAddress = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

        /// <summary>
        /// Метод возвращает поток
        /// </summary>
        /// <returns></returns>
        private static async Task<Stream> GetDataStream()
        {
            var client = new HttpClient(); // 2. сервер ответит
            var response =
                await client.GetAsync(_DataSourceAddress,
                    HttpCompletionOption.ResponseHeadersRead); //3. http  клиент скачает только заголовок ответа, тело ответа скачано не будет либо зависнет в буфере сетевой карты либо сервер просто приостановит передачу данных
            return
                await response.Content.ReadAsStreamAsync(); // 4. после этого response вернет содержимоее ответа в виде потока данных
        }

        /// <summary>
        /// Метод возвращает перечисление строк
        /// </summary>
        private static IEnumerable<string> GetDataLines()
        {
            using var data_steam =Task.Run(GetDataStream).Result; // 1. произодится запрос к серверу (захватываем поток)
            using var data_reader = new StreamReader(data_steam); // 5. создаем объект для чтения потока байт за байтом

            while (!data_reader.EndOfStream)  // 6.пока поток не кончится 
            {
                var line = data_reader.ReadLine(); // 7. считываем одну строчку
                if (string.IsNullOrWhiteSpace(line)) continue; // 8. и если она не пустая
                yield return line.Replace("Korea,", "Korea -").Replace("Bonaire,", "Bonaire -"); // 9. возвращаем эту строчку как результат(и еще почистим строки так как разделитель запятая с Кореей будут проблемы)
            }
        }

        private static DateTime[] GetDates() => GetDataLines()
            .First()
            .Split(',')
            .Skip(4)
            .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture))
            .ToArray();

        private static IEnumerable<(string Province, string Country, (double Lat, double Lon) Place, int[] Counts)> GetCountiesData()
        {
            var lines = GetDataLines()
                .Skip(1)
                .Select(line => line.Split(','));

            foreach (var row in lines)
            {
                var province = row[0].Trim();
                var country_name = row[1].Trim(' ', '"');

                if (!double.TryParse(row[2], out double latitude)) latitude = 0;
                if (!double.TryParse(row[3], out double longitude)) longitude = 0;
                   
                
                // var latitude = double.Parse(row[2], CultureInfo.InvariantCulture);
                  var counts = row.Skip(4).Select(str => 
                        { int value; 
                          int.TryParse(str, out value); 
                          return value;
                        }).ToArray();
                //  var counts = row.Skip(4).Select(int.Parse).ToArray();

                yield return (province, country_name, (latitude, longitude),  counts);
            }

        }
        //static void Main(string[] args)
        //{
        //    //WebClient client = new WebClient();
        //    //var client = new HttpClient();
        //    //var response = client.GetAsync(data_url).Result;
        //    //var csv_str = response.Content.ReadAsStringAsync().Result;
        //    //Console.ReadLine();

        //    //foreach (var data_line in GetDataLines())
        //    //{
        //    //    Console.WriteLine(data_line);
        //    //}

        //    //var dates = GetDates();
        //    //Console.WriteLine(string.Join("\r\n", dates));

        //    var russia_data = GetData()
        //        .First(v => v.Country.Equals("Russia", StringComparison.OrdinalIgnoreCase));

        //    Console.WriteLine(string.Join("\r\n", GetDates().Zip(russia_data.Counts, (date, count) => $"{date:dd:MM:yyyy} - {count}")));
        //    Console.ReadLine();



            public IEnumerable<CountryInfo> GetData()
            {
                var dates = GetDates();

                var data = GetCountiesData().GroupBy(d => d.Country);

                foreach (var country_info in data)
                {
                    var country = new CountryInfo()
                    {
                        Name = country_info.Key,
                        ProvinceCounts = country_info.Select(c => new PlaceInfo()
                            {
                                Name = c.Province,
                                Location = new Point(c.Place.Lat, c.Place.Lon),
                                Counts = dates.Zip(c.Counts, (date,count) => new ConfirmedCount()
                                {
                                    Date=date, Count = count
                                })

                            }
                        )
                    };
                    yield return country;
                }
        
            //return Enumerable.Empty<CountryInfo>();
        }
    }
}
    