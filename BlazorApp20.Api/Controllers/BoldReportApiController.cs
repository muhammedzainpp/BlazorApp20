using BoldReports.Web.ReportViewer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BlazorApp20.Api.Controllers;

[Route("api/{controller}/{action}/{id?}")]
[ApiController]

public class BoldReportApiController : ControllerBase, IReportController
{
    private readonly IMemoryCache _cache;
    private readonly IWebHostEnvironment _hostingEnvironment;
    public BoldReportApiController(IMemoryCache cache, IWebHostEnvironment hostingEnvironment)
    {
        _cache = cache;
        _hostingEnvironment = hostingEnvironment;

    

    [ActionName("GetResource")]
    [AcceptVerbs("GET")]
    public object GetResource(ReportResource resource)
    {
        return ReportHelper.GetResource(resource, this, _cache);
    }

    [NonAction]
    public void OnInitReportOptions(ReportViewerOptions reportOption)
    {
        string basePath = _hostingEnvironment.ContentRootPath;
        System.IO.FileStream inputStream = new System.IO.FileStream(basePath + @"BlazorApp20.Shared\Resources\" + "sales-order-detail" + ".rdl", System.IO.FileMode.Open, System.IO.FileAccess.Read);
        MemoryStream reportStream = new MemoryStream();
        inputStream.CopyTo(reportStream); 
        reportStream.Position = 0;
        inputStream.Close();
        reportOption.ReportModel.Stream = reportStream;
    }

    [NonAction]
    public void OnReportLoaded(ReportViewerOptions reportOption)
    {

    }

    [HttpPost]
    public object PostFormReportAction()
    {
        return ReportHelper.ProcessReport(null, this, _cache);
    }

    [HttpPost]
    public object PostReportAction([FromBody] Dictionary<string, object> jsonArray)
    {
        return ReportHelper.ProcessReport(jsonArray, this, this._cache);
    }
}
