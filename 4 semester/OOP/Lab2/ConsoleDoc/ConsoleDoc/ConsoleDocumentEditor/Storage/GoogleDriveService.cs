using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
namespace ConsoleDoc.ConsoleDocumentEditor.Storage;

public abstract class GoogleDriveService
{
    private static readonly string[] Scopes = { DriveService.Scope.Drive };
    private static readonly string ApplicationName = "ConsoleDoc";

    [Obsolete("Obsolete")]
    public static DriveService GetDriveService()
    {
        UserCredential credential;

        using (var stream = new FileStream("..\\..\\..\\ConsoleDocumentEditor\\Storage\\credentials.json", FileMode.Open, FileAccess.Read))
        {
            string credPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ConsoleDoc", "token.json");
            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                Scopes,
                "user",
                CancellationToken.None,
                new FileDataStore(credPath, true)).Result;
        }

        return new DriveService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential,
            ApplicationName = ApplicationName,
        });
    }
}