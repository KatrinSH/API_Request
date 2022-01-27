using RestSharp;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V85.CacheStorage;
using System.Collections.Generic;
using System.Net;
using NUnit.Framework;
using Xunit;


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
            request.AddFile("image", "/Users/ekaterinasugaj/Downloads/image.jpeg");
        }
        

        IRestResponse response = client.Execute(request);//браузер выполни API запрос и сохранил в response
        return  HandlerAPIError(response);
    }

    public static IRestClient DownloadFile(string urlRequest, string nameFile)
    {
        RestClient restClient = new RestClient(@urlRequest);
        var fileBytes = restClient.DownloadData(new RestRequest("#", Method.GET));
        File.WriteAllBytes(Path.Combine("/Users/ekaterinasugaj/Downloads/", nameFile), fileBytes);

        return restClient;
    }

    public static string HandlerAPIError(IRestResponse response)
    {
        if(response.StatusCode == HttpStatusCode.OK)
        {
            return response.Content;
        }
        else
        {
            Console.WriteLine(response.ErrorMessage);
            Console.WriteLine(response.Content);
            throw new NotImplementedException(response.Content);
        }
    }
}