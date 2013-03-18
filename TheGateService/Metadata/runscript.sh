#!/bin/bash

# Helper script to easily run all three
# database creation scripts in one go

# Get the directory this script is located in
DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

CREATE=database_script_mysql.sql
PROCEDURES=stored_procedures.sql
TESTDATA=testdata.sql

USER=thegate

if [[ -z $(which mysql) ]]; then
    echo MySQL is not installed, or is not in your PATH.
    exit 1
fi

filename="$DIR/script.sql"

checkfile() {
    if [[ ! -f $1 ]]; then
        echo $1 is missing!
        exit 1
    fi
}

checkfile "$DIR/$CREATE"
checkfile "$DIR/$PROCEDURES"
checkfile "$DIR/$TESTDATA"

cat "$DIR/$CREATE" > "$filename"
cat "$DIR/$PROCEDURES" >> "$filename"
cat "$DIR/$TESTDATA" >> "$filename"

while [[ -z $pass ]]; do
    echo Enter database password for user '"'$USER'"':
    read -s pass
done

mysql -h home.jaysan1292.com -u $USER -p$pass < "$filename"

if [[ $? -ne 0 ]]; then
    echo Error executing script!
    exit 1
else
    echo Script executed successfully!
    rm "$filename"
fi
