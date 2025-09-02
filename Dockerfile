# Usamos una imagen base de ASP.NET Core runtime 6.0
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base

# Establecemos el directorio de trabajo a /app
WORKDIR /app

# Actualizamos los paquetes y luego instalamos curl y jq
RUN apt-get update \
    && apt-get install -y smbclient curl jq iputils-ping nano telnet libcap2-bin \
    && setcap cap_net_raw+p /bin/ping \
    && rm -rf /var/lib/apt/lists/*

# Exponemos el puerto 8080 para la aplicación
EXPOSE 8080

# Usamos una imagen de .NET SDK 6.0 para construir la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Establecemos el directorio de trabajo a /src
WORKDIR /src

# Copiamos el archivo de solución a /src
COPY Backend.sln ./

# copiar todos los minfin.nugets en una carpeta local
# minfin.nugets es una carpeta que contiene todos los nugets minfin.nugets
COPY minfin.nugets /src/packages/

# Copiamos el archivo de configuración de NuGet a /src
COPY NuGet_docker.Config ./

# Copiamos los archivos de proyecto a sus respectivos directorios en /src
COPY Core/*.csproj ./Core/
COPY Repositorios/*.csproj ./Repositorios/
COPY WebApi/*.csproj ./WebApi/

# Restauramos las dependencias de NuGet especificadas en los archivos .csproj
RUN dotnet restore --configfile NuGet_docker.Config -nowarn:msb3202,nu1503 --verbosity diag

# Copiamos todos los archivos del contexto actual a /src
COPY . .

# Cambiamos el directorio de trabajo a /src/WebApi
WORKDIR /src/WebApi

# Construimos el proyecto en configuración Release y salimos el output a /app-bin
RUN dotnet build -c Release -o /app-bin

# Utilizamos la etapa de build como base para la etapa de publish
FROM build AS publish

# Publicamos la aplicación en configuración Release y salimos el output a /app
RUN dotnet publish -c Release -o /app

# Listamos los archivos en /app-bin para verificar la salida de build
RUN ls /app-bin

# Copiamos archivos XML a /app
COPY WebApi/sso-qa.xml /app
COPY WebApi/sso-prod.xml /app

# Usamos la imagen base de ASP.NET Core runtime 6.0 como base para la imagen final
FROM base AS final

# Establecemos el directorio de trabajo a /app
WORKDIR /app

# Exponemos el puerto 8080 nuevamente
EXPOSE 8080

# Copiamos los archivos publicados desde la etapa publish a la imagen final
COPY --from=publish /app .

# Creamos un usuario sin privilegios para correr la aplicación
RUN useradd -r clima_laboral_user -d /clima-laboral/home/clima_laboral_user -s /sbin/nologin;

# Cambiamos al usuario sin privilegios
USER clima_laboral_user

# Establecemos el punto de entrada para la aplicación
ENTRYPOINT ["dotnet", "WebApi.dll"]
