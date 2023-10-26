package com.drash.example.mapper;

import com.drash.example.dto.ProfileDTO;
import com.drash.example.entity.Profile;
import org.springframework.cglib.beans.BeanCopier;

public class ProfileDTOMapper {
    public static ProfileDTO map(Profile profile) {
        BeanCopier beanCopier = BeanCopier.create(Profile.class, ProfileDTO.class, false);

        ProfileDTO profileDTO = new ProfileDTO();
        beanCopier.copy(profile, profileDTO, null);

        return profileDTO;
    }
}
