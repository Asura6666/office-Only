using System;
using System.Collections.Generic;
using System.Text;

namespace webapi.Services
{
    public interface IBaseService<TEntity>
       where TEntity : class
    {
    }
}
