FROM mcr.microsoft.com/dotnet/core/sdk:6.0

WORKDIR /home/app

COPY . .

RUN dotnet restore

RUN dotnet publish ./MyLeaveManagement/MyLeaveManagement.csproj -o /publish/

WORKDIR /publish

ENV ASPNETCORE_URLS="http://0.0.0.0:5000"

ENTRYPOINT ["dotnet", "MyLeaveManagement.dll"]