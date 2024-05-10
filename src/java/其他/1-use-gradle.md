# 使用 Gradle

- [如何使用私有仓库](#如何使用私有仓库)
- [发布你的程序到中央仓库](#发布你的程序到中央仓库)

## 如何使用私有仓库

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
    url '<你的 Maven 仓库地址>'
    allowInsecureProtocol true
}
```

指定访问凭证：

记录账号密码在 `gradle.properties` 配置文件中：

```properties
nexusUsername=<你的 Maven 仓库用户名>
nexusPassword=<你的 Maven 仓库密码>
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

## 发布你的程序到中央仓库

创建密钥：

- 安装 [GunPG](https://www.gnupg.org/download/index.html)。
- 创建 OpenPGP 密钥对：`gpg --gen-key`，并填写 GPG 名称、电子邮箱、密码完成密钥生成。
- 发布公钥到 OpenPGP：`gpg --keyserver https://keys.openpgp.org --send-keys <你的 PGP Key>`。

如果执行 `send-key` 失败，可以尝试导出公钥手动上传到 [OpenPGP](https://keys.openpgp.org/)。

配置项目，编辑 `build.gradle` 配置文件：

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

publishing {
    publications {
        mavenJava(MavenPublication) {
            from components.java
            artifactId = archivesBaseName
            groupId = group
            version = version
            pom {
                name = archivesBaseName
                packaging = 'jar'
                description = 'App description'
                url = 'https://github.com/<你的项目仓库>'
                inceptionYear = '2023'
                licenses {
                    license {
                        name = 'MIT License'
                        url = 'https://opensource.org/licenses/MIT'
                    }
                }
                scm {
                    connection = 'scm:git:git@github.com:<你的项目仓库>.git'
                    developerConnection = 'scm:git:git@github.com:<你的项目仓库>.git'
                    url = 'https://github.com/<你的项目仓库>'
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
        def releaseRepoUrl = "https://s01.oss.sonatype.org/service/local/staging/deploy/maven2/"
        if (version.endsWith("-SNAPSHOT")) {
            releaseRepoUrl = "https://s01.oss.sonatype.org/content/repositories/snapshots/"
        }
        maven {
            name = "ossrh"
            url = releaseRepoUrl
            credentials {
                username = project.properties['nexusUsername']
                password = project.properties['nexusPassword']
            }
        }
    }
}

signing {
    sign publishing.publications.mavenJava
}
```

导出 PGP 私钥、公钥：

- 导出公钥：`gpg --armor --export <你的 PGP Key> > secring.pub.gpg`
- 导出私钥：`gpg --armor --export-secret-keys <你的 PGP Key> > secring.gpg`

编辑 `gradle.properties` 配置文件：

```properties
signing.keyId=<PGP Key 后8位字符>
signing.password=<PGP 密码>
signing.secretKeyRingFile=<PGP 密钥文件（.pgp 文件）>

nexusUsername=<Sonatype 用户名>
nexusPassword=<Sonatype 密码>
```

> 可以通过 `gpg --list-keys` 命令查看 PGP Key。

编译：

```bash
./gradlew clean build
```

编译成功后执行发布：

```bash
./gradlew publish
```
