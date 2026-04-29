using AffairList.Constants;
using System.Text;

namespace AffairList.Services.Models
{
    public class Affair
    {
        public bool IsPrioritized { get; set; } = false;
        public bool HasDeadline { get; set; } = false;
        public DateOnly? Deadline { get; set; } = null;
        public string InnerText { get; set; }

        public Affair(string innerText)
        {
            InnerText = innerText;
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            if (Deadline != null)
            {
                sb.Append(Deadline);
                sb.Append(" ");
            }
            sb.Append(InnerText);
            if (IsPrioritized)
            {
                sb.Append(" ");
                sb.Append(AffairConstants.PriorityWord);
            }
            return sb.ToString();
        }
    }
}
