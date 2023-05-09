# TicketManagement
Ticket Management Test App

To build and start App in docker container be sure to provide HTTPS DEV cert password in to .env file HTTPS_CERT_PASSWORD variable.
<br>
<br>
If you dont have DEV cert or do net remember password you can create new one DEV cert.
1. Remove any development certificates that already exist on the local machine.
	dotnet dev-certs https --clean
2. Generate certificate and configure local machine:
	Windows using Linux containers
		dotnet dev-certs https -ep "$env:USERPROFILE\.aspnet\https\aspnetapp.pfx"  -p $CREDENTIAL_PLACEHOLDER$
	Windows using Windows containers
		dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx -p $CREDENTIAL_PLACEHOLDER$
	macOS or Linux
		dotnet dev-certs https -ep ${HOME}/.aspnet/https/aspnetapp.pfx -p $CREDENTIAL_PLACEHOLDER$
3. Trust created cert
	dotnet dev-certs https --trust

After creating cert provide DEV cert password in to .env file.
And finali build and start app by executing
	docker compose -f docker-compose.local.yaml up

For more information visit https://learn.microsoft.com/en-us/aspnet/core/security/docker-compose-https?view=aspnetcore-6.0