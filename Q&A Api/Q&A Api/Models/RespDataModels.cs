namespace Q_A_Api.Core
{
    public class RespDataModels
    {
        public int QuestionId { get; set; }
        public int Ref_QuestionId { get; set; }
        public int Type { get; set; }
        public int CustomerId { get; set; }
        public string Tx_Value { get; set; }
        public DateTime Create_Dt { get; set; }
        public DateTime Update_Dt { get; set; }
        public int Point { get; set; }
        public string Tag { get; set; }
    }
}
