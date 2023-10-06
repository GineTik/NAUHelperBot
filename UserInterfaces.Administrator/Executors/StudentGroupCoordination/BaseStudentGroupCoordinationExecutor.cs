using NauHelper.Core.Entities;
using NauHelper.Core.Interfaces.Services;
using Telegram.Bot;
using Telegramper.Core.AdvancedBotClient.Extensions;
using Telegramper.Core.Context;
using Telegramper.Core.Helpers.Builders;
using Telegramper.Dialog.Service;
using Telegramper.Executors.Common.Models;
using Telegramper.Sessions.Interfaces;

namespace UserInterfaces.Administrator.Executors.StudentGroupCoordination
{
    public class BaseStudentGroupCoordinationExecutor : Executor
    {
        protected Task<IEnumerable<Group>> Groups => _groupCoordinationService.GetAllGroupsAsync();
        protected Task<IEnumerable<Specialty>> Specialties => _groupCoordinationService.GetAllSpecialtiesAsync();
        protected Task<IEnumerable<Faculty>> Faculties => _groupCoordinationService.GetAllFacultiesAsync();

        protected readonly IStudentGroupCoordinationService _groupCoordinationService;
        protected readonly IUserSession _userSession;
        protected readonly IDialogService _dialogService;

        public BaseStudentGroupCoordinationExecutor(IStudentGroupCoordinationService groupCoordinationService, IUserSession userSession, IDialogService dialogService)
        {
            _groupCoordinationService = groupCoordinationService;
            _userSession = userSession;
            _dialogService = dialogService;
        }

        protected async Task SelectItem<T>(IEnumerable<T> items, Func<T, string> name, 
            Func<T, string> argsCallbackDatas, string callbackRemoveMethodName)
        {
            await Client.SendTextMessageAsync(
                "Зробіть вибір",
                replyMarkup: new InlineKeyboardBuilder()
                    .CallbackButtonList(
                        items,
                        (item, _) => name(item),
                        (item, _) => $"{callbackRemoveMethodName} {argsCallbackDatas(item)}")
                    .Build()
            );
        }

        protected async Task Remove(Func<long, int, Task> removeMethod, int id, string name)
        {
            await removeMethod.Invoke(UpdateContext.TelegramUserId!.Value, id);
            await Client.AnswerCallbackQueryAsync($"Факультет {name} видалено");
            await Client.DeleteMessageAsync();
        }
    }
}
