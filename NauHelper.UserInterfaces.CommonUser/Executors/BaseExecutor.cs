using System.Globalization;
using Telegram.Bot;
using Telegramper.Core.AdvancedBotClient.Extensions;
using Telegramper.Executors.Common.Models;
using Telegramper.Executors.QueryHandlers.Attributes.Targets;

namespace UserInterfaces.Student.Executors
{
    public class BaseExecutor : Executor
    {
        [TargetCommand]
        public async Task Start(string culture)
        {
            CultureInfo.CurrentUICulture = new CultureInfo(culture);
            await Client.SendTextMessageAsync(
                "Привіт✋\n\n" +
                "😋 Це бот, який допоможе тобі організовувати своє навчання легче та швидше.\n\n" +
                "Щоб взнати, що цей бот може ↪ /help");
        }

        [TargetCommand]
        public async Task Help()
        {
            await Client.SendTextMessageAsync("Це команда help");
        }
    }
}
