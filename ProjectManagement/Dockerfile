FROM microsoft/aspnetcore:1.1.4
WORKDIR /app
COPY . .
# ENTRYPOINT ["dotnet", "out/ProjectManagement.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet out/ProjectManagement.dll