FROM node:24-alpine AS build
WORKDIR /app

COPY package*.json ./

RUN npm install

COPY . .

RUN npm run build

FROM nginx:1.27-alpine

RUN rm -rf /usr/share/nginx/html/*

COPY --from=build /app/dist/file-processor-web/browser/ /usr/share/nginx/html

EXPOSE 80
