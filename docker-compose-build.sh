ENV_NAME=$1
GIT_SHA=$2
HOST_PORT=$3
TARGET_HOST=$4

ENV_NAME=$ENV_NAME GIT_SHA=$GIT_SHA HOST_PORT=$HOST_PORT envsubst < ./services/docker-compose.yml.template > docker-compose.yml

scp docker-compose.yml service@$TARGET_HOST:/srv/strengthjournal/$ENV_NAME/