ENV="prod"
buildah login -u desadti -p Desadti.. srv-osnexus01.minfin.gob.gt:8006

VERSION="v$(git describe --tags --always)"
COMMIT_ID=$(git rev-parse --short=8 HEAD)

# Crear la imagen usando buildah
buildah --format=docker bud -t clima-laboral-backend-img --add-host=srv-osnexus01.minfin.gob.gt:172.18.27.115 .

# Etiquetar la imagen con la versión y el commit SHA para rastreo en PROD
buildah tag clima-laboral-backend-img srv-osnexus01.minfin.gob.gt:8006/clima-laboral-backend-img:${VERSION}_${COMMIT_ID}
buildah push srv-osnexus01.minfin.gob.gt:8006/clima-laboral-backend-img:${VERSION}_${COMMIT_ID}

# Mantener "prod-latest" actualizado
buildah tag srv-osnexus01.minfin.gob.gt:8006/clima-laboral-backend-img:${VERSION}_${COMMIT_ID} srv-osnexus01.minfin.gob.gt:8006/clima-laboral-backend-img:latest
buildah push srv-osnexus01.minfin.gob.gt:8006/clima-laboral-backend-img:latest