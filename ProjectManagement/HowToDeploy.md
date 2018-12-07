# How to deploy ASP Net Core applications with Heroku?

How to deploy C# / ASP.NET Applications

1. Install Docker.
2. Download ASP.NET Core 1.1.4 sdk.
3. Run `cd <project-name>.Solutions/<project-name>`
4. Run `<dotnet 1.1.4 sdk folder location>/dotnet restore`
5. Run `<dotnet 1.1.4 sdk folder location>/dotnet publish -c Release  -o out`
6. Create `Dockerfile` file in the `<project-name>.Solutions/<project-name>` directory. Add the text below.
<pre>
  <code>
    FROM microsoft/aspnetcore:1.1.4
    WORKDIR /app
    COPY . .
    ENTRYPOINT ["dotnet", "out/<project-name>.dll"]
  </code>
</pre>
7. Create `.dockerignore` file in the `<project-name>.Solutions/<project-name>` directory. Add the text below.
<pre>
  <code>
    bin/
    obj/
  </code>
</pre>
8. Run `docker build -t <docker-container-name> . `
9. To test its running status, run `docker run -d -p 8000:80 <docker-container-name>` and go to `localhost:8000`.
10. If you fail to connect, run `docker ps -a` command and check container status and container id. To see logs, run `docker logs -f <docker-container-id>`.
11. Make an app at the Heroku website.
12. Run `npm install -g heroku`.
13. Run `heroku login` and `heroku container:login`.
14. Update the last line of  `Dockerfile`.
<pre>
  <code>
  -- ENTRYPOINT [`dotnet`, `out/<project-name>.dll`] 
  ++ CMD ASPNETCORE_URLS=http://*:$PORT dotnet out/<project-name>.dll
  </code>
</pre>
15. Run this command(`docker build -t <docker-container-name> . `) again to apply the change we've made.
16. Run `docker tag <docker-container-name> registry.heroku.com/<heroku-app-name>/web`.
17. Run `heroku container:push web -a <heroku-app-name>`
18. Run `heroku container:release web -a <heroku-app-name>`