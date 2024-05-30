using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nextech.news.Core.Model;
using nextech.news.Core.Utils;
using nextech.news.Services.Interfaces;
using Newtonsoft.Json;
using System.Reflection.Metadata;
using System.Runtime.Caching;
using System.Text.RegularExpressions;

namespace nextech.news.Core.Services;

public class StoryService: IStoryService
{
    private readonly RestClientUtil _restClient;
    private readonly MemoryCacheUtil _memoryCache;

    public StoryService(RestClientUtil restClient, MemoryCacheUtil memoryCache)
    {
        _restClient = restClient;
        _memoryCache = memoryCache;
    }


    public  Item Get(int id)
    {
        
        Item item = new Item();
        var cacheItem = _memoryCache.Get($"stories_{id}");
        if (cacheItem != null)
        {
            return JsonConvert.DeserializeObject<Item>(cacheItem!);
        }

        string resource = $"/item/{id}.json";
        try
        {
            item =  this._restClient.GetResourceAsync<Item>(resource).Result;
            if (item != null)
            {
                 this._memoryCache.Set($"stories_{id}",JsonConvert.SerializeObject(item),TimeSpan.FromHours(24));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while fetching item {id}: {ex.Message}");
            throw;
        }

        return item;

    }

    public  List<Item> GetNewestList(int page, int pageSize)
    {
        string resource = "/newstories.json";

        var response =  this._restClient.GetResourceAsync<List<int>>(resource).Result;
        var itemIds = response.Skip((page - 1) * pageSize).Take(pageSize);

        var itemListTasks = itemIds.Select(id => Task.FromResult(this.Get(id)));

        var items =  Task.WhenAll(itemListTasks);
        var itemList = items.Result.ToList();

        return itemList;
    }

    public List<Item> GetItemsBySearch(string query)
    {
        var keysList = this._memoryCache.GetAllKeys();
        var itemList = new List<Item>();

        foreach (var keyItem in keysList)
        {
            var cacheItem = _memoryCache.Get(keyItem);

            if (cacheItem == null) continue;

            var item = JsonConvert.DeserializeObject<Item>(cacheItem);
            string pattern = $@"\b{Regex.Escape(query.ToLower())}\b";
            
            if (item.Title != null && Regex.IsMatch(item.Title.ToLower(),pattern))
            {
                itemList.Add(JsonConvert.DeserializeObject<Item>(cacheItem));
            }
        }

        return itemList;
    }


}