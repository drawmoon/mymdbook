# 使用 Maven

- [如何使用私有仓库](#如何使用私有仓库)

## 如何使用私有仓库

编辑 Maven 配置文件 `conf.xml` 或 `settings.xml`：

```xml
<settings
    xmlns="http://maven.apache.org/SETTINGS/1.0.0"
    xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
    xsi:schemaLocation="http://maven.apache.org/SETTINGS/1.0.0 https://maven.apache.org/xsd/settings-1.0.0.xsd">
    <localRepository>${user.home}/.m2/repository</localRepository>
    <mirrors>
        <mirror>
            <id>my-repo</id>
            <url>http://127.0.0.1:8081/repository/maven-public</url>
            <mirrorOf>central,my-maven</mirrorOf>
        </mirror>
    </mirrors>
    <profiles>
        <profile>
            <id>default-profile</id>
            <activation>
                <activeByDefault>true</activeByDefault>
            </activation>
            <properties>
                <repo.releases.url>http://127.0.0.1:8081/repository/maven-releases</repo.releases.url>
                <repo.snapshots.url>http://127.0.0.1:8081/repository/maven-snapshots</repo.snapshots.url>
            </properties>
            <repositories>
                <repository>
                    <id>my-maven</id>
                    <url>http://my-maven</url>
                    <releases>
                        <enabled>false</enabled>
                    </releases>
                    <snapshots>
                        <enabled>true</enabled>
                    </snapshots>
                </repository>
            </repositories>
            <pluginRepositories>
                <pluginRepository>
                    <id>my-maven</id>
                    <url>http://my-maven</url>
                    <releases>
                        <enabled>true</enabled>
                    </releases>
                    <snapshots>
                        <enabled>true</enabled>
                    </snapshots>
                </pluginRepository>
            </pluginRepositories>
        </profile>
    </profiles>
    <servers>
        <server>
            <id>my-repo</id>
            <username>admin</username>
            <password>123456</password>
        </server>
    </servers>
</settings>
```
