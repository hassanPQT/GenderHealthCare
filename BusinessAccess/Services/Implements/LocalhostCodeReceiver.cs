using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Requests;
using Google.Apis.Auth.OAuth2.Responses;
using System.Diagnostics;
using System.Net;
using System.Text;

public class LocalhostCodeReceiver : ICodeReceiver
{
    private const int Port = 5006;
    public string RedirectUri => $"http://localhost:{Port}/authorize/";

    public async Task<AuthorizationCodeResponseUrl> ReceiveCodeAsync(AuthorizationCodeRequestUrl url, CancellationToken cancellationToken)
    {
        string authUrl = url.Build().ToString();
        Console.WriteLine("Mở trình duyệt và xác thực: " + authUrl);
        Process.Start(new ProcessStartInfo { FileName = authUrl, UseShellExecute = true });

        using var listener = new HttpListener();
        listener.Prefixes.Add($"http://localhost:{Port}/authorize/");
        listener.Start();
        Console.WriteLine($"🟢 Listening on http://localhost:{Port}/authorize/");

        var context = await listener.GetContextAsync();
        var code = context.Request.QueryString["code"];
        var error = context.Request.QueryString["error"];

        var responseHtml = "<html><body><h2>✅ Successfully!.</h2></body></html>";
        var buffer = Encoding.UTF8.GetBytes(responseHtml);
        context.Response.ContentLength64 = buffer.Length;
        await context.Response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
        context.Response.OutputStream.Close();

        listener.Stop();

        return new AuthorizationCodeResponseUrl { Code = code, Error = error };
    }
}
