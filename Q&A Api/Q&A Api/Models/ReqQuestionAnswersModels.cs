using Q_A_Api.Const;

namespace Q_A_Api.Models
{
    public class ReqQuestionAnswersModels
    {
        public int Mode { get; set; } = QAPlatformConst.Mode.QUESTION;
        public int? QuestionId { get; set; }
        public int Customer_Id { get; set; }
        public DateTime? Created_Date { get; set; }
        public DateTime? Update_Date { get; set; }
        public string? Tag { get; set; }

        // case create question
        public string? Question { get; set; }

        // case answer
        public string? Answer { get; set; }

        // case reply and edit
        public int? Ref_QuestionId { get; set; }
    }
}
