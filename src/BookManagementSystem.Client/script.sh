#!/bin/sh
#save this file in LF line endings

jq --arg aVar "$(printenv API_BASE_URL)" '.ApiBaseUrl = $aVar' /usr/share/nginx/html/appsettings.json > /usr/share/nginx/html/appsettings.json.tmp && mv /usr/share/nginx/html/appsettings.json.tmp /usr/share/nginx/html/appsettings.json
