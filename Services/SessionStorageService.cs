using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Blazored.SessionStorage;

namespace OnlineCoursesUI.Services
{
    public static class SessionStorageService
    {
        public static async Task SaveStorage<T>(
            this ISessionStorageService sessionStorageService,
            string key, T item
        ) where T : class
        {
            var ItemJson = JsonSerializer.Serialize(item);
            await sessionStorageService.SetItemAsStringAsync(key,ItemJson); 
        }
        #nullable enable
        public static async Task<T?> GetStorage<T>(
            this ISessionStorageService sessionStorageService,
            string key
        ) where T : class
        {
            var ItemJson = await sessionStorageService.GetItemAsStringAsync(key); 
            if (ItemJson != null)
            {
                var Item = JsonSerializer.Deserialize<T>(ItemJson);
                return Item;
            } else
            {
                return null;
            }
        }
    }
}