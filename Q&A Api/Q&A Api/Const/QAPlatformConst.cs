namespace Q_A_Api.Const
{
    public class QAPlatformConst
    {
        public static class Mode
        {
            public const int QUESTION = 1;
            public const int ANSWER = 2;
            public const int REPLY = 3;
            public const int EDIT = 4;

            public static List<int> list_Valid = new List<int>() { QUESTION, ANSWER, REPLY, EDIT };
        }
    }
}
