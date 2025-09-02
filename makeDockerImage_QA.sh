buildah login -u desadti -p Desadti.. srv-osnexus01.minfin.gob.gt:8006

# Generar versión usando timestamp (formato: YYYYMMDD-HHMMSS)
VERSION="qa-$(date +%Y%m%d-%H%M%S)"
COMMIT_ID=$(git rev-parse --short=8 HEAD)

# Crear la imagen usando buildah
buildah --format=docker bud -t meet-backend-img --add-host=srv-osnexus01.minfin.gob.gt:172.18.27.115 .

# Etiquetar la imagen con la versión y el commit SHA para rastreo en QA
buildah tag meet-backend-img srv-osnexus01.minfin.gob.gt:8006/meet-backend-img:${VERSION}
buildah push srv-osnexus01.minfin.gob.gt:8006/meet-backend-img:${VERSION}

# Mantener "qa-latest" actualizado
buildah tag srv-osnexus01.minfin.gob.gt:8006/meet-backend-img:${VERSION} srv-osnexus01.minfin.gob.gt:8006/meet-backend-img:qa-latest
buildah push srv-osnexus01.minfin.gob.gt:8006/meet-backend-img:qa-latest




