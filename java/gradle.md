# Gradle

## 使用私有仓库

编辑 `build.gradle` 配置文件：

```groovy
repositories {
  maven { url 'https://maven.aliyun.com/repository/public/' }
  maven { url 'https://maven.aliyun.com/repository/central' }
  mavenLocal()
  mavenCentral()
}
```

允许使用不安全的协议：

```groovy
maven {
  url '<your maven repo url>'
  allowInsecureProtocol true
}
```

指定访问凭证：

记录账号密码在 `gradle.properties` 配置文件中：

```properties
nexusUsername=<your repo credentials username>
nexusPassword=<your repo credentials password>
```

```groovy
maven {
  url '<your maven repo url>'
  credentials {
    username = project.properties['nexusUsername']
    password = project.properties['nexusPassword']
  }
}
```

## 发布

编辑 `build.gradle` 配置文件：

```groovy
plugins {
  id 'java-library'
  id 'maven-publish'
  id "com.github.johnrengelman.shadow" version "7.1.2"
}

apply plugin: 'java-library'
apply plugin: 'maven-publish'
apply plugin: 'signing'
apply plugin: 'com.github.johnrengelman.shadow'

version = '0.0.1'
archivesBaseName = 'example'
group = 'com.example'

sourceCompatibility = JavaVersion.VERSION_1_8
targetCompatibility = JavaVersion.VERSION_1_8

repositories {
  mavenLocal()
  mavenCentral()
}

[compileJava, compileTestJava].each() {
  it.options.encoding = "UTF-8"
}

jar {
  manifest {
    attributes('Implementation-Title': archivesBaseName,
            'Implementation-Version': version,
            'Built-By': 'drash',
            'Built-JDK': System.getProperty('java.version'),
            'Source-Compatibility': sourceCompatibility,
            'Target-Compatibility': targetCompatibility)
  }
}

java {
  toolchain {
    languageVersion = JavaLanguageVersion.of(8)
  }
  withJavadocJar()
  withSourcesJar()
}

javadoc.options {
  encoding = 'UTF-8'
  links 'https://docs.oracle.com/javase/8/docs/api/'
}

artifacts {
  archives javadocJar, sourcesJar, shadowJar
}

signing {
  if (project.properties.containsKey('signing.keyId')) {
    sign configurations.archives
  }
}

afterEvaluate {
  publishing {
    publications {
      plugin(MavenPublication) {
        from components.java
        artifactId = archivesBaseName
        groupId = group
        version = version
        pom {
          name = archivesBaseName
          packaging = 'jar'
          description = 'App description'
          url = 'https://github.com/<your repo>'
          inceptionYear = '2023'
          licenses {
            license {
              name = 'MIT License'
              url = 'https://opensource.org/licenses/MIT'
            }
          }
          scm {
            connection = 'scm:git:git@github.com:<your repo>.git'
            developerConnection = 'scm:git:git@github.com:<your repo>.git'
            url = 'https://github.com/<your repo>'
          }
          developers {
            developer {
              id = 'drash'
              name = 'drawmoon'
              email = '1340260725@qq.com'
            }
          }
        }
      }
    }
    repositories {
      maven {
        name = "ossrh"
        url = "https://s01.oss.sonatype.org/service/local/staging/deploy/maven2/"
        credentials {
            username = project.properties['nexusUsername']
            password = project.properties['nexusPassword']
        }
      }
    }
  }
}
```

编辑 `gradle.properties` 配置文件：

```properties
signing.keyId=<your pgp key id>
signing.password=<your pgp password>
signing.secretKeyRingFile=<your pgp secret key file path>

nexusUsername=<your repo credentials username>
nexusPassword=<your repo credentials password>
```

编译：

```bash
./gradlew clean build -Prelease
```

编译成功后执行发布：

```bash
./gradlew publish
```

或指定发布到指定的仓库，与 `publishing.repositories` 的 `name` 对应：

```bash
./gradlew publishAllPublicationsToOssrhRepository
```
