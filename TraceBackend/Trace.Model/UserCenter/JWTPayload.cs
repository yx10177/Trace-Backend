using System;
using System.Collections.Generic;
using System.Text;

namespace Trace.Model
{
    public class JWTPayload
    {
        /// <summary>
        /// User Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 使用者姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 建立時間
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 到期時間
        /// </summary>
        public DateTime Expiration { get; set; }
    }
}
