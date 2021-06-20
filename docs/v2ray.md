# 使用 V2ray 配置代理

## 下载安装

在 [Github Releases](https://github.com/v2fly/v2ray-core/releases) 中下载预编译的二进制 ZIP 格式压缩包，解压到任意目录下。

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

```bash
./v2ray
```

> 如果是 Windows 下，可通过配置 `环境变量`，将 V2ray 的所在路径添加到 `系统变量` 的 `Path` 中，通过 `v2ray` 命令启动 V2ray。

## 自动配置最优的服务器

克隆 [v2ray-maid](https://github.com/mokeyish/v2ray-maid) 仓库，编译为可执行程序，并复制到 `v2ray` 的所在目录下。

在主目录中新建 `.v2ray-maid.json` 配置文件，并输入以下内容：

```json
{
  "sub_url": "订阅地址",
  "program": "v2ray 可执行文件的目标路径",
  "ping_times": 10,
  "concurrency": 6,
  "proxies": [
    {
      "selector": "倍率1",
      "tag": "x1",
      "target_file": "config.json 的目标路径",
      "limit_count": 1
    },
    {
      "selector": "倍率5",
      "tag": "x5",
      "target_file": "config.json 的目标路径",
      "limit_count": 1
    }
  ]
}
```

在终端应用中执行以下命令，自动配置最优的服务器：

```bash
./v2ray-maid
```

执行完成后重启 V2ray
