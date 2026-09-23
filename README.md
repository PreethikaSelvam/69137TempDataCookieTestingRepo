# ASP.NET Core TempData Cookie Validation

This repository contains an isolated Blazor Web App used to validate the
ASP.NET Core TempData cookie behavior in .NET 11 RC1 for
[`dotnet/aspnetcore#69137`](https://github.com/dotnet/aspnetcore/issues/69137).

The validation is black-box only. It uses public documentation, browser tools,
HTTP observations, and synthetic values without reading framework source or
implementation pull requests.

## Environment

- .NET SDK `11.0.100-rc.1.26425.128`
- ASP.NET Core runtime `11.0.0-rc.1.26425.128`
- Blazor Web App with a static SSR TempData page
- Windows 11 24H2 x64
- Google Chrome 153.0.8010.48 (Official Build, 64-bit)
- Visual Studio Code 1.138.0

The required SDK is selected by [global.json](global.json).

## Run the Sample

From the repository root:

```powershell
$env:TempDataTest__KeyDirectory = "$PWD\Evidence\Keys\ManualA"
$env:TempDataTest__ApplicationName = "TempDataCookieTest-ManualA"
Remove-Item Env:TempDataTest__CookieName -ErrorAction Ignore
Remove-Item Env:TempDataTest__PathBase -ErrorAction Ignore
dotnet run --project .\TempDataCookieTest --launch-profile https
```

Open:

```text
https://localhost:7130/tempdata-cookie
```

Enter a payload size and select **Write and redirect**. A successful small-value
round trip displays:

```text
Received 64 characters; match: True
```

## Configurations

Custom cookie name:

```powershell
$env:TempDataTest__CookieName = ".Validation.CustomTempData"
```

Application path base:

```powershell
Remove-Item Env:TempDataTest__CookieName -ErrorAction Ignore
$env:TempDataTest__PathBase = "/cookie-test"
```

For the path-base configuration, open:

```text
https://localhost:7130/cookie-test/tempdata-cookie
```

Unreadable-cookie comparisons use dedicated directories under `Evidence/Keys`
and distinct `TempDataTest__ApplicationName` values. Do not delete or modify a
shared development or production key ring.

## Validation Coverage

The following cases were tested:

1. Small TempData cookie round trip and attributes
2. `HttpOnly`, script access, and protected content
3. Bounded multi-chunk payload and exact round trip
4. One-character cookie tampering and clean recovery
5. Incompatible Data Protection application identity
6. Incompatible dedicated Data Protection key ring
7. Custom cookie name for writing and reading
8. Cookie path under an application path base

All required feature behavior checks passed.

## Results and Evidence

- [Completed test report](Evidence/TempDataCookieValidationReport.docx)
- [Build evidence](Evidence/Build)

## Identified Documentation Issue

The .NET 11 Blazor state-management documentation states `SameSiteMode.Strict` for the TempData cookie, while the tested default responses consistently emitted `SameSite=Lax` without explicit SameSite configuration. This is a documentation bug, not a TempData runtime bug.

The documentation describes the 4 KB browser cookie limit, automatic chunking, and Data Protection encryption, but doesn't state the recovery behavior when a cookie can't be decrypted.