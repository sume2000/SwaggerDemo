# set JAVA_HOME environment variable
[System.Environment]::SetEnvironmentVariable("JAVA_HOME", "C:\Program Files\Java\jdk1.8.0_111")

# clone openapi project
git clone https://github.com/openapitools/openapi-generator
cd openapi-generator

# build openapi project with maven (-wrapper)
./mvnw clean install
mvn clean package

# generate ASPNET Core client from yaml
java -jar modules\openapi-generator-cli\target\openapi-generator-cli.jar generate -i c:\temp\swaggerdemo.yaml -g aspnetcore -o c:\temp\swaggerdemo_client