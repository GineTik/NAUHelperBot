using NauHelper.Core.Interfaces.Services;
using Telegram.Bot;
using Telegramper.Core.AdvancedBotClient.Extensions;
using Telegramper.Executors.Common.Models;
using Telegramper.Executors.QueryHandlers.Attributes.Targets;

namespace UserInterfaces.Moderator.Executors
{
    public class ApplyCreatingGroupExecutor : Executor
    {
        private readonly IStudentGroupCoordinationService _studentGroupCoordinationService;
        private readonly IRoleService _roleService;

        public ApplyCreatingGroupExecutor(IStudentGroupCoordinationService studentGroupCoordinationService, IRoleService roleService)
        {
            _studentGroupCoordinationService = studentGroupCoordinationService;
            _roleService = roleService;
        }
    }
}
