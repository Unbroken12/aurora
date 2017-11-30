FROM microsoft/aspnetcore:2.0
ARG source
WORKDIR /app
EXPOSE 80
RUN bash -c 'ls'
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "Aurora.API.dll"]
