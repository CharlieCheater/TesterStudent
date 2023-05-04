using System.Net.Http;
using System;
using TesterStudent.Client.Enums;

namespace TesterStudent.Client.Utils;

public static class Utilities
{
    public static HttpMethod GetHttpMethod(HttpMethodTypes method)
    {
        return method switch
        {
            HttpMethodTypes.Delete => HttpMethod.Delete,
            HttpMethodTypes.Get => HttpMethod.Get,
            HttpMethodTypes.Post => HttpMethod.Post,
            HttpMethodTypes.Put => HttpMethod.Put,
            _ => throw new NotImplementedException(),
        };
    }
}