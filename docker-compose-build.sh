ENV_NAME=$1
GIT_SHA=$2
HOST_PORT=$3

ENV_NAME=$ENV_NAME GIT_SHA=$GIT_SHA HOST_PORT=$HOST_PORT envsubst < ./services/docker-compose.yml.template > docker-compose.yml