FROM node:14.18.1 AS build
WORKDIR /source
COPY . .
RUN npm install \
    && npm run build

FROM node:14.18.1-alpine
WORKDIR /app
COPY --from=build /source/dist .
COPY --from=build /source/node_modules node_modules
ENTRYPOINT [ "node", "main" ]