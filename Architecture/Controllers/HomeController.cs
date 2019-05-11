using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Architecture.Models;
using Architecture.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Architecture.Controllers
{
    public class HomeController : Controller
    {
        private Context _context;
        private readonly Settings _settings;

        public HomeController(Context context, IOptions<Settings> settings)
        {
            _context = context;
            _settings = settings.Value;
        }
        
        public async Task<IActionResult> Index()
        {
            //Prepare azureStorage connectionstring
            var cloudStorage = Microsoft.WindowsAzure.Storage.CloudStorageAccount.Parse(_settings.Blob.ConnectionString);
            //Get client
            var cloudBlobClient = cloudStorage.CreateCloudBlobClient();
            //Get container
            var cloudBlobContainer = cloudBlobClient.GetContainerReference("first");
            //Create container if not exist
            var result = cloudBlobContainer.CreateIfNotExistsAsync().Result;

            //Create and Upload
            //var bFileInfo = cloudBlobContainer.GetBlockBlobReference("sampleText2.json");
            //await bFileInfo.UploadTextAsync("{\"data\":\"測試資料\"}");

            //Get Folder and Upload
            //CloudBlobDirectory cloudBlobDirectory = cloudBlobContainer.GetDirectoryReference("A/A_1");
            //var bFileInfo = cloudBlobDirectory.GetBlockBlobReference("sampleText3.json");
            //await bFileInfo.UploadTextAsync("{\"data\":\"測試資料\"}");

            //Get Folder and Delete
            //CloudBlobDirectory cloudBlobDirectory = cloudBlobContainer.GetDirectoryReference("A/A_1");
            //await cloudBlobDirectory.GetBlockBlobReference("sampleText3.json").DeleteIfExistsAsync();

            //Get Folder and Download
            //var text = await cloudBlobContainer.GetBlockBlobReference("sampleText.json").DownloadTextAsync();

            // 設定可以開啟120秒，並且此簽章只能夠讀取，最後取得下載網址
            //var policy = new SharedAccessBlobPolicy()
            //{
            //    SharedAccessExpiryTime = DateTime.UtcNow.AddSeconds(10),
            //    Permissions = SharedAccessBlobPermissions.Read,
            //};
            //var sasContainerToken = cloudBlobContainer.GetSharedAccessSignature(policy, null);
            //var uri = cloudBlobContainer.GetBlockBlobReference("sampleText.json").Uri;
            //return Content($"{uri}{sasContainerToken}");

            //Get Folder and upload file
            //CloudBlobDirectory cloudBlobDirectory = cloudBlobContainer.GetDirectoryReference("IMAGES");
            //await cloudBlobDirectory.GetBlockBlobReference("4.jpg").UploadFromFileAsync("wwwroot/image/4.jpg");

            //Get File and Get Attributes
            //var fileReference = cloudBlobContainer.GetDirectoryReference("IMAGES").GetBlockBlobReference("4.jpg");
            //await fileReference.FetchAttributesAsync();
            //var attrs = $"{fileReference.Properties.Length},{fileReference.Properties.ContentType},{fileReference.Properties.Created.Value.ToString("yyyy-MM-dd hh:mm:ss")}";
            //return Content(attrs);

            //Get file list
            var res = new List<string>();
            //這種方法在 Azure Storge 包括 table 很常使用
            BlobContinuationToken continuationToken = null;
            do
            {
                var listingResult = cloudBlobContainer.ListBlobsSegmentedAsync(continuationToken).Result;
                continuationToken = listingResult.ContinuationToken;
                //files
                //res.AddRange(listingResult.Results.Select(x => System.IO.Path.GetFileName(x.Uri.AbsolutePath)).ToList());
                //folders
                res.AddRange(listingResult.Results.Where(x => x as CloudBlobDirectory != null).Select(x => x.Uri.Segments.Last()).ToList());
            }
            while (continuationToken != null);
            return Content($"{string.Join(", ", res)}");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
