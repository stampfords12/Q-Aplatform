namespace Q_A_Api.Models
{
    public static class QuestionAnswersModels
    {
        public static int QuestionId { get; set; }
        public static int Ref_QuestionId { get; set; }
        public static int Type { get; set; }
        public static int CustomerId { get; set; }
        public static string Tx_Value { get; set; }
        public static DateTime Create_Dt { get; set; }
        public static DateTime Update_Dt { get; set; }
        public static int Point { get; set; }
        public static string Tag { get; set; }
    }
}
