
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
COPY . /app
WORKDIR /app

ENV ASPNETCORE_URLS http://*:5000
ENV TZ Asia/Shanghai
EXPOSE 5000
ENTRYPOINT ["dotnet", "Simple.WebApi.dll"]