using Telegram.Bot;
using Telegramper.Core.AdvancedBotClient.Extensions;
using Telegramper.Executors.Common.Models;
using Telegramper.Executors.QueryHandlers.Attributes.Targets;

namespace UserInterfaces.Administrator.Executors
{
    public class BaseExecutor : Executor
    {
        [TargetCommand(UserStates = "Role:Administrator")]
        public async Task Help()
        {
            await Client.SendTextMessageAsync("😁Додаткові налаштування для адміністратора\n\n" +
                "/add_faculty - додати факультет\n" +
                "/remove_faculty - видалити факультет\n" +
                "/add_specialty - додати спеціальність\n" +
                "/remove_specialty - видалити спеціальність\n" +
                "/add_group - додати групу\n" +
                "/remove_group - видалити групу\n");
        }
    }
}
