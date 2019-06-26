using System.ComponentModel.DataAnnotations;

namespace MVC5Course.DataTypeAttributes
{
    public class NoAllowSpecialWordsAttribute : DataTypeAttribute
    {
        public string[] Words { get; set; }

        public NoAllowSpecialWordsAttribute() : base(DataType.Text)
        {
            Words = new string[]
            {
                "Git",
                "Mvc"
            };

            ErrorMessage = "不可以輸入特定文字";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            string str = (string)value;
            foreach (var item in Words)
            {
                if (str.Contains(item))
                    return false;
            }

            return true;
        }
    }
}