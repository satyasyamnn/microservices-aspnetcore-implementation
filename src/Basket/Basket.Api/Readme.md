# Setup Redis using docker desktop

## Commands 

### Pull redis image 
<b>docker pull redis</b><br />

### Run redis in container detached mode 
<b>docker run -d -p 6379:6379 --name aspnetrun-redis redis</b>

### Check the logs of the docker container
<b>docker logs -f aspnetrun-redis</b>

### Run docker container interactively
<b>docker exec -it aspnetrun-redis bash</b>

### To access redis-cli from redis container 
<b>redis-cli</b>

### Some redis commands 
PING <br/>
SET name shyam <br/>
get name  <br/>
