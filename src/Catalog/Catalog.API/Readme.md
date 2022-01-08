# Mongo database setup using Docker desktop 

## Commands 

To pull mongo latest image from docker registry <br>
<b>docker pull mongo</b> <br/>

To run mongo in container detached mode detached <br>
<b>docker run -d -p 27017:27017 --name shopping-mongo mongo</b><br/>

To inspect logs of the container
<b>docker logs -f shopping-mongo</b> <br />

To access container in interactive mode 
<b>docker exec -it shopping-mongo bash</b> 

docker ps -aq

docker stop $(docker ps -aq)


## Setup mongo client

<b> docker run -d -p 3000:3000 mongoclient/mongoclient </b>


## Mongo commands in shell 

mongo <br />
show dbs <br />
use CatalogDb <br />
show collections <br />
db.createCollection('Products') <br />
db.Products.insertMany([{ 'Name':'Asus Laptop','Category':'Computers', 'Summary':'Summary', 'Description':'Description', 'ImageFile':'ImageFile', 'Price':54.93 }, { 'Name':'HP Laptop','Category':'Computers', 'Summary':'Summary', 'Description':'Description', 'ImageFile':'ImageFile', 'Price':88.93 } ])<br />
db.Products.find({}).pretty() <br />
db.Products.remove() <br />


## Docker compose commands 

docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml up <br />
docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml down <br />