package com.example;

/**
 * The default IVersion implementation.
 */
public class Version {

    private final int major;
    private final int minor;
    private final int build;

    public Version(int major, int minor, int build) {
        this.major = major;
        this.minor = minor;
        this.build = build;
    }

    public int getMajor() {
        return major;
    }

    public int getMinor() {
        return minor;
    }

    public int getBuild() {
        return build;
    }

    @Override
    public String toString() {
        return "Version " + major + "." + minor + "." + build;
    }
}
