package com.drash.example.service.impl;

import com.drash.example.dto.LicenseDTO;
import com.drash.example.entity.License;
import com.drash.example.mapper.LicenseDTOMapper;
import com.drash.example.repository.ILicenseRepository;
import com.drash.example.service.ILicenseService;
import java.util.List;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class LicenseServiceImpl implements ILicenseService {
    @Autowired
    private ILicenseRepository licenseRepository;

    @Override
    public List<LicenseDTO> getLicenses() {
        List<License> licenses = licenseRepository.findAll();
        return LicenseDTOMapper.map(licenses);
    }
}
