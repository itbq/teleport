using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            var request = ObtainData();
            var response = await TeleportAgent.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(
                ResponseMessageMap.TryGetValue(content, out var message)
                    ? message
                    : "Неизвестный ответ сервера!");
            await Task.Delay(Settings.Default.SendDelay);
        }

        private static Request.Request ObtainData()
        {
            // ToDo: get data from repository/db
            return
                new FullRequest
                    {
                        FirtsName = "dummy",
                        MiddleName = "funny",
                        LastName = "kaplan",
                        Birthday = DateTime.UtcNow,
                        Amount = 133.76M,
                        Period = 4,
                        Phone = "+79160000001",
                        ResidentialRegion = "Москва",
                        ResidentialCity = "Зеленоград",
                        RegistrationRegion = "Москва",
                        RegistrationCity = "Зеленоград"
                    };
        }
    }
}
