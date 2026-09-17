PS C:\Users\PreethikaSelvam\TempDataCookieTest> Add-Type -AssemblyName System.Net.Http
PS C:\Users\PreethikaSelvam\TempDataCookieTest> 
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $handler = New-Object System.Net.Http.HttpClientHandler
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $handler.AllowAutoRedirect = $false
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $handler.UseCookies = $false

PS C:\Users\PreethikaSelvam\TempDataCookieTest> $client = New-Object System.Net.Http.HttpClient($handler)
PS C:\Users\PreethikaSelvam\TempDataCookieTest> 
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $requestA = New-Object System.Net.Http.HttpRequestMessage(
>>     [System.Net.Http.HttpMethod]::Post,
>>     "http://localhost:5127/tempdata-cookie"
>> )
PS C:\Users\PreethikaSelvam\TempDataCookieTest> 
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $requestA.Content = New-Object System.Net.Http.StringContent(
>>     "_handler=WriteTempData&size=64",
>>     [System.Text.Encoding]::UTF8,
>>     "application/x-www-form-urlencoded"
>> )
PS C:\Users\PreethikaSelvam\TempDataCookieTest> 
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $responseA = $client.SendAsync($requestA).GetAwaiter().GetResult()
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $setCookieA = $responseA.Headers.GetValues("Set-Cookie") |
>>     Select-Object -First 1
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $cookieFromKeyA = $setCookieA.Split(";")[0]
PS C:\Users\PreethikaSelvam\TempDataCookieTest> 
PS C:\Users\PreethikaSelvam\TempDataCookieTest> Write-Output "Write status: $([int]$responseA.StatusCode)"
Write status: 302
PS C:\Users\PreethikaSelvam\TempDataCookieTest> Write-Output "Cookie name: $($cookieFromKeyA.Split('=')[0])"
Cookie name: .AspNetCore.Components.TempData
PS C:\Users\PreethikaSelvam\TempDataCookieTest> Write-Output "Cookie captured: $($null -ne $cookieFromKeyA)"
Cookie captured: True
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $requestB = New-Object System.Net.Http.HttpRequestMessage(
>>     [System.Net.Http.HttpMethod]::Get,
>>     "http://localhost:5127/tempdata-cookie"
>> )
PS C:\Users\PreethikaSelvam\TempDataCookieTest> 
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $requestB.Headers.Add("Cookie", $cookieFromKeyA)
PS C:\Users\PreethikaSelvam\TempDataCookieTest> 
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $responseB = $client.SendAsync($requestB).GetAwaiter().GetResult()
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $bodyB = $responseB.Content.ReadAsStringAsync().GetAwaiter().GetResult()
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $removalB = $responseB.Headers.GetValues("Set-Cookie") |
>>     Select-Object -First 1
PS C:\Users\PreethikaSelvam\TempDataCookieTest> 
PS C:\Users\PreethikaSelvam\TempDataCookieTest> Write-Output "Unreadable-key status: $([int]$responseB.StatusCode)"
Unreadable-key status: 200
PS C:\Users\PreethikaSelvam\TempDataCookieTest> Write-Output "No message: $($bodyB.Contains('No message'))"
No message: True
PS C:\Users\PreethikaSelvam\TempDataCookieTest> Write-Output "Removal header: $removalB"
Removal header: .AspNetCore.Components.TempData=; expires=Thu, 01 Jan 1970 00:00:00 GMT; path=/; samesite=lax; httponly
PS C:\Users\PreethikaSelvam\TempDataCookieTest> 
