FROM ubuntu:24.10

RUN apt-get update -yqq && apt-get upgrade -yqq
RUN apt-get install -yqq dotnet-sdk-9.0
RUN apt-get install -yqq aspnetcore-runtime-9.0
RUN apt-get install -yqq curl

WORKDIR /app
COPY Robochat.csproj .
RUN dotnet restore
COPY . .

CMD ["dotnet", "watch", "--no-hot-reload"]