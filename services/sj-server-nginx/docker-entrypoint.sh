#!/usr/bin/env sh
set -eu

envsubst '${FQDN} ${FQDN}' < /etc/nginx/conf.d/default.conf.template > /etc/nginx/conf.d/default.conf

exec "$@"