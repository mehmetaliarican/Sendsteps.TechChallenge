FROM node:16-alpine as builder
CMD test.bat && cmd
WORKDIR /app
COPY . .
RUN npm install 
RUN npm ci
RUN npm run build
FROM nginx:1.21.0-alpine as production
ENV NODE_ENV production
COPY --from=builder /app/build /usr/share/nginx/html
# Add your nginx.conf
COPY /nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]