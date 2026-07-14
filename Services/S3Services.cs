using Amazon;
using Amazon.S3;
using Microsoft.Extensions.Configuration;
//import the IConfiguration interface from the Microsoft.Extensions.Configuration namespace, which provides access to configuration settings in the application


namespace sp1.Services
{
    public class S3Service
    {
        private readonly AmazonS3Client _s3Client;
        private readonly string _bucketName;

        public S3Service(IConfiguration configuration)
        {
            var accessKey= configuration["AWS:AccessKey"];
            var secretKey= configuration["AWS:SecretKey"];
            var region= configuration["AWS:Region"];

            _bucketName= configuration["AWS:BucketName"]?? throw new Exception("Bucket name is not configured in appsettings.json");

            var regionEndpoint= RegionEndpoint.GetBySystemName(region);
            //region is only a plain text the pc knows it is just a text but the pc does not know what it is so we need to convert it into a region endpoint object using the GetBySystemName method of the RegionEndpoint class to get the info about the region

            _s3Client= new AmazonS3Client(accessKey, secretKey, regionEndpoint);
        }
    }
}