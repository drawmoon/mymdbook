package com.drash.example.controller;

import com.drash.example.dto.ProfileApiDTO;
import com.drash.example.service.ILicenseService;
import com.drash.example.service.IProfileService;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import io.swagger.annotations.ApiResponse;
import io.swagger.annotations.ApiResponses;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@Api(tags = "Profile")
@RestController
@RequestMapping("api/profile")
public class ProfileController {
    @Autowired
    private IProfileService profileService;

    @Autowired
    private ILicenseService licenseService;

    @GetMapping()
    @ApiOperation(value = "Get profile", consumes = "application/json", produces = "application/json")
    @ApiResponses(
            value = {
                @ApiResponse(code = 200, message = "Get profile successfully"),
                @ApiResponse(code = 500, message = "Server error")
            })
    public ProfileApiDTO getProfile() {
        return new ProfileApiDTO(profileService.getProfile(), licenseService.getLicenses());
    }
}
