package com.drash.example.dto;

import io.swagger.annotations.ApiModelProperty;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class LicenseDTO {
    @ApiModelProperty(value = "License title")
    private String title;

    @ApiModelProperty(value = "License url")
    private String url;
}
