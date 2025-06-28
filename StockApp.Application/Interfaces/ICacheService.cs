using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.Interfaces
{
    public interface ICacheService
    {
        Task SetAsync<T>(string Key, T value, TimeSpan? absoluteExpiration = null);
        Task<T?> GetAsync<T>(string key);
        Task RemoveAsync(string key);
    }
}
