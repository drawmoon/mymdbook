package com.drash.example.dto;

import io.swagger.annotations.ApiModelProperty;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class ProfileDTO {
    @ApiModelProperty(value = "Application title")
    private String title;

    @ApiModelProperty(value = "Application description")
    private String description;
}
