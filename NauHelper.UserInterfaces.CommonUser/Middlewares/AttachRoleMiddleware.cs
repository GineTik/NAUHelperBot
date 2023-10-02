using NauHelper.Core.Interfaces.Repositories;
using System.Runtime.InteropServices;
using Telegramper.Core.Configuration.Middlewares;
using Telegramper.Core.Context;
using Telegramper.Core.Delegates;
using Telegramper.Executors.QueryHandlers.UserState;

namespace UserInterfaces.CommonUser.Middlewares
{
    public class AttachRoleMiddleware : IMiddleware
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserStates _userStates;

        public AttachRoleMiddleware(IRoleRepository roleRepository, IUserStates userStates)
        {
            _roleRepository = roleRepository;
            _userStates = userStates;
        }

        public async Task InvokeAsync(UpdateContext updateContext, NextDelegate next)
        {
            if (updateContext.TelegramUserId == null)
            {
                await next();
                return;
            }

            var roles = await _roleRepository.GetUserRolesAsync(updateContext.TelegramUserId!.Value);
            var states = await _userStates.GetAsync();

            foreach (var role in roles)
            {
                var roleState = "Role:" + role.Name;

                if (states.Contains(roleState) == false)
                {
                    await _userStates.AddAsync(roleState);
                }
            }

            await next();
        }
    }
}
