package com.drash.example;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.cloud.client.discovery.EnableDiscoveryClient;
import org.springframework.cloud.openfeign.EnableFeignClients;

@SpringBootApplication
@EnableDiscoveryClient
@EnableFeignClients(value = {"com.drash"})
public class Application {
    public static void main(String[] args) {
        System.setProperty("spring.cloud.bootstrap.enabled", "true");

        SpringApplication.run(Application.class, args);
    }
}
