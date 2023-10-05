using Microsoft.AspNetCore.Mvc;
using PriceTagPrinter.Config;

namespace PriceTagPrinter.Controllers;

[ApiController]
[Route("upload")]
public class UploadController : ControllerBase
{
  private readonly BackendUrlConfig urlConfig;

  public UploadController(BackendUrlConfig urlConfig)
  {
    this.urlConfig = urlConfig;
  }

  [HttpPost]
  public async Task<IActionResult> SubmitForm()
  {
    try
    {
      // Todo: Do all of this somewhere else
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
      
      string path = "Data/Databases/Goods/goods.db";
      Console.WriteLine($"Copying file to path: {path}");
      using (Stream stream = new FileStream(path, FileMode.Create))
      {
        await files[0].CopyToAsync(stream);
      }
      Console.WriteLine("Finished copying file.");
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