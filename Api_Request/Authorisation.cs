namespace Api_Request;
using RestSharp;
using Newtonsoft.Json;

public class Authorisation
{
    public string GetToken()
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
        var responseLogin = ApiHelper.SendApiRequest(body, headers, "https://api.newbookmodels.com/api/v1/auth/signin/",
            Method.POST);
        var result = JsonConvert.DeserializeObject<dynamic>(responseLogin); //преоброзуем строчку в JSON
        
        string token = result.token_data.token; //получаем токен

        return token;
    }
}