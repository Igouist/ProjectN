namespace ProjectN.Parameter
{
    /// <summary>
    /// 卡片參數
    /// </summary>
    public class CardParameter
    {
        /// <summary>
        /// 卡片名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 卡片描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 攻擊力
        /// </summary>
        public int Attack { get; set; }

        /// <summary>
        /// 血量
        /// </summary>
        public int Health { get; set; }

        /// <summary>
        /// 花費
        /// </summary>
        public int Cost { get; set; }
    }
}
