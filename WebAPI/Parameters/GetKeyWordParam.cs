using System.Text.RegularExpressions;

namespace WebAPI.Parameters
{
    public class GetKeyWordParam
    {
        // ?表示: 這個參數不一定會有值過來，如果沒有值傳過來就會是null。
        public String? Title { get; set; }
        public String? Content { get; set; }
        public DateTime? StartDateTime { get; set; }

        private int? _click;
        public int? Click 
        {
            get { return _click; }
            set {

                //檢查是否 >= 0
                if (value >= 0)
                {
                    _click = value;
                }
                else
                {
                    // 如果不符合條件，拋出異常
                    throw new ArgumentException("點擊數不能為負數");
                }

            } 
        }
    }
}
