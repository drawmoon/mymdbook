# Table of contents

- [CentOS Notes](#centos-notes)
  - [安装 NodeJs](#安装-nodejs)
  - [安装 Yarn](#安装-yarn)
  - [安装 .NET](#安装-net)

# CentOS Notes

## 安装 NodeJs

添加 Nodesource 包存储库

```bash
curl --silent --location https://rpm.nodesource.com/setup_10.x | sudo bash -
```

安装 NodeJs

```bash
sudo yum install nodejs
```

安装 Node 管理工具

```bash
npm install -g n
```

安装最新`Stable`版

```bash
n stable
```

## 安装 Yarn

添加 Yarn 包存储库

```bash
curl --silent --location https://dl.yarnpkg.com/rpm/yarn.repo | sudo tee /etc/yum.repos.d/yarn.repo
sudo rpm --import https://dl.yarnpkg.com/rpm/pubkey.gpg
```

安装 Yarn

```bash
sudo yum install yarn
```

## 安装 .NET

添加 Microsoft 包存储库

```bash
sudo rpm -Uvh https://packages.microsoft.com/config/centos/7/packages-microsoft-prod.rpm
```

安装 SDK

```bash
sudo yum install dotnet-sdk-5.0
```

安装 Runtime

```bash
# ASP.NET Core Runtime
sudo yum install aspnetcore-runtime-5.0

# .NET Runtime
sudo yum install dotnet-runtime-5.0
```
