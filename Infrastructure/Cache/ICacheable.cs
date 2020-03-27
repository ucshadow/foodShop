using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStore.Infrastructure.Cache
{
    public interface ICacheable
    {
        ICacheable Cache();
        object GetUniqueIdentifier<T>();
    }
}
