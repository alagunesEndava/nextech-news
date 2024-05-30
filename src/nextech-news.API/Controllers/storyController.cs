using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using Nextech.news.Core.Model;
using nextech.news.Services.Interfaces;
using RestSharp;

namespace nextech_news.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoryController: ControllerBase
    {
        private readonly IStoryService _storyService;

        public StoryController(IStoryService storyService)
        {
            this._storyService = storyService;
        }

        [HttpGet()]
        public List<Item> GetNewest(int page, int pageSize)
        {

            return _storyService.GetNewestList(page, pageSize);
        }


        // GET: storyController/Details/5
        [HttpGet("{id}")]
        public IResult Details(int id)
        {
            var item = _storyService.Get(id);
            return item == null ? Results.NotFound() : Results.Ok(item);

        }

        [HttpGet("/story/search")]
        public IResult Search(string query)
        {

            var item = _storyService.GetItemsBySearch(query);
            return item == null ? Results.NotFound() : Results.Ok(item);
        }





    }
}
