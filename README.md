## Without docker-compose.yml
- Pull mysql image: `docker pull mysql:9.2.0`
- Run product_db container: `docker run --rm -it --name product_db -p 3308:3306 -e MYSQL_ROOT_PASSWORD=mypass -e MYSQL_DATABASE=product_db -d mysql:9.2.0`
- Run user_db container: `docker run --rm -it --name user_db -p 3309:3306 -e MYSQL_ROOT_PASSWORD=mypass -e MYSQL_DATABASE=user_db -d mysql:9.2.0`

### At root path
- Build images:
	+ product: `docker build -t product_image -f ProductService/Dockerfile .`
	+ user: `docker build -t user_image -f UserService/Dockerfile .`
	+ migrator: `docker build -t product_migrator -f ProductMigrator/Dockerfile .`

- Run container:
	+ product_server: `docker run --rm -dp 8080:8080 -p 8081:8081 --name product_server product_image`
	+ user_server: `docker run --rm -dp 8090:8090 -p 8091:8091 --name user_server user_image`
	+ migrator_server: `docker run --name migrator product_migrator`

- Run migration:
	+ Check: `docker exec -it product_server dotnet list package`
