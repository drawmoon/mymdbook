package com.drash.example.conf;

import io.swagger.annotations.Api;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import springfox.documentation.builders.ApiInfoBuilder;
import springfox.documentation.builders.PathSelectors;
import springfox.documentation.builders.RequestHandlerSelectors;
import springfox.documentation.oas.annotations.EnableOpenApi;
import springfox.documentation.service.ApiInfo;
import springfox.documentation.spi.DocumentationType;
import springfox.documentation.spring.web.plugins.Docket;

@Configuration
@EnableOpenApi
public class SwaggerConf {

    // NOTE: This is the only method that needs to be changed to enable Swagger.
    //       `http://localhost:8080/swagger-ui/index.html` is the default URL.
    @Bean
    public Docket createRestApi() {
        ApiInfo info = new ApiInfoBuilder()
                .title("Web API")
                .description("Demo project for Spring Boot")
                .version("1.0.0")
                .build();

        return new Docket(DocumentationType.OAS_30)
                .enable(true)
                .apiInfo(info)
                .select()
                .apis(RequestHandlerSelectors.withClassAnnotation(Api.class))
                .paths(PathSelectors.any())
                .build();
    }
}
