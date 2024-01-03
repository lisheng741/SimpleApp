using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Common.EFCore;

namespace Simple.Services.Providers
{
    public class EntityDataProvider : DefaultEntityDataProvider
    {
        private readonly ICurrentUserService _currentUser;

        public override Guid? UserId => _currentUser.UserId;

        public override Guid? TenantId => _currentUser.TenantId;

        public EntityDataProvider(ICurrentUserService currentUser)
        {
            _currentUser = currentUser;
        }
    }
}
