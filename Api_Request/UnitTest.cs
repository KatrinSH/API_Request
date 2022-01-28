using Xunit;
using RestSharp;

namespace Api_Request;

public class UnitTest
{
    [Fact]
    public void TestAuthorisation()
    {
        Authorisation authorisation = new Authorisation();
        authorisation.GetToken();
    }

    [Fact]
    public void TestUpdateProfile()
    {
        Authorisation authorisation = new Authorisation();
        string token = authorisation.GetToken();
        var headerUpdateProfile = new Dictionary<string, string>
        {
            {"authorization", token},
        };
        var bodyUpdateProfile = new Dictionary<string, string>
        {
            {"company_name", "Ivanko"},
        };

        ApiHelper.SendApiRequest(bodyUpdateProfile, headerUpdateProfile,
            "https://api.newbookmodels.com/api/v1/client/profile/", Method.PATCH);
    }
    
    [Fact]
    public void TestUploadImage()
    {
        Authorisation authorisation = new Authorisation();
        string token = authorisation.GetToken();
        var headerUpload = new Dictionary<string, string>
        {
            {"content-type","multipart/form-data"},
            {"authorization", token}
        };

        ApiHelper.SendApiRequest(null, headerUpload,
            "https://api.newbookmodels.com/api/images/upload/", Method.POST, "/Users/ekaterinasugaj/Downloads/picture.png");
    }

    [Fact]
    public void TestDownloadImage()
    {
        //Download images
        var responseDownlaodImages =
            ApiHelper.DownloadFile(
                "https://storage.googleapis.com/ford-modeling-media/small/0ac6aeda-3a62-4d43-ac54-938534b674d2",
                "picture1.png");
    }
}