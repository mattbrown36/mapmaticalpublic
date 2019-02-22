for /f "delims=" %%A in ('docker ps -l -q') do set "var=%%A"
docker exec -i -t %var% /bin/bash