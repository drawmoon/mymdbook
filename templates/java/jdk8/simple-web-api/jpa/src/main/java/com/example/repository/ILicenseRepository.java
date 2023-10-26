package com.drash.example.repository;

import com.drash.example.entity.License;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.JpaSpecificationExecutor;

public interface ILicenseRepository extends JpaRepository<License, String>, JpaSpecificationExecutor<License> {}
