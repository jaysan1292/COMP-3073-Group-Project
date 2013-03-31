#!/bin/bash

while [[ -z $pass ]]; do
    echo 'Enter database password for user "thegate":'
    read -s pass
done

mysql -u thegate -p$pass -h home.jaysan1292.com -D thegate
