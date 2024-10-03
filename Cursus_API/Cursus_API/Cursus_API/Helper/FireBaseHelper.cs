using Cursus_Business.Service.Interfaces;
using Cursus_Business.Service.Implements;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;

namespace Cursus_API.Helper;


    public static class FireBaseHelper
    {
    public static IServiceCollection AddFirebaseServices(this IServiceCollection services)
    {
        var credentialPath = Path.Combine(Directory.GetCurrentDirectory(),
            "lmsproject-5a473-firebase-adminsdk-u6ceo-6e3e76a494.json");
        FirebaseApp.Create(new AppOptions()
        {
            Credential = GoogleCredential.FromFile(credentialPath)
        });
        services.AddSingleton(StorageClient.Create(GoogleCredential.FromFile(credentialPath)));
        services.AddScoped<IFirebaseService, FirebaseService>();
        return services;
    }

}

