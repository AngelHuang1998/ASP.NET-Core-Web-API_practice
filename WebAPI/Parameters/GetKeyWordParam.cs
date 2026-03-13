namespace WebAPI.Parameters
{
    public class GetKeyWordParam
    {
        // ?表示: 這個參數不一定會有值過來，如果沒有值傳過來就會是null。
        public String? Title { get; set; }
        public String? Content { get; set; }
        public DateTime? StartDateTime { get; set; }
    }
}
