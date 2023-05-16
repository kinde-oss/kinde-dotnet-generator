## .NET SDK generator


Install OpenApi generator.<br />
Please read [OpenAPI generator installation](https://github.com/OpenAPITools/openapi-generator#1---installation) or [OpenAPI generator installation](https://openapi-generator.tech/docs/installation).

---
Clone the repository to your computer:
```
git clone https://github.com/kinde-oss/kinde-dotnet-generator
```
---
To generate SDK please run:
```
openapi-generator-cli generate -i https://kinde.com/api/kinde-mgmt-api-specs.yaml -g csharp-netcore -o Kinde.Sdk --package-name=Kinde.Api -c config.yaml --library=httpclient --additional-properties=targetFramework=net6.0,packageGuid=9A19103F-16F7-4668-BE54-9A1E7A4F7556,packageVersion=1.2.0,sourceFolder= --global-property apiTests=false,modelTests=false

```
---
Folder `Kinde.Sdk` contains our final SDK after build.
In order to copy files generated from Kinde.Sdk folder to the development repository, eg: `../kinde-dotnet-sdk` , please run
```
cp -r Kinde.Sdk/Kinde.Api ../kinde-dotnet-sdk/
```
