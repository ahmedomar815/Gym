using Gym.BusinessLogic.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Hosting;

namespace Gym.BusinessLogic.AttachmentRules;

public class AttachmentService(IHostEnvironment hostEnvironment) : IAttachmentService
{
    private readonly string _root=Path.Combine(hostEnvironment.ContentRootPath, "Attachments");
   

    public Task<Result> DeleteAsync(string storageKey, CancellationToken cancellationToken)
    {
      var fullPath = ToFullPath(storageKey);
        if(File.Exists(fullPath))
        {
            File.Delete(fullPath);
            return Task.FromResult(Result.Success());
        }
        else
        {
            return Task.FromResult(Result.Failure("File not found"));
        } 
    }

    public Task<Result<Stream>> GetAsync(string storageKey, CancellationToken cancellationToken)
    {
        var fullPath = ToFullPath(storageKey);
        if (!File.Exists(fullPath))
        {
            return Task.FromResult(Result.Failure<Stream>("File not found"));
        }
        var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
        return Task.FromResult(Result.Success<Stream>(stream));
    }

    public async Task<Result<string>> SaveAsync(IFormFile file, string category, CancellationToken cancellationToken)
    {
        var validation=await ValidateImageAsync(file, cancellationToken);

        if(validation.IsFailure) return Result.Failure<string>(validation.Error!, validation.ErrorCode);

        var ext = NormalizedExtension(file.FileName);
        var fileName = $"{Guid.NewGuid():N}{ext}";
        var dir=Path.Combine(_root, category);
        Directory.CreateDirectory(dir);
        await using var stream = new FileStream(Path.Combine(dir, fileName), FileMode.CreateNew);
        await file.CopyToAsync(stream, cancellationToken);
        return Result.Success(Path.Combine(category, fileName));
    }

    

    private string NormalizedExtension(string ext)
    {
        var normalizedText = string.Equals(".jpeg", ext, StringComparison.OrdinalIgnoreCase) ? ".jpg" : ext.ToLowerInvariant();
        return normalizedText;
    }

    private async Task<Result> ValidateImageAsync(IFormFile file, CancellationToken cancellationToken)
    {
        if(file!=null&&file.Length==0)return Result.Failure("no file uploaded");    

        if(file!.Length> AttachmentsRules.MaxBytes)return Result.Failure("File size exceeds the maximum allowed size of 5MB");

        try
        {
            await using var stream = file.OpenReadStream();
            var info = await SixLabors.ImageSharp.Image.IdentifyAsync(stream,cancellationToken);
            if(info==null) return Result.Failure("Invalid image file");

            if(info.Width>4000||info.Height > 4000) 
                return Result.Failure("Image dimensions exceed the maximum allowed size of 4000x4000 pixels");
            return Result.Success();
        }
        catch
        {
            return Result.Failure("An error occurred while validating the image");
        }
        
    }

    private string ToFullPath(string storageKey)
    {
        return Path.Combine(_root, storageKey.Replace('/',Path.DirectorySeparatorChar));
    }


}
