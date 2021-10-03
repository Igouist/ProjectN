namespace ProjectN.Parameter
{
    /// <summary>
    /// 卡片搜尋參數
    /// </summary>
    public class CardSearchParameter
    {
        /// <summary>
        /// 卡片名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 攻擊力下限
        /// </summary>
        public int? MinAttack { get; set; }

        /// <summary>
        /// 攻擊力上限
        /// </summary>
        public int? MaxAttack { get; set; }

        /// <summary>
        /// 血量下限
        /// </summary>
        public int? MinHealth { get; set; }

        /// <summary>
        /// 血量上限
        /// </summary>
        public int? MaxHealth { get; set; }

        /// <summary>
        /// 花費值下限
        /// </summary>
        public int? MinCost { get; set; }

        /// <summary>
        /// 花費值上限
        /// </summary>
        public int? MaxCost { get; set; }
    }
}
