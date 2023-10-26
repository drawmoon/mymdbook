package com.example;

import static org.junit.jupiter.api.Assertions.*;

import org.junit.jupiter.api.Test;

public class VersionTest {

    @Test
    public void test() {
        Version version = new Version(1, 0, 0);
        assertEquals("Version 1.0.0", version.toString());
    }
}
