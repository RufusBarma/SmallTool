using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SmallTool.Extensions
{
    public static class SystemExtension
    {
        public static string GetString(this TagBuilder builder)
        {
            using var writer = new StringWriter();
            builder.WriteTo(writer, HtmlEncoder.Default);
            return writer.ToString();
        }
    }
}