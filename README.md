# PokeSpeare

This application returns a pokemon description in Shakespearean English.

## Getting the application

The easiest way to get the application is by cloning the repository with [git](https://git-scm.com/downloads), using the following instructions.

```console
git clone https://github.com/mrnafraz/PokeSpeare
```

You can also [download the repository as a zip](https://github.com/mrnafraz/PokeSpeare/archive/master.zip).

## Build and run the sample with Docker

If you don't have Docker installed download and install Docker Desktop for [Windows](https://docs.docker.com/docker-for-windows/install/) or [Mac](https://docs.docker.com/docker-for-mac/install/).
If you are new to docker [follow this guide](https://docs.docker.com/get-started/).

You can build and run the application in Docker using the following commands. The instructions assume that you are in the root of the repository.

```console
docker build -t pokespeare .
docker run --rm -p 5000:80 pokespeare
```

To access the application on the browser navigate to http://localhost:5000/pokemon/{pokemon-name} where `pokemon-name` can be substituted with a pokemon name. For example  `charizard` can be accessed using [http://localhost:5000/pokemon/charizard](http://localhost:5000/pokemon/charizard).

The commands above run unit tests as part of `docker build`. You can also run .NET unit tests as part of `docker run`. The following instructions provide you with the simplest way of doing that.

```console
docker build --target testrunner -t pokespeare:test .
docker run --rm -it pokespeare:test
```

## Build and run the sample locally with the .NET SDK

You can build this .NET 5.0 application locally with the [.NET 5.0 SDK](https://dotnet.microsoft.com/download/dotnet/5.0) using the following instructions. The instructions assume that you are in the root of the repository.

You must have the [.NET 5.0 SDK](https://dotnet.microsoft.com/download/dotnet/5.0) installed.

```console
cd src
cd PokeSpeare.Service
dotnet run
```

To access the application on the browser navigate to http://localhost:5000/pokemon/{pokemon-name} where `pokemon-name` can be substituted with a pokemon name. For example  `charizard` can be accessed using [http://localhost:5000/pokemon/charizard](http://localhost:5000/pokemon/charizard).
