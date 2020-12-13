using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Bedrock.Framework.Protocols;
using Microsoft.AspNetCore.Connections;

namespace ServerApplication
{
    public class HttpApplication : ConnectionHandler
    {
        public override async Task OnConnectedAsync(ConnectionContext connection)
        {
            var httpConnection = new HttpClientProtocol(connection);

            int i = 1000;

            while (i-- > 0)
            {
                await Task.Delay(500);
                var response = await httpConnection.SendAsync(new HttpRequestMessage(HttpMethod.Get, "/"), HttpCompletionOption.ResponseContentRead);
                if (!response.IsSuccessStatusCode)
                    continue;
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"[{i}]: {content}");
            }
        }
    }

    //public class HttpApplication : ConnectionHandler
    //{
    //    public override async Task OnConnectedAsync(ConnectionContext connection)
    //    {
    //        var httpConnection = new HttpServerProtocol(connection);

    //        while (true)
    //        {
    //            var request = await httpConnection.ReadRequestAsync();

    //            Console.WriteLine(request);

    //            // Consume the request body
    //            await request.Content.CopyToAsync(Stream.Null);

    //            await httpConnection.WriteResponseAsync(new HttpResponseMessage(HttpStatusCode.OK)
    //            {
    //                Content = new StringContent("Hello World")
    //            });
    //        }
    //    }
    //}
}
