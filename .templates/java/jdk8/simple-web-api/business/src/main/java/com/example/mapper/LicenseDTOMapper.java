package com.drash.example.mapper;

import com.drash.example.dto.LicenseDTO;
import com.drash.example.entity.License;
import java.util.Collection;
import java.util.List;
import java.util.stream.Collectors;
import org.springframework.cglib.beans.BeanCopier;

public class LicenseDTOMapper {
    public static LicenseDTO map(License license) {
        BeanCopier beanCopier = BeanCopier.create(License.class, LicenseDTO.class, false);

        LicenseDTO licenseDTO = new LicenseDTO();
        beanCopier.copy(license, licenseDTO, null);

        return licenseDTO;
    }

    public static List<LicenseDTO> map(Collection<License> licenses) {
        return licenses.stream().map(LicenseDTOMapper::map).collect(Collectors.toList());
    }
}
