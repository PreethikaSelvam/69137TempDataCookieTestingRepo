PS C:\Users\PreethikaSelvam\TempDataCookieTest> $env:TempDataTest__KeyDirectory = "$PWD\Evidence\Keys\ManualA"
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $env:TempDataTest__ApplicationName = "TempDataCookieTest-ManualA"
PS C:\Users\PreethikaSelvam\TempDataCookieTest> dotnet run --project .\TempDataCookieTest --launch-profile http
Using launch settings from .\TempDataCookieTest\Properties\launchSettings.json...
Building...
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5127
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: C:\Users\PreethikaSelvam\TempDataCookieTest\TempDataCookieTest
warn: Microsoft.AspNetCore.HttpsPolicy.HttpsRedirectionMiddleware[3]
      Failed to determine the https port for redirect.
info: Microsoft.Hosting.Lifetime[0]
      Application is shutting down...
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $env:TempDataTest__KeyDirectory = "$PWD\Evidence\Keys\ManualB"
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $env:TempDataTest__ApplicationName = "TempDataCookieTest-ManualA"
PS C:\Users\PreethikaSelvam\TempDataCookieTest> dotnet run --project .\TempDataCookieTest --launch-profile http
Using launch settings from .\TempDataCookieTest\Properties\launchSettings.json...
Building...
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5127
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: C:\Users\PreethikaSelvam\TempDataCookieTest\TempDataCookieTest
warn: Microsoft.AspNetCore.HttpsPolicy.HttpsRedirectionMiddleware[3]
      Failed to determine the https port for redirect.
warn: Microsoft.AspNetCore.Components.Endpoints.CookieTempDataProvider[2]
      The temp data cookie .AspNetCore.Components.TempData could not be loaded.
      System.Security.Cryptography.CryptographicException: The key {e0a4325e-f997-44b9-812a-ac481ec32bb1} was not found in the key ring. For more information go to https://aka.ms/aspnet/dataprotectionwarning
         at Microsoft.AspNetCore.DataProtection.KeyManagement.KeyRingBasedSpanDataProtector.Unprotect[TWriter](ReadOnlySpan`1 protectedData, TWriter& destination)
         at Microsoft.AspNetCore.Components.Endpoints.CookieTempDataProvider.LoadTempData(HttpContext context)
info: Microsoft.Hosting.Lifetime[0]
      Application is shutting down...
PS C:\Users\PreethikaSelvam\TempDataCookieTest> 