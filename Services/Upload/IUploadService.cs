namespace PriceTagPrinter.Services;

public interface IUploadService
{
  public Task OverwriteDatabase(IFormFile newDatabase);
}