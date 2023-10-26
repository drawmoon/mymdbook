package com.drash.example.dto;

import io.swagger.annotations.ApiModelProperty;
import java.util.Collections;
import java.util.List;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
public class ProfileApiDTO {
    @ApiModelProperty(value = "Profile")
    private ProfileDTO profile;

    @ApiModelProperty(value = "Licenses")
    private List<LicenseDTO> licenses = Collections.emptyList();
}
