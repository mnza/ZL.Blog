using System.Text;
namespace System
{
    public static class StringExtension
    {
        public static string Base64Encode(this string str)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
        }
    }
}
