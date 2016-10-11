FROM microsoft/dotnet:latest
MAINTAINER Ragnar B. Johannsson <ragnar@igo.is>

ADD *.fs *.sh NuGet.Config project.json /app/

WORKDIR /app

RUN dotnet restore
RUN dotnet publish

VOLUME ["/opt/keys"]

EXPOSE 3000/tcp

CMD ["dotnet", "run"]
