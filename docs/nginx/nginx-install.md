# Nginx 安装

## 安装依赖包

### Ubuntu

```bash
sudo apt install gcc libpcre3-dev zlib1g-dev openssl libssl-dev make
```

### CentOS

```bash
sudo yum install gcc pcre-devel zlib-devel openssl openssl-devel make
```

## 安装 Nginx

```bash
wget https://nginx.org/download/nginx-1.18.0.tar.gz
tar -zxvf nginx-1.18.0.tar.gz
```

执行 Nginx 默认配置

```bash
cd nginx-1.18.0/
./configure
```

执行编译、安装

```bash
make & make install
```

将 Nginx 添加到全局变量中

```bash
ln -s /usr/local/nginx/sbin/nginx /usr/local/bin/
```

启动 Nginx

```bash
nginx
```
