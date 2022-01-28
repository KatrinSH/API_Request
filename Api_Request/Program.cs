using RestSharp;
using RestSharp.Extensions;



public static class ApiHelper
{
    public static string SendApiRequest(object body, Dictionary<string, string> headers, string link, Method type, string filePath = null )
    {
        RestClient client = new RestClient(link)
        {
            Timeout = 30000
        };
        RestRequest request = new RestRequest(type); //запрос с типом
        foreach (var header in headers)
        {
            request.AddHeader(header.Key, header.Value);
        }

        request.AddJsonBody(body);
        request.RequestFormat = DataFormat.Json;
        if (filePath != null)
        {
            request.AddFile("file", @filePath, "image/jpeg");
        }
        

        IRestResponse response = client.Execute(request); //браузер выполни API запрос и сохранил в response
        return  HandlerApiError(response);
    }

    public static string DownloadFile(string urlRequest, string nameFile)
    {
        RestClient restClient = new RestClient(@urlRequest);
        RestRequest request = new RestRequest(Method.GET);
        IRestResponse response = restClient.Execute(request);
        byte[] bytes = response.RawBytes; //достаем биты из ответа
        bytes.SaveAs($"../../../assets/{nameFile}"); // сохраняем в файл

        return HandlerApiError(response);
    }

    public static string HandlerApiError(IRestResponse response) //обработчик API ошибок
    {
        if(response.IsSuccessful)
        {
            return response.Content;
        }
        else {
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.ErrorMessage);
            Console.WriteLine(response.Content);
            throw new NotImplementedException(response.Content);
        }
    }
}