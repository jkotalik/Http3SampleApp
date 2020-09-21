# Http3SampleApp

This is a sample application that runs on Kestrel.

## Getting started for Windows

There are a few prerequisites that need to be done before starting to use HTTP/3.

1. Download the latest 5.0 build of .NET from <https://github.com/dotnet/installer>. Download the 5.0.100 RC1 branch.
2. Latest [Windows Insider Builds](https://insider.windows.com/en-us/), Insiders Fast build. This is required for Schannel support for QUIC.
    To confirm you have a new enough build, run winver on command line and confirm you version is greater than Version 2004 (OS Build 20145.1000)
3. Enabling TLS 1.3. Add the following registry keys to enable TLS 1.3.

    ```text
    reg add "HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Protocols\TLS 1.3\Server" /v DisabledByDefault /t REG_DWORD /d 0 /f
    reg add "HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Protocols\TLS 1.3\Server" /v Enabled /t REG_DWORD /d 1 /f
    reg add "HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Protocols\TLS 1.3\Client" /v DisabledByDefault /t REG_DWORD /d 0 /f
    reg add "HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Protocols\TLS 1.3\Client" /v Enabled /t REG_DWORD /d 1 /f
    ```
4. Updating the dotnet dev-cert. You will probably need to regenerate the ASP.NET Core Dev Cert as it needs to be able to support TLS 1.3. You can do this by running:
    ```
    dotnet dev-certs https --clean
    dotnet dev-certs https
    dotnet dev-certs https --trust
    ```
    
Next, after cloning, you should be able to execute `dotnet run` and it should start successfully.

## Sending a request with HTTP/3.

1. Download Edge Dev or Canary <https://www.microsoftedgeinsider.com/en-us/download.>
2. Either launch edge on the command line with
   ```text
    & "C:\Users\<user>\AppData\Local\Microsoft\Edge SxS\Application\msedge.exe" --enable-quic --quic-version=h3-29 --origin-to-force-quic-on=localhost:5557
   ```
   or adding the flags to the Microsoft Edge Dev Properties Target.
3. Hit localhost:5557 from the browser, check the network tab, the request should be HTTP/3 with the spec h3-29.
