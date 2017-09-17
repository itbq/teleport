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

        public static void Main() => SendAsync();

        private static async void SendAsync()
        {
            while (true)
            {
                var request = ObtainData();
                var response = await TeleportAgent.SendAsync(request).ConfigureAwait(false);
                Console.WriteLine(
                    ResponseMessageMap.TryGetValue(response.Content.ToString(), out string message)
                        ? message
                        : "Неизвестный ответ сервера!");
                await Task.Delay(Settings.Default.SendDelay);
            }
            // ReSharper disable once FunctionNeverReturns
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
