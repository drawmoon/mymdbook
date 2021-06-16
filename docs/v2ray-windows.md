# Windows10 使用 V2ray 配置代理

## 下载安装

在 [Github Releases](https://github.com/v2fly/v2ray-core/releases) 中下载预编译的二进制 ZIP 格式压缩包。并解压到主目录下。

配置 `环境变量`，在 `系统变量` 中编辑 `Path`，将 `v2ray` 的路径添加到 `Path` 中。

## 配置 V2ray

编辑 `config.json` 文件。

```json
{
  "dns": {
    "servers": [
      {
        "address": "202.96.128.166",
        "port": 53,
        "domains": [
          "geosite:cn"
        ]
      },
      {
        "address": "8.8.8.8",
        "port": 53,
        "domains": [
          "regexp:.*$",
          "domain:github.io"
        ]
      }
    ],
    "hosts": {
      "geosite:category-ads": "127.0.0.1",
      "example.com": "1.2.3.4"
    }
  },
  "inbounds": [
    {
      "listen": "0.0.0.0",
      "port": 10808,
      "protocol": "socks",
      "settings": {
        "auth": "noauth",
        "udp": true
      }
    },
    {
      "listen": "0.0.0.0",
      "port": 10809,
      "protocol": "http",
      "settings": {
        "timeout": 360
      }
    }
  ],
  "outbounds": [
    {
      "tag": "x5",
      "protocol": "vmess",
      "settings": {
        "vnext": [
          {
            "address": "hk7.hjynode.com",
            "port": 443,
            "users": [
              {
                "id": "c084c24a-c833-3605-b9a1-577f021d48b5",
                "alterId": 2,
                "security": "auto",
                "level": 8
              }
            ]
          }
        ]
      },
      "streamSettings": {
        "network": "ws",
        "security": "tls",
        "tlsSettings": {
          "serverName": "hk7.hjynode.com",
          "allowInsecure": false
        },
        "wsSettings": {
          "path": "/v2ray",
          "headers": {
            "host": "hk7.hjynode.com"
          }
        }
      },
      "mux": {
        "enabled": false,
        "concurrency": 8
      }
    },
    {
      "tag": "x1",
      "protocol": "vmess",
      "settings": {
        "vnext": [
          {
            "address": "jpkddi.hjynode.com",
            "port": 443,
            "users": [
              {
                "id": "c084c24a-c833-3605-b9a1-577f021d48b5",
                "alterId": 2,
                "security": "auto",
                "level": 8
              }
            ]
          }
        ]
      },
      "streamSettings": {
        "network": "ws",
        "security": "tls",
        "tlsSettings": {
          "serverName": "jpkddi.hjynode.com",
          "allowInsecure": false
        },
        "wsSettings": {
          "path": "/v2ray",
          "headers": {
            "host": "jpkddi.hjynode.com"
          }
        }
      },
      "mux": {
        "enabled": false,
        "concurrency": 8
      }
    },
    {
      "tag": "tor-out",
      "protocol": "socks",
      "settings": {
        "servers": [
          {
            "address": "127.0.0.1",
            "port": 9050
          }
        ]
      }
    },
    {
      "tag": "direct",
      "protocol": "freedom",
      "settings": {
        "domainStrategy": "UseIP"
      }
    },
    {
      "tag": "blocked",
      "protocol": "blackhole",
      "settings": {}
    },
    {
      "tag": "dns-out",
      "protocol": "dns",
      "settings": {}
    }
  ],
  "routing": {
    "domainStrategy": "IPOnDemand",
    "balancers": [
      {
        "tag": "x5-proxy",
        "selector": [
          "x5"
        ]
      },
      {
        "tag": "x1-proxy",
        "selector": [
          "x1"
        ]
      }
    ],
    "rules": [
      {
        "type": "field",
        "domain": [
          "geosite:category-ads-all"
        ],
        "outboundTag": "blocked"
      },
      {
        "type": "field",
        "domain": [
          "domain:onion"
        ],
        "outboundTag": "tor-out"
      },
      {
        "type": "field",
        "ip": [
          "geoip:cn",
          "geoip:private"
        ],
        "outboundTag": "direct"
      },
      {
        "type": "field",
        "ip": [
          "8.8.8.8",
          "1.1.1.1"
        ],
        "balancerTag": "x5-proxy"
      },
      {
        "type": "field",
        "domain": [
          "domain:safebrowsing.googleapis.com",
          "domain:apple.com",
          "domain:icloud.com",
          "geosite:cn",
          "domain:local"
        ],
        "outboundTag": "direct"
      },
      {
        "type": "field",
        "domain": [
          "geosite:microsoft",
          "geosite:stackexchange",
          "geosite:google"
        ],
        "balancerTag": "x5-proxy"
      },
      {
        "type": "field",
        "network": "tcp,udp",
        "balancerTag": "x1-proxy"
      }
    ]
  }
}
```

## 启动 V2ray

```powershell
v2ray
```

## 配置开机启动

下载 [NSSM](http://www.nssm.cc/release/nssm-2.24.zip)，并解压到主目录下。

配置 `环境变量`，在 `系统变量` 中编辑 `Path`，将 `NSSM` 的路径添加到 `Path` 中。

启动终端应用，执行以下指令安装服务：

```powershell
nssm install
```

执行指令后会启动 `NSSM` 服务安装界面。

- 在 `Path` 中输入 `v2ray` 的所在路径。
- 在 `Service Name` 中输入 `V2ray-Service`，点击 `Install service` 安装服务。

执行 `nssm start V2ray-Service` 或在 `服务` 中启动 `v2ray`。

## 自动配置最优的服务器

克隆 [v2ray-maid](https://github.com/mokeyish/v2ray-maid) 仓库，编译为 EXE 可执行程序，并复制到 `v2ray` 的所在目录下。

在主目录中新建 `.v2ray-maid.json` 配置文件，并输入以下内容：

```json
{
  "sub_url": "https://订阅地址.com",
  "program": "C:\\Users\\用户名\\v2ray\\v2ray.exe",
  "ping_times": 10,
  "concurrency": 6,
  "proxies": [
    {
      "selector": "倍率1",
      "tag": "x1",
      "target_file": "C:\\Users\\用户名\\v2ray\\config.json",
      "limit_count": 1
    },
    {
      "selector": "倍率5",
      "tag": "x5",
      "target_file": "C:\\Users\\用户名\\v2ray\\config.json",
      "limit_count": 1
    }
  ]
}
```

- `program` 输入 `v2ray` 主程序的目标路径
- `target_file` 输入 `v2ray` 的配置文件的目标路径

在终端应用中执行以下命令，自动配置最优的服务器：

```powershell
v2ray-maid
```
