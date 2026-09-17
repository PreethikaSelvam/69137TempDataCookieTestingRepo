PS C:\Users\PreethikaSelvam\TempDataCookieTest> Add-Type -AssemblyName System.Net.Http
PS C:\Users\PreethikaSelvam\TempDataCookieTest> 
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $handler = New-Object System.Net.Http.HttpClientHandler
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $handler.AllowAutoRedirect = $false
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $handler.UseCookies = $false
PS C:\Users\PreethikaSelvam\TempDataCookieTest> 
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $client = New-Object System.Net.Http.HttpClient($handler)
PS C:\Users\PreethikaSelvam\TempDataCookieTest> 
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $request = New-Object System.Net.Http.HttpRequestMessage(
>>     [System.Net.Http.HttpMethod]::Post,
>>     "http://localhost:5127/tempdata-cookie"
>> )
PS C:\Users\PreethikaSelvam\TempDataCookieTest> 
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $request.Content = New-Object System.Net.Http.StringContent(
>>     "_handler=WriteTempData&size=64",
>>     [System.Text.Encoding]::UTF8,
>>     "application/x-www-form-urlencoded"
>> )
PS C:\Users\PreethikaSelvam\TempDataCookieTest> 
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $response = $client.SendAsync($request).GetAwaiter().GetResult()
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $setCookie = $response.Headers.GetValues("Set-Cookie") |
>>     Select-Object -First 1
PS C:\Users\PreethikaSelvam\TempDataCookieTest> 
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $cookieA = $setCookie.Split(";")[0]
PS C:\Users\PreethikaSelvam\TempDataCookieTest> 
PS C:\Users\PreethikaSelvam\TempDataCookieTest> Write-Output "Status: $([int]$response.StatusCode)"
Status: 302
PS C:\Users\PreethikaSelvam\TempDataCookieTest> Write-Output "Cookie name: $($cookieA.Split('=')[0])"
Cookie name: .AspNetCore.Components.TempData
PS C:\Users\PreethikaSelvam\TempDataCookieTest> Write-Output "Cookie captured: $($null -ne $cookieA)"
Cookie captured: True
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $requestB = New-Object System.Net.Http.HttpRequestMessage(
>>     [System.Net.Http.HttpMethod]::Get,
>>     "http://localhost:5127/tempdata-cookie"
>> )
PS C:\Users\PreethikaSelvam\TempDataCookieTest> 
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $requestB.Headers.Add("Cookie", $cookieA)
PS C:\Users\PreethikaSelvam\TempDataCookieTest> 
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $responseB = $client.SendAsync($requestB).GetAwaiter().GetResult()
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $bodyB = $responseB.Content.ReadAsStringAsync().GetAwaiter().GetResult()
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $removal = $responseB.Headers.GetValues("Set-Cookie") |
>>     Select-Object -First 1
PS C:\Users\PreethikaSelvam\TempDataCookieTest> 
PS C:\Users\PreethikaSelvam\TempDataCookieTest> Write-Output "Status: $([int]$responseB.StatusCode)"
Status: 200
PS C:\Users\PreethikaSelvam\TempDataCookieTest> Write-Output "No message: $($bodyB.Contains('No message'))"
No message: True
PS C:\Users\PreethikaSelvam\TempDataCookieTest> Write-Output "Removal header: $removal"
Removal header: .AspNetCore.Components.TempData=; expires=Thu, 01 Jan 1970 00:00:00 GMT; path=/; samesite=lax; httponly
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $followup = $client.GetAsync(
>>     "http://localhost:5127/tempdata-cookie"
>> ).GetAwaiter().GetResult()
PS C:\Users\PreethikaSelvam\TempDataCookieTest> 
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $followupBody = $followup.Content.ReadAsStringAsync().GetAwaiter().GetResult()

PS C:\Users\PreethikaSelvam\TempDataCookieTest> 
PS C:\Users\PreethikaSelvam\TempDataCookieTest> Write-Output "Follow-up status: $([int]$followup.StatusCode)"
Follow-up status: 200
PS C:\Users\PreethikaSelvam\TempDataCookieTest> Write-Output "Follow-up no message: $($followupBody.Contains('No message'))"
Follow-up no message: True
PS C:\Users\PreethikaSelvam\TempDataCookieTest> 
