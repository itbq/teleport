using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

using Dapper;
using Itbq.Bbm.Integration.Teleport.Properties;
using Itbq.Bbm.Integration.Teleport.Request;

namespace Itbq.Bbm.Integration.Teleport
{
    public static class Program
    {
        private static readonly IDictionary<string, string> ResponseMessageMap =
            new Dictionary<string, string>
                {
                    { "unauthorized", "Ошибка работы формы, свяжитесь с техподдержкой сайта" },
                    { "error incomplete", "Не все необходимые поля заполнены" },
                    { "error double", "Заявка с таким номером телефона уже существует" },
                    { "success", "Заявка успешно отправлена" },
                    { "error", "Заявка не принята" }
                };

        public static void Main()
        {
            while (true)
            {
                SendAsync().Wait();
            }
            // ReSharper disable once FunctionNeverReturns
        }

        private static async Task SendAsync()
        {
            var requests = ObtainData();
            foreach (var request in requests)
            {
                var response = await TeleportAgent.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(
                    ResponseMessageMap.TryGetValue(content, out var message)
                        ? message
                        : "Неизвестный ответ сервера!");
                await Task.Delay(Settings.Default.SendDelay);
            }
        }

        private static IEnumerable<Request.Request> ObtainData()
        {
            // пример данных для телепорта
            //    new FullRequest
            //        {
            //            FirtsName = "dummy",
            //            MiddleName = "funny",
            //            LastName = "kaplan",
            //            Birthday = DateTime.UtcNow,
            //            Amount = 133.76M,
            //            Period = 4,
            //            Phone = "+79160000001",
            //            ResidentialRegion = "Москва",
            //            ResidentialCity = "Зеленоград",
            //            RegistrationRegion = "Москва",
            //            RegistrationCity = "Зеленоград"
            //        };

            using (var connection = new SqlConnection(Settings.Default.BbmConnectionString))
            {
               return
                    connection.Query<FullRequest>(
                        " SELECT TOP 10" +
                        " , FirstName" +
                        " , LastName" +
                        " , BirthdayDate AS Birthday" +
                        " , 0 AS Amount" +
                        " , 4 AS Period" +
                        " , ba.PhoneNumber AS Phone" +
                        " , bst.Name AS ResidentialRegion" +
                        " , ba.City AS ResidentialCity" +
                        " , sst.Name AS RegistrationRegion" +
                        " , sa.City AS RegistrationCity" +
                        " FROM [dbo].[Customer] c (NOLOCK)" +
                        " INNER JOIN [dbo].[Address] ba ON ba.Id = c.BillingAddress_Id" +
                        " INNER JOIN [dbo].[Address] sa ON sa.Id = c.ShippingAddress_Id" +
                        " INNER JOIN [dbo].[StateProvince] bst ON bst.Id = ba.StateProvinceId" +
                        " INNER JOIN [dbo].[StateProvince] sst ON sst.Id = sa.StateProvinceId" +
                        " WHERE c.CreatedOnUtc > CAST(GETUTCDATE() - 1 AS DATE)");
            }
        }
    }
}
