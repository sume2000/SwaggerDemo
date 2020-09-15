# Swagger Demo
This project demonstrates the usage of Swagger via the Swashbuckle package in .NET Core and the recreation of the project via OpenAPI generator.  
The project itself uses .NET Core 3.1.

Detailed information for Swashbuckle options can be found in-code via comments starting with "SWAGGER-HOWTO".

## Pre-Requisites
* .NET Core 3.1/ASP.NET Core 3.1
* Current Java Runtime (for OpenApi Generator)
* Its recommended to use VisualStudio as IDE

## Quick-Guide Swashbuckle
* Install nuget package "Swashbuckle.AspNetCore"
* *optional: Install nuget package "Microsoft.AspNetcore.StaticFiles" if swagger UI is used**
* Configure swagger in startup class
* *optional: Configure XML documentation generation in csproj file and add XML comments to classes*
  *  *the following lines must be set in the .csproj file to let VS generate the XML document:*
        ```
        <PropertyGroup>
            <GenerateDocumentationFile>true</GenerateDocumentationFile>
            <NoWarn>$(NoWarn);1591</NoWarn>
        </PropertyGroup>
        ```
* Build the project

## Quick-Guide OpenApiGenerator
* Install current java runtime
* *optional: Convert the swagger.json file to .yaml (i.e. with Swagger Editor)*
* Adapt file paths etc. in "createClientFromSwagger.ps1" and copy file to temp folder
* Execute the powershell script
* Copy generated solution folder to target directory
* Check and if needed, correct generated code

## Additional resources
* [Microsoft Docs](https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-3.1&tabs=visual-studio)
* [Swashbuckle Git Repo](https://github.com/domaindrivendev/Swashbuckle.AspNetCore/blob/master/README.md)
* [OpenAPIGenerator Git Repo](https://github.com/OpenAPITools/openapi-generator)
* [Swagger Editor](https://editor.swagger.io/)