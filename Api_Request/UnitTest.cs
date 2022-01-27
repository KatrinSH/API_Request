using System.Net.Mime;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;
using RestSharp;
using System.Drawing;

namespace Api_Request;

public class UnitTest
{
    [Fact]
    public void Test()
    {
        var headers = new Dictionary<string, string>
        {
            {"Content-Type", " application/json"},
        
        };
        var body = new Dictionary<string, string>
        {
            {"email", "ivanebanko4@gmail.com"}, //ivanebanko4@gmail.com
            {"password", "Aa1234567890!"} //Aa1234567890!
        };
      
        //send Api login
        var responseLogin = ApiHelper.SendApiRequest(body, headers, "https://api.newbookmodels.com/api/v1/auth/signin/", Method.POST);
        // get cookie from API response (of login)
        // change cookie (API) for browser(to UI cookie)
        var result = JsonConvert.DeserializeObject<dynamic>(responseLogin);
        string token = result.token_data.token;

        
        var headersAutorization = new Dictionary<string, string>
        {
            {"authorization", token},
        };

        //Update Profile
        var bodyUpdateProfile = new Dictionary<string, string>
        {
            {"company_name", "Ivanko"}
        };
            
        var responseProfile = ApiHelper.SendApiRequest(bodyUpdateProfile, headersAutorization, "https://api.newbookmodels.com/api/v1/client/profile/", Method.PATCH);
        
        //Upload images
        // byte[] imageArray = System.IO.File.ReadAllBytes(@"/Users/ekaterinasugaj/Downloads/image.jpeg");
        // string base64ImageRepresentation = Convert.ToString(imageArray);

        var headerUpload = new Dictionary<string, string>
        {
            {"Content-Type", "multipart/form-data"},
            {"authorization", token},
        };
        
        var bodyUploadImage = new Dictionary<string, string>
        {};
        
        var responseUploadImages = ApiHelper.SendApiRequest(bodyUploadImage, headerUpload, "https://api.newbookmodels.com/api/images/upload/", Method.POST, "/Users/ekaterinasugaj/Downloads/image.jpeg");
        
        //Download images
        var responseDownlaodImages =
            ApiHelper.DownloadFile(
                "https://storage.googleapis.com/ford-modeling-media/small/0ac6aeda-3a62-4d43-ac54-938534b674d2",
                "picture1.jpg");
    }
}
