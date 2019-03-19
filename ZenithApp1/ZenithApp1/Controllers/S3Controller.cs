using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZenithApp1.Models;

namespace ZenithApp1.Controllers
{
    public class S3Controller
    {
        private const string bucketName = "zenith-user-images";
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USEast1;
        public ZenithContext db = new ZenithContext();

        private static IAmazonS3 client;

        public bool UploadFiles(IEnumerable<HttpPostedFileBase> files, string ownerUsername)
        {
            try
            {
                client = new AmazonS3Client(bucketRegion);

                foreach (var file in files)
                {
                    string filePath = Guid.NewGuid() + Path.GetExtension(file.FileName);

                    var request = new PutObjectRequest()
                    {
                        BucketName = bucketName + "/" + ownerUsername,
                        CannedACL = S3CannedACL.Private,
                        Key = file.FileName,
                        InputStream = file.InputStream
                    };

                    client.PutObject(request);


                    Medium newMedium = new Medium();

                    newMedium.UserID = LoggedInUser.UserID;
                    newMedium.MediaName = Path.GetFileNameWithoutExtension(file.FileName);
                    newMedium.MediaPath = "https://s3.amazonaws.com/"+ bucketName + "/" + ownerUsername
                        + "/" + file.FileName;
                    newMedium.IsPublic = true;
                    newMedium.CreatedDate = DateTime.Now;
                    newMedium.UpdatedDate = DateTime.Now;

                    db.Media.Add(newMedium);
                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}