using System.Collections.Generic;
using System.Threading.Tasks;
using Nextech.news.Core.Model;

namespace nextech.news.Services.Interfaces;

public interface IStoryService
{
    public Item Get(int id);
    public List<Item> GetNewestList(int page, int pageSize);

    public List<Item> GetItemsBySearch(string query);

}