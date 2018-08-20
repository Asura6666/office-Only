using System;
using System.Collections.Generic;
using System.Text;

namespace webapi.Services.Impl
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
    }
}
