using Microsoft.AspNetCore.Mvc;
using PriceTagPrinter.Config;
using PriceTagPrinter.Services;

namespace PriceTagPrinter.Controllers;

[ApiController]
[Route("upload")]
public class UploadController : ControllerBase
{
  private readonly BackendUrlConfig urlConfig;
  private readonly IUploadService uploadService;

  public UploadController(BackendUrlConfig urlConfig, IUploadService uploadService)
  {
    this.urlConfig = urlConfig;
    this.uploadService = uploadService;
  }

  [HttpPost]
  public async Task<IActionResult> SubmitForm()
  {
    try
    {      
      IFormFileCollection files = HttpContext.Request.Form.Files;
      if (files.Count < 1)
      {
        Console.WriteLine("Found no files!");
        return Redirect(urlConfig.Url);
      }
      if (files.Count > 1)
      {
        Console.WriteLine("Found too many files!");
        return Redirect(urlConfig.Url);
      }

      await uploadService.OverwriteDatabase(files[0]);
    }
    catch (Exception ex)
    {
      Console.WriteLine("Exception when trying to process form:");
      Console.WriteLine(ex.Message);
    }
    // The front end is too stupid to understand anything so just refresh the page regardless.
    // Todo: Redirect hte user to a "upload success" or "upload failed" page.
        return Redirect(urlConfig.Url);
  }
}

public class UploadRequest
{
  public FormFile? UploadedFile { get; set; }
}