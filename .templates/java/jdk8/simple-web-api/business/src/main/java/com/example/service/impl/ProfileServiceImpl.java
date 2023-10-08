package com.drash.example.service.impl;

import com.drash.example.dto.ProfileDTO;
import com.drash.example.entity.Profile;
import com.drash.example.mapper.ProfileDTOMapper;
import com.drash.example.repository.IProfileRepository;
import com.drash.example.service.IProfileService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class ProfileServiceImpl implements IProfileService {
    @Autowired
    private IProfileRepository profileRepository;

    @Override
    public ProfileDTO getProfile() {
        Profile profile = profileRepository.findAll().get(0);
        return ProfileDTOMapper.map(profile);
    }
}
