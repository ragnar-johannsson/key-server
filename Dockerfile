FROM microsoft/dotnet:latest
MAINTAINER Ragnar B. Johannsson <ragnar@igo.is>

ADD *.fs NuGet.Config project.json create_user.sh /app/

WORKDIR /app

RUN dotnet restore
RUN dotnet publish

VOLUME ["/opt/keys"]

EXPOSE 3000/tcp

CMD ["dotnet", "run"]
