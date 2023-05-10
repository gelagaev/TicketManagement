FROM node:16-alpine AS builder
WORKDIR /app
COPY . .
RUN npm install
RUN ./node_modules/.bin/ng build --configuration=production

FROM nginx:stable-alpine
RUN rm -rf /usr/share/nginx/html/*
COPY ./nginx.conf /etc/nginx/nginx.conf
COPY ./mime.types /etc/nginx/mime.types
COPY --from=builder /app/dist /usr/share/nginx/html

CMD ["nginx", "-g", "daemon off;"]
