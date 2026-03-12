using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos;
using WebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        //先在全域宣告資料庫物件
        private readonly WebContext _webContext;

        // 透過建構子，取得依賴注入的物件
        public NewsController(WebContext webContext)
        {
            _webContext = webContext;
        }

        // get News的清單
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<NewsDto> Get()
        {
            var result = from a in _webContext.News
                                 select new NewsDto
                                 {
                                     NewsId = a.NewsId,
                                     Title = a.Title,
                                     Content = a.Content,
                                     StartDateTime = a.StartDateTime,
                                     EndDateTime = a.EndDateTime,
                                     Click = a.Click
                                 };
            return result;
        }

        // get 特定的News 
        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public ActionResult<NewsDto> Get(Guid id)
        {
            var result = ( from a in _webContext.News
                                    where a.NewsId == id
                                    select new NewsDto
                                    {
                                        NewsId = a.NewsId,
                                        Title = a.Title,
                                        Content = a.Content,
                                        StartDateTime = a.StartDateTime,
                                        EndDateTime = a.EndDateTime,
                                        Click = a.Click
                                    }).SingleOrDefault();  // 預期結果會唯一。若找不到則回傳 null，多於一筆則拋出異常。

            if (result == null)
            {
                return NotFound(); // 如果找不到，回傳 404
            }

            return result; // 找到就回傳資料
        }


        // get的關鍵字搜尋 (針對Title、Content和StartDateTime做過濾)
        [HttpGet("GetKeyWord")]
        public ActionResult<IEnumerable<NewsDto>> GetKeyWord(String? Title, String? Content, DateTime? StartDateTime) // ?表示: 這個參數不一定會有值過來，如果沒有值傳過來就會是null。
        {

            // 先取得所有 (SQL還未送出去)
            var result = from a in _webContext.News
                         select new NewsDto
                         {
                             Title = a.Title,
                             Content = a.Content,
                             NewsId = a.NewsId,
                             StartDateTime = a.StartDateTime,
                             EndDateTime = a.EndDateTime,
                             Click = a.Click
                         };

            // 再過濾 (SQL還未送出去)
            // 1. 過濾title
            if (!string.IsNullOrWhiteSpace(Title))   // 非null或空，才往下執行
            {
                result = result.Where(a => a.Title.Contains(Title));
            }
            // 2. 過濾content
            if (!string.IsNullOrWhiteSpace(Content))   // 非null或空，才往下執行
            {
                result = result.Where(a => a.Content.Contains(Content));
            }
            // 3. 過濾StartDateTime
            if (StartDateTime != null) 
            {
                result = result.Where(a => a.StartDateTime.Date == ((DateTime)StartDateTime).Date);
            }

            // 執行查詢並回傳 (ToList() => 送出SQL)
            var finalData = result.ToList();

            if (finalData.Count == 0)
            {
                return NotFound("找不到符合條件的新聞");
            }

            return finalData;
        }



        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
