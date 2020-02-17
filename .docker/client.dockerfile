## arguments
ARG NGINX_VERSION=alpine
ARG NODE_VERSION=latest

## stage - base
FROM node:${NODE_VERSION} as base
WORKDIR /angular
COPY package*.json ./
RUN npm clean-install

## stage - build
FROM base as build
COPY . .
RUN npm run build


## stage - deploy
FROM nginx:${NGINX_VERSION} as deploy
COPY --from=build /angular/dist/FlashCard /usr/share/nginx/html/
