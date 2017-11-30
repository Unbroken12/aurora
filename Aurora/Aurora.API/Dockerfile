FROM microsoft/aspnetcore:2.0
ARG source
WORKDIR /app
EXPOSE 80
RUN /bin/echo -e 'Source: ' + $source
COPY obj/Docker/publish .
ENTRYPOINT ["dotnet", "Aurora.API.dll"]
