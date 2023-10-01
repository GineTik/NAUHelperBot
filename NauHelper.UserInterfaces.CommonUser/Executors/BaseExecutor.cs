using Microsoft.Extensions.Configuration;
using NauHelper.Core.Localization;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Telegram.Bot;
using Telegramper.Core.AdvancedBotClient.Extensions;
using Telegramper.Executors.Common.Models;
using Telegramper.Executors.QueryHandlers.Attributes.Targets;

namespace UserInterfaces.CommonUser.Executors
{
    public class BaseExecutor : Executor
    {
        private readonly ILocalizer _localizer;

        public BaseExecutor(ILocalizer localizer)
        {
            _localizer = localizer;
        }

        [TargetCommand]
        public async Task Start()
        {
            await Client.SendTextMessageAsync(_localizer.Get("Start"));

            //await Client.SendTextMessageAsync(
            //    "Привіт✋\n\n" +
            //    "😋 Це бот, який допоможе тобі організовувати своє навчання легче та швидше.\n\n" +
            //    "Щоб взнати, що цей бот може ↪ /help");
        }

        [TargetCommand]
        public async Task Help()
        {
            await Client.SendTextMessageAsync("Це команда help");
        }
    }
}
