FROM microsoft/aspnetcore:1.1.0
ENTRYPOINT ["dotnet", "mlWebsite.dll"]
ARG source=.
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
COPY $source .
