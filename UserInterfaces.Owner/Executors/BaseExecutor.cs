using Telegram.Bot;
using Telegramper.Core.AdvancedBotClient.Extensions;
using Telegramper.Executors.Common.Models;
using Telegramper.Executors.QueryHandlers.Attributes.Targets;

namespace UserInterfaces.Owner.Executors
{
    public class BaseExecutor : Executor
    {
        [TargetCommand(UserStates = "Role:Owner")]
        public async Task Help()
        {
            await Client.SendTextMessageAsync("😁Додаткові налаштування для власника\n\n" +
                "/attach_role - видача ролі користовачу (потрібно знати id користувача)\n" +
                "/remove_role - видалення ролі користувача (потрібно знати id користувача)");
        }
    }
}
