#!/bin/bash
cd /usr/local/tomcat/webapps
unzip -oq example-0.0.1-SNAPSHOT.war -d example
rm -rf *.war
set -e
if [ $CUSTOMER ]
then
   sed -i "s/^customer=.*$/customer=$CUSTOMER/g" example/WEB-INF/classes/application.properties
fi
sed -i "s/\r//g" example/WEB-INF/classes/application.properties
cd /usr/local/tomcat/bin
./startup.sh
cd /usr/local/tomcat/logs
if [ ! -f catalina.out ]
then
   sleep 3s
fi
tail -f catalina.out
