#pragma checksum "D:\dotnet\TazeProject\WEB\Views\Article\Read.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b075496060f4cd88c3b5855f35dd814cc1ef2ca7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Article_Read), @"mvc.1.0.view", @"/Views/Article/Read.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 2 "D:\dotnet\TazeProject\WEB\Views\_ViewImports.cshtml"
using DTO.UserDtos;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\dotnet\TazeProject\WEB\Views\_ViewImports.cshtml"
using CORE.Prevail;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\dotnet\TazeProject\WEB\Views\_ViewImports.cshtml"
using Domain;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b075496060f4cd88c3b5855f35dd814cc1ef2ca7", @"/Views/Article/Read.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2feb9c6073f73be7c29f63ad333663b0204dd9e1", @"/Views/_ViewImports.cshtml")]
    public class Views_Article_Read : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Article>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\dotnet\TazeProject\WEB\Views\Article\Read.cshtml"
  
    ViewData["Title"] = "Read";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

<div class=""container"">
    <h2 class=""text-center text-dark text-capitalize w-100"">
        Yazılar
    </h2>
    <div class=""row"">
        <div class=""col-lg-6 col-md-8 col-sm-12 col-12 offset-lg-3 col-md-2"">
            <div class=""card h-100"">
                <div class=""p-4 text-center w-100"">
                    <img");
            BeginWriteAttribute("src", " src=\"", 394, "\"", 414, 1);
#nullable restore
#line 16 "D:\dotnet\TazeProject\WEB\Views\Article\Read.cshtml"
WriteAttributeValue("", 400, Model.ImgPath, 400, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"img-fluid img-thumbnail\" style=\"height:300px\" alt=\"article image\">\r\n                </div>\r\n                <div class=\"card-body pb-5\">\r\n                    <h3 class=\"card-title text-center text-uppercase mb-3\"><b>");
#nullable restore
#line 19 "D:\dotnet\TazeProject\WEB\Views\Article\Read.cshtml"
                                                                         Write(Model.Caption);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b></h3>\r\n                    <p class=\"card-text\"><u>");
#nullable restore
#line 20 "D:\dotnet\TazeProject\WEB\Views\Article\Read.cshtml"
                                       Write(Model.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</u></p>\r\n                    <hr />\r\n                    <div>\r\n                        ");
#nullable restore
#line 23 "D:\dotnet\TazeProject\WEB\Views\Article\Read.cshtml"
                   Write(Html.Raw(Model.Content));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                    </div>
                    <div class=""d-block text-end"">
                        <a class=""btn btn-secondary"" href=""/Article/Index"">
                            Geri
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>


");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Article> Html { get; private set; }
    }
}
#pragma warning restore 1591