using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
//import the IConfiguration interface from the Microsoft.Extensions.Configuration namespace, which provides access to configuration settings in the application


namespace sp1.Services
{
    public class S3Service
    {
        private readonly AmazonS3Client _s3Client;
        private readonly string _bucketName;

        private readonly string _region;
        public S3Service(IConfiguration configuration)
        {
            var accessKey= configuration["AWS:AccessKey"];
            var secretKey= configuration["AWS:SecretKey"];
            _region= configuration["AWS:Region"]?? throw new Exception("region missing");

            _bucketName= configuration["AWS:BucketName"]?? throw new Exception("Bucket name is not configured in appsettings.json");

            var regionEndpoint= RegionEndpoint.GetBySystemName(_region);
            //region is only a plain text the pc knows it is just a text but the pc does not know what it is so we need to convert it into a region endpoint object using the GetBySystemName method of the RegionEndpoint class to get the info about the region

            _s3Client= new AmazonS3Client(accessKey, secretKey, regionEndpoint);
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            var fileName= $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            var request= new PutObjectRequest
            {
                BucketName= _bucketName,
                Key= fileName,
                InputStream= file.OpenReadStream(),
                //openreadstream is used to read the contents of the uploaded file as a stream, which can then be sent to S3 for storage
                ContentType= file.ContentType,
                //ContentType — sets the S3 object's Content-Type metadata, so when someone later fetches the file via URL, the browser/client knows how to handle it (e.g. image/png, application/pdf).
            };

            await _s3Client.PutObjectAsync(request);

            return $"https://{_bucketName}.s3.{_region}.amazonaws.com/{fileName}";
        }
    }
}