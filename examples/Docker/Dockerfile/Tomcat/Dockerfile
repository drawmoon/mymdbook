FROM openjdk:8-slim AS build
WORKDIR /source
COPY source .
RUN chmod +x ./mvnw \
    && ./mvnw clean install package

FROM tomcat:8-jdk8
COPY run.sh .
COPY --from=build /source/target/example-0.0.1-SNAPSHOT.war webapps/
ENV TZ=PRC
RUN apt update \
    && apt install -y unzip \
    && rm -rf /var/lib/apt/lists/* \
    && ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone \
    && chmod +x ./run.sh
ENTRYPOINT ["./run.sh"]