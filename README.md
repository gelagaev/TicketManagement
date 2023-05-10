# TicketManagement

Ticket Management Test App

To build and start the app in a Docker container, be sure to provide the HTTPS DEV cert password in the .env file(HTTPS_CERT_PASSWORD variable).
<br>
<br>
If you don't have a DEV cert or don't remember the password, you can create a new one.
<br>

1. Remove any development certificates that already exist on the local machine.
   <ul><li><code>dotnet dev-certs https --clean</code></li></ul>
2. Generate certificate and configure local machine:
<br>
   Windows using Linux containers
   <ul><li><code>dotnet dev-certs https -ep "$env:USERPROFILE\.aspnet\https\aspnetapp.pfx"  -p $CREDENTIAL_PLACEHOLDER$</code></li></ul>
   Windows using Windows containers
   <ul><li><code>dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx -p $CREDENTIAL_PLACEHOLDER$</code></li></ul>
   macOS or Linux
   <ul><li><code>dotnet dev-certs https -ep ${HOME}/.aspnet/https/aspnetapp.pfx -p $CREDENTIAL_PLACEHOLDER$</code></li></ul>
   For Mac arm-based users, be sure to use Docker v4.16 or higher and enable "Use Rosetta for x86/amd64 emulation on Apple Silicon" in settings.
   <br>
3. Trust created cert
<br>
   <ul><li><code>dotnet dev-certs https --trust</code></li></ul>

After creating the DEV cert, provide the password in the .env file.
<br>
Finally, build and start the app by executing the following command:
   <ul><li><code>docker compose -f docker-compose.local.yaml up</code></li></ul>

For more information, visit https://learn.microsoft.com/en-us/aspnet/core/security/docker-compose-https?view=aspnetcore-6.0