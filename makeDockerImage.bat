docker build -t clima-laboral-backend-img --add-host=srv-osnexus01.minfin.gob.gt:172.18.27.115 .
docker tag clima-laboral-backend-img srv-osnexus01.minfin.gob.gt:8006/clima-laboral-backend-img:latest
docker push srv-osnexus01.minfin.gob.gt:8006/clima-laboral-backend-img:latest
