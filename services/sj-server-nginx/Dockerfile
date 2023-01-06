FROM nginx AS base
RUN rm /etc/nginx/nginx.conf /etc/nginx/conf.d/default.conf
COPY sj-server-nginx/nginx.conf /etc/nginx/nginx.conf
COPY sj-server-nginx/default.conf /etc/nginx/conf.d/default.conf

FROM node:16 AS build
WORKDIR /usr/src
COPY sj-app-angular app
WORKDIR /usr/src/app
RUN npm install
RUN npm run build

FROM base AS final
COPY --from=build /usr/src/app/dist /usr/share/nginx/html/app