using Microsoft.EntityFrameworkCore;
using NauHelper.Core.Entities;
using NauHelper.Infrastructure.Database.EF;
using Telegramper.Core.Configuration.Middlewares;
using Telegramper.Core.Context;
using Telegramper.Core.Delegates;

namespace UserInterfaces.CommonUser.Middlewares
{
    public class RegistrationUserMiddleware : IMiddleware
    {
        private readonly DataContext _dataContext;
        
        public RegistrationUserMiddleware(
            DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task InvokeAsync(UpdateContext updateContext, NextDelegate next)
        {
            var telegramUserId = updateContext.TelegramUserId;

            if (telegramUserId == null)
            {
                await next();
                return;
            }

            var isExists = await _dataContext.Users.AnyAsync(user => user.TelegramId == telegramUserId);

            if (isExists == false)
            {
                _dataContext.Users.Add(new User
                {
                    TelegramId = telegramUserId!.Value
                });

                await _dataContext.SaveChangesAsync();
            }

            await next();
        }
    }
}
