#!/bin/sh
# vim:sw=2:ts=2:sts=2:et

jq --arg aVar "$(printenv API_BASE_URL)" '.ApiBaseUrl = $aVar' /usr/share/nginx/html/appsettings.json > /usr/share/nginx/html/appsettings.json.tmp && mv /usr/share/nginx/html/appsettings.json.tmp /usr/share/nginx/html/appsettings.json
