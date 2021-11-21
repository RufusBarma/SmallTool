using System;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using LanguageExt;
using LanguageExt.SomeHelp;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmallTool.Models;

namespace SmallTool.Models
{
    public record BranchTuple(Option<Branch> LocalBuild, Option<Branch> RemoteBuild);

    public enum BranchStatus
    {
        Manual,
        Actual,
        Outdated,
        Deleted,
        New
    }
    
    public static class BranchTupleExtension
    {
        public static string GetString(this TagBuilder builder)
        {
            using var writer = new StringWriter();
            builder.WriteTo(writer, HtmlEncoder.Default);
            return writer.ToString();
        }
        
        public static HtmlString CreateStatus(this IHtmlHelper html, BranchStatus status)
        {
            Option<string> cssClass = status switch
            {
                BranchStatus.Manual => ".manual-status",
                BranchStatus.Actual => ".actual-status",
                BranchStatus.Outdated => ".outdated-status",
                BranchStatus.Deleted => ".deleted-status",
                BranchStatus.New => ".new-status",
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
            var span = new TagBuilder("span");
            span.InnerHtml.Append(status.GetString());
            cssClass.IfSome(value => span.Attributes.Add("class", value));
            return new HtmlString(span.GetString());
        }
        
        public static string GetString(this BranchStatus status) => status switch
            {
                BranchStatus.Manual => "ручная сборка",
                BranchStatus.Actual => "последняя версия",
                BranchStatus.Outdated => "устаревшая версия",
                BranchStatus.Deleted => "удалена на сервере",
                BranchStatus.New => "есть обновления",
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
        
        public static Build GetFresh(this Branch branch) => branch.Builds.OrderBy(b => b.CreatedTime).First();
        
        public static BranchStatus GetStatus(this BranchTuple tuple) =>
            tuple.LocalBuild.Match(
                local => tuple.RemoteBuild.Match(remote =>
                    {
                        var localBuild = local.GetFresh();
                        var remoteBuild= remote.GetFresh();
                        return localBuild.CreatedTime < remoteBuild.CreatedTime 
                            ? BranchStatus.Outdated 
                            : BranchStatus.Actual;
                    },
                    BranchStatus.Deleted),
                ()=> tuple.RemoteBuild.Match(_ => BranchStatus.New, BranchStatus.Deleted));

        public static Option<string> GetName(this BranchTuple tuple) => 
            tuple.LocalBuild.Match(
                branch => Option<string>.Some(branch.Name), 
                tuple.RemoteBuild.Match(
                        rb => Option<string>.Some(rb.Name), 
                        Option<string>.None));
    }
}