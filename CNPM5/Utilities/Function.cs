using System.Security.Cryptography;
using System.Text;

namespace CNPM5.Utilities
{
    public class Function
    {
        public static int _UserID = 0;
        public static string _UserName = string.Empty;
        public static string _Email = string.Empty;
        public static string _Message = string.Empty;
        public static string _MessageEmail = string.Empty;
        public static string TitleSlugGenerationAlias(string title)
        {
            return SlugGenerator.SlugGenerator.GenerateSlug(title);
        }
        public static bool IsLogin()
        {
            if (string.IsNullOrEmpty(Function._UserName) || Function._UserID <= 0)
            {
                return false;
            }
            return true;
        }
    }
}
