using BusinessAccess.Services.Interfaces;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Net;

public class GoogleMeetService : IGoogleMeetService
{
    private readonly IConfiguration _configuration;
    private const string RedirectUri = "http://localhost:7094/authorize/";
    private const string ApplicationName = "GenderHealthCareSystem";

    public GoogleMeetService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> CreateMeetingAsync(DateTime startTime, DateTime endTime)
    {
        var clientId = _configuration["Authentication:Google:ClientId"];
        var clientSecret = _configuration["Authentication:Google:ClientSecret"];

        var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
        {
            ClientSecrets = new ClientSecrets { ClientId = clientId, ClientSecret = clientSecret },
            Scopes = new[] { CalendarService.Scope.Calendar }
        });

        var receiver = new LocalhostCodeReceiver();

        var credential = await new AuthorizationCodeInstalledApp(flow, receiver)
            .AuthorizeAsync("user", CancellationToken.None);

        var service = new CalendarService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential,
            ApplicationName = ApplicationName
        });

        var newEvent = new Event
        {
            Summary = "Cuộc hẹn tư vấn",
            Start = new EventDateTime { DateTime = startTime },
            End = new EventDateTime { DateTime = endTime },
            ConferenceData = new ConferenceData
            {
                CreateRequest = new CreateConferenceRequest
                {
                    RequestId = Guid.NewGuid().ToString(),
                    ConferenceSolutionKey = new ConferenceSolutionKey { Type = "hangoutsMeet" }
                }
            }
        };

        var request = service.Events.Insert(newEvent, "primary");
        request.ConferenceDataVersion = 1;

        var created = await request.ExecuteAsync();

        return created.ConferenceData?.EntryPoints?
            .FirstOrDefault(e => e.EntryPointType == "video")?.Uri;
    }
}
