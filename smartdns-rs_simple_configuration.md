# SmartDNS simple configuration

编辑 `/usr/local/etc/smartdns/smartdns.conf` 配置文件：

```conf
bind [::]:53

cache-size 16384

# enable persist cache when restart
cache-persist yes

force-qtype-SOA 65

rr-ttl-reply-max 5

log-level error

# remote https dns server list
server https://223.5.5.5/dns-query -bootstrap-dns -exclude-default-group
server https://doh.pub/dns-query
server https://dns.alidns.com/dns-query
server https://dns.google/dns-query
server https://cloudflare-dns.com/dns-query
```

保存后 `sudo /usr/local/sbin/smartdns service restart` 重新启动 SmartDNS 服务。
