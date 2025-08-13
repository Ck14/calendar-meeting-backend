buildah login -u desadti -p Desadti.. srv-osnexus01.minfin.gob.gt:8006
buildah --format=docker bud -t clima-laboral-backend-img --add-host=srv-osnexus01.minfin.gob.gt:172.18.27.115 .
buildah tag clima-laboral-backend-img srv-osnexus01.minfin.gob.gt:8006/clima-laboral-backend-img:latest
buildah push srv-osnexus01.minfin.gob.gt:8006/clima-laboral-backend-img:latest