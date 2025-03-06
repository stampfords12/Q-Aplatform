namespace Q_A_Api.Models
{
    public class ReqFilterModels
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 10;
        public List<FilterModels>? Filter_Models { get; set; }
        public List<OrderModels>? Order_Models { get; set; }
    }

    public class FilterModels
    {
        public int Seq { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class OrderModels
    {
        public int Seq { get; set; }
        public string Key { get; set; }
        public string Sort { get; set; } = "desc";
    }
}
