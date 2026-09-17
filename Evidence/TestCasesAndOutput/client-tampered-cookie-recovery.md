PS C:\Users\PreethikaSelvam\TempDataCookieTest> Add-Type -AssemblyName System.Net.Http
PS C:\Users\PreethikaSelvam\TempDataCookieTest> 
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $handler = New-Object System.Net.Http.HttpClientHandler
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $handler.AllowAutoRedirect = $false

PS C:\Users\PreethikaSelvam\TempDataCookieTest> $handler.UseCookies = $false
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $client = New-Object System.Net.Http.HttpClient($handler)
PS C:\Users\PreethikaSelvam\TempDataCookieTest> 
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $writeRequest = New-Object System.Net.Http.HttpRequestMessage(
>>     [System.Net.Http.HttpMethod]::Post,
>>     "http://localhost:5127/tempdata-cookie"
>> )
PS C:\Users\PreethikaSelvam\TempDataCookieTest> 
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $writeRequest.Content = New-Object System.Net.Http.StringContent(
>>     "_handler=WriteTempData&size=64",
>>     [System.Text.Encoding]::UTF8,
>>     "application/x-www-form-urlencoded"
>> )
PS C:\Users\PreethikaSelvam\TempDataCookieTest> 
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $write = $client.SendAsync($writeRequest).GetAwaiter().GetResult()
PS C:\Users\PreethikaSelvam\TempDataCookieTest> 
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $setCookie = $write.Headers.GetValues("Set-Cookie") |
>>     Select-Object -First 1
PS C:\Users\PreethikaSelvam\TempDataCookieTest> 
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $cookiePair = $setCookie.Split(";")[0]
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $name, $value = $cookiePair.Split("=", 2)
PS C:\Users\PreethikaSelvam\TempDataCookieTest> 
PS C:\Users\PreethikaSelvam\TempDataCookieTest> Write-Output "Write status: $([int]$write.StatusCode)"
Write status: 302
PS C:\Users\PreethikaSelvam\TempDataCookieTest> Write-Output "Cookie name: $name"
Cookie name: .AspNetCore.Components.TempData
PS C:\Users\PreethikaSelvam\TempDataCookieTest> Write-Output "Protected value length: $($value.Length)"
Protected value length: 283
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $originalLast = $value[$value.Length - 1]
PS C:\Users\PreethikaSelvam\TempDataCookieTest> 
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $replacement = if ($originalLast -eq "A") {
>>     "B"
>> } else {
>>     "A"
>> }
PS C:\Users\PreethikaSelvam\TempDataCookieTest> 
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $tampered = $value.Substring(0, $value.Length - 1) + $replacement
PS C:\Users\PreethikaSelvam\TempDataCookieTest> 
PS C:\Users\PreethikaSelvam\TempDataCookieTest> Write-Output "Changed final character: $originalLast -> $replacement"
Changed final character: c -> A
PS C:\Users\PreethikaSelvam\TempDataCookieTest> Write-Output "Original length: $($value.Length)"
Original length: 283
PS C:\Users\PreethikaSelvam\TempDataCookieTest> Write-Output "Tampered length: $($tampered.Length)"
Tampered length: 283
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $tamperedRequest = New-Object System.Net.Http.HttpRequestMessage(
>>     [System.Net.Http.HttpMethod]::Get,
>>     "http://localhost:5127/tempdata-cookie"
>> )
PS C:\Users\PreethikaSelvam\TempDataCookieTest> 
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $tamperedRequest.Headers.Add(
>>     "Cookie",
>>     "$name=$tampered"
>> )
PS C:\Users\PreethikaSelvam\TempDataCookieTest> 
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $tamperedResponse = $client.SendAsync(
>>     $tamperedRequest
>> ).GetAwaiter().GetResult()
PS C:\Users\PreethikaSelvam\TempDataCookieTest> 
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $tamperedBody = $tamperedResponse.Content.ReadAsStringAsync().
>>     GetAwaiter().GetResult()
PS C:\Users\PreethikaSelvam\TempDataCookieTest> 
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $removal = $tamperedResponse.Headers.GetValues("Set-Cookie") |
>>     Select-Object -First 1
PS C:\Users\PreethikaSelvam\TempDataCookieTest> 
PS C:\Users\PreethikaSelvam\TempDataCookieTest> Write-Output "Tampered status: $([int]$tamperedResponse.StatusCode)"
Tampered status: 200
PS C:\Users\PreethikaSelvam\TempDataCookieTest> Write-Output "No message: $($tamperedBody.Contains('No message'))"
No message: True
PS C:\Users\PreethikaSelvam\TempDataCookieTest> Write-Output "Original message returned: $($tamperedBody.Contains('Received 64 characters'))"
Original message returned: False
PS C:\Users\PreethikaSelvam\TempDataCookieTest> Write-Output "Removal header: $removal"
Removal header: .AspNetCore.Components.TempData=; expires=Thu, 01 Jan 1970 00:00:00 GMT; path=/; samesite=lax; httponly
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $followupRequest = New-Object System.Net.Http.HttpRequestMessage(
>>     [System.Net.Http.HttpMethod]::Get,
>>     "http://localhost:5127/tempdata-cookie"
>> )
PS C:\Users\PreethikaSelvam\TempDataCookieTest> 
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $followupResponse = $client.SendAsync(
>>     $followupRequest
>> ).GetAwaiter().GetResult()
PS C:\Users\PreethikaSelvam\TempDataCookieTest> 
PS C:\Users\PreethikaSelvam\TempDataCookieTest> $followupBody = $followupResponse.Content.ReadAsStringAsync().
>>     GetAwaiter().GetResult()
PS C:\Users\PreethikaSelvam\TempDataCookieTest> 
PS C:\Users\PreethikaSelvam\TempDataCookieTest> Write-Output "Follow-up status: $([int]$followupResponse.StatusCode)"
Follow-up status: 200
PS C:\Users\PreethikaSelvam\TempDataCookieTest> Write-Output "Follow-up no message: $($followupBody.Contains('No message'))"
Follow-up no message: True
PS C:\Users\PreethikaSelvam\TempDataCookieTest> 